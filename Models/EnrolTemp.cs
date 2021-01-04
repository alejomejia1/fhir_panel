using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspStudio.Models
{

    [Table("EnrolTemp")]
    public class EnrolTemp
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Key]
        public int Id { get; set; }

        // [Column("id_lenel")]
        // public string IdLenel { get; set; }

        [Column("lastname")]
        public string LastName { get; set; }

        [Column("firstname")]
        public string FirstName { get; set; }

        [Column("ssno")]
        public string SSNO { get; set; }

        [Column("id_status")]
        public string IdStatus { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("documento")]
        public string Documento { get; set; }

        [Column("empresa")]
        public string Empresa { get; set; }

        [Column("imageUrl")]
        public string imageUrl { get; set; }

        [Column("created")]
        public DateTime? Created { get; set; }

        [Column("ciudadEnroll")]
        public string Ciudad { get; set; }

        [Column("Regional")]
        public Int16 Regional { get; set; }

        [Column("Instalacion")]
        public Byte Instalacion { get; set; }

        [Column("CiudadOrigen")]
        public string CiudadOrigen { get; set; }

        [Column("Metadatos")]
        public string Metadatos { get; set; }

        [Column("Badge_id")]
        public string Badge_id { get; set; }

        [Column("acepta_terminos")]
        public Boolean acepta_terminos { get; set; }

    }
}
