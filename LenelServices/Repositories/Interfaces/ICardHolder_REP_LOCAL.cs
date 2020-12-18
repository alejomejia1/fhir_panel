using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataConduitManager.Repositories.DTO;

namespace LenelServices.Repositories.Interfaces
{
    public interface ICardHolder_REP_LOCAL
    {
        /// <summary>
        /// Crea un nuevo tarjetaHabiente por medio de DataConduIT
        /// </summary>
        /// <param name="newBadge"></param>
        /// <returns></returns>
        Task<object> CrearPersona(AddCardHolder_DTO newCardHolder);

        /// <summary>
        /// Obtiene un Empleado por medio de DataConduIT
        /// </summary>
        /// <param name="idLenel"></param>
        /// <returns></returns>
        Task<object> ObtenerEmpleado(string idLenel);

        /// <summary>
        /// Obtiene un Visitante por medio de DataConduIT
        /// </summary>
        /// <param name="idLenel"></param>
        /// <returns></returns>
        Task<object> ObtenerVisitante(string idLenel);
    }
}
