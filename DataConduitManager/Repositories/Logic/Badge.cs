using System;
using System.Management;
using System.Threading.Tasks;
using DataConduitManager.Repositories.Interfaces;
using DataConduitManager.Repositories.DTO;

namespace DataConduitManager.Repositories.Logic
{
    public class Badge : IBadge
    {
        private readonly IDataConduITMgr _dataConduITMgr;

        #region CONSTRUCTOR
        public Badge(IDataConduITMgr dataConduITMgr)
        {
            _dataConduITMgr = dataConduITMgr;
        }
        #endregion

        #region METODOS
        public async Task<object> AddBadge(AddBadge_DTO newBadge, string path, string user, string pass)
        {
            try
            {
                ManagementScope badgeScope = _dataConduITMgr.GetManagementScope(path, user, pass);

                ManagementClass badgeClass = new ManagementClass(badgeScope, new ManagementPath("Lnl_Badge"), null);

                ManagementObject newBadgeInstance = badgeClass.CreateInstance();

                newBadgeInstance["ID"] = newBadge.id;
                newBadgeInstance["PERSONID"] = newBadge.personId;
                newBadgeInstance["STATUS"] = newBadge.status; //Active 
                newBadgeInstance["TYPE"] = newBadge.type; // Employee
                newBadgeInstance["DEST_EXEMPT"] = newBadge.dest_exemp; // Sometimes teh value is required

                ManagementBaseObject inParams = badgeClass.GetMethodParameters("AddBadge");
                inParams.Properties["BadgeIn"].Value = newBadgeInstance;

                //Execute the method
                ManagementBaseObject outParamObject = badgeClass.InvokeMethod("AddBadge", inParams, null);

                if (outParamObject != null)
                {
                    //Display results
                    object outObj = outParamObject["BadgeOut"];
                    ManagementBaseObject addedBadge = (ManagementBaseObject)outObj;
                    return addedBadge["BADGEKEY"];
                }
                else
                {
                    throw new Exception("No se pudo crear el nuevo Badge");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ManagementObjectSearcher> GetBadge(string personId, string path, string user, string pass)
        {
            try
            {
                ManagementScope badgeScope = _dataConduITMgr.GetManagementScope(path, user, pass);
                ObjectQuery badgeSearcher = new ObjectQuery(@"SELECT * FROM Lnl_Badge WHERE PERSONID = '" + personId + "'");
                ManagementObjectSearcher getBadge = new ManagementObjectSearcher(badgeScope, badgeSearcher);

                try { return getBadge; }
                catch (Exception ex) { throw new Exception("error: " + ex.Message + " " + ex.StackTrace + " " + ex.InnerException); }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ManagementObjectSearcher> GetPersonBadge(string badgeId, string path, string user, string pass)
        {
            try
            {
                ManagementScope badgeScope = _dataConduITMgr.GetManagementScope(path, user, pass);
                ObjectQuery badgeSearcher = new ObjectQuery(@"SELECT * FROM Lnl_Badge WHERE ID = '" + badgeId + "'");
                ManagementObjectSearcher getBadge = new ManagementObjectSearcher(badgeScope, badgeSearcher);

                try { return getBadge; }
                catch (Exception ex) { throw new Exception("error: " + ex.Message + " " + ex.StackTrace + " " + ex.InnerException); }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}
