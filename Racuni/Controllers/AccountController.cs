using Microsoft.AspNetCore.Mvc;
using Racuni.Interfaces;
using Racuni.Models;

namespace Racuni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountHeader>))]
        public IActionResult GetAccounts()
        {
            var accounts = _accountRepository.GetAccounts();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(accounts);
        }

        [HttpGet("{invoiceNumber}")]
        [ProducesResponseType(200, Type = typeof(AccountHeader))]
        [ProducesResponseType(400)]
        public IActionResult GetAccountByInvoiceNumber(int invoiceNumber)
        {
            if (!_accountRepository.AccountExists(invoiceNumber))
            {
                return NotFound();
            }

            var account = _accountRepository.GetAccountByInvoiceNumber(invoiceNumber);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(account);
        }
    }
}
