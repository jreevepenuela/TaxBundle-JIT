using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using PX.Common;
using PX.Data;
using PX.SM;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.Repositories;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.SO;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using CashAccountAttribute = PX.Objects.GL.CashAccountAttribute;
using PX.Objects.GL.Helpers;
using PX.Objects;
using PX.Objects.AR;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class CustomerMaint_Extension : PXGraphExtension<CustomerMaint>
  {
        public static class Globalvar
        {
            static string _CustomerBuilder;
            public static string CustomerBuilder
            {
                get
                {
                    return _CustomerBuilder;
                }
                set
                {
                    _CustomerBuilder = value;
                }
            }



            public static bool GlobalBoolean = false;
        }
        #region Event Handlers
        public string fname = "";
        public string mname = "";
        public string lname = "";
        public string stringbuild;
        protected void Customer_UsrLastName_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
    {
            Customer cust = e.Row as Customer;
            if (cust != null)
            {
                BAccountExt bAccountExt = cust.GetExtension<BAccountExt>();
                if (bAccountExt != null)
                {
                    lname = bAccountExt.UsrLastName;
                }
                foreach (Customer rowxx in Base.BAccount.Cache.Cached)
                {
                    var rowExxT = PXCache<BAccount>.GetExtension<BAccountExt>(rowxx);
                    var firstn = rowExxT.UsrFirstName;
                    var midn = rowExxT.UsrMiddleName;
                    var lasn = rowExxT.UsrLastName;
                    stringbuild = firstn + " " + midn + " " + lasn;
                }
                cache.SetValue<Customer.acctName>(cust, stringbuild);
            }
            var row = (Customer)e.Row;
      
    }

    

    protected void Customer_UsrMiddleName_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
    {
            Customer cust = e.Row as Customer;
        
            if (cust != null)
            {
                BAccountExt bAccountExt = cust.GetExtension<BAccountExt>();
                if (bAccountExt != null)
                {
                    mname = bAccountExt.UsrMiddleName;
                }

                foreach (Customer rowxx in Base.BAccount.Cache.Cached)
                {
                    var rowExxT = PXCache<BAccount>.GetExtension<BAccountExt>(rowxx);
                    var firstn = rowExxT.UsrFirstName;
                    var midn = rowExxT.UsrMiddleName;
                    var lasn = rowExxT.UsrLastName;
                    stringbuild = firstn + " " + midn + " " + lasn;
                }
                

                cache.SetValue<Customer.acctName>(cust, stringbuild);
            }
            var row = (Customer)e.Row;
      
    }

    

    protected void Customer_UsrFirstName_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
    {
            Customer cust = e.Row as Customer;
            if (cust != null)
            {
                BAccountExt bAccountExt = cust.GetExtension<BAccountExt>();
                if (bAccountExt != null)
                {
                    fname = bAccountExt.UsrFirstName;
                }
                foreach (Customer rowxx in Base.BAccount.Cache.Cached)
                {
                    var rowExxT = PXCache<BAccount>.GetExtension<BAccountExt>(rowxx);
                    var firstn = rowExxT.UsrFirstName;
                    var midn = rowExxT.UsrMiddleName;
                    var lasn = rowExxT.UsrLastName;
                    stringbuild = firstn + " " + midn + " " + lasn;
                }

                cache.SetValue<Customer.acctName>(cust, stringbuild);
            }
            var row = (Customer)e.Row;
    }
    #endregion
  }
   
}