using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClimaTempo.Models
{
    [Table("Estado")]
    public class Estado
    {
        [Key]
        public int ID { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
    }
}