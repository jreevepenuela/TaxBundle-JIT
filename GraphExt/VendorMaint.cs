using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Linq;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.CA;
using PX.Objects.CR;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.GL;
using PX.Objects.PO;
using PX.Objects.TX;
using PX.SM;
using Branch = PX.SM.Branch;
using PX.Objects;
using PX.Objects.AP;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class VendorMaint_Extension : PXGraphExtension<VendorMaint>
  {
        #region Event Handlers
        public string fname = "";
        public string mname = "";
        public string lname = "";
        public string stringbuild;
        protected void VendorR_UsrLastName_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
    {
            VendorR vend = e.Row as VendorR;
            if (vend != null)
            {
                BAccountExt bAccountExt = vend.GetExtension<BAccountExt>();
                if (bAccountExt != null)
                {
                    lname = bAccountExt.UsrLastName;
                }
                foreach (VendorR rowxx in Base.BAccount.Cache.Cached)
                {
                    var rowExxT = PXCache<BAccount>.GetExtension<BAccountExt>(rowxx);
                    var firstn = rowExxT.UsrFirstName;
                    var midn = rowExxT.UsrMiddleName;
                    var lasn = rowExxT.UsrLastName;
                    stringbuild = firstn + " " + midn + " " + lasn;
                }
                cache.SetValue<VendorR.acctName>(vend, stringbuild);
            }
            var row = (VendorR)e.Row;
      
    }

    

    protected void VendorR_UsrMiddleName_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
    {
            VendorR vend = e.Row as VendorR;
            if (vend != null)
            {
                BAccountExt bAccountExt = vend.GetExtension<BAccountExt>();
                if (bAccountExt != null)
                {
                    lname = bAccountExt.UsrLastName;
                }
                foreach (VendorR rowxx in Base.BAccount.Cache.Cached)
                {
                    var rowExxT = PXCache<BAccount>.GetExtension<BAccountExt>(rowxx);
                    var firstn = rowExxT.UsrFirstName;
                    var midn = rowExxT.UsrMiddleName;
                    var lasn = rowExxT.UsrLastName;
                    stringbuild = firstn + " " + midn + " " + lasn;
                }
                cache.SetValue<VendorR.acctName>(vend, stringbuild);
            }
            var row = (VendorR)e.Row;
      
    }

    

    protected void VendorR_UsrFirstName_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
    {
            VendorR vend = e.Row as VendorR;
            if (vend != null)
            {
                BAccountExt bAccountExt = vend.GetExtension<BAccountExt>();
                if (bAccountExt != null)
                {
                    lname = bAccountExt.UsrLastName;
                }
                foreach (VendorR rowxx in Base.BAccount.Cache.Cached)
                {
                    var rowExxT = PXCache<BAccount>.GetExtension<BAccountExt>(rowxx);
                    var firstn = rowExxT.UsrFirstName;
                    var midn = rowExxT.UsrMiddleName;
                    var lasn = rowExxT.UsrLastName;
                    stringbuild = firstn + " " + midn + " " + lasn;
                }
                cache.SetValue<VendorR.acctName>(vend, stringbuild);
            }
            var row = (VendorR)e.Row;
      
    }

    
  
    #endregion
  }
    
}