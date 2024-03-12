using Castle.DynamicProxy;
using System.Transactions;
using Aspects.Autofac.Interceptors;
using CrossCuttingConcerns.IoC;
using Microsoft.EntityFrameworkCore;

namespace Aspects.Autofac.Transaction;

/// <summary>
/// TransactionScopeAspect
/// </summary>
public class TransactionScopeAspectAsync : MethodInterception
{

    public static void InterceptDbContext(IInvocation invocation)
    {
        var db = ServiceTool.ServiceProvider.GetService(default) as DbContext;
        using var transactionScope = db.Database.BeginTransaction();
        try
        {
            invocation.Proceed();
            transactionScope.Commit();
        }
        finally
        {
            transactionScope.Rollback();
        }
    }

    public override void Intercept(IInvocation invocation)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        invocation.Proceed();
        tx.Complete();
    }
}