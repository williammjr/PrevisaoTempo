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
        public ActionResult Index()
        {
            context db = new context();
            ViewBag.Cidade = new SelectList(db.Cidade.Select(s => new { Text = s.Nome, Value = s.ID }).ToList(), "Value", "Text");
            return View();
        }

        // GET: ClimaTempo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClimaTempo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClimaTempo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ClimaTempo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClimaTempo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ClimaTempo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClimaTempo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CardsPrevisaoSemana(int idCidade = 0)
        {
            context db = new context();
            string[] hojeArray = "2021-03-03".Split('-');

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

            return PartialView("_CardsPrevisaoSemana");
        }

        public ActionResult CardsCidadesMaisQuentesFrias()
        {
            string[] hojeArray = "2021-03-03".Split('-');

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
                              Estado = estado.UF,
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
                                        Estado = estado.UF,
                                        TemperaturaMaxima = previsao.TemperaturaMaxima,
                                        TemperaturaMinima = previsao.TemperaturaMinima
                                    })
                          .OrderByDescending(o => o.TemperaturaMaxima).ThenBy(t => t.Cidade).ThenBy(t => t.Estado)
                          .Take(3).ToList();

            ViewBag.CidadesMaisFrias = cidadesMaisFrias;
            ViewBag.CidadesMaisQuentes = cidadesMaisQuentes;

            return PartialView("_CardsCidadesMaisQuentesFrias");
        }
    }
}
