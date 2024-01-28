using System;
using PX.Data;

namespace SustainabilityShipping
{
    using PX.Data;
    using PX.Objects.IN;
    using PX.Objects.SO;
    using System;

    [System.SerializableAttribute()]
    [PXTable(typeof(SOShipLineSplitPackage.recordID), IsOptional = true)]
    [PXCacheName("SSHSOShipLinePackageExtension")]
  public class SSHSOShipLinePackageExtension : PXCacheExtension<SOShipLineSplitPackage>
  {
    #region RecordID
    [PXDBInt(IsKey = true)]
    [PXUIField(DisplayName = "Record ID")]
    public virtual int? RecordID { get; set; }
    public abstract class recordID : PX.Data.BQL.BqlInt.Field<recordID> { }
        #endregion

    #region PreferredPackingMaterial
    [Inventory(typeof(Search<InventoryItem.inventoryID, Where<SustainabilityShipping.SSHPackingMaterialsExtension.isPackingMaterial, Equal<True>>>)
            , typeof(InventoryItem.inventoryCD), typeof(InventoryItem.descr))]
    [PXUIField(DisplayName = "Preferred Packing Material")]
    public virtual int? PreferredPackingMaterial { get; set; }
    public abstract class preferredPackingMaterial : PX.Data.BQL.BqlInt.Field<preferredPackingMaterial> { }
    #endregion

    #region PackingMaterialQty
    [PXDBInt()]
    [PXUIField(DisplayName = "Packing Material Qty")]
    public virtual int? PackingMaterialQty { get; set; }
    public abstract class packingMaterialQty : PX.Data.BQL.BqlInt.Field<packingMaterialQty> { }
    #endregion
  }
}