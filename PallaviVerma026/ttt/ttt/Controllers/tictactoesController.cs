using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ttt.Models;

namespace ttt.Controllers
{
    public class tictactoesController : ApiController
    {
        private tttEntities1 db = new tttEntities1();

        // GET: api/tictactoes
        public IQueryable<tictacto> Gettictactoes()
        {
            return db.tictactoes;
        }

        // GET: api/tictactoes/5
        [ResponseType(typeof(tictacto))]
        public IHttpActionResult Gettictacto(int id)
        {
            tictacto tictacto = db.tictactoes.Find(id);
            if (tictacto == null)
            {
                return NotFound();
            }

            return Ok(tictacto);
        }

        // PUT: api/tictactoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttictacto(int id, tictacto tictacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tictacto.PID)
            {
                return BadRequest();
            }

            db.Entry(tictacto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tictactoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/tictactoes
        [ResponseType(typeof(tictacto))]
        public IHttpActionResult Posttictacto(tictacto tictacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tictactoes.Add(tictacto);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tictactoExists(tictacto.PID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tictacto.PID }, tictacto);
        }

        // DELETE: api/tictactoes/5
        [ResponseType(typeof(tictacto))]
        public IHttpActionResult Deletetictacto(int id)
        {
            tictacto tictacto = db.tictactoes.Find(id);
            if (tictacto == null)
            {
                return NotFound();
            }

            db.tictactoes.Remove(tictacto);
            db.SaveChanges();

            return Ok(tictacto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tictactoExists(int id)
        {
            return db.tictactoes.Count(e => e.PID == id) > 0;
        }
    }
}