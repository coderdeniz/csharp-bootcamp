using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

ProductManager productManager = new ProductManager(new EfProductDal());
CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

//GetAllProductsByUnitPrice(productManager);
//CategoryGetAll(categoryManager);

GetProductDetails(productManager);




static void GetAllProductsByUnitPrice(ProductManager productManager)
{
    foreach (var p in productManager.GetAllByUnitPrice(3, 100).OrderByDescending(p => p.UnitPrice))
    {
        Console.WriteLine(p.ProductName + " - price: " + p.UnitPrice + "$");
    }
}

static void CategoryGetAll(CategoryManager categoryManager)
{
    foreach (var category in categoryManager.GetAll())
    {
        Console.WriteLine(category.CategoryName);
    }
}

static void GetProductDetails(ProductManager productManager)
{
    foreach (var productDetails in productManager.GetProductDetails())
    {
        Console.WriteLine(productDetails.ProductName + " - " + productDetails.ProductId + " - " + productDetails.CategoryName + " - " + productDetails.UnitsInStock);
    }
}