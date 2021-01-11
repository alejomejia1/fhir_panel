using System;
using System.Management;
using DataConduitManager.Repositories.Interfaces;

namespace DataConduitManager.Repositories.Logic
{
    public class DataConduITMgr : IDataConduITMgr
    {
        #region PROPIEDADES
        public string serverPath { get; set; }
        public string serverUser { get; set; }
        public string serverPassword { get; set; }
        #endregion

        #region CONSTRUCTOR 

        public DataConduITMgr()
        {
            this.serverPath = @"\\.\root\OnGuard"; 
        }
        #endregion

        #region METODOS
        public ManagementScope GetManagementScope() {
            return CreateDataConduitScope();
        }

        public ManagementScope GetManagementScope(string path, string user, string pass)
        {
            this.serverPath = path;
            this.serverUser = user;
            this.serverPassword = pass;
            return CreateDataConduitScope();
        }

        private ManagementScope CreateDataConduitScope()
        {
            /*ESTABLECE UN SCOPE DE DATACONDUIT PARA REALIZAR UNA ACCION*/
            ConnectionOptions conexion = new ConnectionOptions();
            if (!string.IsNullOrEmpty(this.serverUser))
                conexion.Username = this.serverUser;
            if (!string.IsNullOrEmpty(this.serverPassword))
                conexion.Password = this.serverPassword;
            conexion.Authentication = AuthenticationLevel.Default;
            conexion.Impersonation = ImpersonationLevel.Impersonate;
            conexion.EnablePrivileges = true;
            return new ManagementScope(this.serverPath, conexion);
        }

        public string ReceiveEvent(ManagementScope scope)
        {
            string strSalida;
            strSalida = "";
            try
            {

                ObjectQuery selectQuery = new System.Management.ObjectQuery("Select * from Lnl_AlarmDefinition");

                //Instanciamos un buscador de objetos
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, selectQuery);

                //Usamos un foreach para la selección individual de cada uno de los procesos para añadirlos a un ListBox
                foreach (ManagementObject proceso in searcher.Get())
                {
                    //Console.Write("ID:" + proceso["ID"].ToString() + " - " + "Description:" + proceso["Description"].ToString() + "\n\r");
                    strSalida = strSalida + "ID:" + proceso["ID"].ToString() + " - " + "Description:" + proceso["Description"].ToString() + " - " + "TextInstructionName:" + proceso["TextInstructionName"].ToString() + Environment.NewLine;
                }

                return strSalida;

            }
            catch (ManagementException err)
            {
                //MessageBox.Show("An error occurred while trying to receive an event: " + err.Message);
                return "ERROR";
            }
        }
        #endregion
    }
}
