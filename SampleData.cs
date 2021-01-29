using Pizza.Models;
using System.Linq;

namespace Pizza
{
    public static class SampleData
    {
        public static void Initialize(PizzaContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "BlankPizza",
                        Category = "Pizza",
                        Description = "Blank pizza for test",
                        Price = 10,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankPizza2",
                        Category = "Pizza",
                        Description = "Another blank pizza for test",
                        Price = 20,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankPizza3",
                        Category = "Pizza",
                        Description = "Blank pizza for test",
                        Price = 30,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankPizza4",
                        Category = "Pizza",
                        Description = "Another blank pizza for test",
                        Price = 40,
                        ImagePath = null
                    }, new Product
                    {
                        Name = "BlankPizza5",
                        Category = "Pizza",
                        Description = "Blank pizza for test",
                        Price = 50,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankPizza6",
                        Category = "Pizza",
                        Description = "Another blank pizza for test",
                        Price = 60,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankDrink",
                        Category = "Drink",
                        Description = "Blank drink for test",
                        Price = 10,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankDrink2",
                        Category = "Drink",
                        Description = "Another blank drink for test",
                        Price = 20,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankDrink3",
                        Category = "Drink",
                        Description = "Blank drink for test",
                        Price = 30,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankDrink4",
                        Category = "Drink",
                        Description = "Another blank drink for test",
                        Price = 40,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankDrink5",
                        Category = "Drink",
                        Description = "Blank drink for test",
                        Price = 50,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankDrink6",
                        Category = "Drink",
                        Description = "Another blank drink for test",
                        Price = 60,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankSnack",
                        Category = "Snack",
                        Description = "Blank snack for test",
                        Price = 10,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankSnack2",
                        Category = "Snack",
                        Description = "Another blank snack for test",
                        Price = 20,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankSnack3",
                        Category = "Snack",
                        Description = "Blank snack for test",
                        Price = 30,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankSnack4",
                        Category = "Snack",
                        Description = "Another blank snack for test",
                        Price = 40,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankSnack5",
                        Category = "Snack",
                        Description = "Blank snack for test",
                        Price = 50,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankSnack6",
                        Category = "Snack",
                        Description = "Another blank snack for test",
                        Price = 60,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankSalad",
                        Category = "Salad",
                        Description = "Blank salad for test",
                        Price = 10,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankSalad2",
                        Category = "Salad",
                        Description = "Another blank salad for test",
                        Price = 20,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankSalad3",
                        Category = "Salad",
                        Description = "Blank salad for test",
                        Price = 30,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankSalad4",
                        Category = "Salad",
                        Description = "Another blank salad for test",
                        Price = 40,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankSalad5",
                        Category = "Salad",
                        Description = "Blank salad for test",
                        Price = 50,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankSalad6",
                        Category = "Salad",
                        Description = "Another blank salad for test",
                        Price = 60,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankKombo",
                        Category = "Kombo",
                        Description = "Blank kombo for test",
                        Price = 10,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankKombo2",
                        Category = "Kombo",
                        Description = "Another blank kombo for test",
                        Price = 20,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankKombo3",
                        Category = "Kombo",
                        Description = "Blank kombo for test",
                        Price = 30,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankKombo4",
                        Category = "Kombo",
                        Description = "Another blank kombo for test",
                        Price = 40,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankKombo5",
                        Category = "Kombo",
                        Description = "Blank kombo for test",
                        Price = 50,
                        ImagePath = null
                    },
                    new Product
                    {
                        Name = "BlankKombo6",
                        Category = "Kombo",
                        Description = "Another blank kombo for test",
                        Price = 60,
                        ImagePath = null
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
