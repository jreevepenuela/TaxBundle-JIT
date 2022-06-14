using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudianGlobal
{
    // Acuminator disable once PX1094 NoPXHiddenOrPXCacheNameOnDac [Justification]
    public class Margincodex : IBqlTable
    {
        #region Idnbr
        [PXDBIdentity()]
        [PXUIField(DisplayName = "Idnbr")]
        public virtual int? Idnbr { get; set; }
        public abstract class idnbr : PX.Data.BQL.BqlInt.Field<idnbr> { }
        #endregion

        #region Code
        [PXDBString(250, InputMask = "", IsKey = true)]
        [PXUIField(DisplayName = "Code")]
        public virtual string Code { get; set; }
        public abstract class code : PX.Data.BQL.BqlString.Field<code> { }
        #endregion

        #region Mark_up
        // Acuminator disable once PX1026 UnderscoresInDacDeclaration [Justification]
        [PXDBString(250, InputMask = "")]
        [PXUIField(DisplayName = "Mark_up")]
        public virtual string Mark_up { get; set; }
        // Acuminator disable once PX1026 UnderscoresInDacDeclaration [Justification]
        public abstract class mark_up : PX.Data.BQL.BqlString.Field<mark_up> { }
        #endregion

        #region Commission
        [PXDBString(250, InputMask = "")]
        [PXUIField(DisplayName = "Commission")]
        public virtual string Commission { get; set; }
        public abstract class commission : PX.Data.BQL.BqlString.Field<commission> { }
        #endregion

        #region Type
        [PXDBString(250, InputMask = "")]
        [PXUIField(DisplayName = "Type")]
        public virtual string Type { get; set; }
        public abstract class type : PX.Data.BQL.BqlString.Field<type> { }
        #endregion

        #region Remarks_from_FTC
        // Acuminator disable once PX1026 UnderscoresInDacDeclaration [Justification]
        [PXDBString(250, InputMask = "")]
        [PXUIField(DisplayName = "Remarks_from_ FTC")]
        public virtual string Remarks_from_FTC { get; set; }
        // Acuminator disable once PX1026 UnderscoresInDacDeclaration [Justification]
        public abstract class remarks_from_FTC : PX.Data.BQL.BqlString.Field<remarks_from_FTC> { }
        #endregion

        #region Checkbox
        [PXDBBool()]
        [PXUIField(DisplayName = "Checkbox")]
        public virtual bool? Checkbox { get; set; }
        public abstract class checkbox : PX.Data.BQL.BqlBool.Field<checkbox> { }
        #endregion

        #region FabType
        [PXDBString(250, InputMask = "")]
        [PXUIField(DisplayName = "Fab Type")]
        public virtual string FabType { get; set; }
        public abstract class fabType : PX.Data.BQL.BqlString.Field<fabType> { }
        #endregion

        #region CreditTerms
        [PXDBString(250, InputMask = "")]
        [PXUIField(DisplayName = "Credit Terms")]
        public virtual string CreditTerms { get; set; }
        public abstract class creditTerms : PX.Data.BQL.BqlString.Field<creditTerms> { }
        #endregion

        #region Show
        [PXDBBool()]
        [PXUIField(DisplayName = "Show")]
        public virtual bool? Show { get; set; }
        public abstract class show : PX.Data.BQL.BqlBool.Field<show> { }
        #endregion
    }
}
