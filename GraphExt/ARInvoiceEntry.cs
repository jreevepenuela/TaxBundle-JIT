using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using PX.Common;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Data;
using PX.Data.DependencyInjection;
using PX.Objects.GL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.CR;
using PX.Objects.TX;
using PX.Objects.IN;
using PX.Objects.BQLConstants;
using PX.Objects.EP;
using PX.Objects.SO;
using PX.Objects.DR;
using PX.Objects.CA;
using SOInvoiceEntry = PX.Objects.SO.SOInvoiceEntry;
using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Objects.GL.Reclassification.UI;
using PX.Objects.AR.BQL;
using PX.Objects.Common.Extensions;
using PX.Objects.PM;
using System.Text;
using PX.Objects.Common.Bql;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.GL.FinPeriods;
using PX.Data.BQL.Fluent;
using PX.Data.BQL;
using PX.Objects;
using PX.Objects.AR;

namespace CloudianGlobal
{
    public static class Globalvar
    {
        static string _globalvalue;
        public static string GlobalValue
        {
            get
            {
                return _globalvalue;
            }
            set
            {
                _globalvalue = value;
            }
        }
    }
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class ARInvoiceEntry_Extension : PXGraphExtension<ARInvoiceEntry>
    {

        public PXAction<ARInvoice> WithholdingTax;
        [PXButton]
        [PXUIField(DisplayName = "Withholding Tax")]
        protected void withholdingTax()
        {
            var Intid = 0;
            InventoryItem wtax = PXSelect<InventoryItem, Where<InventoryItem.inventoryCD, 
                Equal<Required<InventoryItem.inventoryCD>>>>.Select(Base, "WTAX");

            if (wtax != null)
                Intid = Convert.ToInt32(wtax.InventoryID);

            PXCache cache1 = Base.Document.Cache;
            PXCache cache2 = Base.Transactions.Cache;
            PXCache cache3 = Base.Adjustments_1.Cache;
            var status = Base.Document.Current;
            //var refnum = "";
            var totalwhol = 0.00;

            foreach (ARTran wTaxExist in Base.Transactions.Select())
            {
                if (wTaxExist.InventoryID == Intid)
                    return;
            }

            foreach (ARInvoice row in Base.Document.Cache.Cached)
            {
                if (row.DocType == "CRM")
                {
                    foreach (ARTran rowss in Base.Transactions.Cache.Cached)
                    {
                        var rowssext = PXCache<ARTran>.GetExtension<ARTranExt>(rowss);
                        totalwhol = totalwhol + Convert.ToDouble(rowssext.UsrTaxAmount);
                        PXCache cache = Base.Transactions.Cache;
                        cache.Delete(rowss);

                    }
                    cache1.SetValue<ARInvoice.docDesc>(row, "Withholding Tax");
                    ARTran red = new ARTran();
                    red.InventoryID = Intid;
                    red.CuryExtPrice = Convert.ToInt32(totalwhol);
                    cache2.Insert(red);


                    ARAdjust red1 = new ARAdjust();
                    red1.AdjdRefNbr = row.OrigRefNbr;
                    red1.CuryAdjgAmt = Convert.ToInt32(totalwhol);
                    cache3.Insert(red1);
                }
                else if (row.Released == false)
                {
                    // Acuminator disable once PX1050 HardcodedStringInLocalizationMethod [Justification]
                    throw new PXException("Release and Reverse Before Applying WithHolding Tax");
                }
            }
        }

        public delegate IEnumerable ReleaseDelegate(PXAdapter adapter);
        [PXOverride]
        public IEnumerable Release(PXAdapter adapter, ReleaseDelegate baseMethod)
        {
            string errorSaving = "Error on saving WTAX line click WTAX button";

            foreach(ARInvoice aRInvoice in Base.Document.Cache.Cached)
            {
                if (aRInvoice != null)
                {
                    if (aRInvoice.DocType == "CRM")
                    {
                        foreach (ARTran aRTran in Base.Transactions.Cache.Cached)
                        {    
                            ARTranExt aRTranExt = aRTran.GetExtension<ARTranExt>();
                            if (aRTranExt.UsrATC != "N/A")
                            {
                                throw new PXException(errorSaving);
                            }
                                    
                        }
                    }

                }
            }
           
            return baseMethod(adapter);
        }


        #region Event Handlers
        protected void _(Events.RowSelected<ARInvoice> e)
        {
            var row = e.Row;
            PXUIFieldAttribute.SetEnabled<ARRegisterExt.usrCounterDate>(e.Cache, e.Row, true);

            foreach (ARInvoice aRInvoice in Base.Document.Cache.Cached)
            {
                ARRegisterExt aRRegisterExt = aRInvoice.GetExtension<ARRegisterExt>();
                var getInvoicenoooo = aRRegisterExt.UsrInvoicenoooo;
                var getType = aRRegisterExt.UsrInvoiceType;
                var getFinalInvoiceno = aRRegisterExt.Usrfinalinvoiceno;
                if (getInvoicenoooo != null && getType != null)
                {
                    var finalValue = getType + getInvoicenoooo;
                    aRRegisterExt.Usrfinalinvoiceno = finalValue;
                }
                if (getFinalInvoiceno == null)
                    aRRegisterExt.Usrfinalinvoiceno = " <NEW>";

                if (getFinalInvoiceno == " <NEW>" || getFinalInvoiceno == null)
                    PXUIFieldAttribute.SetEnabled<ARRegisterExt.usrInvoiceType>(e.Cache, e.Row, true);
                else
                    PXUIFieldAttribute.SetEnabled<ARRegisterExt.usrInvoiceType>(e.Cache, e.Row, false);
            }
                
            if (row.DocType == "INV")
                WithholdingTax.SetEnabled(false);
            else if (row.DocType == "CRM" && row.Status == "H")
                WithholdingTax.SetEnabled(true);

            if (row.Released == true)
                WithholdingTax.SetEnabled(false);

            if (row.Status == "B")
                WithholdingTax.SetEnabled(false);
        }

        protected void _(Events.RowSelected<ARTran> e)
        {
            var doc = Base.Document.Current;
            ARTran row = e.Row as ARTran;
            PXUIFieldAttribute.SetEnabled<ARTranExt.usrATC>(e.Cache, e.Row, true);
        }


        protected void _(Events.FieldUpdated<ARInvoice, ARRegisterExt.usrCounterDate> e)
        {
            var row = e.Row;
            ARRegisterExt aRRegisterExt = row.GetExtension<ARRegisterExt>();
            var termsID = row.TermsID;
            if (termsID != null)
            {
                Terms terms = SelectFrom<Terms>.Where<Terms.termsID.
                    IsEqual<@P.AsString>>.View.Select(Base, termsID);

                var dayTerms = terms.DayDue00;

                DateTime counterDate = Convert.ToDateTime(aRRegisterExt.UsrCounterDate);
                Int64 termsDay = Convert.ToInt64(dayTerms);
                DateTime dueDate = counterDate.AddDays(termsDay);

                e.Cache.SetValue<ARInvoice.dueDate>(row, dueDate);
            }
        }




        protected void _(Events.RowPersisting<ARInvoice> e)
        {
            var row = e.Row;

            var ini = 0;
            var final = 0;
            var lastnum = 0;

            if (row != null)
            {
                foreach (ARInvoice docu in Base.Document.Cache.Cached)
                {
                    PXCache cache = Base.Document.Cache;
                    Globalvar.GlobalValue = docu.RefNbr;
                }
                PXCache cache1 = Base.Document.Cache;
                var newew = row.RefNbr;
                ARRegisterExt aRRegisterExt = row.GetExtension<ARRegisterExt>();
                var cunbr = aRRegisterExt.UsrInvoicenoooo;

                if (row.DocType == "INV")
                {
                    if (newew == " <NEW>" || cunbr == null)
                    {
                        var gettypes = aRRegisterExt.UsrInvoiceType;
                        if (gettypes == null)
                            return;
                        else if (gettypes == "SI")
                        {
                            ARRegister ress = PXSelect<ARRegister, Where<ARRegisterExt.usrInvoiceType, Equal<Required<ARRegisterExt.usrInvoiceType>>,
                                And<ARRegisterExt.usrInvoicenoooo, IsNotNull,
                                And<ARRegister.branchID, Equal<Required<ARRegister.branchID>>>>>,
                                OrderBy<Desc<ARRegister.createdDateTime>>>.Select(Base, "SI", row.BranchID);
                            if (ress != null)
                            {
                                var ressext = PXCache<ARRegister>.GetExtension<ARRegisterExt>(ress);

                                if (ressext.UsrInvoicenoooo == null)
                                {
                                    lastnum = 0;
                                }
                                else
                                {
                                    if (Convert.ToInt32(ressext.UsrInvoicenoooo) == lastnum)
                                    {
                                        ini = Convert.ToInt32(ressext.UsrInvoicenoooo);
                                        lastnum = Convert.ToInt32(ressext.UsrInvoicenoooo);

                                    }
                                    else if (Convert.ToInt32(ressext.UsrInvoicenoooo) > lastnum)
                                    {
                                        lastnum = Convert.ToInt32(ressext.UsrInvoicenoooo);

                                    }

                                }
                            }
                        }
                        else if (gettypes == "BI")
                        {
                            ARRegister ress = PXSelect<ARRegister, Where<ARRegisterExt.usrInvoiceType, Equal<Required<ARRegisterExt.usrInvoiceType>>,
                                And<ARRegisterExt.usrInvoicenoooo, IsNotNull,
                                And<ARRegister.branchID, Equal<Required<ARRegister.branchID>>>>>,
                                OrderBy<Desc<ARRegister.createdDateTime>>>.Select(Base, "BI", row.BranchID);
                            if (ress != null)
                            {
                                var ressext = PXCache<ARRegister>.GetExtension<ARRegisterExt>(ress);

                                if (ressext.UsrInvoicenoooo == null)
                                {
                                    lastnum = 0;
                                }
                                else
                                {
                                    if (Convert.ToInt32(ressext.UsrInvoicenoooo) == lastnum)
                                    {
                                        ini = Convert.ToInt32(ressext.UsrInvoicenoooo);
                                        lastnum = Convert.ToInt32(ressext.UsrInvoicenoooo);

                                    }
                                    else if (Convert.ToInt32(ressext.UsrInvoicenoooo) > lastnum)
                                    {
                                        lastnum = Convert.ToInt32(ressext.UsrInvoicenoooo);

                                    }

                                }
                            }
                        }
                        else if (gettypes == "NI")
                        {
                            ARRegister ress = PXSelect<ARRegister, Where<ARRegisterExt.usrInvoiceType, Equal<Required<ARRegisterExt.usrInvoiceType>>,
                                And<ARRegisterExt.usrInvoicenoooo, IsNotNull,
                                And<ARRegister.branchID, Equal<Required<ARRegister.branchID>>>>>,
                                OrderBy<Desc<ARRegister.createdDateTime>>>.Select(Base, "NI", row.BranchID);
                            if (ress != null)
                            {
                                var ressext = PXCache<ARRegister>.GetExtension<ARRegisterExt>(ress);

                                if (ressext.UsrInvoicenoooo == null)
                                {
                                    lastnum = 0;
                                }
                                else
                                {
                                    if (Convert.ToInt32(ressext.UsrInvoicenoooo) == lastnum)
                                    {
                                        ini = Convert.ToInt32(ressext.UsrInvoicenoooo);
                                        lastnum = Convert.ToInt32(ressext.UsrInvoicenoooo);

                                    }
                                    else if (Convert.ToInt32(ressext.UsrInvoicenoooo) > lastnum)
                                    {
                                        lastnum = Convert.ToInt32(ressext.UsrInvoicenoooo);

                                    }

                                }
                            }

                        }
                        cache1.SetValueExt<ARRegisterExt.usrInvoicenoooo>(row, Convert.ToString(Convert.ToInt32(lastnum) + 1));
                        lastnum = 0;
                    }
                }
                else
                    cache1.SetValueExt<ARRegisterExt.usrfinalinvoiceno>(row, null); 
            }

        }

        protected void _(Events.FieldUpdated<ARTran, ARTranExt.usrDescription> e)
        {
            ARTran tram = e.Row as ARTran;
            if (tram != null)
            {
                ARTranExt tranExt = tram.GetExtension<ARTranExt>();
                if (tranExt != null)
                {
                    var curyam = tram.CuryExtPrice;
                    var concuryam = Convert.ToDecimal(curyam);
                    var taxrate = tranExt.UsrDescription;
                    var contaxrate = Convert.ToDecimal(taxrate);
                    var red = concuryam * contaxrate;
                    var redx = Convert.ToString(red);
                    e.Cache.SetValue<ARTranExt.usrTaxAmount>(tram, redx);
                }
            }
        }

        protected void ARTran_UsrATC_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
        {
                cache.SetDefaultExt<ARTranExt.usrDescription>(e.Row);
                var row = (ARTran)e.Row;
        }

        protected void _(Events.FieldUpdated<ARTran, ARTran.curyExtPrice> e)
        {
            ARTran tram = e.Row as ARTran;
            if (tram != null)
            {
                ARTranExt tranExt = tram.GetExtension<ARTranExt>();
                if (tranExt != null)
                {
                    var curyam = tram.CuryExtPrice;
                    var concuryam = Convert.ToDecimal(curyam);
                    var taxrate = tranExt.UsrDescription;
                    var contaxrate = Convert.ToDecimal(taxrate);
                    var red = concuryam * contaxrate;
                    var redx = Convert.ToString(red);
                    e.Cache.SetValue<ARTranExt.usrTaxAmount>(tram, redx);
                }
            }      
        }
        #endregion
    }
}