using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class InventoryItemExt : PXCacheExtension<PX.Objects.IN.InventoryItem>
    {
        #region UsrWHoldingtax
        [PXDBString(10)]
        [PXUIField(DisplayName = "Withholding Tax Code")]
        [PXSelector(
        typeof(Search<withholdingtax.atc>),
            typeof(withholdingtax.description))]
        public string UsrWHoldingtax { get; set; }
        public abstract class usrWHoldingtax : PX.Data.BQL.BqlString.Field<usrWHoldingtax> { }
        #endregion
    }
}
