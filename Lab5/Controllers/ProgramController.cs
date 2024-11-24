using LabsLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Lab5.Controllers
{
    public class ProgramController : Controller
    {
        private readonly LabRunner _labRunner;

        public ProgramController()
        {
            _labRunner = new LabRunner();
        }

        // Сторінка для Lab1
        [Authorize]
        [HttpGet]
        public IActionResult RunLab1()
        {
            return View("~/Views/Labs/RunLab1.cshtml");
        }
        [Authorize]
        [HttpPost]
        public IActionResult RunLab1(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    // Copy the file's data to memory
                    file.CopyTo(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin); // Reset stream position

                    // Process the file in memory
                    var result = _labRunner.RunLab1(memoryStream);

                    // Display result
                    ViewData["Result"] = result;
                }
            }

            return View("~/Views/Labs/RunLab1.cshtml");
        }
        [Authorize]
        [HttpGet]
        public IActionResult RunLab2()
        {
            return View("~/Views/Labs/RunLab2.cshtml");
        }
        [Authorize]
        [HttpPost]
        public IActionResult RunLab2(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    // Копіюємо дані файлу в пам'ять
                    file.CopyTo(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin); // Повертаємо позицію потоку на початок

                    // Викликаємо метод обробки файлу у пам'яті
                    var result = _labRunner.RunLab2(memoryStream);

                    // Передаємо результат на представлення
                    ViewData["Result"] = result;
                }
            }

            return View("~/Views/Labs/RunLab2.cshtml");
        }
        [Authorize]
        [HttpGet]
        public IActionResult RunLab3()
        {
            return View("~/Views/Labs/RunLab3.cshtml");
        }
        [Authorize]
        [HttpPost]
        public IActionResult RunLab3(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    var result = _labRunner.RunLab3(memoryStream);
                    ViewData["Result"] = result;
                }
            }

            return View("~/Views/Labs/RunLab3.cshtml");
        }
    }
}
