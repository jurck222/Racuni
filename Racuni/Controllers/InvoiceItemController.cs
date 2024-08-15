using Microsoft.AspNetCore.Mvc;
using Racuni.Interfaces;
using Racuni.Models;

namespace Racuni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceItemController : Controller
    {
        private readonly IInvoiceItemRepository _invoiceItemRepository;

        public InvoiceItemController(IInvoiceItemRepository invoiceItemRepository)
        {
            _invoiceItemRepository = invoiceItemRepository;
        }

        [HttpDelete("{itemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteInvoiceItem(int itemId)
        {
            if (!_invoiceItemRepository.InvoiceItemExists(itemId))
            {
                return NotFound();
            }

            var item = _invoiceItemRepository.GetInvoiceItemById(itemId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_invoiceItemRepository.DeleteInvoiceItem(item))
            {
                return StatusCode(500, "Something went wrong when deleting the invoice item");
            }

            return Ok("Invoice item successfully deleted");
        }

        [HttpPost("{invoiceId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult AddInvoiceItem(int invoiceId, [FromBody] InvoiceItem item)
        {
            if (item == null)
            {
                return BadRequest("Invoice item is null.");
            }

            var success = _invoiceItemRepository.AddInvoiceItem(invoiceId, item);

            if (success)
            {
                return Ok("Invoice item added successfully.");
            }
            else
            {
                return NotFound("Invoice header not found.");
            }
        }
    }
}
