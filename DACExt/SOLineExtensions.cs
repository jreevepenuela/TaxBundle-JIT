using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Discount.Attributes;
using PX.Objects.Common.Discount;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.Objects;
using System.Collections.Generic;
using System.Collections;
using System;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class SOLineExt : PXCacheExtension<PX.Objects.SO.SOLine>
  {
    #region UsrWarrantydate
    [PXDBDate]
    [PXUIField(DisplayName="Warrantydate")]

    public DateTime? UsrWarrantydate { get; set; }
    public abstract class usrWarrantydate : PX.Data.BQL.BqlDateTime.Field<usrWarrantydate> { }
    #endregion

    #region UsrCustomerLocation
    [PXDBString]
[PXUIField(DisplayName="CustomerLocation")]
[PXSelector(typeof(Search<Location.locationCD, Where<Location.bAccountID, Equal<Current<SOOrder.customerID>>>>))]
    public string UsrCustomerLocation { get; set; }
    public abstract class usrCustomerLocation : PX.Data.BQL.BqlString.Field<usrCustomerLocation> { }
    #endregion

    #region UsrWarrantyperiod
    [PXDBInt]
    [PXUIField(DisplayName="Warrantyperiod")]

    public int? UsrWarrantyperiod { get; set; }
    public abstract class usrWarrantyperiod : PX.Data.BQL.BqlInt.Field<usrWarrantyperiod> { }
    #endregion
  }
}