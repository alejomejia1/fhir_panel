using System;
using System.Collections.Generic;
using System.Management;
using System.Threading.Tasks;
using LenelServices.Repositories.Interfaces;
using LenelServices.Repositories.DTO;
using DataConduitManager.Repositories.Interfaces;
using DataConduitManager.Repositories.DTO;
using Microsoft.Extensions.Configuration;

namespace LenelServices.Repositories.Logic
{
    public class CardHolder_REP_LOCAL:ICardHolder_REP_LOCAL
    {
        #region PROPIEDADES
        private readonly ICardHolder _cardHolder_REP;
        private readonly IDataConduITMgr _dataConduITMgr;
        private readonly IConfiguration _config;
        private readonly IBadge_REP_LOCAL _badge_REP_LOCAL;
        private string _path;
        private string _user;
        private string _pass;
        #endregion

        #region CONSTRUCTOR
        public CardHolder_REP_LOCAL(ICardHolder cardHolder_REP, IBadge_REP_LOCAL badge_REP_LOCAL,
            IDataConduITMgr dataConduITMgr, IConfiguration config)
        {
            _cardHolder_REP = cardHolder_REP;
            _badge_REP_LOCAL = badge_REP_LOCAL;
            _dataConduITMgr = dataConduITMgr;
            _config = config;
            _path = _config.GetSection("SERVER_PATH").Value.ToString();
            _user = _config.GetSection("SERVER_USER").Value.ToString();
            _pass = _config.GetSection("SERVER_PASSWORD").Value.ToString();
        }
        #endregion

        #region METODOS
        public async Task<object> CrearPersona(AddCardHolder_DTO newCardHolder, string idTipo)
        {
            switch (idTipo) 
            {
                case "V":
                    return await _cardHolder_REP.AddVisitor(newCardHolder, _path, _user, _pass);
                default:
                    return await _cardHolder_REP.AddCardHolder(newCardHolder, _path, _user, _pass);
            }
            
        }

        public async Task<GetCardHolder_DTO> ObtenerPersona(string idPersona, string idTipo) 
        { 
            GetCardHolder_DTO persona = new GetCardHolder_DTO();
            ManagementObjectSearcher cardHolder = new ManagementObjectSearcher();
            switch (idTipo) 
            {
                case "V":
                    cardHolder = await _cardHolder_REP.GetVisitor(idPersona, _path, _user, _pass);
                    break;
                default:
                    cardHolder = await _cardHolder_REP.GetCardHolder(idPersona, _path, _user, _pass);
                    break;
            }
            
                
            try
            {
                foreach (ManagementObject queryObj in cardHolder.Get())
                {
                    try { persona.apellidos = queryObj["LASTNAME"].ToString(); } catch { persona.apellidos = null; }
                    try { persona.nombres = queryObj["FIRSTNAME"].ToString(); } catch { persona.nombres = null; }
                    try { persona.ssno = queryObj["SSNO"].ToString();} catch { persona.ssno = null; }
                    try { persona.status = queryObj["STATE"].ToString(); } catch { persona.status = null; }
                    try { persona.documento = queryObj["OPHONE"].ToString(); } catch { persona.documento = null; }
                    try { persona.empresa = queryObj["DIVISION"].ToString(); } catch { persona.empresa = null; }
                    try { persona.ciudad = queryObj["CITY"].ToString(); } catch { persona.ciudad = null; }
                    try { persona.email = queryObj["EMAIL"].ToString(); } catch { persona.email = null; }
                    List<GetBadge_DTO> badges = await _badge_REP_LOCAL.ConsultarBadge(queryObj["ID"].ToString());
                    persona.Badges = badges;
                }

                return persona;
            }
            catch (Exception ex) { throw new Exception ( "message: " + ex.Message + "|||query: " + cardHolder.Query.QueryString + 
                "|||path: " + cardHolder.Scope.Path + "|||st: " + ex.StackTrace + "|||inne: " + ex.InnerException + "|||data: " + 
                ex.Data + "|||helplink: " + ex.HelpLink + "|||Hresult: " + ex.HResult); } 
        }

        public async Task<string> ActualizarPersona(UpdateCardHolder_DTO cardHolder, string idPersona) 
        {
            bool actualizado = false;
            if (cardHolder.visitante)
                actualizado = await _cardHolder_REP.UpdateCardHolder(cardHolder, idPersona, _path, _user, _pass);
            else
                actualizado = await _cardHolder_REP.UpdateVisitor(cardHolder, idPersona, _path, _user, _pass);

            if (actualizado)
                return "El empleado fue actualizado satisfactoriamente"; 
            else throw new Exception("No fue posible realizar la actualización de datos");
        }

        public async Task<object> ObtenerVisitante(string idLenel) {
            return await _cardHolder_REP.GetVisitor(idLenel, _path, _user, _pass);
        }
        #endregion
    }
}