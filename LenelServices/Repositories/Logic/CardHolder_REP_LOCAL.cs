using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LenelServices.Repositories.Interfaces;
using DataConduitManager.Repositories.Interfaces;
using DataConduitManager.Repositories.DTO;

namespace LenelServices.Repositories.Logic
{
    public class CardHolder_REP_LOCAL:ICardHolder_REP_LOCAL
    {
        private readonly ICardHolder _cardHolder_REP;

        #region Constructor
        public CardHolder_REP_LOCAL(ICardHolder cardHolder_REP)
        {
            _cardHolder_REP = cardHolder_REP;
        }
        #endregion

        #region Métodos
        public async Task<object> CrearPersona(AddCardHolder_DTO newCardHolder)
        {
            return await _cardHolder_REP.AddCardHolder(newCardHolder);
        }

        public async Task<object> ObtenerEmpleado(string idLenel) {
            return await _cardHolder_REP.GetCardHolder(idLenel);
        }

        public async Task<object> ObtenerVisitante(string idLenel) {
            return await _cardHolder_REP.GetVisitor(idLenel);
        }
        #endregion
    }
}
