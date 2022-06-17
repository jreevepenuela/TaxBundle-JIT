using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PX.Common;
using PX.Data;
using PX.Objects.GL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.CR;
using PX.Objects.TX;
using PX.Objects.IN;
using PX.Objects.EP;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.SO;
using PX.TM;
using SOOrder = PX.Objects.SO.SOOrder;
using SOLine = PX.Objects.SO.SOLine;
using PX.CS.Contracts.Interfaces;
using PX.Data.DependencyInjection;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.PM;
using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Objects.AP.MigrationMode;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects;
using PX.Objects.PO;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class POOrderEntry_Extension : PXGraphExtension<POOrderEntry>
  {
    #region Event Handlers

    public PXAction<PX.Objects.PO.POOrder> PrintContract;
  
    [PXButton(CommitChanges = true)]
    [PXUIField(DisplayName = "Print Contract", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights. Select)]
    [PXLookupButton]
    protected void printContract()
    {
  
      POOrder doc = Base.Document.Current;
     if (doc.OrderNbr != null)
     {
       Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["OrderNbr"] = doc.OrderNbr;
            parameters["OrderType"] = doc.OrderType; 
       
     
       throw new PXReportRequiredException(parameters, "PO999999", null);
     }    
      
      
      
    }

      
      


    #endregion
  }
}