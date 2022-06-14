using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace CloudianGlobal
{
  public class Withholdingtax : PXGraph<Withholdingtax>
  {
    [PXImport]
    public SelectFrom<withholdingtax>.View withholdingTax;

    public PXSave<withholdingtax > Save;
    public PXCancel<withholdingtax > Cancel;

  }
}