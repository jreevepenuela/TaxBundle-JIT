using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PX.Common;
using PX.Data;
using PX.Data.DependencyInjection;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Discount;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.CR;
using PX.Objects.CT;
using PX.Objects.PM;
using PX.Objects.TX;
using PX.Objects.IN;
using PX.Objects.CA;
using PX.Objects.BQLConstants;
using PX.Objects.EP;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.Objects.DR;
using PX.Objects.AR;
using PX.TM;
using AP1099Hist = PX.Objects.AP.Overrides.APDocumentRelease.AP1099Hist;
using AP1099Yr = PX.Objects.AP.Overrides.APDocumentRelease.AP1099Yr;
using PX.Objects.GL.Reclassification.UI;
using Branch = PX.Objects.GL.Branch;
using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Objects.AP.BQL;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects;
using PX.Objects.AP;
using PX.Data.BQL.Fluent;
using PX.Data.BQL;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class APInvoiceEntry_Extension : PXGraphExtension<APInvoiceEntry>
    {
        #region Event Handlers

        public PXAction<APInvoice> WithholdingTax;
        [PXButton]
        [PXUIField(DisplayName = "Withholding Tax")]
        protected void withholdingTax()
        {
            APTran aPTran = Base.Transactions.Current;
            if (aPTran != null) 
                Wtax();
        }

        public PXAction<APInvoice> Print2307;
        [PXUIField(DisplayName = "Print 2307", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXLookupButton(Category = "Reports")]
        protected void print2307()
        {
            APInvoice doc = Base.Document.Current;
            if (doc.RefNbr != null)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters["RefNbr"] = doc.RefNbr;
                throw new PXReportRequiredException(parameters, "TX641000", null);
            }
        }


        public delegate IEnumerable ReleaseDelegate(PXAdapter adapter);
        [PXOverride]
        public IEnumerable Release(PXAdapter adapter, ReleaseDelegate baseMethod)
        {
            CheckWTax();
            return baseMethod(adapter);
        }

        public void Wtax()
        {
            int? wTaxID = 0;
            decimal? wTaxAmount = 0M;
            decimal? curyTranAmount = 0M;
            decimal? prePayment = 0M;
            bool? checkWtaxExist = false;

            InventoryItem wTax = SelectFrom<InventoryItem>.
                     Where<InventoryItem.inventoryCD.IsEqual<@P.AsString>>.View.
                     Select(Base, "WTAX");

            if (wTax != null)
                wTaxID = wTax.InventoryID;

            foreach (APTran aptran in Base.Transactions.Select())
            {
                APTranExt aPTranExt = aptran.GetExtension<APTranExt>();

                if (aPTranExt.UsrWholdingatc != "N/A")
                    checkWtaxExist = true;

                if (aptran.InventoryID == wTaxID)
                    Base.Transactions.Delete(aptran);
                else
                {
                    wTaxAmount = Convert.ToDecimal(aPTranExt.UsrWholdingtaxAmount);
                    curyTranAmount += wTaxAmount;
                    prePayment = aptran.PrepaymentPct;
                }
            }

            if (checkWtaxExist != false)
            {
                APTran aPTran = Base.Transactions.Insert();
                aPTran.InventoryID = wTaxID;
                aPTran.CuryTranAmt = curyTranAmount * -1;
                aPTran.PrepaymentPct = prePayment;
                Base.Transactions.Update(aPTran);
            }
        }

        public void CheckWTax()
        {
            var wTaxID = 0;
            bool wTaxExist = false;
            decimal wTaxAmountTotal = 0;
            decimal wTaxAmount = 0;
            decimal wTaxAmountTotalFinal = 0;
            decimal? curyLineAmount = 0;
            decimal? curyLineAmountTotal = 0;
            string errorSaving = "Error on saving WTAX line click WTAX button";

            var row = Base.Transactions.Current;

            InventoryItem wTax = SelectFrom<InventoryItem>.Where<InventoryItem.inventoryCD.
                IsEqual<@P.AsString>>.View.Select(Base, "WTAX");

            if (wTax != null)
                wTaxID = Convert.ToInt32(wTax.InventoryID);

            foreach (APTran aPTran in Base.Transactions.Select())
            {
                APTranExt aPTranExt = aPTran.GetExtension<APTranExt>();
                wTaxAmount = Convert.ToDecimal(aPTranExt.UsrWholdingtaxAmount);

                if (aPTranExt.UsrWholdingatc != null)
                    wTaxExist = true;

                wTaxAmountTotal += wTaxAmount;
            }

            if (wTaxAmountTotal > 0)
            {
                wTaxAmountTotalFinal = wTaxAmountTotal * -1;

                foreach (APTran aPTran in Base.Transactions.Select())
                {
                    APTranExt aPTranExt = aPTran.GetExtension<APTranExt>();
                    if (aPTran.InventoryID == wTaxID)
                    {
                        curyLineAmount = aPTran.CuryTranAmt;
                        curyLineAmountTotal = Math.Round(wTaxAmountTotalFinal, 2, MidpointRounding.AwayFromZero);
                        if (curyLineAmountTotal == curyLineAmount)
                            return;
                        else if (wTaxExist == true)
                            throw new PXException(errorSaving);
                        else
                            throw new PXException(errorSaving);
                    }
                }
                if (row.InventoryID != wTaxID)
                    throw new PXException(errorSaving);
            }
        }

        protected void _(Events.FieldUpdated<APTran, APTran.inventoryID> e)
        {
            var row = e.Row;
            if (row.InventoryID != null)
            {
                InventoryItem item = PXSelectorAttribute.Select<APTran.inventoryID>(e.Cache, row) as InventoryItem;
                InventoryItemExt itemExt = item.GetExtension<InventoryItemExt>();
                APTranExt aPTranExt = row.GetExtension<APTranExt>();
                aPTranExt.UsrWholdingatc = itemExt.UsrWHoldingtax;
            }
            e.Cache.SetDefaultExt<APTranExt.usrWholdingtaxrate>(e.Row);
            
        }

        protected void _(Events.FieldUpdated<APTran, APTranExt.usrWholdingatc> e)
        {
            APTran aPTran = e.Row;
            withholdingtax withholdingTax = PXSelectorAttribute.
                Select<APTranExt.usrWholdingatc>(e.Cache, aPTran) as withholdingtax;

            APTranExt aPTranExt = aPTran.GetExtension<APTranExt>();
            if (aPTranExt.UsrWholdingatc != null)
                aPTranExt.UsrWholdingtaxrate = withholdingTax.TaxRate;

            e.Cache.SetDefaultExt<APTranExt.usrWholdingtaxrate>(e.Row);
            e.Cache.SetDefaultExt<APTranExt.usrWtaxDesc>(e.Row);
        }

        protected void _(Events.RowSelected<APInvoice> e)
        {
            var row = e.Row;
            WithholdingTax.SetEnabled(row.Status == APDocStatus.Hold);
        }

        protected void _(Events.FieldUpdated<APTran, APTranExt.usrWholdingtaxrate> e)
        {
            APTran aPTran = e.Row;
            if (aPTran != null)
            {
                APTranExt aPTranExt = aPTran.GetExtension<APTranExt>();
                if (aPTranExt != null)
                {
                    var curyTranAmount = aPTran.CuryTranAmt;
                    var conCuryTranAMount = Convert.ToDecimal(curyTranAmount);
                    var taxRate = aPTranExt.UsrWholdingtaxrate;
                    var conTaxRate = Convert.ToDecimal(taxRate);
                    var totalAmount = conCuryTranAMount * conTaxRate;
                    var roundOffTotal = Math.Round(totalAmount, 2, MidpointRounding.AwayFromZero);
                    var stringAmount = Convert.ToString(roundOffTotal);
                    e.Cache.SetValue<APTranExt.usrWholdingtaxAmount>(aPTran, stringAmount);
                }
            }
         }

    
        protected void _(Events.FieldUpdated<APTran, APTran.curyTranAmt> e)
        {
            APTran aPTran = e.Row;
            if (aPTran != null)
            {
                APTranExt aPTranExt = aPTran.GetExtension<APTranExt>();
                if (aPTranExt != null)
                {
                    var curyTranAmount = aPTran.CuryTranAmt;
                    var conCuryTranAMount = Convert.ToDecimal(curyTranAmount);
                    var taxRate = aPTranExt.UsrWholdingtaxrate;
                    var conTaxRate = Convert.ToDecimal(taxRate);
                    var totalAmount = conCuryTranAMount * conTaxRate;
                    var roundOffTotal = Math.Round(totalAmount, 2, MidpointRounding.AwayFromZero);
                    var stringAmount = Convert.ToString(roundOffTotal);
                    e.Cache.SetValue<APTranExt.usrWholdingtaxAmount>(aPTran, stringAmount);
                }
            }
        }
        #endregion
    }
}