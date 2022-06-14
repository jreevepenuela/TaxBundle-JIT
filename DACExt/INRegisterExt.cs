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
    public sealed class INRegisterExt : PXCacheExtension<PX.Objects.IN.INRegister>
    {
        #region UsrWB
        [PXDBString]
        [PXUIField(DisplayName = "Withdrawn By")]
        [PXSelector(typeof(Search<EPEmployee.acctCD>))]

        public string UsrWB { get; set; }
        public abstract class usrWB : PX.Data.BQL.BqlString.Field<usrWB> { }
        #endregion

        #region UsrRB
        [PXDBString]
        [PXUIField(DisplayName = "Released By")]
        [PXSelector(typeof(Search<EPEmployee.acctCD>))]

        public  string UsrRB { get; set; }
        public abstract class usrRB : PX.Data.BQL.BqlString.Field<usrRB> { }
        #endregion

        #region UsrRD
        [PXDBDate]
        [PXUIField(DisplayName = "Released Date")]

        public  DateTime? UsrRD { get; set; }
        public abstract class usrRD : PX.Data.BQL.BqlDateTime.Field<usrRD> { }
        #endregion
    }

}
