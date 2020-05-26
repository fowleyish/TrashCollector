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
                customerDashBoardViewModel.Street = _context.Addresses.Where(x => x.AddressId == customer.AddressId).Select(x => x.Street).FirstOrDefault();
                customerDashBoardViewModel.City = _context.Addresses.Where(x => x.AddressId == customer.AddressId).Select(x => x.City).FirstOrDefault();
                customerDashBoardViewModel.State = _context.Addresses.Where(x => x.AddressId == customer.AddressId).Select(x => x.State).FirstOrDefault();
                customerDashBoardViewModel.Zip = _context.Addresses.Where(x => x.AddressId == customer.AddressId).Select(x => x.Zip).FirstOrDefault();
                customerDashBoardViewModel.AccountBalance = _context.Accounts.Where(x => x.AccountId == customer.AccountId).Select(x => x.Balance).FirstOrDefault();
                customerDashBoardViewModel.AccountBalanceFormatted = customerDashBoardViewModel.AccountBalance.ToString("C2");
                return View(customerDashBoardViewModel);
            }
            catch
            {
                return RedirectToAction(nameof(FirstTimeSetup));
            }
        }

        // GET: UpdateContactInfo view
        public ActionResult UpdateContactInfo()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(x => x.Id == userId).SingleOrDefault();
            var userToUpdate = new FirstTimeSetupViewModel();
            userToUpdate.FirstName = customer.FirstName;
            userToUpdate.LastName = customer.LastName;
            userToUpdate.Street = _context.Addresses.Where(x => x.AddressId == customer.AddressId).Select(x => x.Street).SingleOrDefault();
            userToUpdate.City = _context.Addresses.Where(x => x.AddressId == customer.AddressId).Select(x => x.City).SingleOrDefault();
            userToUpdate.State = _context.Addresses.Where(x => x.AddressId == customer.AddressId).Select(x => x.State).SingleOrDefault();
            userToUpdate.Zip = _context.Addresses.Where(x => x.AddressId == customer.AddressId).Select(x => x.Zip).SingleOrDefault();
            return View(userToUpdate);
        }

        // POST: UpdateContactInfo from input
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateContactInfo(FirstTimeSetupViewModel userToUpdate)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(x => x.Id == userId).SingleOrDefault();
            var customersAddress = _context.Addresses.Where(x => x.AddressId == customer.AddressId).SingleOrDefault();
            customer.FirstName = userToUpdate.FirstName;
            customer.LastName = userToUpdate.LastName;
            customersAddress.Street = userToUpdate.Street;
            customersAddress.City = userToUpdate.City;
            customersAddress.State = userToUpdate.State;
            customersAddress.Zip = userToUpdate.Zip;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDay(CustomerDashboardViewModel thisCustomer)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(x => x.Id == userId).SingleOrDefault();
            var customersDayId = _context.Days.Where(x => x.DayOfWeek == thisCustomer.SelectedDay).Select(x => x.DayId).SingleOrDefault();
            customer.DayId = customersDayId;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // POST: Special pickup date
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestSpecialPickupDate(CustomerDashboardViewModel thisCustomer)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(x => x.Id == userId).SingleOrDefault();
            customer.SpecialPickup = thisCustomer.SpecialPickup;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // POST: Suspend Services
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuspendServices(CustomerDashboardViewModel thisCustomer)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(x => x.Id == userId).SingleOrDefault();
            customer.SuspendStart = thisCustomer.SuspendStart;
            customer.SuspendEnd = thisCustomer.SuspendEnd;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
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