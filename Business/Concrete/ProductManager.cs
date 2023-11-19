using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //ValidationTool.Validate(new ProductValidator(), product);
            //loglama
            //cacheremove
            //performance
            //transaction
            //authorization
            //business codes

            //business katmanında business yapılır.
            //business'da yer alması gereken örnek: bir kategoride max 10 ürün olmalı           
            //aynı isimde ürün eklenemez
            //eğer mevcut kategori sayısı 15'i geçtiyse sisteme yeni ürün eklenemez.

            var result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId)
                , CheckIfProductNameExist(product.ProductName)
                , CheckIfCountOfCategory()
                , CheckIfCountOfCategory());

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            // iş kodları yazılır
            // yetkisi var mı?

            if (DateTime.Now.Hour == 16)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == categoryId)); 
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId == productId));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            // select count(*) from products where categoryId = 1
            if (_productDal.GetAll(x => x.CategoryId == categoryId).Count >= 10)
            {
                return new ErrorResult("bir kategoride max 10 ürün olmalı");
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExist(string productName)
        {
            if (_productDal.GetAll(x => x.ProductName == productName).Any())
            {
                return new ErrorResult("aynı isimde ürün eklenemez");
            }
            return new SuccessResult();
        }

        private IResult CheckIfCountOfCategory()
        {
            var result = _categoryService.GetAll();

            if (result.Data.Count >= 15)
            {
                return new ErrorResult("mevcut kategori sayısı 15'i geçtiği için sistemeyeniürün eklenemez");
            }
            
            return new SuccessResult();
        }
    }
}
