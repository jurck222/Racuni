namespace Racuni.Models
{
    public class InvoicesHeader
    {
        public int Id { get; set; }
        public long InvoiceNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Recipient { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
