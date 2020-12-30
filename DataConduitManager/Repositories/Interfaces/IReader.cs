using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace DataConduitManager.Repositories.Interfaces
{
    public interface IReader
    {
        Task<ManagementObjectSearcher> GetReaderData(string panelID, string readerID);
        Task<int> ReaderGetMode(string panelID, string readerID);
        Task<bool> OpenDoor(string panelID, string readerID);
        Task<object> BlockDoor(string panelID, string readerID);
    }
}
