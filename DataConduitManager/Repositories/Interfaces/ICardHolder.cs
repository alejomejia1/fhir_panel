using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Threading.Tasks;
using DataConduitManager.Repositories.DTO;

namespace DataConduitManager.Repositories.Interfaces
{
    public interface ICardHolder
    {
        /// <summary>
        /// Crea un nuevo CardHolder en Lenel
        /// </summary>
        /// <param name="newCardHolder"></param>
        /// <param name="path"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        Task<object> AddCardHolder(AddCardHolder_DTO newCardHolder, string path, string user, string pass);

        /// <summary>
        /// Actualiza un CardHolder en Lenel
        /// </summary>
        /// <param name="cardHolder"></param>
        /// <param name="idLenel"></param>
        /// <param name="path"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        Task<bool> UpdateCardHolder(UpdateCardHolder_DTO cardHolder, string idLenel, string path, string user, string pass);

        /// <summary>
        /// Obtiene un CardHolder en Lenel
        /// </summary>
        /// <param name="idLenel"></param>
        /// <param name="path"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ManagementObjectSearcher> GetCardHolder(string idLenel, string path, string user, string password);

        /// <summary>
        /// Obtiene un Visitor en Lenel
        /// </summary>
        /// <param name="idLenel"></param>
        /// <param name="path"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        Task<object> GetVisitor(string idLenel, string path, string user, string pass);
    }
}
