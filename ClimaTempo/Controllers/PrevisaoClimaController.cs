using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClimaTempo.DAL;
using ClimaTempo.Models;

namespace ClimaTempo.Controllers
{
    public class PrevisaoClimaController : Controller
    {
        private context db = new context();

        // GET: PrevisaoClima
        public ActionResult Index()
        {
            return View(db.PrevisaoClima.ToList());
        }

        // GET: PrevisaoClima/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrevisaoClima previsaoClima = db.PrevisaoClima.Find(id);
            if (previsaoClima == null)
            {
                return HttpNotFound();
            }
            return View(previsaoClima);
        }

        // GET: PrevisaoClima/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrevisaoClima/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CidadeId,DataPrevisao,Clima,TemperaturaMinima,TemperaturaMaxima")] PrevisaoClima previsaoClima)
        {
            if (ModelState.IsValid)
            {
                db.PrevisaoClima.Add(previsaoClima);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(previsaoClima);
        }

        // GET: PrevisaoClima/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrevisaoClima previsaoClima = db.PrevisaoClima.Find(id);
            if (previsaoClima == null)
            {
                return HttpNotFound();
            }
            return View(previsaoClima);
        }

        // POST: PrevisaoClima/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CidadeId,DataPrevisao,Clima,TemperaturaMinima,TemperaturaMaxima")] PrevisaoClima previsaoClima)
        {
            if (ModelState.IsValid)
            {
                db.Entry(previsaoClima).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(previsaoClima);
        }

        // GET: PrevisaoClima/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrevisaoClima previsaoClima = db.PrevisaoClima.Find(id);
            if (previsaoClima == null)
            {
                return HttpNotFound();
            }
            return View(previsaoClima);
        }

        // POST: PrevisaoClima/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrevisaoClima previsaoClima = db.PrevisaoClima.Find(id);
            db.PrevisaoClima.Remove(previsaoClima);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
