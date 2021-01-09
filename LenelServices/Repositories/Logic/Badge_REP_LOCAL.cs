using System.Threading.Tasks;
using LenelServices.Repositories.Interfaces;
using DataConduitManager.Repositories.DTO;
using DataConduitManager.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LenelServices.Repositories.Logic
{
    public class Badge_REP_LOCAL : IBadge_REP_LOCAL
    {
        #region PROPIEDADES
        private readonly IBadge _badge_REP;
        private readonly IConfiguration _config;
        private string _path;
        private string _user;
        private string _pass;
        #endregion

        #region CONSTRUCTOR
        public Badge_REP_LOCAL(IBadge badge_REP, IConfiguration config)
        {
            _badge_REP = badge_REP;
            _config = config;
            _path = _config.GetSection("SERVER_PATH").Value.ToString();
            _user = _config.GetSection("SERVER_USER").Value.ToString();
            _pass = _config.GetSection("SERVER_PASSWORD").Value.ToString();
        }
        #endregion

        #region METODOS
        public async Task<object> CrearBadge(AddBadge_DTO newBadge)
        {
            return await _badge_REP.AddBadge(newBadge, _path, _user, _pass);
        }
        #endregion
    }
}
