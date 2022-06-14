using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudianGlobal
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class BAccountExt : PXCacheExtension<PX.Objects.CR.BAccount>
    {
        #region UsrFirstName
        [PXDBString(50)]
        [PXUIField(DisplayName = "FirstName")]

        public  string UsrFirstName { get; set; }
        public abstract class usrFirstName : PX.Data.BQL.BqlString.Field<usrFirstName> { }
        #endregion

        #region UsrMiddleName
        [PXDBString(50)]
        [PXUIField(DisplayName = "MiddleName")]

        public string UsrMiddleName { get; set; }
        public abstract class usrMiddleName : PX.Data.BQL.BqlString.Field<usrMiddleName> { }
        #endregion

        #region UsrLastName
        [PXDBString(50)]
        [PXUIField(DisplayName = "LastName")]

        public string UsrLastName { get; set; }
        public abstract class usrLastName : PX.Data.BQL.BqlString.Field<usrLastName> { }
        #endregion
    }
}
