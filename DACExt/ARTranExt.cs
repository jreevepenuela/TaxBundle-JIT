using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class ARTranExt : PXCacheExtension<PX.Objects.AR.ARTran>
    {
        #region UsrATC
        [PXDBString(10)]
        [PXUIField(DisplayName = "WTax Code")]
        [PXSelector(
        typeof(Search<withholdingtax.atc>),
            typeof(withholdingtax.description),
            typeof(withholdingtax.taxRate))]
        public string UsrATC { get; set; }
        public abstract class usrATC : PX.Data.BQL.BqlString.Field<usrATC> { }
        #endregion

        #region UsrDescription
        [PXDBString(100)]
        [PXUIField(DisplayName = "WTax Rate")]
        [PXDefault(typeof(Search<withholdingtax.taxRate, Where<withholdingtax.atc, Equal<Current<ARTranExt.usrATC>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        public  string UsrDescription { get; set; }
        public abstract class usrDescription : PX.Data.BQL.BqlString.Field<usrDescription> { }
        #endregion

        #region UsrTaxAmount
        [PXDBString(10)]
        [PXUIField(DisplayName = "WTax Amount")]

        public string UsrTaxAmount { get; set; }
        public abstract class usrTaxAmount : PX.Data.BQL.BqlString.Field<usrTaxAmount> { }
        #endregion

        #region UsrAmtPrint
        [PXDBString]
        [PXUIField(DisplayName = "Amount (For Print-out)")]

        public  string UsrAmtPrint { get; set; }
        public abstract class usrAmtPrint : PX.Data.BQL.BqlString.Field<usrAmtPrint> { }
        #endregion

        #region UsrUOMPrint
        [PXDBString]
        [PXUIField(DisplayName = "UOM (For Print-out)")]

        public  string UsrUOMPrint { get; set; }
        public abstract class usrUOMPrint : PX.Data.BQL.BqlString.Field<usrUOMPrint> { }
        #endregion

        #region UsrQtyPrint
        [PXDBString]
        [PXUIField(DisplayName = "Qty (For Print-out)")]

        public string UsrQtyPrint { get; set; }
        public abstract class usrQtyPrint : PX.Data.BQL.BqlString.Field<usrQtyPrint> { }
        #endregion
    }

}
