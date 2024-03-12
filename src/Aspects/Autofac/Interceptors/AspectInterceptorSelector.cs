using Castle.DynamicProxy;
using System.Reflection;
using Aspects.Autofac.Logging;
using CrossCuttingConcerns.Logging.Serilog.Logger;

namespace Aspects.Autofac.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes =
                type.GetMethods()?.Where(p => p.Name == method.Name).FirstOrDefault().GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            if (methodAttributes != null)
            {
                classAttributes.AddRange(methodAttributes);
            }

            var logAspect = new LogAspect(typeof(FileLogger));
            var exceptionLogAspect = new ExceptionLogAspect(typeof(FileLogger));
            logAspect.Priority = -1;
            exceptionLogAspect.Priority = -2;
            classAttributes.Add(logAspect);
            classAttributes.Add(exceptionLogAspect);
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}