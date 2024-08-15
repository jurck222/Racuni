using Microsoft.AspNetCore.Mvc;
using Moq;
using Racuni.Controllers;
using Racuni.Interfaces;
using Racuni.Models;
namespace RacuniTests.Controllers
{
    public class InvoiceControllerTest
    {
        private readonly InvoiceController _controller;
        private readonly Mock<IInvoiceRepository> _mockInvoiceRepository;

        public InvoiceControllerTest()
        {
            _mockInvoiceRepository = new Mock<IInvoiceRepository>();
            _controller = new InvoiceController(_mockInvoiceRepository.Object);
        }

        [Fact]
        public void GetInvoices_Returns_BadRequest_WhenModelStateIsInvalid()
        {
            _mockInvoiceRepository.Setup(repo => repo.GetInvoices())
                           .Returns(TestData.invoices);

            var result = _controller.GetInvoices();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(TestData.invoices, okResult.Value);
            _mockInvoiceRepository.Verify(repo => repo.GetInvoices(), Times.Once);
        }

        [Fact]
        public void GetInvoices_Returns_OkResult()
        {
            _mockInvoiceRepository.Setup(repo => repo.GetInvoices())
                           .Returns(TestData.invoices);

            _controller.ModelState.AddModelError("Error", "ModelState is invalid");

            var result = _controller.GetInvoices();

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
            _mockInvoiceRepository.Verify(repo => repo.GetInvoices(), Times.Once);
        }

        [Fact]
        public void DeleteInvoice_Returns_Ok_WhenDeletionIsSuccessful()
        {
            int invoiceNumber = 12345;
            _mockInvoiceRepository.Setup(repo => repo.InvoiceExists(invoiceNumber)).Returns(true);
            _mockInvoiceRepository.Setup(repo => repo.GetInvoiceByInvoiceNumber(invoiceNumber)).Returns(TestData.invoice);
            _mockInvoiceRepository.Setup(repo => repo.DeleteInvoice(TestData.invoice)).Returns(true);

            var result = _controller.DeleteInvoice(invoiceNumber);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Invoice successfully deleted", okResult.Value);
            _mockInvoiceRepository.Verify(repo => repo.InvoiceExists(invoiceNumber), Times.Once);
            _mockInvoiceRepository.Verify(repo => repo.GetInvoiceByInvoiceNumber(invoiceNumber), Times.Once);
            _mockInvoiceRepository.Verify(repo => repo.DeleteInvoice(TestData.invoice), Times.Once);
        }

        [Fact]
        public void DeleteInvoice_Returns_StatusCode500_WhenDeletionFails()
        {
            int invoiceNumber = 12345;
            _mockInvoiceRepository.Setup(repo => repo.InvoiceExists(invoiceNumber)).Returns(true);
            _mockInvoiceRepository.Setup(repo => repo.GetInvoiceByInvoiceNumber(invoiceNumber)).Returns(TestData.invoice);
            _mockInvoiceRepository.Setup(repo => repo.DeleteInvoice(TestData.invoice)).Returns(false);

            var result = _controller.DeleteInvoice(invoiceNumber);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Something went wrong when deleting the invoice", statusCodeResult.Value);
            _mockInvoiceRepository.Verify(repo => repo.InvoiceExists(invoiceNumber), Times.Once);
            _mockInvoiceRepository.Verify(repo => repo.GetInvoiceByInvoiceNumber(invoiceNumber), Times.Once);
            _mockInvoiceRepository.Verify(repo => repo.DeleteInvoice(TestData.invoice), Times.Once);
        }

        [Fact]
        public void DeleteInvoice_Returns_NotFound_WhenInvoiceIsNotFound()
        {
            int invoiceNumber = 12345;
            _mockInvoiceRepository.Setup(repo => repo.InvoiceExists(invoiceNumber)).Returns(false);

            var result = _controller.DeleteInvoice(invoiceNumber);

            Assert.IsType<NotFoundResult>(result);
            _mockInvoiceRepository.Verify(repo => repo.InvoiceExists(invoiceNumber), Times.Once);
        }

        [Fact]
        public void DeleteInvoice_Returns_BadRequest_WhenModelStateIsInvalid()
        {
            int invoiceNumber = 12345;
            _mockInvoiceRepository.Setup(repo => repo.InvoiceExists(invoiceNumber)).Returns(true);
            _mockInvoiceRepository.Setup(repo => repo.GetInvoiceByInvoiceNumber(invoiceNumber))
                                  .Returns(new InvoicesHeader());

            _controller.ModelState.AddModelError("Error", "ModelState is invalid");

            var result = _controller.DeleteInvoice(invoiceNumber);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
            _mockInvoiceRepository.Verify(repo => repo.InvoiceExists(invoiceNumber), Times.Once);
            _mockInvoiceRepository.Verify(repo => repo.GetInvoiceByInvoiceNumber(invoiceNumber), Times.Once);
        }

        [Fact]
        public void GetInvoiceByInvoiceNumber_Returns_BadRequest_WhenModelStateIsInvalid()
        {
            int invoiceNumber = 12345;
            _mockInvoiceRepository.Setup(repo => repo.InvoiceExists(invoiceNumber)).Returns(true);
            _mockInvoiceRepository.Setup(repo => repo.GetInvoiceByInvoiceNumber(invoiceNumber))
                                  .Returns(new InvoicesHeader());

            _controller.ModelState.AddModelError("Error", "ModelState is invalid");

            var result = _controller.GetInvoiceByInvoiceNumber(invoiceNumber);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
            _mockInvoiceRepository.Verify(repo => repo.InvoiceExists(invoiceNumber), Times.Once);
            _mockInvoiceRepository.Verify(repo => repo.GetInvoiceByInvoiceNumber(invoiceNumber), Times.Once);
        }

        [Fact]
        public void GetInvoiceByInvoiceNumber_Returns_OkResult()
        {
            int invoiceNumber = 12345;
            _mockInvoiceRepository.Setup(repo => repo.InvoiceExists(invoiceNumber)).Returns(true);
            _mockInvoiceRepository.Setup(repo => repo.GetInvoiceByInvoiceNumber(invoiceNumber))
                                  .Returns(TestData.invoice);

            var result = _controller.GetInvoiceByInvoiceNumber(invoiceNumber);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(TestData.invoice, okResult.Value);
            _mockInvoiceRepository.Verify(repo => repo.InvoiceExists(invoiceNumber), Times.Once);
            _mockInvoiceRepository.Verify(repo => repo.GetInvoiceByInvoiceNumber(invoiceNumber), Times.Once);
        }
    }
}

