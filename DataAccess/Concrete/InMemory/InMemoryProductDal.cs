using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;

        public InMemoryProductDal()
        {
            _products = new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    CategoryId = 1,
                    ProductName = "PC",
                    UnitPrice = 25000,
                    UnitsInStock = 7
                },
                new Product
                {
                    ProductId = 2,
                    CategoryId = 2,
                    ProductName = "Buzdolabı",
                    UnitPrice = 15000,
                    UnitsInStock = 13
                },
                new Product
                {
                    ProductId = 3,
                    CategoryId = 1,
                    ProductName = "TV",
                    UnitPrice = 30000,
                    UnitsInStock = 17
                },
                new Product
                {
                    ProductId = 4,
                    CategoryId = 3,
                    ProductName = "Bardak",
                    UnitPrice = 25,
                    UnitsInStock = 100
                },
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId == product.ProductId);

            if (productToDelete != null)
            {
                _products.Remove(productToDelete);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p=>p.CategoryId== categoryId).ToList();
        }

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            if (productToUpdate != null)
            {
                productToUpdate.ProductName = product.ProductName;
                productToUpdate.CategoryId= product.CategoryId;
                productToUpdate.UnitPrice= product.UnitPrice;
                productToUpdate.UnitsInStock= product.UnitsInStock;
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
