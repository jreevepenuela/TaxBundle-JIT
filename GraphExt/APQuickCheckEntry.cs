using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PX.Data;
using PX.Data.DependencyInjection;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Extensions;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.CM;
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
using APQuickCheck = PX.Objects.AP.Standalone.APQuickCheck;
using AP1099Hist = PX.Objects.AP.Overrides.APDocumentRelease.AP1099Hist;
using AP1099Yr = PX.Objects.AP.Overrides.APDocumentRelease.AP1099Yr;
using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Objects;
using PX.Objects.AP;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class APQuickCheckEntry_Extension : PXGraphExtension<APQuickCheckEntry>
    {
        #region Event Handlers
        public delegate IEnumerable ReleaseDelegate(PXAdapter adapter);
        [PXOverride]
        public IEnumerable Release(PXAdapter adapter, ReleaseDelegate baseMethod)
        { 
            foreach (APTran atc in Base.Transactions.Cache.Cached)
            {
                var rowExt = PXCache<APTran>.GetExtension<APTranExt>(atc);
                if (rowExt.UsrWholdingtaxrate == "N/A" || rowExt.UsrWholdingatc == null)
                {
                    PXCache xcache = Base.Transactions.Cache;
                    xcache.SetValue<APTranExt.usrWholdingatc>(atc, "N/A");
                    xcache.SetValue<APTranExt.usrWholdingtaxrate>(atc, "0.00");
                    xcache.SetValue<APTranExt.usrWholdingtaxAmount>(atc, "0.00");
                    xcache.SetValue<APTranExt.usrWtaxDesc>(atc, "N/A");
                } 

            }  
            return baseMethod(adapter);
        }

        public delegate void PersistDelegate();
        [PXOverride]
        public void Persist(PersistDelegate baseMethod)
        {
   
            foreach (APTran atc in Base.Transactions.Cache.Cached)
                {
                    var rowExt = PXCache<APTran>.GetExtension<APTranExt>(atc);
                    if (rowExt.UsrWholdingtaxrate == "N/A" || rowExt.UsrWholdingatc == null)
                    {
                        PXCache xcache = Base.Transactions.Cache;
                        xcache.SetValue<APTranExt.usrWholdingatc>(atc, "N/A");
                        xcache.SetValue<APTranExt.usrWholdingtaxrate>(atc, "0.00");
                        xcache.SetValue<APTranExt.usrWholdingtaxAmount>(atc, "0.00");
                        xcache.SetValue<APTranExt.usrWtaxDesc>(atc, "N/A");
                    }
            }   
              baseMethod();
        }
        #endregion
    }
    
}