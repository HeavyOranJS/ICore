using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using moit_lab.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace moit_lab.Controllers
{
    [Authorize(Roles = "admin")]
    public class HumanResourcesController : Controller
    {
        private readonly HumanResourcesContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public HumanResourcesController(HumanResourcesContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [AllowAnonymous]
        public IActionResult StaffList() => View(_context.StaffMember.ToList());

        [AllowAnonymous]
        public async Task<IActionResult> More(long staffMemberId)
        {

            // get staffMember
            HumanResourcesModel staffMember = await _context.StaffMember.FindAsync(staffMemberId);

            if (staffMember != null)
            {
                // get user's roles
                HumanResourcesModel model = new HumanResourcesModel
                {
                    Id = staffMember.Id,
                    FamilyName = staffMember.FamilyName,
                    Name = staffMember.Name,
                    Surname = staffMember.Surname,
                    BirthDate = staffMember.BirthDate,
                    Image = staffMember.Image
                };

                return View(model);
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(long staffMemberId)
        {
            // get staffMember
            HumanResourcesModel staffMember = await _context.StaffMember.FindAsync(staffMemberId);

            if (staffMember != null)
            {
                // get user's roles
                HumanResourcesModel model = new HumanResourcesModel
                {
                    Id = staffMember.Id,
                    FamilyName = staffMember.FamilyName,
                    Name = staffMember.Name,
                    Surname = staffMember.Surname,
                    BirthDate = staffMember.BirthDate,
                    Image = staffMember.Image
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, string familyName, string name, string surname, IFormFile image, DateTime birthDate)
        {
            //check if ID have value on form
            // get staffMember
            HumanResourcesModel staffMember = await _context.StaffMember.FindAsync(id);
            //get path to image
            string path = await UploadImg(image);
            if (staffMember != null)
            {
                // update all staff's fields
                staffMember.FamilyName = familyName;
                staffMember.Name = name;
                staffMember.Surname = surname;
                staffMember.BirthDate = birthDate;
                staffMember.Image = path;

                //update and save staffmember in database
                _context.StaffMember.Update(staffMember);
                await _context.SaveChangesAsync();

                return RedirectToAction("StaffList");
            }

            return NotFound();
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(long id, string familyName, string name, string surname, IFormFile image, DateTime birthDate)
        {
            string path = await UploadImg(image);
            HumanResourcesModel staffMember = new HumanResourcesModel
            {
                // fill all staff's fields
                FamilyName = familyName,
                Name = name,
                Surname = surname,
                BirthDate = birthDate,
                Image = path
            };

            //create and save staffmember in database
            _context.Add(staffMember);
            //_context.StaffMember.Update(staffMember);
            await _context.SaveChangesAsync();
            return RedirectToAction("StaffList");
        }


        private async Task<string>UploadImg(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = "/Files/" + uploadedFile.FileName;
                // save file to wwwroot/Files
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                return path;
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            HumanResourcesModel staffMember = await _context.StaffMember.FindAsync(id);
            if (staffMember != null)
            {
                _context.StaffMember.Remove(staffMember);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("StaffList");
        }
    }
}
