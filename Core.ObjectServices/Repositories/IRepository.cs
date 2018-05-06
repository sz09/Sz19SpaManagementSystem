namespace Core.ObjectServices.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T> where T: class
    {
        /// <summary>
        /// Get number of all records
        /// </summary>
        /// <returns>Number of all records</returns>
        int Count();

        /// <summary>
        /// Add new object tye Generic
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        void Add(T obj);

        /// <summary>
        ///  Find object and delete
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        void Delete(T obj);

        /// <summary>
        /// Update object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        void Update(T obj);

        /// <summary>
        /// Find the entity and it's state to Deleted
        /// </summary>
        /// <param name="keyValues">Primary key of the entity</param>
        void Delete(params object[] keyValues);

        /// <summary>
        /// Change the entity state all element in list to Deleted
        /// </summary>
        /// <param name="list">A collection that store all entity to delete</param>
        void Delete(IEnumerable<T> list);

        /// <summary>
        /// Change the entity state all element match with a predicated to Deleted
        /// </summary>
        /// <param name="predicated">A function to test each element for a condition</param>
        void Delete(Expression<Func<T, bool>> predicated);

        /// <summary>
        /// Filters a sequence of values based on a predicated
        /// </summary>
        /// <param name="predicated">A function to test each element for a condition</param>
        /// <returns>A collection that match with a predicated</returns>
        IQueryable<T> Find(Expression<Func<T, bool>> predicated);

        /// <summary>
        /// Filters a sequence of values based on a predicated
        /// </summary>
        /// <param name="predicated">A function to test each element for a condition</param>
        /// <param name="includes">Include many lambda expression representing the path to include</param>
        /// <returns>A collection that match with a predicated</returns>
        IQueryable<T> Find(Expression<Func<T, bool>> predicated, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Finds an entity with the given primary key values
        /// </summary>
        /// <param name="keyValues">Primary key of the entity</param>
        /// <returns>If found, return an entity that match with primary key, otherwise is null</returns>
        T Get(params object[] keyValues);

        /// <summary>
        /// Finds the first entity match with predicated
        /// </summary>
        /// <param name="predicated">A function to test each element for a condition</param>
        /// <returns>If found, return the first entity that match with predicated, otherwise is null</returns>
        T Get(Expression<Func<T, bool>> predicated);

        /// <summary>
        /// Finds the first entity match with predicated
        /// </summary>
        /// <param name="predicated">A function to test each element for a condition</param>
        /// <param name="includes">Include many lambda expression representing the path to include</param>
        /// <returns>If found, return the first entity that match with predicated, otherwise is null</returns>
        T Get(Expression<Func<T, bool>> predicated, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// A collection that represents the collection of all entities in the context
        /// </summary>
        /// <returns>A sequence of values</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// A collection that represents the collection of all entities in the context
        /// </summary>
        /// <param name="includes">Include many lambda expression representing the path to include</param>
        /// <returns>A sequence of values</returns>
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Projects each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the function represented by selector</typeparam>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <returns>An colection that store all elements are the result of invoking a projection 
        /// function on each element of source.</returns>
        IQueryable<TResult> Select<TResult>(Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Projects each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TResult">The type of the elements of source</typeparam>
        /// <param name="selector">A projection function to apply to each element</param>
        /// <param name="includes">Include many lambda expression representing the path to include</param>
        /// <returns>An colection that store all elements are the result of invoking a projection 
        /// function on each element of source.</returns>
        IQueryable<TResult> Select<TResult>(Expression<Func<T, TResult>> selector, params Expression<Func<T, object>>[] includes);
        DateTime GetDateTimeServer();
    }
}
