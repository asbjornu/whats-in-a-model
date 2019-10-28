using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Admin.Models;
using Admin.Services;
using Admin.Factories;

namespace Admin.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly CustomerService customerService;
        private readonly TransactionService transactionService;
        private readonly NavigationService navigationService;
        private readonly AuthorizationService authorizationService;

        public CustomerController(CustomerService customerService, TransactionService transactionService, NavigationService navigationService, AuthorizationService authorizationService)
        {
            this.customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            this.transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public async Task<IActionResult> Index()
        {
            var menu = await this.navigationService.GetMenuAsync();
            var user = await this.authorizationService.GetAuthorizedUserAsync();
            var customers = await this.customerService.GetCustomers();
            var urlFactory = new UrlFactory(Url);

            var customersModel = new CustomersModel
            {
                Customers = customers,
                Menu = menu,
                User = user,
                UrlFactory = urlFactory
            };

            return View(customersModel);
        }

        [Route("{id}")]
        public async Task<IActionResult> Customer(string id)
        {
            var menu = await this.navigationService.GetMenuAsync();
            var user = await this.authorizationService.GetAuthorizedUserAsync();
            var customer = await this.customerService.GetCustomer(id);
            var urlFactory = new UrlFactory(Url);
            var transactions = await this.transactionService.GetTransactionsForCustomer(id);
            
            customer.Menu = menu;
            customer.User = user;
            customer.UrlFactory = urlFactory;
            customer.Transactions = transactions;

            return View(customer);
        }

        [Route("{id}/edit")]
        public async Task<IActionResult> Edit(string id)
        {
            return await Customer(id);
        }

        [HttpPost]
        [Route("{id}/update")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromForm] CustomerModel customer)
        {
            customer.Id = id;
            await this.customerService.UpdateCustomer(customer);
            return RedirectToAction("Customer", new { Id = id });
        }
    }
}
