using Business.Concrete;
using DataAccess.Concrete.InMemory;

ProductManager productManager = new ProductManager(new InMemoryProductDal());

foreach (var p in productManager.GetAll())
{
    Console.WriteLine(p.ProductName);
}


