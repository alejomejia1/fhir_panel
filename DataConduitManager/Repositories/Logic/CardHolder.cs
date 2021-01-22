﻿using System;
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
            ObjectQuery cardHolderSearcher = new ObjectQuery(@"SELECT * FROM Lnl_CardHolder WHERE OPHONE = '" + idLenel + "'");
            ManagementObjectSearcher getCardHolder = new ManagementObjectSearcher(cardHolderScope, cardHolderSearcher);

            try { return getCardHolder; }
            catch (Exception ex) { throw new Exception("error: " + ex. Message + " " + ex.StackTrace + " " + ex.InnerException); }

        }

        public async Task<ManagementObjectSearcher> GetCardHolderByID(string idPersona, string path, string user, string password)
        {
            ManagementScope cardHolderScope = _dataConduITMgr.GetManagementScope(path, user, password);
            ObjectQuery cardHolderSearcher = new ObjectQuery(@"SELECT * FROM Lnl_CardHolder WHERE ID = '" + idPersona + "'");
            ManagementObjectSearcher getCardHolder = new ManagementObjectSearcher(cardHolderScope, cardHolderSearcher);

            try { return getCardHolder; }
            catch (Exception ex) { throw new Exception("error: " + ex.Message + " " + ex.StackTrace + " " + ex.InnerException); }

        }

        public async Task<object> AddCardHolder(AddCardHolder_DTO newCardHolder, string path, string user, string password)
        {
            ManagementScope cardHolderScope = _dataConduITMgr.GetManagementScope(path,user,password);

            ManagementClass cardHolderClass = new ManagementClass(cardHolderScope, new ManagementPath("Lnl_Cardholder"), null);

            ManagementObject newCardHolderInstance = cardHolderClass.CreateInstance();

            newCardHolderInstance["FIRSTNAME"] = newCardHolder.firstName;
            newCardHolderInstance["LASTNAME"] = newCardHolder.lastName;
            newCardHolderInstance["CITY"] = newCardHolder.city;
            newCardHolderInstance["OPHONE"] = newCardHolder.idpersona;
            newCardHolderInstance["SSNO"] = newCardHolder.ssno;
            //newCardHolderInstance["LOCATION"] = newCardHolder.instalacion;
            //newCardHolderInstance["DIVISION"] = newCardHolder.empresa;
            newCardHolderInstance["EMAIL"] = newCardHolder.email;


            PutOptions options = new PutOptions();
            options.Type = PutType.CreateOnly;

            //SE REALIZA COMMIT DE LA INSTANCIA
            newCardHolderInstance.Put(options); 

            return true;

        }

        public async Task<bool> UpdateCardHolder(UpdateCardHolder_DTO cardHolder, string idPersona, string path, string user, string password)
        {
            try
            {
                ManagementScope cardHolderScope = _dataConduITMgr.GetManagementScope(path, user, password);
                ObjectQuery cardHolderSearcher = new ObjectQuery("SELECT * FROM Lnl_CardHolder WHERE OPHONE = " + idPersona);
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
        public async Task<ManagementObjectSearcher> GetVisitor(string idLenel, string path, string user, string password)
        {
            ManagementScope VisitorScope = _dataConduITMgr.GetManagementScope(path, user, password);
            ObjectQuery  visitorSearcher = new ObjectQuery("SELECT * FROM Lnl_Visitor WHERE OPHONE = " + idLenel);
            ManagementObjectSearcher getVisitor = new ManagementObjectSearcher(VisitorScope, visitorSearcher);

            try { return getVisitor; }
            catch(Exception ex) { throw new Exception(ex.Message); }
            
        }

        public async Task<object> AddVisitor(AddCardHolder_DTO newCardHolder, string path, string user, string password)
        {
            ManagementScope visitorScope = _dataConduITMgr.GetManagementScope(path, user, password);

            ManagementClass visitorClass = new ManagementClass(visitorScope, new ManagementPath("Lnl_Visitor"), null);

            ManagementObject newVisitorInstance = visitorClass.CreateInstance();

            newVisitorInstance["FIRSTNAME"] = newCardHolder.firstName;
            newVisitorInstance["LASTNAME"] = newCardHolder.lastName;
            newVisitorInstance["CITY"] = newCardHolder.city;
            newVisitorInstance["OPHONE"] = newCardHolder.idpersona;
            newVisitorInstance["SSNO"] = newCardHolder.ssno;

            PutOptions options = new PutOptions();
            options.Type = PutType.CreateOnly;

            //SE REALIZA COMMIT DE LA INSTANCIA
            newVisitorInstance.Put(options);

            return true;

        }

        public async Task<bool> UpdateVisitor(UpdateCardHolder_DTO cardHolder, string idPersona, string path, string user, string password)
        {
            try
            {
                ManagementScope visitorScope = _dataConduITMgr.GetManagementScope(path, user, password);
                ObjectQuery visitorSearcher = new ObjectQuery("SELECT * FROM Lnl_Visitor WHERE OPHONE = " + idPersona);
                ManagementObjectSearcher getVisitor = new ManagementObjectSearcher(visitorScope, visitorSearcher);

                //redefine properties value  
                foreach (ManagementObject queryObj in getVisitor.Get())
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
    }
}
