using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LenelServices.Repositories.DTO
{
    public class SetBadgeStatus_DTO
    {
        public int estadoBadge { get; set; }
        public DateTime? fechaDesactivacion { get; set; }
    }
}
