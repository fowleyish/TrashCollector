using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrashCollector.Data;
using TrashCollector.Models;
using TrashCollector.ViewModels;

namespace TrashCollector.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        // GET: First time setup for new customer
        public ActionResult FirstTimeSetup()
        {
            var FirstTimeSetupViewModel = new FirstTimeSetupViewModel();
            var daysList = _context.Days.ToList();
            FirstTimeSetupViewModel.DaysOfWeek = new SelectList(daysList, "DayId", "DayOfWeek");
            return View(FirstTimeSetupViewModel);
        }

        // POST: Create customer in Customers table
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FirstTimeSetup(FirstTimeSetupViewModel data)
        {
            var newAddress = new Address();
            newAddress.Street = data.Street;
            newAddress.City = data.City;
            newAddress.State = data.State;
            newAddress.Zip = data.Zip;
            _context.Addresses.Add(newAddress);
            _context.SaveChanges();
            var newAccount = new Account();
            newAccount.Balance = 0;
            _context.Accounts.Add(newAccount);
            _context.SaveChanges();
            var newCustomer = new Customer();
            newCustomer.Id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            newCustomer.FirstName = data.FirstName;
            newCustomer.LastName = data.LastName;
            newCustomer.DayId = _context.Days.Where(x => x.DayId == int.Parse(data.DayOfWeek)).Select(x => x.DayId).FirstOrDefault();
            newCustomer.AddressId = newAddress.AddressId;
            newCustomer.AccountId = newAccount.AccountId;
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Customer
        public ActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(x => x.Id == userId).SingleOrDefault();
            try
            {
                var customer = _context.Customers.Where(x => x.Id == user.Id).SingleOrDefault();
                var customerDashBoardViewModel = new CustomerDashboardViewModel();
                customerDashBoardViewModel.Customer = customer;
                var daysList = _context.Days.ToList();
                customerDashBoardViewModel.DaysOfWeek = new SelectList(daysList, "DayId", "DayOfWeek");
                customerDashBoardViewModel.DayOfWeek = _context.Days.Where(x => x.DayId == customer.DayId).Select(x => x.DayOfWeek).FirstOrDefault();
                return View(customerDashBoardViewModel);
            }
            catch
            {
                return RedirectToAction(nameof(FirstTimeSetup));
            }
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}