using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Common;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Extensions;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL;
using PX.Objects.IN.RelatedItems;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO.Attributes;
using PX.Objects.SO.Interfaces;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.Objects;
using PX.TM;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class SOOrderExt : PXCacheExtension<SOOrder>
    {
        #region UsrSalesOrderName
        [PXDBString]
        [PXUIField(DisplayName="Sales Order  Name")]
        public  string UsrSalesOrderName { get; set; }
        public abstract class usrSalesOrderName : PX.Data.BQL.BqlString.Field<usrSalesOrderName> { }
        #endregion

        #region Usrapproverr
        [PXDBBool]
        [PXUIField(DisplayName="Approve RR")]
        public bool? Usrapproverr { get; set; }
        public abstract class usrapproverr : PX.Data.BQL.BqlBool.Field<usrapproverr> { }
        #endregion

        #region UsrOldQty
        [PXDBInt]
        [PXUIField(DisplayName = "OldQty")]
        public int? UsrOldQty { get; set; }
        public abstract class usrOldQty : PX.Data.BQL.BqlInt.Field<usrOldQty> { }
        #endregion

        #region UsrPOHold
        [PXDBBool]
        [PXUIField(DisplayName = "PO Hold")]

        [PXDefault(true, PersistingCheck = PXPersistingCheck.Nothing)]
        public bool? UsrPOHold { get; set; }
        public abstract class usrPOHold : PX.Data.BQL.BqlBool.Field<usrPOHold> { }
        #endregion
    }

    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    [PXNonInstantiatedExtension]
    public sealed class SO_SOOrder_ExistingColumn : PXCacheExtension<PX.Objects.SO.SOOrder>
    {
        #region CuryOrderTotal  
        [PXMergeAttributes(Method = MergeMethod.Append)]
        public Decimal? CuryOrderTotal { get; set; }
        #endregion

        #region Hold  
        [PXMergeAttributes(Method = MergeMethod.Append)]
        public bool? Hold { get; set; }
        #endregion
    }
}