using System;
using PX.Data;

namespace SustainabilityShipping
{
  [Serializable]
  [PXCacheName("SSHLevelSustainability")]
  public class SSHLevelSustainability : IBqlTable
  {
    #region SustainabilityID
    [PXDBInt(IsKey = true)]
    [PXUIField(DisplayName = "Sustainability ID")]
    public virtual int? SustainabilityID { get; set; }
    public abstract class sustainabilityID : PX.Data.BQL.BqlInt.Field<sustainabilityID> { }
    #endregion

    #region LevelName
    [PXDBString(40, InputMask = "")]
    [PXUIField(DisplayName = "Level Name")]
    public virtual string LevelName { get; set; }
    public abstract class levelName : PX.Data.BQL.BqlString.Field<levelName> { }
    #endregion

    #region LevelDescription
    [PXDBString(255, InputMask = "")]
    [PXUIField(DisplayName = "Level Description")]
    public virtual string LevelDescription { get; set; }
    public abstract class levelDescription : PX.Data.BQL.BqlString.Field<levelDescription> { }
    #endregion
  }
}