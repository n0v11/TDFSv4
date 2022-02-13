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
                foreach (var founder in client.Founders)
                {
                    founder.CreationDate = DateTime.Now;
                    founder.UpdateDate = founder.CreationDate;
                }
                client.CreationDate = DateTime.Now;
                client.UpdateDate = client.CreationDate;
                db.Add(client);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(client);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var client = await db.Clients.Include(x => x.Founders).SingleAsync(x => x.Id == id);
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
            Client client = await db.Clients.Include(x => x.Founders).SingleAsync(x => x.Id == id);
            if (!client.Name.Equals(updatedUserData.Name) || !client.Tin.Equals(updatedUserData.Tin))
            {
                client.UpdateDate = DateTime.Now;
            }
            client.Name = updatedUserData.Name;
            client.Tin = updatedUserData.Tin;
            FoundersEdit(updatedUserData);

            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");

            void FoundersEdit(Client updatedUserData)
            {
                if (client.Founders != null)
                {
                    foreach (var founder in client.Founders)
                    {
                        var updatedFounderData = updatedUserData.Founders.FirstOrDefault(x => x.Id == founder.Id);
                        if (!founder.Fio.Equals(updatedFounderData.Fio) || !founder.Tin.Equals(updatedFounderData.Tin)) //можно учесть доп факторы
                        {
                            founder.UpdateDate = DateTime.Now;
                        }
                        founder.Fio = updatedFounderData.Fio;
                        founder.Tin = updatedFounderData.Tin;
                    }
                }
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            Client client = await db.Clients.Include(x => x.Founders).SingleAsync(x => x.Id == id);
            foreach (var founder in client.Founders)
            {
                db.Founders.Remove(founder);
            }
            db.Clients.Remove(client);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
