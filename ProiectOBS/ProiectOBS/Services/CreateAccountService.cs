﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProiectOBS.Models;
using ProiectOBS.Data;
using ProiectOBS.Models;

namespace ProiectOBS.Services
{
    public class CreateAccountService
    {
        private readonly ProiectOBSDbContext _dbContext;

        public CreateAccountService(ProiectOBSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAccountAsync(Client client, Address address, Card card)
        {
            try
            {
                // Check if the email is already registered
                if (await _dbContext.Client.AnyAsync(a => a.Email == client.Email))
                {
                    return false;
                }

                // Create a new Account entity
                var account = new Client
                {
                    Name = client.Name,
                    Surname = client.Surname,
                    Email = client.Email,
                    Password = client.Password,
                    PhoneNumber = client.PhoneNumber,
                    Address = new Address
                    {
                        //Country = address.Country,
                        //County = address.County,
                        //City = address.City,
                        //Postalcode = address.Postalcode,
                        //Block = address.Block,
                        //Floor = address.Floor,
                        //Apartment = address.Apartment,
                        //Staircase = address.Staircase,
                        Street = address.Street,
                    },
                    Card = new Card
                    {
                        Number = card.Number,
                        Date = card.Date,
                        CVV = card.CVV,
                    },
                };

                // Add the new account to the database
                _dbContext.Client.Add(account);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                // Log the exception and return false
                return false;
            }
        }

        //private async Task<string> SaveProfileImageAsync(string profileImage)
        //{
        //    if (!string.IsNullOrEmpty(profileImage))
        //    {
        //        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profileImage);
        //        var filePath = Path.Combine("wwwroot/images/profiles", fileName);

        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(profileImage)))
        //            {
        //                await stream.CopyToAsync(fileStream);
        //            }
        //        }

        //        return fileName;
        ////    }

        //    return null;
        //}


    }
}
//using Microsoft.AspNetCore.Mvc;
//using ProiectOBS.Data;
//using ProiectOBS.Models;

//namespace ProiectOBS.Controllers
//{
//    public class CreateAccount : Controller
//    {
//        private readonly ProiectOBSDbContext _context;

//        public CreateAccount(ProiectOBSDbContext context)
//        {
//            _context = context;
//        }

//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(Client account)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Client.Add(account);
//                _context.SaveChanges();
//                return RedirectToAction("Index", "Home");
//            }
//            return View(account);
//        }
//    }
//}