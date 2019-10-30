﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace Customers
{
    public class CustomerApiController : Controller
    {
        private readonly CustomerRepository customerRepository;

        public CustomerApiController (CustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        [HttpGet]
        public IActionResult Index()
        {
            var customers = this.customerRepository.GetCustomers();
            var customerListResponse = new CustomerListResponse(customers);
            return Json(customerListResponse);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Customer(string id)
        {
            var customer = this.customerRepository.GetCustomer(id);
            return Json(customer);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] string id, [FromBody] CustomerFormModel customerFormModel)
        {
            var customer = this.customerRepository.GetCustomer(id);
            customer = customerFormModel.Map(customer);
            customer = this.customerRepository.Update(customer);
            return Json(customer);
        }
    }
}
