using System;
using PX.Data;
using PX.Objects.IN;

namespace SustainabilityShipping
{

    [PXTable(typeof(InventoryItem.inventoryID), IsOptional = true)]
    public  class SSHPackingMaterialsExtension : PXCacheExtension<InventoryItem>

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
        [PXUIField(DisplayName = "Sustainability ID")]
        public virtual int? SustainabilityID { get; set; }
        public abstract class sustainabilityID : PX.Data.BQL.BqlInt.Field<sustainabilityID> { }
        #endregion

        #region FragilityLevel
        [PXDBInt()]
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
        [PXUIField(DisplayName = "Co2ekg")]
        public virtual Decimal? Co2ekg { get; set; }
        public abstract class co2ekg : PX.Data.BQL.BqlDecimal.Field<co2ekg> { }
        #endregion

        #region PreferredPackingMaterial
        [PXDBInt()]
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

        #region CreatedByID
        [PXDBCreatedByID()]
        public virtual Guid? CreatedByID { get; set; }
        public abstract class createdByID : PX.Data.BQL.BqlGuid.Field<createdByID> { }
        #endregion

        #region CreatedByScreenID
        [PXDBCreatedByScreenID()]
        public virtual string CreatedByScreenID { get; set; }
        public abstract class createdByScreenID : PX.Data.BQL.BqlString.Field<createdByScreenID> { }
        #endregion

        #region CreatedDateTime
        [PXDBCreatedDateTime()]
        public virtual DateTime? CreatedDateTime { get; set; }
        public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
        #endregion

        #region LastModifiedByID
        [PXDBLastModifiedByID()]
        public virtual Guid? LastModifiedByID { get; set; }
        public abstract class lastModifiedByID : PX.Data.BQL.BqlGuid.Field<lastModifiedByID> { }
        #endregion

        #region LastModifiedByScreenID
        [PXDBLastModifiedByScreenID()]
        public virtual string LastModifiedByScreenID { get; set; }
        public abstract class lastModifiedByScreenID : PX.Data.BQL.BqlString.Field<lastModifiedByScreenID> { }
        #endregion

        #region LastModifiedDateTime
        [PXDBLastModifiedDateTime()]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
        #endregion

        #region Tstamp
        [PXDBTimestamp()]
        [PXUIField(DisplayName = "Tstamp")]
        public virtual byte[] Tstamp { get; set; }
        public abstract class tstamp : PX.Data.BQL.BqlByteArray.Field<tstamp> { }
        #endregion

        #region Noteid
        [PXNote()]
        public virtual Guid? Noteid { get; set; }
        public abstract class noteid : PX.Data.BQL.BqlGuid.Field<noteid> { }
        #endregion
    }
}