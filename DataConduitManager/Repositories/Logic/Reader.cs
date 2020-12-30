using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Threading.Tasks;
using DataConduitManager.Repositories.Interfaces;

namespace DataConduitManager.Repositories.Logic
{
    public class Reader:IReader
    {
        private readonly IDataConduITMgr _dataConduITMgr;
        #region Constructor
        public Reader(IDataConduITMgr dataConduITMgr)
        {
            _dataConduITMgr = dataConduITMgr;
        }
        #endregion

        #region Metodos
        public async Task<ManagementObjectSearcher> GetReaderData(string panelID, string readerID) 
        {
            ManagementScope readerScope = _dataConduITMgr.GetManagementScope();
            if (readerScope == null) { throw new Exception("No fue posible establecer conexion con OnGuard"); }

            ObjectQuery readerSearcher = new ObjectQuery("SELECT * FROM Lnl_Reader WHERE PanelID='" + panelID + "' " +
                "AND ReaderID='" + readerID + "'");
            ManagementObjectSearcher getReader = new ManagementObjectSearcher(readerScope, readerSearcher);

            try { return getReader; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<int> ReaderGetMode (string panelID, string readerID)
        {
            try
            {
                ManagementScope readerScope = _dataConduITMgr.GetManagementScope();

                ObjectQuery readerSearcher = new ObjectQuery("SELECT * FROM Lnl_Reader WHERE PanelID='" + panelID + "' " +
                    "AND ReaderID='" + readerID + "'");
                ManagementObjectSearcher getreader = new ManagementObjectSearcher(readerScope, readerSearcher);

                foreach (ManagementObject queryObj in getreader.Get())
                {
                    ManagementBaseObject outParamObject = queryObj.InvokeMethod("GetMode", null, null);

                    if (outParamObject != null)
                    {
                        object outObj = outParamObject["Mode"];
                        Int32 modeReader = (Int32)outObj;
                        return modeReader;
                    }
                    else
                    {
                        throw new Exception("No se pudo consultar");
                    }
                }

                throw new Exception("No se pudo consultar");
            }
            catch (Exception ex)
            {
                throw new Exception("No fue posible realizar el bloqueo " + ex.Message);
            }

        }
        

        public async Task<bool> OpenDoor(string panelID, string readerID) {
            try
            {
                ManagementScope readerScope = _dataConduITMgr.GetManagementScope();

                ObjectQuery readerSearcher = new ObjectQuery("SELECT * FROM Lnl_Reader WHERE PanelID='" + panelID + "' " +
                    "AND ReaderID='" + readerID +"'");
                ManagementObjectSearcher getreader = new ManagementObjectSearcher(readerScope, readerSearcher);

                foreach (ManagementObject queryObj in getreader.Get())
                {
                    //string receive = _dataConduITMgr.ReceiveEvent(readerScope);???
                    queryObj.InvokeMethod("OpenDoor", null, null);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("No fue posible realizar la apertura " + ex.Message);
            }

        }

        public async Task<object> BlockDoor(string panelID, string readerID)
        {
            try
            {
                ManagementScope readerScope = _dataConduITMgr.GetManagementScope();

                ObjectQuery readerSearcher = new ObjectQuery("SELECT * FROM Lnl_Reader WHERE PanelID='" + panelID + "' " +
                    "AND ReaderID='" + readerID + "'");
                ManagementObjectSearcher getreader = new ManagementObjectSearcher(readerScope, readerSearcher);

                foreach (ManagementObject queryObj in getreader.Get())
                {
                    ManagementBaseObject inParams = queryObj.GetMethodParameters("SetMode");

                    inParams.Properties["Mode"].Value = 0;

                    queryObj.InvokeMethod("SetMode", inParams, null);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("No fue posible realizar el bloqueo " + ex.Message);
            }

        }
        #endregion
    }
}
