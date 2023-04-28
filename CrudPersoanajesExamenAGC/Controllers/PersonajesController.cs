using CrudPersoanajesExamenAGC.Models;
using CrudPersoanajesExamenAGC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudPersoanajesExamenAGC.Controllers
{
    public class PersonajesController : Controller
    {

        private PersonajesService service;

        public PersonajesController(PersonajesService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes = await this.service.GetPersonajes();
            return View(personajes);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.CreatePersonaje(personaje);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int id)
        {
            return View(await this.service.FindPersonaje(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personaje personaje)
        {
            await this.service.UpdatePersonaje(personaje);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonaje(id);
            return RedirectToAction("Index");
        }
    }
}
