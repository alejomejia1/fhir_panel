using System.Threading.Tasks;
using LenelServices.Repositories.DTO;
using DataConduitManager.Repositories.DTO;

namespace LenelServices.Repositories.Interfaces
{
    public interface IReader_REP_LOCAL
    {
        /// <summary>
        /// obtiene parametros de la lectora y su readerMode actual por medio de DataConduIT
        /// </summary>
        /// <param name="panelID"></param>
        /// <param name="readerID"></param>
        /// <returns></returns>
        Task<object> ConfiguracionLectora(string panelID, string readerID);

        /// <summary>
        /// Establece la controla en modo UNLOCKED por medio de DataConduIT
        /// </summary>
        /// <param name="readerPath"></param>
        /// <returns></returns>
        Task<bool> AbrirPuerta(ReaderPath_DTO readerPath);

        /// <summary>
        /// Establece la lectora en modo LOCKED por medio de DataConduIT
        /// </summary>
        /// <param name="readerPath"></param>
        /// <returns></returns>
        Task<object> BloquearPuerta(ReaderPath_DTO readerPath);

        /// <summary>
        /// Genera un evento generico por medio de DataConduIT
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
        Task<bool> EnviarEventoGenerico(SendEvent_DTO evento);
    }
}
