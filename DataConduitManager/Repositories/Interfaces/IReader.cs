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
        Task<ManagementObjectSearcher> GetReaderData(string panelID, string readerID);
        Task<int> ReaderGetMode(string panelID, string readerID);
        Task<bool> ReaderSetMode(string panelID, string readerID, IReader.readerMode modeID);
        Task<bool> OpenDoor(string panelID, string readerID);
        Task<object> BlockDoor(string panelID, string readerID);
    }
}
