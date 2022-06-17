using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class ARRegisterExt : PXCacheExtension<PX.Objects.AR.ARRegister>
    {
        #region UsrCounterDate
        [PXDBDate]
        [PXUIField(DisplayName = "Counter Date")]

        public DateTime? UsrCounterDate { get; set; }
        public abstract class usrCounterDate : PX.Data.BQL.BqlDateTime.Field<usrCounterDate> { }
        #endregion

        #region UsrInvoiceType
        [PXDBString]
        [PXUIField(DisplayName = "Invoice Type")]
        [PXStringList(new string[] { "SI", "BI", "NI" }, new string[] { "Sales Invoice", "Billing Invoice", "No Invoice" })]
        public string UsrInvoiceType { get; set; }
        public abstract class usrInvoiceType : PX.Data.BQL.BqlString.Field<usrInvoiceType> { }
        #endregion

        #region UsrInvoiceNbr
        [PXDBString]
        [PXUIField(DisplayName = "Invoice No.")]

        public string UsrInvoiceNbr { get; set; }
        public abstract class usrInvoiceNbr : PX.Data.BQL.BqlString.Field<usrInvoiceNbr> { }
        #endregion

        #region UsrInvoicenoooo
        [PXDBString]
        [PXUIField(DisplayName = "InvoiceNbr")]

        public string UsrInvoicenoooo { get; set; }
        public abstract class usrInvoicenoooo : PX.Data.BQL.BqlString.Field<usrInvoicenoooo> { }
        #endregion

        #region Usrfinalinvoiceno
        [PXDBString]
        [PXUIField(DisplayName = "Invoice Nbr", IsReadOnly = true)]
        public string Usrfinalinvoiceno { get; set; }
        public abstract class usrfinalinvoiceno : PX.Data.BQL.BqlString.Field<usrfinalinvoiceno> { }
        #endregion
    }
}
