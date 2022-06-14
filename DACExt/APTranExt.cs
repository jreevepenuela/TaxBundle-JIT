using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Objects.IN;
using PX.Objects.AP;
using PX.Objects.FA;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class APTranExt : PXCacheExtension<PX.Objects.AP.APTran>
    {
        #region UsrWholdingatc
        [PXDBString(10)]
        [PXUIField(DisplayName = "Withholding Tax Codes")]
        [PXDefault(TypeCode.String, "N/A", typeof(Search<InventoryItemExt.usrWHoldingtax, Where<InventoryItem.inventoryID, Equal<Current<APTran.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXSelector(
        typeof(Search<withholdingtax.atc>),
            typeof(withholdingtax.description),
            typeof(withholdingtax.taxRate))]
        public string UsrWholdingatc { get; set; }
        public abstract class usrWholdingatc : PX.Data.BQL.BqlString.Field<usrWholdingatc> { }
        #endregion

        #region UsrWtaxDesc
        [PXDBString(3000)]
        [PXUIField(DisplayName = "Withholding Tax Description")]
        [PXDefault(TypeCode.String, "N/A", typeof(Search<withholdingtax.description, Where<withholdingtax.atc, Equal<Current<APTranExt.usrWholdingatc>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        public string UsrWtaxDesc { get; set; }
        public abstract class usrWtaxDesc : PX.Data.BQL.BqlString.Field<usrWtaxDesc> { }
        #endregion

        #region UsrWholdingtaxrate
        [PXDBString(5)]
        [PXUIField(DisplayName = "Withholding Tax Rate")]
        [PXDefault(TypeCode.String, "0.00", typeof(Search<withholdingtax.taxRate, Where<withholdingtax.atc, Equal<Current<APTranExt.usrWholdingatc>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        public string UsrWholdingtaxrate { get; set; }
        public abstract class usrWholdingtaxrate : PX.Data.BQL.BqlString.Field<usrWholdingtaxrate> { }
        #endregion

        #region UsrWholdingtaxAmount
        [PXDBString(10)]
        [PXDefault(TypeCode.String, "0.00", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Withholding Tax Amount")]
        public string UsrWholdingtaxAmount { get; set; }
        public abstract class usrWholdingtaxAmount : PX.Data.BQL.BqlString.Field<usrWholdingtaxAmount> { }
        #endregion

        #region UsrFixedAsset
        [PXDBString]
        [PXUIField(DisplayName = "FixedAsset")]
        [PXSelector(typeof(Search<FixedAsset.assetCD>))]
        public string UsrFixedAsset { get; set; }
        public abstract class usrFixedAsset : PX.Data.BQL.BqlString.Field<usrFixedAsset> { }
        #endregion
    }
}
