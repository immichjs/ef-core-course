using AppConsoleEF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace AppConsoleEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new Data.ApplicationContext();

            bool migrationsPendingExists = db.Database.GetPendingMigrations().Any();

            if (migrationsPendingExists)
            {
                // Logic
            }

            //InsertData();
            //InsertManyData();
            //ConsultData();
            //CreateOrder();
            //ConsultOrderAdvanceLoading();
            //UpdateData();
            RemoveData();

		}

		private static void InsertData()
		{
            Product product = new Product
            {
                Description = "Notebook",
                CodeBar = "1234567891011",
                Price = 1990.99m,
                ProductType = Enums.ProductType.MercadoriaParaRevenda,
                Status = true
            };

            using var db = new Data.ApplicationContext();

            db.Set<Product>().Add(product);

            var registers = db.SaveChanges();
            Console.WriteLine($"Total registros: {registers}");
		}

        private static void InsertManyData()
        {
            Customer[] customers = new[]
            {
                new Customer
                {
                    Name = "Michel França",
                    CEP = "18072070",
                    City = "Sorocaba",
                    UF = "SP",
                    Phone = "997905964"
                },
				new Customer
				{
					Name = "Laura Armendane",
					CEP = "18072070",
					City = "Sorocaba",
					UF = "SP",
					Phone = "996662498"
				},
			};

			using var db = new Data.ApplicationContext();

            db.Set<Customer>().AddRange(customers);

            var registers = db.SaveChanges();
            Console.WriteLine($"Total registros(s): {registers}");
		}

        private static void ConsultData()
        {
            using var db = new Data.ApplicationContext();
            //var consultSqlSyntax = (from c in db.Customers where c.Id > 0 select c).ToList();
            var consultForMethods = db.Customers
                .Where(_ => _.Id > 0)
                .OrderBy(_ => _.Id)
                .ToList();

            foreach (var customer in consultForMethods)
            {
                Console.WriteLine($"Consultando Cliente: {customer.Id}");
                //db.Customers.Find(customer.Id);
                db.Customers.FirstOrDefault();
            }
        }

        private static void CreateOrder()
        {
            using var db = new Data.ApplicationContext();

            var customer = db.Customers.FirstOrDefault();
            var product = db.Products.FirstOrDefault();

            var order = new Order
            {
                CustomerId = customer.Id,
                CreatedAt = DateTime.Now,
                FinishedAt = DateTime.Now,
                Observation = "Pedido teste",
                Status = Enums.OrderStatus.Analise,
                FreightType = Enums.FreightType.SemFrete,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = product.Id,
                        Discount = 0,
                        Quantity = 1,
                        Price = 10
                    }
                }
            };

            db.Orders.Add(order);
            db.SaveChanges();
        }

        private static void ConsultOrderAdvanceLoading()
        {
            using var db = new Data.ApplicationContext();
            var orders = db.Orders
                .Include(_ => _.Items)
                    .ThenInclude(_ => _.Product )
                .ToList();

            Console.WriteLine(orders.Count);
        }

        private static void UpdateData()
        {
            using var db = new Data.ApplicationContext();
            var customer = db.Customers.Find(1);

            customer.Name = "Cliente alterado #2";
            db.SaveChanges();
        }

        private static void RemoveData()
        {
            using var db = new Data.ApplicationContext();
            var customer = db.Customers.Find(2);

            if (customer != null)
            {
				db.Customers.Remove(customer);
				db.SaveChanges();
                return;
			}

            Console.WriteLine("Não foi possível encontrar o registro.");
        }
	}
}