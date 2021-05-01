using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using work1.Models;

namespace work1.Controllers
{
    public class HomeController : Controller
    {
        SykContext db;

        public HomeController(SykContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Students.ToList());
        }
        [HttpGet]
        public IActionResult Index1()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Addendum(Student student)
        {
            if (student.Name == null)
            {
                ViewBag.Mistake = "Имя не задано";
                return View("Index1");
            }
            else if (student.Surname == null)
            {
                ViewBag.Mistake = "Фамилия не задана";
                return View("Index1");
            }
            else if (student.Patronymic == null)
            {
                ViewBag.Mistake = "Отчество не задано";
                return View("Index1");
            }
            else if (student.Date == null)
            {
                ViewBag.Mistake = "Дата не задана";
                return View("Index1");
            }
            else if (student.State == null)
            {
                ViewBag.Mistake = "Статус не задан";
                return View("Index1");
            }
            db.Students.Add(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Editing(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var student = await db.Students.FirstOrDefaultAsync(p => p.id == id);
            ViewBag.Student = student;
            ViewBag.StudentId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Editing(Student student)
        {
            try
            {
                db.Students.Update(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Mistake = "Ошибка";
                return View();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Student student = await db.Students.FirstOrDefaultAsync(p => p.id == id);
                if (student != null)
                {
                    db.Students.Remove(student);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
    
}
