using CRUDApplication.DbEntities;
using CRUDApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUDApplication.Controllers
{
    public class CustomerController : Controller
    {
        private CRUDContext context;

        public CustomerController(CRUDContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<CustomerViewModel> model = context.Set<Customer>().ToList().Select(s => new CustomerViewModel
            {
                Id = s.Id,
                Name = $"{s.FirstName} {s.LastName}",
                MobileNo = s.MobileNo,
                Email = s.Email
            });
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult AddEditCustomer(long? id)
        {
            CustomerViewModel model = new CustomerViewModel();
            if (id.HasValue)
            {
                Customer customer = context.Set<Customer>().SingleOrDefault(c => c.Id == id.Value);
                if (customer != null)
                {
                    model.Id = customer.Id;
                    model.FirstName = customer.FirstName;
                    model.LastName = customer.LastName;
                    model.MobileNo = customer.MobileNo;
                    model.Email = customer.Email;
                }
            }
            return PartialView("~/Views/Customer/_AddEditCustomer.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddEditCustomer(long? id, CustomerViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isNew = !id.HasValue;
                    Customer customer = isNew ? new Customer
                    {
                        AddedDate = DateTime.UtcNow
                    } : context.Set<Customer>().SingleOrDefault(s => s.Id == id.Value);
                    customer.FirstName = model.FirstName;
                    customer.LastName = model.LastName;
                    customer.MobileNo = model.MobileNo;
                    customer.Email = model.Email;
                    customer.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    customer.ModifiedDate = DateTime.UtcNow;
                    if (isNew)
                    {
                        context.Add(customer);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteCustomer(long id)
        {
            Customer customer = context.Set<Customer>().SingleOrDefault(c => c.Id == id);
            CustomerViewModel model = new CustomerViewModel
            {
                Name = $"{customer.FirstName} {customer.LastName}"
            };
            return PartialView("~/Views/Customer/_DeleteCustomer.cshtml", model);
        }
        [HttpPost]
        public IActionResult DeleteCustomer(long id, FormCollection form)
        {
            Customer customer = context.Set<Customer>().SingleOrDefault(c => c.Id == id);
            context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
