using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LenelServices.Repositories.DTO
{
    public class ConfigLectora_DTO
    {
        public string ControlType { get; set; }
        public string Name { get; set; }
        public string PanelID { get; set; }
        public string ReaderID { get; set; }
        public string TimeAttendanceType { get; set; }
        public int ReaderMode { get; set; }
    }
}
