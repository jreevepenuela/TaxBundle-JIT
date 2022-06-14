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
        #region Event Handlers

        protected void _(Events.RowSelected<ARTran> e)
        {
            var doc = Base.Document.Current;
            ARTran row = e.Row as ARTran;
            PXUIFieldAttribute.SetEnabled<ARTranExt.usrATC>(e.Cache, e.Row, true);
        }

    

        protected void ARInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
        {

            var row = (ARInvoice)e.Row;
            PXUIFieldAttribute.SetEnabled<ARRegisterExt.usrCounterDate>(cache, e.Row, true);

            foreach (ARInvoice rows in Base.Document.Cache.Cached)
            {
                PXCache cache1 = Base.Document.Cache;

                var rowsExt = PXCache<ARRegister>.GetExtension<ARRegisterExt>(rows);
                var getnoo = rowsExt.UsrInvoicenoooo;
                var gettype = rowsExt.UsrInvoiceType;
                var finalnobr = rowsExt.Usrfinalinvoiceno;
                if (getnoo != null && gettype != null)
                {
                    var finalval = gettype + getnoo;
                    cache1.SetValue<ARRegisterExt.usrfinalinvoiceno>(rows, finalval);

                }
                if (finalnobr == null)
                {
                    cache1.SetValue<ARRegisterExt.usrfinalinvoiceno>(rows, " <NEW>");
                }
                if (finalnobr == " <NEW>" || finalnobr == null)
                {
                    PXUIFieldAttribute.SetEnabled<ARRegisterExt.usrInvoiceType>(cache, e.Row, true);
                }
                else
                {
                    PXUIFieldAttribute.SetEnabled<ARRegisterExt.usrInvoiceType>(cache, e.Row, false);
                }
            }
        }

        protected void ARInvoice_UsrCounterDate_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
        {
            var row = (ARInvoice)e.Row;
           
            var Rowext = PXCache<ARRegister>.GetExtension<ARRegisterExt>(row);
            var addplus = row.TermsID;
            if (addplus == null)
            {
            }
            else
            {
            Terms termsdays = PXSelect<Terms, Where<Terms.termsID, Equal<Required<Terms.termsID>>>>.Select(Base, addplus);
            var dayinterms = termsdays.DayDue00;

                DateTime date1 = Convert.ToDateTime(Rowext.UsrCounterDate);
                Int64 date2 = Convert.ToInt64(dayinterms);
                DateTime enddate = date1.AddDays(date2);
                
            cache.SetValue<ARInvoice.dueDate>(row, enddate);
            }
        }

        #region Extended initialization
        public override void Initialize()
        {
            base.Initialize();
        }
        #endregion

        public PXAction<ARInvoice> WithHoldingTax;
        [PXButton]
        [PXUIField(DisplayName = "WithHoldingTax")]
        protected void withHoldingTax()
        {
            var Intid = 0;
            InventoryItem wtax = PXSelect<InventoryItem, Where<InventoryItem.inventoryCD, 
                Equal<Required<InventoryItem.inventoryCD>>>>.Select(Base, "WTAX");

            if (wtax != null)
            {
                Intid = Convert.ToInt32(wtax.InventoryID);
            }

            PXCache cache1 = Base.Document.Cache;
            PXCache cache2 = Base.Transactions.Cache;
            PXCache cache3 = Base.Adjustments_1.Cache;
            var refnum = "";
            var totalwhol = 0.00;

            foreach (ARInvoice row in Base.Document.Cache.Cached)
            {
                refnum = row.RefNbr;
                if (row.DocType == "CRM")
                {
                    foreach (ARTran rowss in Base.Transactions.Cache.Cached)
                    {
                        var rowssext = PXCache<ARTran>.GetExtension<ARTranExt>(rowss);
                        totalwhol = totalwhol + Convert.ToDouble(rowssext.UsrTaxAmount);
                        PXCache cache = Base.Transactions.Cache;
                        cache.Delete(rowss);

                    }
                    cache1.SetValue<ARInvoice.docDesc>(row, "WithHolding Tax");
                    ARTran red = new ARTran();
                    red.InventoryID = Intid;
                    red.CuryExtPrice = Convert.ToInt32(totalwhol);
                    cache2.Insert(red);

                    foreach (ARAdjust rowsss in Base.Adjustments_1.Cache.Cached)
                    {
                        
                       
                        PXCache cache = Base.Adjustments_1.Cache;
                        cache.Delete(rowsss);

                    }

                    ARAdjust red1 = new ARAdjust();
                    red1.AdjdRefNbr = Globalvar.GlobalValue;
                    red1.CuryAdjgAmt = Convert.ToInt32(totalwhol);
                    cache3.Insert(red1);

                   
                }
                else if (row.Released == false)
                {
                    // Acuminator disable once PX1050 HardcodedStringInLocalizationMethod [Justification]
                    throw new PXException("Release and Reverse Before Applying WithHolding Tax");
                }
            }
            cache3.SetValue<ARAdjust.curyAdjdAmt>(cache3, Convert.ToInt32(totalwhol));
        }

        public delegate void PersistDelegate();
        [PXOverride]
        public void Persist(PersistDelegate baseMethod)
        {
            foreach (ARTran row in Base.Transactions.Cache.Cached)
            {
                var rowExt = PXCache<ARTran>.GetExtension<ARTranExt>(row);
                if (rowExt.UsrDescription == null)
                {
                    PXCache cache = Base.Transactions.Cache;

                    cache.SetValue<ARTranExt.usrDescription>(row, "0.00");

                }
                if (rowExt.UsrATC == null)
                {
                    PXCache cache = Base.Transactions.Cache;

                    cache.SetValue<ARTranExt.usrATC>(row, "N/A");
                }
            }
            //baseMethod();
            foreach (ARInvoice docu in Base.Document.Cache.Cached)
            {
                PXCache cache = Base.Document.Cache;
                Globalvar.GlobalValue = docu.RefNbr;
            }
            var ini = 0;
            var final = 0;
            var lastnum = 0;

            ARInvoice rows = Base.Document.Current;
            PXCache cache1 = Base.Document.Cache;
            var newew = rows.RefNbr;
            var rowsExt = PXCache<ARRegister>.GetExtension<ARRegisterExt>(rows);
            var cunbr = rowsExt.UsrInvoicenoooo;
            if(rows.DocType == "INV")
            {

                if (newew == " <NEW>" || cunbr == null)
                {
                    var gettypes = rowsExt.UsrInvoiceType;
                    if (gettypes == null)
                    {
                        baseMethod();
                        return;
                    }
                    else if (gettypes == "SI")
                    {
                        ARRegister ress = PXSelect<ARRegister, Where<ARRegisterExt.usrInvoiceType, Equal<Required<ARRegisterExt.usrInvoiceType>>,
                            And<ARRegisterExt.usrInvoicenoooo, IsNotNull,
                            And<ARRegister.branchID, Equal<Required<ARRegister.branchID>>>>>,
                            OrderBy<Desc<ARRegister.createdDateTime>>>.Select(Base, "SI", rows.BranchID);

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
                            OrderBy<Desc<ARRegister.createdDateTime>>>.Select(Base, "BI", rows.BranchID);

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
                            OrderBy<Desc<ARRegister.createdDateTime>>>.Select(Base, "NI", rows.BranchID);

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
                    cache1.SetValueExt<ARRegisterExt.usrInvoicenoooo>(rows, Convert.ToString(Convert.ToInt32(lastnum) + 1));
                    lastnum = 0;
                }
            }
            else
            {
               // cache1.SetValueExt<ARRegisterExt.usrInvoiceType>(rows, null);
                cache1.SetValueExt<ARRegisterExt.usrfinalinvoiceno>(rows, null);
            }
            foreach (ARInvoice rowss in Base.Document.Cache.Cached)
            {
                var rowssExt = PXCache<ARRegister>.GetExtension<ARRegisterExt>(rowss);
                if (rowssExt.UsrInvoiceType == null && rowss.Status == "B")
                {
                    // Acuminator disable once PX1050 HardcodedStringInLocalizationMethod [Justification]
                    throw new PXException("Invoice Type Cannot Be null");

                }
                else
                {
                    baseMethod();
                }
            }
        }  

        public delegate IEnumerable ReleaseDelegate(PXAdapter adapter);
        [PXOverride]
        public IEnumerable Release(PXAdapter adapter, ReleaseDelegate baseMethod)
        {
            foreach (ARTran row in Base.Transactions.Cache.Cached)
            {
                var rowExt = PXCache<ARTran>.GetExtension<ARTranExt>(row);
                if (rowExt.UsrDescription == null)
                {
                    PXCache cache = Base.Transactions.Cache;

                    cache.SetValue<ARTranExt.usrDescription>(row, "0.00");

                }
                if (rowExt.UsrATC == null)
                {
                    PXCache cache = Base.Transactions.Cache;

                    cache.SetValue<ARTranExt.usrATC>(row, "N/A");
                }
            }
            return baseMethod(adapter);
        }

        protected void ARTran_UsrDescription_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
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
                    cache.SetValue<ARTranExt.usrTaxAmount>(tram, redx);
                }
            }
            var row = (ARTran)e.Row;
        }

        protected void ARTran_UsrATC_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
        {
                cache.SetDefaultExt<ARTranExt.usrDescription>(e.Row);
                var row = (ARTran)e.Row;
        }

        protected void ARTran_CuryExtPrice_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
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
                    cache.SetValue<ARTranExt.usrTaxAmount>(tram, redx);
                }
            }
            var row = (ARTran)e.Row;
       
        }
        #endregion
    }
}