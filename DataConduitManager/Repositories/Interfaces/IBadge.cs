using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using DataConduitManager.Repositories.DTO;

namespace DataConduitManager.Repositories.Interfaces
{
    public interface IBadge
    {
        /// <summary>
        /// Crea un nuevo Badge en Lenel
        /// </summary>
        /// <param name="newBadge"></param>
        /// <param name="path"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        Task<object> AddBadge(AddBadge_DTO newBadge, string path, string user, string pass);
    }
}
