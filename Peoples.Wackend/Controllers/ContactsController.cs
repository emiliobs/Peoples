using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Peoples.Wackend.Models;
using Peoples.Wackend.Helpers;

namespace Peoples.Wackend.Controllers
{
    public class ContactsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Contacts
        public async Task<ActionResult> Index()
        {
            return View(await db.Contacts.OrderBy(c=>c.FirstName).ToListAsync());
        }

        // GET: Contacts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ContactView view)
        {
            if (ModelState.IsValid)
            {
                var picture = string.Empty;
                var folder = "~/Content/Contacts";

                if (view.ImageFile != null)
                {
                    picture = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    picture = $"{folder}/{picture}";
                }

                //aqui convierto de clase contactView a contact class:
                var contact = ToContact(view);
                contact.Image = picture;
                db.Contacts.Add(contact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private Contact ToContact(ContactView view)
        {
            return new Contact()
            {
               ContactId = view.ContactId,
               Email = view.Email,
               FirstName = view.FirstName,
               Image = view.Image,
               LastName = view.LastName,
               Phone = view.Phone,
            };
        }

        // GET: Contacts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            var view = ToView(contact);

            return View(view);
        }

        private ContactView ToView(Contact contact)
        {
            return new ContactView()
            {
              
                Image = contact.Image,
               LastName = contact.LastName,
               Phone = contact.Phone,
               ContactId = contact.ContactId,
               FirstName = contact.FirstName,
               Email = contact.Email,
               
            };
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ContactView view)
        {
            if (ModelState.IsValid)
            {
                var picture = view.Image;
                var folder = "~/Content/Contacts";

                if (view.ImageFile != null)
                {
                    picture = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    picture = $"{folder}/{picture}";
                }

                var contact = ToContact(view);
                contact.Image = picture;
                db.Entry(contact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }

        // GET: Contacts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Contact contact = await db.Contacts.FindAsync(id);
            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();
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
