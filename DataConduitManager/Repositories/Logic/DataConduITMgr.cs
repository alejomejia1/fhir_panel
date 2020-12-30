using System;
using System.Management;
using DataConduitManager.Repositories.Interfaces;

namespace DataConduitManager.Repositories.Logic
{
    public class DataConduITMgr : IDataConduITMgr
    {
        public ManagementPath path { get; set; }
        public ManagementPath clave { get; set; }

        public DataConduITMgr()
        {
            /*PATH PARA CONECTAR A DATACONDUIT */
            path = new ManagementPath("\\\\10.11.34.96\\root\\OnGuard"); 
            //VERIFICAR SI ESTO SERA UN PARAMETRO O SE FIJARA EN UN ARCHIVO DE CONFIGURACION EN CADA LUGAR DONDE SE EJECUTE 
        }

        public ManagementScope GetManagementScope() {
            return CreateDataConduitScope();
        }

        private ManagementScope CreateDataConduitScope()
        {
            /*ESTABLECE UN SCOPE DE DATACONDUIT PARA REALIZAR UNA ACCION*/
            ConnectionOptions conexion = new ConnectionOptions();
            conexion.Username = "Administrator";//SE PARAMETRIZAR EN UN ARCHIVO DE CONFIGURACION
            conexion.Password = "Ut1ndr40nc0r1732";//SE PARAMETRIZAR EN UN ARCHIVO DE CONFIGURACION
            conexion.Authentication = AuthenticationLevel.Default;
            conexion.Impersonation = ImpersonationLevel.Impersonate;
            conexion.EnablePrivileges = true;
            return new ManagementScope(path, conexion);
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
    }
}
