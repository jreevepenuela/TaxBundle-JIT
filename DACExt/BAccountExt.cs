using PX.Data;
using PX.Objects.EP;
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

        #region UsrCoor
        [PXDBString()]

        [PXUIField(DisplayName = "Coor")]

        [PXSelector(typeof(Search<EPEmployee.acctCD>), SubstituteKey = typeof(EPEmployee.acctName))]
        public  string UsrCoor { get; set; }
        public abstract class usrCoor : PX.Data.BQL.BqlString.Field<usrCoor> { }
        #endregion

        #region UsrCoor2
        [PXDBString]
        [PXUIField(DisplayName = "Coor 2")]

        [PXSelector(typeof(Search<EPEmployee.acctCD>), SubstituteKey = typeof(EPEmployee.acctName))]
        public  string UsrCoor2 { get; set; }
        public abstract class usrCoor2 : PX.Data.BQL.BqlString.Field<usrCoor2> { }
        #endregion

        #region UsrCoor4
        [PXDBString]
        [PXUIField(DisplayName = "Coor 4")]

        [PXSelector(typeof(Search<EPEmployee.acctCD>), SubstituteKey = typeof(EPEmployee.acctName))]
        public  string UsrCoor4 { get; set; }
        public abstract class usrCoor4 : PX.Data.BQL.BqlString.Field<usrCoor4> { }
        #endregion

        #region UsrCoor5
        [PXDBString]
        [PXUIField(DisplayName = "Coor 5")]

        [PXSelector(typeof(Search<EPEmployee.acctCD>), SubstituteKey = typeof(EPEmployee.acctName))]
        public  string UsrCoor5 { get; set; }
        public abstract class usrCoor5 : PX.Data.BQL.BqlString.Field<usrCoor5> { }
        #endregion

        #region UsrCoor6
        [PXDBString]
        [PXUIField(DisplayName = "Coor 6")]

        [PXSelector(typeof(Search<EPEmployee.acctCD>), SubstituteKey = typeof(EPEmployee.acctName))]
        public  string UsrCoor6 { get; set; }
        public abstract class usrCoor6 : PX.Data.BQL.BqlString.Field<usrCoor6> { }
        #endregion

        #region UsrCoor3
        [PXDBString]
        [PXUIField(DisplayName = "Coor 3")]

        [PXSelector(typeof(Search<EPEmployee.acctCD>), SubstituteKey = typeof(EPEmployee.acctName))]
        public  string UsrCoor3 { get; set; }
        public abstract class usrCoor3 : PX.Data.BQL.BqlString.Field<usrCoor3> { }
        #endregion

        #region UsrCustomerAlias
        [PXDBString(255)]
        [PXUIField(DisplayName = "Customer Alias")]

        public  string UsrCustomerAlias { get; set; }
        public abstract class usrCustomerAlias : PX.Data.BQL.BqlString.Field<usrCustomerAlias> { }
        #endregion
    }
}
