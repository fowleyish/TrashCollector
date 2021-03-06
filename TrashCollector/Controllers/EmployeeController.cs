﻿using System;
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
            int customerAddressId = _context.Addresses.Where(x => x.Street == completedStop.Street && x.City == completedStop.City && x.State == completedStop.State && x.Zip == completedStop.Zip).Select(x => x.AddressId).FirstOrDefault();
            Customer customerToCharge = _context.Customers.Where(x => x.AddressId == customerAddressId).FirstOrDefault();
            Account accountToCharge = _context.Accounts.Where(x => x.AccountId == customerToCharge.AccountId).FirstOrDefault();
            accountToCharge.Balance += 10.25;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ViewDaySchedule(EmployeeDashboardViewModel data)
        {
            if (data.SelectedDay == DateTime.Today)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.Employees.Where(x => x.Id == userId).SingleOrDefault();
                var employee = _context.Employees.Where(x => x.Id == user.Id).SingleOrDefault();
                EmployeeFutureDayDashboardViewModel employeeFutureDayDashboardViewModel = new EmployeeFutureDayDashboardViewModel();
                employeeFutureDayDashboardViewModel.Employee = employee;
                employeeFutureDayDashboardViewModel.SelectedDay = data.SelectedDay;
                employeeFutureDayDashboardViewModel.Zip = _context.Addresses.Where(x => x.AddressId == employee.AddressId).Select(x => x.Zip).FirstOrDefault();
                string dayOfWeek = data.SelectedDay.DayOfWeek.ToString();
                int selectedDayId = _context.Days.Where(x => x.DayOfWeek == dayOfWeek).Select(x => x.DayId).FirstOrDefault();
                List<int> customerAddressIds = _context.Customers.Where(x => x.DayId == selectedDayId).Select(x => x.AddressId).ToList();
                List<Address> stopAddressesForDay = new List<Address>();
                foreach (var stop in customerAddressIds)
                {
                    stopAddressesForDay.Add(_context.Addresses.Where(x => stop == x.AddressId && x.Zip == employeeFutureDayDashboardViewModel.Zip).FirstOrDefault());
                }
                employeeFutureDayDashboardViewModel.Stops = stopAddressesForDay;
                return View(employeeFutureDayDashboardViewModel);
            }
        }



    }
}