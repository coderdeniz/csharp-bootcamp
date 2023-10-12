using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

ProductManager productManager = new ProductManager(new EfProductDal());

foreach (var p in productManager.GetAllByUnitPrice(3,100).OrderByDescending(p=>p.UnitPrice))
{
    Console.WriteLine(p.ProductName + " - price: " + p.UnitPrice + "$");
}


