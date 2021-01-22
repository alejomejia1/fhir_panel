﻿using System;
using System.Threading.Tasks;
using System.Management;
using LenelServices.Repositories.DTO;
using LenelServices.Repositories.Interfaces;
using DataConduitManager.Repositories.Interfaces;
using DataConduitManager.Repositories.DTO;
using Microsoft.Extensions.Configuration;

namespace LenelServices.Repositories.Logic
{
    public class Reader_REP_LOCAL:IReader_REP_LOCAL
    {
        #region PROPIEDADES
        private readonly IReader _reader_REP;
        private readonly IConfiguration _config;
        private string _path;
        private string _user;
        private string _pass;
        #endregion

        #region CONSTRUCTOR
        public Reader_REP_LOCAL(IReader reader_REP, IConfiguration config)
        {
            _reader_REP = reader_REP;
            _config = config;
            _path = _config.GetSection("SERVER_PATH").Value.ToString();
            _user = _config.GetSection("SERVER_USER").Value.ToString();
            _pass = _config.GetSection("SERVER_PASSWORD").Value.ToString();
        }
        #endregion

        #region METODOS
        public async Task<object> ConfiguracionLectora(string panelID, string readerID) {
            
            ManagementObjectSearcher readerData = await _reader_REP.GetReaderData(panelID, readerID, _path, _user, _pass);

            int Modo = await _reader_REP.ReaderGetMode(panelID, readerID, _path, _user, _pass);

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
                return await _reader_REP.ReaderSetMode(readerPath.panelID, readerPath.readerID, IReader.readerMode.CARDONLY,
                     _path, _user, _pass);
            }
            catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        public async Task<object> BloquearPuerta(ReaderPath_DTO readerPath)
        {
            try
            {
                return await _reader_REP.ReaderSetMode(readerPath.panelID, readerPath.readerID, IReader.readerMode.LOCKED,
                     _path, _user, _pass);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<object> CambioEstadoPuerta(ReaderPath_DTO readerPath, int estado) 
        {
            IReader.readerMode readerMode = (IReader.readerMode)Enum.Parse(typeof(IReader.readerMode), estado.ToString());

            try
            {
                return await _reader_REP.ReaderSetMode(readerPath.panelID, readerPath.readerID, readerMode,
                     _path, _user, _pass);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> EnviarEventoGenerico(SendEvent_DTO evento) 
        {
            try 
            {
                return await _reader_REP.SendIncomingEvent(evento, _path, _user, _pass);
            } 
            catch (Exception ex){ throw new Exception("no se pudo crear el evento " + ex.Message); }
        }
        #endregion
    }
}
