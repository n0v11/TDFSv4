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
            ViewBag.Types = new SelectList(db.Types, nameof(Type.Id), nameof(Type.Name));
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                //if (client.TypeId == 2 && client.Founders.Count > 1) //тупая проверка на наличие 2 учредителей у ип
                //{
                //    return RedirectToAction("Index", "Home");
                //}
                //client.Type = new Type(client.TypeId);
                //client.Type.Name = db.Types.FirstOrDefault(x => x.Id == client.TypeId).Name;
                client.CreationDate = DateTime.Now;
                client.UpdateDate = DateTime.Now;
                db.Add(client);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(client);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var client = await db.Clients.Include(x => x.Founders).SingleAsync(x => x.Id == id); //не то же ли самое?
            if (id == null || client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Client updatedUserData)
        {
            if (string.IsNullOrWhiteSpace(updatedUserData.Name)) // Нужно больше кейсов для проверки
            {
                return BadRequest("Не введено имя пользователи или инн равен нулю или менее нуля");
            }
            Client client = await db.Clients.FirstOrDefaultAsync(x => x.Id == id);
            client.Name = updatedUserData.Name;
            client.Tin = updatedUserData.Tin;
            client.UpdateDate = DateTime.Now;
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = await db.Clients.Include(x => x.Founders).SingleAsync(x => x.Id == id);
            foreach (var f in client.Founders)
            {
                db.Founders.Remove(f);
            }
            db.Clients.Remove(client);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
