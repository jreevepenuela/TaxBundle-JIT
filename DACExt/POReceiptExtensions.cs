using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Common;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Bql;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects;
using PX.TM;
using System.Collections.Generic;
using System;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class POReceiptExt : PXCacheExtension<PX.Objects.PO.POReceipt>
  {
    #region UsrImportEntry
    [PXDBString(15)]
    [PXUIField(DisplayName="Import Entry")]
    public string UsrImportEntry { get; set; }
    public abstract class usrImportEntry : PX.Data.BQL.BqlString.Field<usrImportEntry> { }
    #endregion

    #region UsrAssesmentDate
    [PXDBDate]
    [PXUIField(DisplayName="Assesment Date")]
    public DateTime? UsrAssesmentDate { get; set; }
    public abstract class usrAssesmentDate : PX.Data.BQL.BqlDateTime.Field<usrAssesmentDate> { }
    #endregion
  }
}