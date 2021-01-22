using System.Threading.Tasks;
using DataConduitManager.Repositories.DTO;
using LenelServices.Repositories.DTO;

namespace LenelServices.Repositories.Interfaces
{
    public interface ICardHolder_REP_LOCAL
    {
        /// <summary>
        /// Crea un nuevo TarjetaHabiente en Lenel por medio de DataConduIT
        /// </summary>
        /// <param name="newCardHolder"></param>
        /// <param name="idTipo">F=FUNCIONARIO -- V= VISITANTE</param>
        /// <returns></returns>
        Task<object> CrearPersona(AddCardHolder_DTO newCardHolder, string idTipo);

        /// <summary>
        /// Obtiene una persona de lenel por medio de DataConduIT
        /// </summary>
        /// <param name="idLenel"></param>
        /// <param name="idTipo">F = FUNCIONARIO -- V = VISITANTE</param>
        /// <returns></returns>
        Task<GetCardHolder_DTO> ObtenerPersona(string idLenel, string idTipo);

        /// <summary>
        /// Obtiene una persona de lenel por medio de DataConduIT
        /// </summary>
        /// <param name="idBadge"></param>
        /// <returns></returns>
        Task<GetCardHolder_DTO> ObtenerPersona(string idBadge);

        /// <summary>
        /// Obtiene un Visitante por medio de DataConduIT
        /// </summary>
        /// <param name="idLenel"></param>
        /// <returns></returns>
        Task<object> ObtenerVisitante(string idLenel);

        /// <summary>
        /// Actualiza la informacion de un empleado por medio de DataConduIT
        /// </summary>
        /// <param name="cardHolder"></param>
        /// <param name="idLenel"></param>
        /// <returns></returns>
        Task<string> ActualizarPersona(UpdateCardHolder_DTO cardHolder, string idPersona);
    }
}
