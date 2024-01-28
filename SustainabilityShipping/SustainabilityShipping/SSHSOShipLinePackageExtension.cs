using System;
using PX.Data;
using PX.Objects.SO;

namespace SustainabilityShipping
{
  [Serializable]
  [PXCacheName("SSHSOShipLinePackageExtension")]
    [PXTable(typeof(SOShipLineSplitPackage.recordID), IsOptional = true)]
  public class SSHSOShipLinePackageExtension : PXCacheExtension<SOShipLineSplitPackage>
  {
        public static bool IsActive() => true;
        #region RecordID
        [PXDBInt(IsKey = true)]
    [PXUIField(DisplayName = "Record ID")]
    public virtual int? RecordID { get; set; }
    public abstract class recordID : PX.Data.BQL.BqlInt.Field<recordID> { }
    #endregion

    #region PreferredPackingMaterial
    [PXDBInt()]
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