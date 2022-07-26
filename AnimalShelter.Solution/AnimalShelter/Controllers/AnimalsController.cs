using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System.Collections.Generic;
using System.Linq;


namespace AnimalShelter.Controllers
{
  public class AnimalsController : Controller
  {
    private readonly AnimalShelterContext _db;
    
    public AnimalsController(AnimalShelterContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Animal> model = _db.Animals
        .OrderBy(x => x.Breed)
        .ThenBy(x => x.DateAdmit)
        .ToList();
  
      // IEnumerable<Animal> model = unsortedModel.OrderBy(p => p.Breed);

      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Animal animal)
    {
      _db.Animals.Add(animal);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Animal thisAnimal = _db.Animals.FirstOrDefault(animal => animal.AnimalId == id);
      return View(thisAnimal);
    }

    [HttpGet]
    public ActionResult ShowSearchForm()
    {
      return View();
    }

    [HttpPost]
    public ActionResult ShowSearchResults(string searchProperty, string searchPhrase)
    {
      if (searchProperty == "Breed")
      {
        List<Animal> model = _db.Animals.Where(p => p.Breed.Contains(searchPhrase)).ToList(); 
        return View("Index", model);    
      } 
      else if (searchProperty == "Name")
      {
        List<Animal> model = _db.Animals.Where(p => p.Name.Contains(searchPhrase)).ToList();
        return View("Index", model);
      }
      else if (searchProperty == "Species")
      {
        List<Animal> model = _db.Animals.Where(p => p.Species.Contains(searchPhrase)).ToList();
        return View("Index", model);
      }
      else 
      {
        return View();
      }
    }
  }
}