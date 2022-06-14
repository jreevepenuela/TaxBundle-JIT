using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudianGlobal
{
    [PXCacheName("Withholding Tax")]
    public class withholdingtax : IBqlTable
    {
        #region Idnbr
        [PXDBInt(IsKey = true)]
        [PXUIField(DisplayName = "Idnbr")]
        public virtual int? Idnbr { get; set; }
        public abstract class idnbr : PX.Data.BQL.BqlInt.Field<idnbr> { }
        #endregion

        #region Atc
        [PXDBString(50, InputMask = "")]
        [PXUIField(DisplayName = "Atc")]
        public virtual string Atc { get; set; }
        public abstract class atc : PX.Data.BQL.BqlString.Field<atc> { }
        #endregion

        #region Type
        [PXDBString(50, InputMask = "")]
        [PXUIField(DisplayName = "Type")]
        public virtual string Type { get; set; }
        public abstract class type : PX.Data.BQL.BqlString.Field<type> { }
        #endregion

        #region Description
        [PXDBString(InputMask = "")]
        [PXUIField(DisplayName = "Description")]
        public virtual string Description { get; set; }
        public abstract class description : PX.Data.BQL.BqlString.Field<description> { }
        #endregion

        #region TaxRate
        [PXDBString(50, InputMask = "")]
        [PXUIField(DisplayName = "Tax Rate")]
        public virtual string TaxRate { get; set; }
        public abstract class taxRate : PX.Data.BQL.BqlString.Field<taxRate> { }
        #endregion

        #region Bir_form
        // Acuminator disable once PX1026 UnderscoresInDacDeclaration [Justification]
        [PXDBString(50, InputMask = "")]
        [PXUIField(DisplayName = "Bir_form")]
        public virtual string Bir_form { get; set; }
        // Acuminator disable once PX1026 UnderscoresInDacDeclaration [Justification]
        public abstract class bir_form : PX.Data.BQL.BqlString.Field<bir_form> { }
        #endregion
    }
}
