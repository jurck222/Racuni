namespace Racuni.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
