using System;
using System.Collections;
using System.Collections.Generic;
using PX.Common;
using PX.Api;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.TX;
using PX.Objects.CA;
using ItemLotSerial = PX.Objects.IN.Overrides.INDocumentRelease.ItemLotSerial;
using SiteLotSerial = PX.Objects.IN.Overrides.INDocumentRelease.SiteLotSerial;
using LocationStatus = PX.Objects.IN.Overrides.INDocumentRelease.LocationStatus;
using LotSerialStatus = PX.Objects.IN.Overrides.INDocumentRelease.LotSerialStatus;
using POLineType = PX.Objects.PO.POLineType;
using POReceipt = PX.Objects.PO.POReceipt;
using POReceiptType = PX.Objects.PO.POReceiptType;
using POReceiptLine = PX.Objects.PO.POReceiptLine;
using SiteStatus = PX.Objects.IN.Overrides.INDocumentRelease.SiteStatus;
using System.Linq;
using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.AR.MigrationMode;
using PX.Objects.Common.Bql;
using PX.Common.Collection;
using PX.Objects.GL.FinPeriods;
using PX.Objects.Extensions.PaymentTransaction;
using PX.Objects;
using PX.Objects.SO;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class SOInvoiceEntry_Extension : PXGraphExtension<SOInvoiceEntry>
  {
    #region Event Handlers

    protected void ARInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
    {
      
      var row = (ARInvoice)e.Row;
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
              if (finalnobr == null){
              cache1.SetValue<ARRegisterExt.usrfinalinvoiceno>(rows, " <NEW>");
              }
              if(finalnobr == " <NEW>" || finalnobr == null)
              {
              PXUIFieldAttribute.SetEnabled<ARRegisterExt.usrInvoiceType>(cache, e.Row, true);
              }
              else {
              PXUIFieldAttribute.SetEnabled<ARRegisterExt.usrInvoiceType>(cache, e.Row, false);
              }
            }
    }

    

    public delegate void PersistDelegate();
    [PXOverride]
    public void Persist(PersistDelegate baseMethod)
    {
            //var ini = 0;
            //var final = 0;
            //var lastnum = 0;

            //ARInvoice rows = Base.Document.Current;

            //PXCache cache = Base.Document.Cache;
            //    var newew = rows.RefNbr;
            //   var rowsExt = PXCache<ARRegister>.GetExtension<ARRegisterExt>(rows);
            //  var cunbr = rowsExt.UsrInvoicenoooo;
            //  if(newew == " <NEW>"||cunbr == "1"){

            //    var gettypes = rowsExt.UsrInvoiceType;
            //    if (gettypes == null)
            //    {

            //    }
            //    else if (gettypes == "SI")
            //    {
            //        foreach (ARRegister ress in PXSelect<ARRegister, Where<ARRegisterExt.usrInvoiceType, Equal<Required<ARRegisterExt.usrInvoiceType>>, And<ARRegisterExt.usrInvoicenoooo,IsNotNull>>>.Select(Base, "SI"))
            //        {
            //            if (ress != null)
            //            {
            //                var ressext = PXCache<ARRegister>.GetExtension<ARRegisterExt>(ress);

            //                if (ressext.UsrInvoicenoooo == null)
            //                {
            //                    lastnum = 0;
            //                }
            //                else
            //                {
            //                    if (Convert.ToInt32(ressext.UsrInvoicenoooo) == lastnum)
            //                    {
            //                        ini = Convert.ToInt32(ressext.UsrInvoicenoooo);
            //                        lastnum = Convert.ToInt32(ressext.UsrInvoicenoooo);

            //                    }
            //                    else if (Convert.ToInt32(ressext.UsrInvoicenoooo) > lastnum)
            //                    {
            //                        lastnum = Convert.ToInt32(ressext.UsrInvoicenoooo);

            //                    }

            //                }
            //            }


            //        }


            //    }
            //    else if (gettypes == "BI")
            //    {
            //        foreach (ARRegister ress in PXSelect<ARRegister, Where<ARRegisterExt.usrInvoiceType, Equal<Required<ARRegisterExt.usrInvoiceType>>, And<ARRegisterExt.usrInvoicenoooo, IsNotNull>>>.Select(Base, "BI"))
            //        {
            //            if (ress != null)
            //            {
            //                var ressext = PXCache<ARRegister>.GetExtension<ARRegisterExt>(ress);

            //                if (ressext.UsrInvoicenoooo == null)
            //                {
            //                    lastnum = 0;
            //                }
            //                else
            //                {
            //                    if (Convert.ToInt32(ressext.UsrInvoicenoooo) == lastnum)
            //                    {
            //                        ini = Convert.ToInt32(ressext.UsrInvoicenoooo);
            //                        lastnum = Convert.ToInt32(ressext.UsrInvoicenoooo);

            //                    }
            //                    else if (Convert.ToInt32(ressext.UsrInvoicenoooo) > lastnum)
            //                    {
            //                        lastnum = Convert.ToInt32(ressext.UsrInvoicenoooo);

            //                    }

            //                }
            //            }


            //        }


            //    }
            //    else if (gettypes == "NI")
            //    {
            //        foreach (ARRegister ress in PXSelect<ARRegister, Where<ARRegisterExt.usrInvoiceType, Equal<Required<ARRegisterExt.usrInvoiceType>>, And<ARRegisterExt.usrInvoicenoooo, IsNotNull>>>.Select(Base, "NI"))
            //        {
            //            if (ress != null)
            //            {
            //                var ressext = PXCache<ARRegister>.GetExtension<ARRegisterExt>(ress);

            //                if (ressext.UsrInvoicenoooo == null)
            //                {
            //                    lastnum = 0;
            //                }
            //                else
            //                {
            //                    if (Convert.ToInt32(ressext.UsrInvoicenoooo) == lastnum)
            //                    {
            //                        ini = Convert.ToInt32(ressext.UsrInvoicenoooo);
            //                        lastnum = Convert.ToInt32(ressext.UsrInvoicenoooo);

            //                    }
            //                    else if (Convert.ToInt32(ressext.UsrInvoicenoooo) > lastnum)
            //                    {
            //                        lastnum = Convert.ToInt32(ressext.UsrInvoicenoooo);

            //                    }

            //                }
            //            }


            //        }


            //    }

            //    //final = ini + 1;
            //    cache.SetValue<ARRegisterExt.usrInvoicenoooo>(rows, Convert.ToString(Convert.ToInt32(lastnum) + 1));
            //    lastnum = 0;
            //    //PXCache cache1 = Base.Caches[typeof(SOShipment)];
            //    //rowsExt.Usrshipmentnoo = Convert.ToString(Convert.ToInt32(lastnum) + 1);
            //    //cache1.Update(rows);


            //  }




            foreach (ARInvoice rowss in Base.Document.Cache.Cached)
            {
                var rowssExt = PXCache<ARRegister>.GetExtension<ARRegisterExt>(rowss);
                if (rowssExt.UsrInvoiceType == null && rowss.Status == "B")
                {
                    throw new PXException("Invoice Type Cannot Be null");

                }
                else
                {
                    baseMethod();

                }
            }
        }


    #endregion
  }
  
}