using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Entities;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            // product validator'in base type'in bul (AbstractValidator<Product>)
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            // methodun parametrelerinde ilgili entity bulunan (Product) onları listeye koy
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType).ToList();

            foreach ( var entity in entities) { 
                ValidationTool.Validate(validator, entity);            
            }
        }

    }
}
