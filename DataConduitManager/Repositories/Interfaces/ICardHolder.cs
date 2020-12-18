using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataConduitManager.Repositories.DTO;

namespace DataConduitManager.Repositories.Interfaces
{
    public interface ICardHolder
    {
        /// <summary>
        /// CREA UN EMPLEADO NUEVA EN LENEL
        /// </summary>
        /// <returns></returns>
        Task<object> AddCardHolder(AddCardHolder_DTO newCardHolder);

        /// <summary>
        /// Obtiene un empleado Tarjeta Habiente en LENEL
        /// </summary>
        /// <param name="idLenel"></param>
        /// <returns></returns>
        Task<object> GetCardHolder(string idLenel);

        /// <summary>
        /// Obtiene un visitante en Lenel
        /// </summary>
        /// <param name="idLenel"></param>
        /// <returns></returns>
        Task<object> GetVisitor(string idLenel);
    }
}
