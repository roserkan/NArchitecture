using System.Linq.Expressions;
using DataAccess.Common.Dynamic;
using DataAccess.Common.Paging;
using Domain.Common;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Common;

public interface IRepository<TEntity> : IQuery<TEntity>
    where TEntity : BaseEntity
{
    #region GET
    TEntity? Get(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true
    );
    #endregion

    #region GET_LIST
    IPaginate<TEntity> GetList(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool isAll = false,
        bool withDeleted = false,
        bool enableTracking = false
    );
    
    IPaginate<TEntity> GetListByDynamic(
        DynamicQuery dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool isAll = false,
        bool withDeleted = false,
        bool enableTracking = false
    );
    #endregion

    #region INSERT
    TEntity Add(TEntity entity);
    ICollection<TEntity> AddRange(ICollection<TEntity> entities);
    #endregion

    #region UPDATE
    TEntity Update(TEntity entity);
    ICollection<TEntity> UpdateRange(ICollection<TEntity> entities);
    #endregion

    #region DELETE
    TEntity Delete(TEntity entity, bool permanent = false);
    ICollection<TEntity> DeleteRange(ICollection<TEntity> entity, bool permanent = false);
    #endregion

    #region ANY
    bool Any(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true);
    #endregion
}