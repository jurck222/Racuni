using Racuni.Data;
using Racuni.Models;

namespace Racuni
{
    public class Seed
    {
        private readonly InvoiceDbContext dataContext;
        public Seed(InvoiceDbContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.InvoiceHeaders.Any())
            {
                var invoices = new List<InvoicesHeader>()
                {
                    new InvoicesHeader()
                    {
                        InvoiceNumber = 245646,
                        CreatedAt = new DateTime(2024, 8, 7, 14, 32, 0, DateTimeKind.Utc),
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
                                ItemName = "RA - JAVNA RABA - PAVŠAL",
                                Quantity = 20,
                                Price = 3.77,
                            },

                            new InvoiceItem()
                            {
                                ItemName = "TV - ZASEBNA RABA - PRAVNA OSEBA",
                                Quantity = 10,
                                Price = 12.75,
                            },
                        }
                    },

                    new InvoicesHeader()
                    {
                        InvoiceNumber = 231324,
                        CreatedAt = new DateTime(2024, 9, 7, 12, 30, 5, DateTimeKind.Utc),
                        Recipient = "Jože Neznani",
                        Address = "Večna pot 113",
                        City = "1000 Ljubljana",
                        InvoiceItems = new List<InvoiceItem>()
                        {
                            new InvoiceItem()
                            {
                                ItemName = "RA - ZASEBNA RABA - PRAVNA OSEBA",
                                Quantity = 4,
                                Price = 12.75,
                            },

                            new InvoiceItem()
                            {
                                ItemName = "RA - JAVNA RABA - PAVŠAL",
                                Quantity = 20,
                                Price = 3.77,
                            },

                            new InvoiceItem()
                            {
                                ItemName = "TV - ZASEBNA RABA - PRAVNA OSEBA",
                                Quantity = 10,
                                Price = 12.75,
                            },
                        }
                    }
                };

                dataContext.InvoiceHeaders.AddRange(invoices);
                dataContext.SaveChanges();
            }
        }
    }
}
