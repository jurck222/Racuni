using Microsoft.AspNetCore.Mvc;
using Racuni.Interfaces;
using Racuni.Models;

namespace Racuni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InvoicesHeader>))]
        [ProducesResponseType(400)]
        public IActionResult GetInvoices()
        {
            var invoices = _invoiceRepository.GetInvoices();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(invoices);
        }

        [HttpGet("{invoiceNumber}")]
        [ProducesResponseType(200, Type = typeof(InvoicesHeader))]
        [ProducesResponseType(400)]
        public IActionResult GetInvoiceByInvoiceNumber(int invoiceNumber)
        {
            if (!_invoiceRepository.InvoiceExists(invoiceNumber))
            {
                return NotFound();
            }

            var invoice = _invoiceRepository.GetInvoiceByInvoiceNumber(invoiceNumber);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(invoice);
        }

        [HttpDelete("{invoiceNumber}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteInvoice(int invoiceNumber)
        {
            if (!_invoiceRepository.InvoiceExists(invoiceNumber))
            {
                return NotFound();
            }

            var invoice = _invoiceRepository.GetInvoiceByInvoiceNumber(invoiceNumber);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_invoiceRepository.DeleteInvoice(invoice))
            {
                return StatusCode(500, "Something went wrong when deleting the invoice");
            }

            return Ok("Invoice successfully deleted");
        }
    }
}
