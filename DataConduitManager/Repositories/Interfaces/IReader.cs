using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace DataConduitManager.Repositories.Interfaces
{
    public interface IReader
    {
        public enum readerMode
        {
            LOCKED = 0,
            CARDONLY = 1,
            PIN_OR_CARD = 2,
            PIN_AND_CARD = 3, 
            UNLOCKED = 4,
            FACCODE_ONLY = 5,
            CYPHERLOCK = 6,
            AUTOMATIC = 7,
            DEFAULT = 100
        }

        /// <summary>
        /// Obtiene la informacion de una lectora por medio de dataConduIT
        /// </summary>
        /// <param name="panelID"></param>
        /// <param name="readerID"></param>
        /// <param name="path"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        Task<ManagementObjectSearcher> GetReaderData(string panelID, string readerID, string path, string user, string pass);

        /// <summary>
        /// Obtiene el reader Mode de la lectora
        /// </summary>
        /// <param name="panelID"></param>
        /// <param name="readerID"></param>
        /// <param name="path"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        Task<int> ReaderGetMode(string panelID, string readerID, string path, string user, string pass);

        /// <summary>
        /// Establece el modo para la lectora solicitada LOCKED = 0, CARDONLY = 1, PIN_OR_CARD = 2, PIN_AND_CARD = 3, 
        /// UNLOCKED = 4, FACCODE_ONLY = 5, CYPHERLOCK = 6, AUTOMATIC = 7, DEFAULT = 100
        /// </summary>
        /// <param name="panelID"></param>
        /// <param name="readerID"></param>
        /// <param name="modeID"></param>
        /// <param name="path"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        Task<bool> ReaderSetMode(string panelID, string readerID, readerMode modeID, string path, string user, string pass);

        /// <summary>
        /// Establece apertura de una puerta solicitada
        /// </summary>
        /// <param name="panelID"></param>
        /// <param name="readerID"></param>
        /// <param name="path"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        Task<bool> OpenDoor(string panelID, string readerID, string path, string user, string pass);

        /// <summary>
        /// Establece el cierre de la puerta solicitada
        /// </summary>
        /// <param name="panelID"></param>
        /// <param name="readerID"></param>
        /// <param name="path"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        Task<object> BlockDoor(string panelID, string readerID, string path, string user, string pass);
    }
}
