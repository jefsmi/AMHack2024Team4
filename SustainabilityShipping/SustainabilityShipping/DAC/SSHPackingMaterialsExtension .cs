using System;
using PX.Data;
using PX.Objects.IN;

namespace SustainabilityShipping
{

    [PXTable(typeof(InventoryItem.inventoryID), IsOptional = true)]
    public class SSHPackingMaterialsExtension : PXCacheExtension<InventoryItem>

    {
        public static bool IsActive() => true;
        #region InventoryID
        [PXDBInt(IsKey = true)]
        [PXUIField(DisplayName = "Inventory ID")]
        public virtual int? InventoryID { get; set; }
        public abstract class inventoryID : PX.Data.BQL.BqlInt.Field<inventoryID> { }
        #endregion

        #region IsPackingMaterial
        [PXDBBool()]
        [PXUIField(DisplayName = "Is Packing Material")]
        public virtual bool? IsPackingMaterial { get; set; }
        public abstract class isPackingMaterial : PX.Data.BQL.BqlBool.Field<isPackingMaterial> { }
        #endregion

        #region SustainabilityID
        [PXDBInt()]
        [PXSelector(typeof(Search<SSHLevelSustainability.sustainabilityID>),

                new Type[] { typeof(SSHLevelSustainability.levelName),
                            typeof(SSHLevelSustainability.levelDescription) },
                SubstituteKey = typeof(SSHLevelSustainability.levelName),
                DescriptionField = typeof(SSHLevelSustainability.levelDescription))]
        [PXUIField(DisplayName = "Sustainability ID")]
        public virtual int? SustainabilityID { get; set; }
        public abstract class sustainabilityID : PX.Data.BQL.BqlInt.Field<sustainabilityID> { }
        #endregion

        #region FragilityLevel
        [PXDBInt()]
        [PXSelector(typeof(Search<SSHLevelFragility.fragilityLevel>),

                new Type[] { typeof(SSHLevelFragility.levelName),
                            typeof(SSHLevelFragility.levelDescription) },
                SubstituteKey = typeof(SSHLevelFragility.levelName),
                DescriptionField = typeof(SSHLevelFragility.levelDescription))]
        [PXUIField(DisplayName = "Fragility Level")]
        public virtual int? FragilityLevel { get; set; }
        public abstract class fragilityLevel : PX.Data.BQL.BqlInt.Field<fragilityLevel> { }
        #endregion

        #region PackingGuidance
        [PXDBString(255, InputMask = "")]
        [PXUIField(DisplayName = "Packing Guidance")]
        public virtual string PackingGuidance { get; set; }
        public abstract class packingGuidance : PX.Data.BQL.BqlString.Field<packingGuidance> { }
        #endregion

        #region Co2ekg
        [PXDBDecimal()]
        [PXUIField(DisplayName = "CO2 KG")]
        public virtual Decimal? Co2ekg { get; set; }
        public abstract class co2ekg : PX.Data.BQL.BqlDecimal.Field<co2ekg> { }
        #endregion

        #region PreferredPackingMaterial

        [Inventory(typeof(Search<InventoryItem.inventoryID, Where<SustainabilityShipping.SSHPackingMaterialsExtension.isPackingMaterial, Equal<True>>>)
            , typeof(InventoryItem.inventoryCD), typeof(InventoryItem.descr))]
        [PXUIField(DisplayName = "Preferred Packing Material")]
        public virtual int? PreferredPackingMaterial { get; set; }
        public abstract class preferredPackingMaterial : PX.Data.BQL.BqlInt.Field<preferredPackingMaterial> { }
        #endregion

        #region PackedWidth
        [PXDBDecimal()]
        [PXUIField(DisplayName = "Packed Width")]
        public virtual Decimal? PackedWidth { get; set; }
        public abstract class packedWidth : PX.Data.BQL.BqlDecimal.Field<packedWidth> { }
        #endregion

        #region PackedHeight
        [PXDBDecimal()]
        [PXUIField(DisplayName = "Packed Height")]
        public virtual Decimal? PackedHeight { get; set; }
        public abstract class packedHeight : PX.Data.BQL.BqlDecimal.Field<packedHeight> { }
        #endregion

        #region PackedLength
        [PXDBDecimal()]
        [PXUIField(DisplayName = "Packed Length")]
        public virtual Decimal? PackedLength { get; set; }
        public abstract class packedLength : PX.Data.BQL.BqlDecimal.Field<packedLength> { }
        #endregion

        #region PackingMaterialQty
        [PXDBInt()]
        [PXUIField(DisplayName = "Packing Material Qty")]
        public virtual int? PackingMaterialQty { get; set; }
        public abstract class packingMaterialQty : PX.Data.BQL.BqlInt.Field<packingMaterialQty> { }
        #endregion

    }
}