using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Admin.Factories;
using Admin.Models;
using Admin.Services;

namespace Admin.Controllers
{
    [Route("transactions")]
    public class TransactionController : Controller
    {
        private readonly TransactionService transactionService;
        private readonly NavigationService navigationService;
        private readonly AuthorizationService authorizationService;

        public TransactionController(TransactionService transactionService, NavigationService navigationService, AuthorizationService authorizationService)
        {
            this.transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public async Task<IActionResult> Index()
        {
            var menu = await this.navigationService.GetMenuAsync();
            var user = await this.authorizationService.GetAuthorizedUserAsync();
            var transactions = await this.transactionService.GetTransactions();
            var urlFactory = new UrlFactory(Url);
            var transactionsModel = new TransactionsModel
            {
                Transactions = transactions,
                Menu = menu,
                User = user,
                UrlFactory = urlFactory
            };

            return View(transactionsModel);
        }

        [Route("{id}")]
        public async Task<IActionResult> CustomerTransactions(string id)
        {
            var menu = await this.navigationService.GetMenuAsync();
            var user = await this.authorizationService.GetAuthorizedUserAsync();
            var transactions = await this.transactionService.GetTransactionsForCustomer(id);
            var urlFactory = new UrlFactory(Url);
            var transactionsModel = new TransactionsModel
            {
                Transactions = transactions,
                Menu = menu,
                User = user,
                UrlFactory = urlFactory
            };

            return View("Index", transactionsModel);
        }
        
        [Route("t/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var menu = await this.navigationService.GetMenuAsync();
            var user = await this.authorizationService.GetAuthorizedUserAsync();
            var transaction = await this.transactionService.GetTransaction(id);
            var urlFactory = new UrlFactory(Url);
            
            var transactionDetails = new TransactionDetailsModel
            {
                Transaction = transaction,
                Menu = menu,
                User = user,
                UrlFactory = urlFactory
            };

            return View("Details", transactionDetails);
        }
        
        [HttpPost]
        [Route("t/{id}/capture")]
        public async Task<IActionResult> Capture([FromRoute] int id, [FromForm] CaptureModel capture)
        {
            await this.transactionService.CaptureTransaction(id, capture.AmountToCapture);
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [Route("t/{id}/reversal")]
        public async Task<IActionResult> Reverse([FromRoute] int id, [FromForm] ReverseModel reverse)
        {
            await this.transactionService.ReverseTransaction(id, reverse.AmountToReverse);
            return RedirectToAction("Details", new { id });
        }
    }
}
