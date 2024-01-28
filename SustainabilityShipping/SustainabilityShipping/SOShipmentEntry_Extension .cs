using PX.Api;
using PX.Common;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static PX.Objects.SO.SOShipmentEntry;
using CromulentBisgetti.ContainerPacking.Entities;
using CromulentBisgetti.ContainerPacking.Algorithms;
using CromulentBisgetti.ContainerPacking;
using System.ComponentModel;
using Container = CromulentBisgetti.ContainerPacking.Entities.Container;

namespace SustainabilityShipping
{
    public class SOShipmentEntry_Extension : PXGraphExtension<PackageDetail, PX.Objects.SO.SOShipmentEntry>
    {
        public static bool IsActive() => true;
        #region Event Handlers 
        public PXAction<SOShipment> reCacluateBoxes;
        [PXButton]
        [PXUIField(DisplayName = "Sustainable Packing")]
        protected virtual IEnumerable ReCacluateBoxes(PXAdapter adapter)
        {
            foreach (SOPackageDetailEx detail in
            Base.Packages.Select())
            {
                Base.Packages.Delete(detail);
            }
            Base.Packages.View.RequestRefresh();

            List<CromulentBisgetti.ContainerPacking.Entities.Container> containers = new List<CromulentBisgetti.ContainerPacking.Entities.Container>();
            var boxes = PXSelect<CSBox, Where<CSBox.activeByDefault, Equal<Required<
                CSBox.activeByDefault>>>>.Select(Base, true);
            int counter = 0;
            var containerMap = new System.Collections.Generic.Dictionary<int, string>();
            foreach (CSBox item in boxes)
            {
                containers.Add(new Container(counter, item.Length.Value, item.Width.Value, item.Height.Value));
                containerMap.Add(counter, item.BoxID);
                counter++;
            }


            List<Item> itemsToPack = new List<Item>();

            foreach (SOShipLine item in Base.Transactions.Select())
            {
                InventoryItem currentItem = PXSelect<InventoryItem, Where<InventoryItem.inventoryID,
                    Equal<Required<InventoryItem.inventoryID>>>>.Select(Base, item.InventoryID);
                SSHPackingMaterialsExtension currentItemExt = currentItem.GetExtension<SSHPackingMaterialsExtension>();
                int currentPackedLenght = 1;
                int currentPackedWidth = 1;
                int currentPackedHeight = 1;
                if (currentItemExt.PackedLength.HasValue && currentItemExt.PackedHeight.HasValue && currentItemExt.PackedWidth.HasValue)
                {
                    currentPackedLenght = (int)currentItemExt.PackedLength.Value;
                    currentPackedWidth = (int)currentItemExt.PackedWidth.Value;
                    currentPackedHeight = (int)currentItemExt.PackedHeight.Value;
                }
                itemsToPack.Add(new Item(item.LineNbr.Value, currentPackedLenght, currentPackedWidth, currentPackedHeight, (int)item.ShippedQty));
            }

            List<int> algorithms = new List<int>();
            algorithms.Add((int)AlgorithmType.EB_AFIT);
            int whileCounter = 0;
            while (itemsToPack.Count > 0 && whileCounter <= 99)
            {
                List<ContainerPackingResult> result = PackingService.Pack(containers, itemsToPack, algorithms);
                var effecientBox = (from r in result
                                    orderby r.AlgorithmPackingResults[0].PercentContainerVolumePacked
                   descending
                                    select r).Take(1).FirstOrDefault();
                SOPackageDetailEx box = new SOPackageDetailEx();
                box.BoxID = containerMap[effecientBox.ContainerID];
                box.Description = effecientBox.AlgorithmPackingResults[0].PercentContainerVolumePacked.ToString();
                Base.Packages.Insert(box);

                var consolidatedItems = new System.Collections.Generic.Dictionary<int, Item>();
                foreach (Item item in effecientBox.AlgorithmPackingResults[0].PackedItems)
                {
                    if (consolidatedItems.ContainsKey(item.ID))
                    {
                        consolidatedItems[item.ID].Quantity = consolidatedItems[item.ID].Quantity + item.Quantity;
                    }
                    else
                    {
                        consolidatedItems.Add(item.ID, item);
                    }

                }

                foreach (Item item in consolidatedItems.Values)
                {
                    SOShipLineSplitPackage stockItem = new SOShipLineSplitPackage();
                    stockItem.ShipmentLineNbr = item.ID;
                    stockItem.PackedQty = item.Quantity;
                    stockItem.PackageLineNbr = box.LineNbr;
                    SOShipLineSplit lineSplit = PXSelect<SOShipLineSplit, Where<SOShipLineSplit.shipmentNbr, Equal<Required<SOShipLineSplit.
                        shipmentNbr>>, And<SOShipLineSplit.lineNbr, Equal<Required<SOShipLineSplit.lineNbr>>>>>.
                        Select(Base, Base.Document.Current.ShipmentNbr, stockItem.ShipmentLineNbr);
                    stockItem.ShipmentSplitLineNbr = lineSplit.SplitLineNbr;
                    stockItem = Base1.PackageDetailSplit.Insert(stockItem);
                    SSHSOShipLinePackageExtension shipLineSplitPackageExt = stockItem.GetExtension<SSHSOShipLinePackageExtension>();
                    SOShipLine shipLine = PXSelect<SOShipLine, Where<SOShipLine.shipmentNbr, Equal<Required<SOShipLine.shipmentNbr>>,
                        And<SOShipLine.lineNbr, Equal<Required<SOShipLine.lineNbr>>>>>.Select(Base, Base.Document.Current.ShipmentNbr, stockItem.ShipmentLineNbr);
                    InventoryItem currentItem = PXSelect<InventoryItem, Where<InventoryItem.inventoryID,
                        Equal<Required<InventoryItem.inventoryID>>>>.Select(Base, shipLine.InventoryID);
                    SSHPackingMaterialsExtension currentItemExt = currentItem.GetExtension<SSHPackingMaterialsExtension>();

                    if (currentItemExt.PreferredPackingMaterial != null)
                    {
                        shipLineSplitPackageExt.PreferredPackingMaterial = currentItemExt.PreferredPackingMaterial;
                        shipLineSplitPackageExt.PackingMaterialQty = currentItemExt.PackingMaterialQty;
                    }
                    else
                    {
                        var suggestedPackingMaterialList = PXSelect<InventoryItem, Where<SSHPackingMaterialsExtension.isPackingMaterial,
                            Equal<Required<SSHPackingMaterialsExtension.isPackingMaterial>>>>.Select(Base, true);

                        int i = 0;
                        foreach (InventoryItem currentPackingMaterial in suggestedPackingMaterialList)
                        {
                            SSHPackingMaterialsExtension currentPackingMaterialExt = currentPackingMaterial.GetExtension<SSHPackingMaterialsExtension>();
                            if (currentPackingMaterialExt.FragilityLevel >= currentItemExt.FragilityLevel)
                            {
                                if (shipLineSplitPackageExt.PreferredPackingMaterial == null)
                                {
                                    shipLineSplitPackageExt.PreferredPackingMaterial = currentPackingMaterialExt.InventoryID;
                                    i = currentPackingMaterialExt.SustainabilityID.Value;
                                }
                                else if (currentPackingMaterialExt.SustainabilityID > i)
                                {
                                    shipLineSplitPackageExt.PreferredPackingMaterial = currentPackingMaterialExt.InventoryID;
                                }

                            }
                        }
                    }
                    Base1.PackageDetailSplit.UpdateCurrent();
                }
                itemsToPack = effecientBox.AlgorithmPackingResults[0].UnpackedItems;
                whileCounter++;
            }
            Base.Packages.View.RequestRefresh();
            return adapter.Get();
        }

        #endregion
        #region CacheAttached
        [PXMergeAttributes(Method = MergeMethod.Append)]
        [PXUIField(DisplayName = "Percent Used")]
        protected virtual void SOPackageDetailEx_Description_CacheAttached(PXCache sender) { }
        #endregion
    }
}
