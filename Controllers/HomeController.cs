using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeltaPlan2100API.Models;
using DeltaPlan2100API.Models.TempModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace DeltaPlan2100API.Controllers
{
    public class HomeController : Controller
    {
        private readonly delta_plan_2100_appContext db = new delta_plan_2100_appContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult API()
        {
            return View();
        }

        public IActionResult UploadImageData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadImageData(TblAppImageDataTemp tblAppImageDataTemp)
        {
            if (ModelState.IsValid)
            {
                TblAppImageData exist = db.TblAppImageData.Where(w => w.MenuId == tblAppImageDataTemp.MenuId && w.MenuLevel == tblAppImageDataTemp.MenuLevel).FirstOrDefault();
                int count = db.TblAppImageData.Count(), x = 0;

                if (exist == null)
                {
                    TblAppImageData imageData = new TblAppImageData
                    {
                        ImageDataId = count + 1,
                        MenuId = tblAppImageDataTemp.MenuId,
                        MenuLevel = tblAppImageDataTemp.MenuLevel,
                        //ImageTitle = tblAppImageDataTemp.ImageTitle.ToCharArray(),
                        ImageBlob = Convert.FromBase64String(ConvertImageToBase64(tblAppImageDataTemp.Image))
                    };

                    if (imageData != null)
                    {
                        db.TblAppImageData.Add(imageData);
                        x = db.SaveChanges();

                        ViewBag.Result = x > 0 ? "success" : "failed";
                    }
                    else
                    {
                        ViewBag.Result = "failed";
                    }
                }
                else
                {
                    exist.ImageBlob = Convert.FromBase64String(ConvertImageToBase64(tblAppImageDataTemp.Image));

                    db.Entry(exist).State = EntityState.Modified;
                    x = db.SaveChanges();

                    ViewBag.Result = x > 0 ? "updated" : "update failed";
                }
            }

            return View();
        }

        public string ConvertImageToBase64(IFormFile file)
        {
            string base64String = string.Empty;

            if (file.Length > 0)
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                //return fileBytes;
                base64String = Convert.ToBase64String(fileBytes);
            }

            return base64String;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
