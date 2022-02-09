using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDFSv4.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Type = TDFSv4.Models.Type;

namespace TDFSv4.Controllers
{
    public class ClientsController : Controller
    {
        private IEnumerable<Type> types = new List<Type>
        {
            new Type {Id = 1, Name = "ЮЛ"},
            new Type {Id = 2, Name = "ИП"}
        };

        private ApplicationContext db;
        public ClientsController(ApplicationContext context)
        {
            db = context;
        }

        //public async Task<IActionResult> Index()
        //{
        //    return RedirectToAction("Index", "Home");
        //}


        // GET: Clients/Create
        public IActionResult Create()
        {
            ViewBag.Types = new SelectList(types, "Id", "Name");
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Tin,CreationDate,UpdateDate")] Client client)
        {
            if (ModelState.IsValid)
            {
                client.Type = types.FirstOrDefault(x => x.Id == client.Tin);
                client.CreationDate = DateTime.Now;
                client.UpdateDate = DateTime.Now;
                db.Add(client);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
                //return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var client = await db.Clients.FindAsync(id); //не то же ли самое?
            if (id == null || client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Client updatedUserData)
        {
            if (string.IsNullOrWhiteSpace(updatedUserData.Name) || updatedUserData.Tin < 0) // Нужно больше кейсов для проверки
            {
                return BadRequest("Не введено имя пользователи или инн равен нулю или менее нуля");
            }
            Client client = await db.Clients.FirstOrDefaultAsync(x => x.Id == id);
            client.Name = updatedUserData.Name;
            client.Tin = updatedUserData.Tin;
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await db.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await db.Clients.FindAsync(id);
            db.Clients.Remove(client);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
            //return RedirectToAction(nameof(Index));
        }
    }
}
