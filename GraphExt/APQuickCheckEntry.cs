using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PX.Data;
using PX.Data.DependencyInjection;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Extensions;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.CM.Extensions;
using PX.Objects.CA;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.Objects.TX;
using PX.Objects.IN;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.Objects.GL.Reclassification.UI;
using PX.Objects.PO;
using APQuickCheck = PX.Objects.AP.Standalone.APQuickCheck;
using AP1099Hist = PX.Objects.AP.Overrides.APDocumentRelease.AP1099Hist;
using AP1099Yr = PX.Objects.AP.Overrides.APDocumentRelease.AP1099Yr;
using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Objects.Extensions.MultiCurrency.AP;
using PX.Objects;
using PX.Objects.AP;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class APQuickCheckEntry_Extension : PXGraphExtension<APQuickCheckEntry>
    {
        #region Event Handlers
        protected void _(Events.FieldUpdated<APTran, APTran.inventoryID> e)
        {
      
            var row = e.Row;
            if (row != null)
            {
                InventoryItem item = PXSelectorAttribute.Select<APTran.inventoryID>(e.Cache, row) as InventoryItem;
                InventoryItemExt inventoryItemExt = item.GetExtension<InventoryItemExt>();
                APTranExt aPTranExt = row.GetExtension<APTranExt>();
                aPTranExt.UsrWholdingatc = inventoryItemExt.UsrWHoldingtax;
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

        protected void _(Events.FieldUpdated<APTran, APTranExt.usrWholdingtaxrate> e)
        {
            APTran aPTran = e.Row;
            if (aPTran != null)
            {
                APTranExt aPTranExt = aPTran.GetExtension<APTranExt>();
                if (aPTranExt != null)
                {
                    var curyCuryLineAmount = aPTran.CuryLineAmt;
                    var conCuryLineAmount = Convert.ToDecimal(curyCuryLineAmount);
                    var taxRate = aPTranExt.UsrWholdingtaxrate;
                    var conTaxRate = Convert.ToDecimal(taxRate);
                    var totalAmount = conCuryLineAmount * conTaxRate;
                    var roundOffTotal = Math.Round(totalAmount, 2, MidpointRounding.AwayFromZero);
                    var stringAmount = Convert.ToString(roundOffTotal);
                    e.Cache.SetValue<APTranExt.usrWholdingtaxAmount>(aPTran, stringAmount);
                }
            }
        }

        protected void _(Events.FieldUpdated<APTran, APTran.curyLineAmt> e)
        {
            APTran aPTran = e.Row;
            if (aPTran != null)
            {
                APTranExt aPTranExt = aPTran.GetExtension<APTranExt>();
                if (aPTranExt != null)
                {
                    var curyCuryLineAmount = aPTran.CuryLineAmt;
                    var conCuryLineAmount = Convert.ToDecimal(curyCuryLineAmount);
                    var taxRate = aPTranExt.UsrWholdingtaxrate;
                    var conTaxRate = Convert.ToDecimal(taxRate);
                    var totalAmount = conCuryLineAmount * conTaxRate;
                    var roundOffTotal = Math.Round(totalAmount, 2, MidpointRounding.AwayFromZero);
                    var stringAmount = Convert.ToString(roundOffTotal);
                    e.Cache.SetValue<APTranExt.usrWholdingtaxAmount>(aPTran, stringAmount);
                }
            }
        }
        #endregion
    }
}