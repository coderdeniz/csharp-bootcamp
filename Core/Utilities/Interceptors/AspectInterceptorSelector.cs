using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            
            classAttributes.AddRange(methodAttributes);

            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));
            //yorum satırındaki kod tüm methodlara loglama altyapısını ekletir. method içinde loglama yazmana gerek kalmaz.

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
