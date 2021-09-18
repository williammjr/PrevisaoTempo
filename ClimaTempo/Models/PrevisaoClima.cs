using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClimaTempo.Models
{
    [Table("PrevisaoClima")]
    public class PrevisaoClima
    {
        [Key]
        public int ID { get; set; }
        public int CidadeId { get; set; }
        public DateTime DataPrevisao { get; set; }
        public string Clima { get; set; }
        public Decimal TemperaturaMinima { get; set; }
        public Decimal TemperaturaMaxima { get; set; }
    }
}