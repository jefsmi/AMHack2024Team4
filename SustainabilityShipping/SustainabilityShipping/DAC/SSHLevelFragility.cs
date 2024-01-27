using System;
using PX.Data;

namespace SustainabilityShipping
{
  [Serializable]
  [PXCacheName("SSHLevelFragility")]
  public class SSHLevelFragility : IBqlTable
  {
    #region FragilityLevel
    [PXDBInt(IsKey = true)]
    [PXUIField(DisplayName = "Fragility Level")]
    public virtual int? FragilityLevel { get; set; }
    public abstract class fragilityLevel : PX.Data.BQL.BqlInt.Field<fragilityLevel> { }
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