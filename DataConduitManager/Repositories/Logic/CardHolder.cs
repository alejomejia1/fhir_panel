using System;
using System.Management;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataConduitManager.Repositories.DTO;
using DataConduitManager.Repositories.Interfaces;

namespace DataConduitManager.Repositories.Logic
{
    public class CardHolder : ICardHolder
    {
        private readonly IDataConduITMgr _dataConduITMgr;

        #region CONSTRUCTOR
        public CardHolder(IDataConduITMgr dataConduITMgr)
        {
            _dataConduITMgr = dataConduITMgr;
        }
        #endregion

        #region METODOS CARDHOLDER
        public async Task<ManagementObjectSearcher> GetCardHolder(string idLenel, string path, string user, string password)
        {
            ManagementScope cardHolderScope = _dataConduITMgr.GetManagementScope(path,user,password);
            ObjectQuery cardHolderSearcher = new ObjectQuery(@"SELECT * FROM Lnl_CardHolder WHERE ID = '" + idLenel + "'");
            ManagementObjectSearcher getCardHolder = new ManagementObjectSearcher(cardHolderScope, cardHolderSearcher);

            try { return getCardHolder; }
            catch (Exception ex) { throw new Exception("error: " + ex. Message + " " + ex.StackTrace + " " + ex.InnerException); }

        }

        public async Task<object> AddCardHolder(AddCardHolder_DTO newCardHolder, string path, string user, string password)
        {
            ManagementScope cardHolderScope = _dataConduITMgr.GetManagementScope(path,user,password);

            ManagementClass cardHolderClass = new ManagementClass(cardHolderScope, new ManagementPath("Lnl_Cardholder"), null);

            ManagementObject newCardHolderInstance = cardHolderClass.CreateInstance();

            newCardHolderInstance["FIRSTNAME"] = newCardHolder.firstName;
            newCardHolderInstance["LASTNAME"] = newCardHolder.lastName;
            newCardHolderInstance["CITY"] = newCardHolder.city;

            PutOptions options = new PutOptions();
            options.Type = PutType.CreateOnly;

            //SE REALIZA COMMIT DE LA INSTANCIA
            newCardHolderInstance.Put(options); 

            return true;

        }

        public async Task<bool> UpdateCardHolder(UpdateCardHolder_DTO cardHolder, string idLenel, string path, string user, string password)
        {
            try
            {
                ManagementScope cardHolderScope = _dataConduITMgr.GetManagementScope(path, user, password);
                ObjectQuery cardHolderSearcher = new ObjectQuery("SELECT * FROM Lnl_CardHolder WHERE ID = " + idLenel);
                ManagementObjectSearcher getCardHolder = new ManagementObjectSearcher(cardHolderScope, cardHolderSearcher);

                //redefine properties value  
                foreach (ManagementObject queryObj in getCardHolder.Get())
                {
                    queryObj["LASTNAME"] = cardHolder.apellidos;
                    queryObj["FIRSTNAME"] = cardHolder.nombres;
                    queryObj["SSNO"] = cardHolder.ssno;
                    queryObj["STATE"] = cardHolder.status;
                    queryObj["OPHONE"] = cardHolder.documento;
                    queryObj["DIVISION"] = cardHolder.empresa;
                    queryObj["CITY"] = cardHolder.ciudad;
                    queryObj["EMAIL"] = cardHolder.email;

                    PutOptions options = new PutOptions();
                    options.Type = PutType.UpdateOnly;
                    queryObj.Put(options);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region METODOS VISITOR
        public async Task<object> GetVisitor(string idLenel, string path, string user, string password)
        {
            ManagementScope VisitorScope = _dataConduITMgr.GetManagementScope(path, user, password);
            ObjectQuery  visitorSearcher = new ObjectQuery("SELECT * FROM Lnl_Visitor WHERE ID = " + idLenel);
            ManagementObjectSearcher getVisitor = new ManagementObjectSearcher(VisitorScope, visitorSearcher);

            try { return getVisitor.Get(); }
            catch(Exception ex) { throw new Exception(ex.Message); }
            
        }
        #endregion
    }
}
