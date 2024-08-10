using Microsoft.AspNetCore.Mvc;
using Racuni.Interfaces;

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
        public IActionResult DeleteInvoice(int itemId)
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
                ModelState.AddModelError("", "Something went wrong when deleting the invoice item");
            }

            return Ok("Invoice item successfully deleted");
        }
    }
}
