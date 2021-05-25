using Legend_2.Data;
using Legend_2.Models;
using Legend_2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legend_2.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CharacterController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Character> characters = _db.Characters.Include(x => x.OrderCharacter);
            return View(characters);
        }

        public IActionResult Upsert(int? id)
        {
            CharacterVM characterVM = new()
            {
                Character = new(),
                Orders = _db.Orders.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.NameOrder
                })
            };

            if (id == null)
            {
                return View(characterVM);
            }
            else
            {
                characterVM.Character = _db.Characters.Find(id);
                if (characterVM.Character == null)
                {
                    return NotFound();
                }
                return View(characterVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Character character)
        {
            if (ModelState.IsValid)
            {
                if (character.Id==0)
                {
                    _db.Characters.Add(character);
                }
                else
                {
                    Character temp = _db.Characters.AsNoTracking().FirstOrDefault(x => x.Id == character.Id);
                    if (temp ==null)
                    {
                        return NotFound();
                    }
                    _db.Characters.Update(character);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            CharacterVM characterVM = new() { Character = character, Orders = _db.Orders.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.NameOrder }) };
            return View(characterVM);
        }

        public IActionResult Delete(int id)
        {
            Character character = new();
            character = _db.Characters.Include(x=>x.OrderCharacter).FirstOrDefault(x=>x.Id==id);
            if (character==null)
            {
                return NotFound();
            }
            return View(character);
        }

        public IActionResult DeletePost(Character character)
        {
            Character temp= _db.Characters.AsNoTracking().FirstOrDefault(x => x.Id == character.Id);
            if (temp==null)
            {
                return NotFound();
            }
            _db.Characters.Remove(character);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
