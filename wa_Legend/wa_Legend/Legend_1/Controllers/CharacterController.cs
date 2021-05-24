using Legend_1.Data;
using Legend_1.Models;
using Legend_1.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legend_1.Controllers
{
    public class CharacterController : Controller
    {
        readonly private ApplicationDbContext _db;

        public CharacterController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Character> characters = _db.Characters;
            foreach (Character character in characters)
            {
                character.OrderCharacter = _db.Orders.Find(character.OrderId);
            }
            return View(characters);
        }

        public IActionResult Upsert(int? id)
        {
            VMCharacter character = new()
            {
                Character = new Character(),
                OrdersSelectList = _db.Orders.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.NameOrder
                })
            };
            if (id != null)
            {
                character.Character = _db.Characters.Find(id);
                if (character.Character == null)
                {
                    return NotFound();
                }
            }
            return View(character);
        }

        [HttpPost]
        public IActionResult Upsert(Character character)
        {
            if (ModelState.IsValid)
            {
                if (character.Id == 0)
                {
                    _db.Characters.Add(character);
                }
                else
                {
                    _db.Characters.Update(character);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            VMCharacter characterVM = new VMCharacter()
            {
                OrdersSelectList = _db.Orders.Select(x => new SelectListItem()
                {
                    Text = x.NameOrder,
                    Value = x.Id.ToString()
                }),
                Character = character
            };
            return View(characterVM);
        }

        public IActionResult Delete(int id)
        {
            Character character = _db.Characters.Find(id);
            if (character == null)
            {
                return NotFound();
            }
            character.OrderCharacter = _db.Orders.Find(character.OrderId);
            return View(character);
        }

        [HttpPost]
        public IActionResult Delete(Character character)
        {
                _db.Characters.Remove(character);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }

    }
}
