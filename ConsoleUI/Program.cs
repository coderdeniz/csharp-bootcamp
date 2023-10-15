using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

ProductManager productManager = new ProductManager(new EfProductDal());


//GetAllProductsByUnitPrice(productManager);

CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
foreach (var category in categoryManager.GetAll())
{
    Console.WriteLine(category.CategoryName);
}


static void GetAllProductsByUnitPrice(ProductManager productManager)
{
    foreach (var p in productManager.GetAllByUnitPrice(3, 100).OrderByDescending(p => p.UnitPrice))
    {
        Console.WriteLine(p.ProductName + " - price: " + p.UnitPrice + "$");
    }
}

