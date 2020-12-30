using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using LenelServices.Repositories.DTO;

namespace LenelServices.Repositories.Interfaces
{
    public interface IReader_REP_LOCAL
    {
        Task<object> ConfiguracionLectora(string panelID, string readerID);
        Task<bool> AbrirPuerta(ReaderPath_DTO readerPath);
        Task<object> BloquearPuerta(ReaderPath_DTO readerPath);
    }
}
