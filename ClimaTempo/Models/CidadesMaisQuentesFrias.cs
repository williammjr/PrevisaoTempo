using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClimaTempo.Models
{
    public class CidadesMaisQuentesFrias
    {
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Decimal TemperaturaMaxima { get; set; }
        public Decimal TemperaturaMinima { get; set; }
    }
}