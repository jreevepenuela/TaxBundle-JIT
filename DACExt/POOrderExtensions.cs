using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Common;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.Common.Bql;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects;
using PX.SM;
using PX.TM;
using System.Collections.Generic;
using System;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class POOrderExt : PXCacheExtension<PX.Objects.PO.POOrder>
    {
        #region UsrContract
        [PXDBBool]
        [PXUIField(DisplayName="PO Contract")]
        public bool? UsrContract { get; set; }
        public abstract class usrContract : PX.Data.BQL.BqlBool.Field<usrContract> { }
        #endregion

        #region UsrPORush
        [PXDBBool]
        [PXUIField(DisplayName="PO Rush")]
        public bool? UsrPORush { get; set; }
        public abstract class usrPORush : PX.Data.BQL.BqlBool.Field<usrPORush> { }
        #endregion

        #region UsrPOName
        [PXDBString(225)]
        [PXUIField(DisplayName="PO Name")]
        public  string UsrPOName { get; set; }
        public abstract class usrPOName : PX.Data.BQL.BqlString.Field<usrPOName> { }
        #endregion
    }
}