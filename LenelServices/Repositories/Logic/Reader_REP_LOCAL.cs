using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Management;
using LenelServices.Repositories.DTO;
using LenelServices.Repositories.Interfaces;
using DataConduitManager.Repositories.Interfaces;

namespace LenelServices.Repositories.Logic
{
    public class Reader_REP_LOCAL:IReader_REP_LOCAL
    {
        private readonly IReader _reader_REP;

        public Reader_REP_LOCAL(IReader reader_REP)
        {
            _reader_REP = reader_REP;
        }

        public async Task<object> ConfiguracionLectora(string panelID, string readerID) {
            
            ManagementObjectSearcher readerData = await _reader_REP.GetReaderData(panelID, readerID);

            int Modo = await _reader_REP.ReaderGetMode(panelID, readerID);

            foreach (ManagementObject queryObj in readerData.Get())
            {
                ConfigLectora_DTO result = new ConfigLectora_DTO
                {
                    ControlType =  queryObj["ControlType"].ToString(),
                    Name = queryObj["Name"].ToString(),
                    PanelID = queryObj["PanelID"].ToString(),
                    ReaderID = queryObj["ReaderID"].ToString(),
                    TimeAttendanceType = queryObj["TimeAttendanceType"].ToString(),
                    ReaderMode = Modo
                };

                return result;
            }

            return "ok";
        }

        public async Task<bool> AbrirPuerta(ReaderPath_DTO readerPath) 
        {
            try{
                return await _reader_REP.OpenDoor(readerPath.panelID, readerPath.readerID);
            }
            catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        public async Task<object> BloquearPuerta(ReaderPath_DTO readerPath)
        {
            try
            {
                return await _reader_REP.BlockDoor(readerPath.panelID, readerPath.readerID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
