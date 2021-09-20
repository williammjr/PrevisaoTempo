using ClimaTempo.DAL;
using ClimaTempo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClimaTempo.Controllers
{
    public class ClimaTempoController : Controller
    {
        // GET: ClimaTempo
        public ActionResult Index(string hoje)
        {
            if (String.IsNullOrEmpty(hoje))
            {
                hoje = DateTime.Now.ToString("yyyy-MM-dd");
            }

            context db = new context();
            ViewBag.Cidade = new SelectList(db.Cidade.Select(s => new { Text = s.Nome, Value = s.ID }).ToList(), "Value", "Text");
            ViewBag.Hoje = hoje;
            return View();
        }

        public ActionResult CardsPrevisaoSemana(string hoje, int idCidade = 0)
        {
            if (String.IsNullOrEmpty(hoje))
            {
                hoje = DateTime.Now.ToString("yyyy-MM-dd");
            }

            context db = new context();
            string[] hojeArray = hoje.Split('-');

            Int32.TryParse(hojeArray[0], out int ano);
            Int32.TryParse(hojeArray[1], out int mes);
            Int32.TryParse(hojeArray[2], out int dia);

            DateTime dataOrigem = new DateTime(ano, mes, dia);
            DateTime setimoDia = dataOrigem.AddDays(6);

            int cidade = idCidade == 0 ? 1 : idCidade;

            List<PrevisaoClima> previsaoSemana = db.PrevisaoClima
                .Where(w => w.DataPrevisao >= dataOrigem && w.DataPrevisao <= setimoDia && w.CidadeId == cidade)
                .OrderBy(o => o.DataPrevisao)
                .ToList();

            ViewBag.PrevisaoSemana = previsaoSemana;
            ViewBag.CidadeFiltrada = db.Cidade.Where(w => w.ID == cidade).Select(s => s.Nome).FirstOrDefault();

            return PartialView("_CardsPrevisaoSemana");
        }

        public ActionResult CardsCidadesMaisQuentesFrias(string hoje)
        {
            if (String.IsNullOrEmpty(hoje))
            {
                hoje = DateTime.Now.ToString("yyyy-MM-dd");
            }

            string[] hojeArray = hoje.Split('-');

            Int32.TryParse(hojeArray[0], out int ano);
            Int32.TryParse(hojeArray[1], out int mes);
            Int32.TryParse(hojeArray[2], out int dia);

            DateTime dataOrigem = new DateTime(ano, mes, dia);

            context db = new context();

            List<CidadesMaisQuentesFrias> cidadesMaisFrias = (from previsao in db.PrevisaoClima
                          join cidade in db.Cidade
                          on previsao.CidadeId equals cidade.ID
                          join estado in db.Estado
                          on cidade.EstadoId equals estado.ID
                          where previsao.DataPrevisao == dataOrigem
                          select new CidadesMaisQuentesFrias
                          {
                              Cidade = cidade.Nome,
                              Estado = estado.Nome,
                              TemperaturaMaxima = previsao.TemperaturaMaxima,
                              TemperaturaMinima = previsao.TemperaturaMinima
                          })
                          .OrderBy(o => o.TemperaturaMaxima).ThenBy(t => t.Cidade).ThenBy(t => t.Estado)
                          .Take(3).ToList();

            List<CidadesMaisQuentesFrias> cidadesMaisQuentes = (from previsao in db.PrevisaoClima
                                    join cidade in db.Cidade
                                    on previsao.CidadeId equals cidade.ID
                                    join estado in db.Estado
                                    on cidade.EstadoId equals estado.ID
                                    where previsao.DataPrevisao == dataOrigem
                                    select new CidadesMaisQuentesFrias
                                    {
                                        Cidade = cidade.Nome,
                                        Estado = estado.Nome,
                                        TemperaturaMaxima = previsao.TemperaturaMaxima,
                                        TemperaturaMinima = previsao.TemperaturaMinima
                                    })
                          .OrderByDescending(o => o.TemperaturaMaxima).ThenBy(t => t.Cidade).ThenBy(t => t.Estado)
                          .Take(3).ToList();

            ViewBag.CidadesMaisFrias = cidadesMaisFrias;
            ViewBag.CidadesMaisQuentes = cidadesMaisQuentes;
            ViewBag.Hoje = hoje;

            return PartialView("_CardsCidadesMaisQuentesFrias");
        }
    }
}
