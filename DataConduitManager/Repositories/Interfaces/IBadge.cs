using System.Threading.Tasks;
using System.Management;
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

        /// <summary>
        /// Obtiene la informacion de un Badge en Lenel
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="path"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        Task<ManagementObjectSearcher> GetBadge(string personId, string path, string user, string pass);

        /// <summary>
        /// Obtiene la informacion de una tarjeta dado su id
        /// </summary>
        /// <param name="badgeId"></param>
        /// <param name="path"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        Task<ManagementObjectSearcher> GetPersonBadge(string badgeId, string path, string user, string pass);
    }
}
