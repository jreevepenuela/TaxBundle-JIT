using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.TX;
using POLine = PX.Objects.PO.POLine;
using POOrder = PX.Objects.PO.POOrder;
using System.Threading.Tasks;
using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using ARRegisterAlias = PX.Objects.AR.Standalone.ARRegisterAlias;
using PX.Objects.AR.MigrationMode;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Extensions;
using PX.Objects.IN.Overrides.INDocumentRelease;
using PX.CS.Contracts.Interfaces;
using PX.Data.DependencyInjection;
using PX.Objects.Extensions.PaymentTransaction;
using PX.Objects.SO.GraphExtensions.CarrierRates;
using PX.Objects;
using PX.Objects.SO;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class SOOrderEntry_Extension : PXGraphExtension<SOOrderEntry>
  {
    #region Event Handlers
    public delegate void PersistDelegate();
    [PXOverride]
    public void Persist(PersistDelegate baseMethod)
    {
      baseMethod();
    
   PXCache cache = Base.Document.Cache;
// var row = (SOOrder)e.Row;  

    // SOOrderExt rowExt = row.GetExtension<SOOrderExt>();
     foreach (SOOrder row in Base.Document.Cache.Cached){
        var old = row.OrderQty;
cache.SetValueExt<SOOrderExt.usrOldQty>(cache, old);
       }
                    
              
              
              foreach (SOOrder rows in Base.Document.Cache.Cached)
            {
                var rowsExt = PXCache<SOOrder>.GetExtension<SOOrderExt>(rows);

                if(rowsExt.Usrapproverr != true  && rows.Status == "N" && rows.OrderType == "RR")
                {
                    // Acuminator disable once PX1050 HardcodedStringInLocalizationMethod [Justification]
                    throw new PXException("SO Order is not approved");
                }
                else
                {
                    baseMethod();
                }
            }
   
    
      
    }

   


    protected void SOOrder_Approved_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
    {
      
      var row = (SOOrder)e.Row;
      
        if (row.Status != "H") {
      cache.SetValueExt<SOOrderExt.usrPOHold>(row, false);
          }
      
    }

    

   /* protected void SOOrder_Status_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e, PXFieldUpdated InvokeBaseHandler)
    {
      if(InvokeBaseHandler != null)
        InvokeBaseHandler(cache, e);
      var row = (SOOrder)e.Row;
       cache.SetValueExt<SOOrderExt.usrPOHold>(row, false);
      
    }
  */
      

    

    protected void SOOrder_CustomerID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
    {
      
      var row = (SOOrder)e.Row;
       cache.SetValueExt<SOOrderExt.usrPOHold>(row, true);
        
    }

    

    protected void SOOrder_OrderNbr_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
    {
      
      var row = (SOOrder)e.Row;
        cache.SetValueExt<SOOrderExt.usrPOHold>(row, true);
    }

    

    protected void SOOrder_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
    {
      
      var row = (SOOrder)e.Row;
      
    }

    

    protected void SOOrder_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
    {
      
      var row = (SOOrder)e.Row;
      PXUIFieldAttribute.SetEnabled<SOOrderExt.usrPOHold>(cache, e.Row, true);
       PXUIFieldAttribute.SetEnabled<SOOrder.customerOrderNbr>(cache, e.Row, true);
    }

    

    protected void SOLine_UsrWarrantyperiod_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
    {
      
      //var row = (SOLine)e.Row;
      
         foreach (SOLine row in Base.Transactions.Cache.Cached)
            {
             
              SOLineExt rowExt = row.GetExtension<SOLineExt>();
      
              if (rowExt.UsrWarrantyperiod != null)
              
                {
                    var pau = Convert.ToInt32(rowExt.UsrWarrantyperiod);
                    var date = Convert.ToDateTime(row.ShipDate).AddMonths(pau);
            
                    cache.SetValueExt<SOLineExt.usrWarrantydate>(row, date);
        
                }

              else
                
                {
                
                
                cache.SetValueExt<SOLineExt.usrWarrantydate>(row, row.ShipDate);
                
                }

            }
    }

    

    #endregion
  }
}