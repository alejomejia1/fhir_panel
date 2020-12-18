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

        public CardHolder(IDataConduITMgr dataConduITMgr)
        {
            _dataConduITMgr = dataConduITMgr;
        }

        public async Task<object> AddCardHolder(AddCardHolder_DTO newCardHolder)
        {
            ManagementScope cardHolderScope = _dataConduITMgr.GetManagementScope();

            ManagementClass cardHolderClass = new ManagementClass(cardHolderScope, new ManagementPath("Lnl_Cardholder"), null);

            ManagementObject newCardHolderInstance = cardHolderClass.CreateInstance();

            newCardHolderInstance["FIRSTNAME"] = newCardHolder.firstName;
            newCardHolderInstance["LASTNAME"] = newCardHolder.lastName;
            newCardHolderInstance["CITY"] = newCardHolder.city;

            PutOptions options = new PutOptions();
            options.Type = PutType.CreateOnly;

            newCardHolderInstance.Put(options); //commit the new instance.
            //blnRta = true;
            return true;

        }

        public async Task<object> GetCardHolder(string idLenel)
        {
            ManagementScope cardHolderScope = _dataConduITMgr.GetManagementScope();
            ObjectQuery cardHolderSearcher = new ObjectQuery("SELECT * FROM Lnl_CardHolder WHERE ID = " + idLenel);
            ManagementObjectSearcher getCardHolder = new ManagementObjectSearcher(cardHolderScope, cardHolderSearcher);

            try
            {
                ObtenerEmpleado_DTO empleado = new ObtenerEmpleado_DTO();

                foreach (ManagementObject queryObj in getCardHolder.Get())
                {
                    empleado.apellidos = queryObj["LASTNAME"].ToString();
                    empleado.nombres = queryObj["FIRSTNAME"].ToString();
                    empleado.ssno = queryObj["SSNO"].ToString();
                    try { empleado.status = queryObj["STATUS"].ToString(); } catch { empleado.status = null; }
                    try { empleado.empresa = queryObj["DIVISION"].ToString(); } catch { empleado.empresa = null; }
                    try { empleado.ciudad = queryObj["CITY"].ToString(); } catch { empleado.ciudad = null; }
                    try { empleado.email = queryObj["EMAIL"].ToString(); } catch { empleado.email = null; }
                }
                return empleado;
            }
            catch (Exception ex) { return ex.Message; }

            
        }

        public async Task<object> GetVisitor(string idLenel)
        {
            ManagementScope VisitorScope = _dataConduITMgr.GetManagementScope();
            ObjectQuery cardHolderSearcher = new ObjectQuery("SELECT * FROM Lnl_Visitor WHERE ID = " + idLenel);
            ManagementObjectSearcher getCardHolder = new ManagementObjectSearcher(VisitorScope, cardHolderSearcher);
            
            return getCardHolder.Get();
        }
    }
}
