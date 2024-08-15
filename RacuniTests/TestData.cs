using Racuni.Models;

namespace RacuniTests
{
    internal class TestData
    {
        public static List<InvoicesHeader> invoices = new List<InvoicesHeader>()
        {
            new InvoicesHeader()
            {
                InvoiceNumber = 245678,
                CreatedAt = new DateTime(2024, 6, 7, 14, 32, 0, DateTimeKind.Utc),
                ExpiresAt = new DateTime(2024, 7, 7, 14, 32, 0, DateTimeKind.Utc),
                Recipient = "Jure Pavlovič",
                Address = "Svetčeva Ulica 9",
                City = "1000 Ljubljana",
                InvoiceItems = new List<InvoiceItem>()
                {
                    new InvoiceItem()
                    {
                        ItemName = "TV - JAVNA RABA - PAVŠAL",
                        Quantity = 4,
                        Price = 12.75,
                    },
                    new InvoiceItem()
                    {
                        ItemName = "TV - ZASEBNA RABA - PRAVNA OSEBA",
                        Quantity = 10,
                        Price = 12.75,
                    },
                }
            },
        };

        public static InvoicesHeader invoice = new InvoicesHeader()
        {
            InvoiceNumber = 245678,
            CreatedAt = new DateTime(2024, 6, 7, 14, 32, 0, DateTimeKind.Utc),
            ExpiresAt = new DateTime(2024, 7, 7, 14, 32, 0, DateTimeKind.Utc),
            Recipient = "Jure Pavlovič",
            Address = "Svetčeva Ulica 9",
            City = "1000 Ljubljana",
            InvoiceItems = new List<InvoiceItem>()
            {
                new InvoiceItem()
                {
                    ItemName = "TV - JAVNA RABA - PAVŠAL",
                    Quantity = 4,
                    Price = 12.75,
                },
                new InvoiceItem()
                {
                    ItemName = "TV - ZASEBNA RABA - PRAVNA OSEBA",
                    Quantity = 10,
                    Price = 12.75,
                },
            }
        };

        public static InvoiceItem invoiceItem = new InvoiceItem()
        {
            ItemName = "TV - JAVNA RABA - PAVŠAL",
            Quantity = 4,
            Price = 12.75,
        };
    }
}
