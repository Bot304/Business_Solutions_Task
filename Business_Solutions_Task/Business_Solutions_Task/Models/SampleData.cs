using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business_Solutions_Task.Models
{
    public class SampleData
    {
        public static void Initialize(ModelContext context)
        {
            if (!context.Providers.Any())
            {
                context.Providers.AddRange(
                    new Provider
                    {
                        Name = "Поставщик 1"
                    },
                    new Provider
                    {
                        Name = "Поставщик 2"
                    },
                    new Provider
                    {
                        Name = "Поставщик 3"
                    }
                );
                context.SaveChanges();
            }
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(
                    new Order
                    {
                        Namber = "001",
                        Date = DateTime.Now,
                        ProviderId = 1
                    },
                    new Order
                    {
                        Namber = "002",
                        Date = DateTime.Now,
                        ProviderId = 2
                    }

                );
                context.SaveChanges();
            }
            if (!context.OrderItems.Any())
            {
                context.OrderItems.AddRange(
                    new OrderItem
                    {
                        Name = "Позиция 1",
                        Quantity = 2,
                        Unit = "шт",
                        OrderId = 1
                    },
                    new OrderItem
                    {
                        Name = "Позиция 2",
                        Quantity = 3,
                        Unit = "шт",
                        OrderId = 1
                    },
                    new OrderItem
                    {
                        Name = "Позиция 3",
                        Quantity = 4,
                        Unit = "шт",
                        OrderId = 1
                    },
                    new OrderItem
                    {
                        Name = "Позиция 1",
                        Quantity = 2,
                        Unit = "шт",
                        OrderId = 2
                    }


                );
                context.SaveChanges();
            }
        }
    }
}
