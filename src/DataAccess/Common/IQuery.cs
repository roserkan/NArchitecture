namespace DataAccess.Common;

public interface IQuery<T>
{
    IQueryable<T> Query();
}