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
    public class EmployeeController : Controller
    {

        private ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        // GET: Redirect to new employee setup view on first signin
        public ActionResult NewEmployeeSetup()
        {
            var NewEmployeeSetupViewModel = new NewEmployeeSetupViewModel();
            return View(NewEmployeeSetupViewModel);
        }

        // POST: Create employee in Employees table
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewEmployeeSetup(NewEmployeeSetupViewModel data)
        {
            var newAddress = new Address();
            newAddress.Street = data.Street;
            newAddress.City = data.City;
            newAddress.State = data.State;
            newAddress.Zip = data.Zip;
            _context.Addresses.Add(newAddress);
            _context.SaveChanges();
            var newEmployee = new Employee();
            newEmployee.Id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            newEmployee.FirstName = data.FirstName;
            newEmployee.LastName = data.LastName;
            newEmployee.AddressId = newAddress.AddressId;
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Employee dashboard/index
        public ActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Employees.Where(x => x.Id == userId).SingleOrDefault();
            try
            {
                var employee = _context.Employees.Where(x => x.Id == user.Id).SingleOrDefault();
                var employeeDashboardViewModel = new EmployeeDashboardViewModel();
                string dayOfWeek = DateTime.Today.DayOfWeek.ToString();
                DateTime today = DateTime.Today;
                int todayId = _context.Days.Where(x => x.DayOfWeek == dayOfWeek).Select(x => x.DayId).FirstOrDefault();
                employeeDashboardViewModel.Employee = employee;
                employeeDashboardViewModel.Zip = _context.Addresses.Where(x => x.AddressId == employee.AddressId).Select(x => x.Zip).FirstOrDefault();
                if (_context.TodayPickups.Where(x => x.Date == DateTime.Today).FirstOrDefault() == null)
                {
                    BuildScheduleForToday(todayId);
                }
                employeeDashboardViewModel.Stops = _context.TodayPickups.Where(x => x.Date == DateTime.Today && x.Completed == false && x.Zip == employeeDashboardViewModel.Zip).ToList();
                return View(employeeDashboardViewModel); 
            }
            catch
            {
                return RedirectToAction(nameof(NewEmployeeSetup));
            }
        }

        // Helper method to create stop rows in TodayPickups
        private void BuildScheduleForToday(int todayId)
        {
            List<int> stopAddressIds = _context.Customers.Where(x => x.DayId == todayId || x.SpecialPickup == DateTime.Today && !( DateTime.Today < x.SuspendEnd && DateTime.Today > x.SuspendStart )).Select(x => x.AddressId).ToList();
            List<Address> stopAddresses = new List<Address>(); 
            foreach (var address in stopAddressIds)
            {
                stopAddresses.Add(_context.Addresses.Where(x => x.AddressId == address).FirstOrDefault());
            }
            foreach (var stop in stopAddresses)
            {
                TodayPickup todayPickup = new TodayPickup();
                todayPickup.Street = stop.Street;
                todayPickup.City = stop.City;
                todayPickup.State = stop.State;
                todayPickup.Zip = stop.Zip;
                todayPickup.Date = DateTime.Today;
                _context.TodayPickups.Add(todayPickup);
            }
            _context.SaveChanges();
        }


        public ActionResult CompleteStop(int id)
        {
            TodayPickup completedStop = _context.TodayPickups.Where(x => x.PickupId == id).FirstOrDefault();
            completedStop.Completed = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


















        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
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

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
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

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
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