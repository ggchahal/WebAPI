using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI
{
    public class DatabaseInitializer : System.Data.Entity.CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);

            var categories = new List<Category>();
            categories.Add(new Category()
            {
                CategoryId = 1001,
                CategoryName = "Electronics",
                Product = new List<Product> {
                                                new Product {
                                                    ProductName="TV", ProductPrice = 2000
                                                },
                                                new Product {
                                                    ProductName="PlayStation",ProductPrice = 400
                                                },
                                                new  Product {
                                                    ProductName="Stereo", ProductPrice = 1600
                                                }
                                          }
            });
            categories.Add(new Category()
            {
                CategoryId = 1002,
                CategoryName = "Clothing",
                Product = new List<Product> {
                                                new Product {
                                                    ProductName="Shirts", ProductPrice = 1100
                                                },
                                                new Product {
                                                    ProductName="Jeans",ProductPrice = 1100
                                                }
                                          }
            });
            categories.Add(new Category()
            {
                CategoryId = 1003,
                CategoryName = "Kitchen",
                Product = new List<Product> {
                                                new Product {
                                                    ProductName="Pots and Pans", ProductPrice = 3000
                                                },
                                                new Product {
                                                    ProductName="Flatware",ProductPrice = 500
                                                },
                                                new  Product {
                                                    ProductName="Knife Set",ProductPrice = 500
                                                },
                                                new  Product {
                                                    ProductName="Misc",ProductPrice = 1000
                                                }
                                          }
            });

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
    }
}