namespace Infrastructure.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Core.ObjectServices.Repositories;
    using System.Collections.Generic;

    public class Repository<T> : IRepository<T> where T: class
    {

        #region Properties
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        #endregion

        #region Constructor
        /// <summary>
        /// The constructor accepts a database context instance and initializes the entity set variable
        /// </summary>
        /// <param name="dbContext">An instance of DbContext</param>
        public Repository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = dbContext.Set<T>();
        }
        #endregion

        #region Operations
        
        /// <summary>
        /// Get number of all records
        /// </summary>
        /// <returns>Number of all records</returns>
        public int Count()
        {
            return this._dbSet.Count();
        }

        /// <summary>
        /// Change the entity state to Added
        /// </summary>
        /// <param name="dbContext">An entity</param>
        public void Add(T obj)
        {
            this._dbContext.Entry<T>(obj).State = EntityState.Added;
        }

        /// <summary>
        /// Change the entity state to Modified
        /// </summary>
        /// <param name="obj">An entity</param>
        public void Update(T obj)
        {
            if (this._dbContext.Entry<T>(obj).State == EntityState.Detached)
            {
                this._dbSet.Attach(obj);
            }
            this._dbContext.Entry<T>(obj).State = EntityState.Modified;
        }

        /// <summary>
        /// Change the entity state to Delete
        /// </summary>
        /// <param name="obj">An entity</param>
        public void Delete(T obj)
        {
            if (this._dbContext.Entry<T>(obj).State == EntityState.Detached)
            {
                this._dbSet.Attach(obj);
            }
            this._dbContext.Entry<T>(obj).State = EntityState.Deleted;
        }

        /// <summary>
        /// Find the entity and it's state to Deleted
        /// </summary>
        /// <param name="keyValues">Primary key of the entity</param>
        public void Delete(params object[] keyValues)
        {
            T obj = this._dbSet.Find(keyValues);
            if (this._dbContext.Entry<T>(obj).State == EntityState.Detached)
            {
                this._dbSet.Attach(obj);
            }
            this._dbContext.Entry<T>(obj).State = EntityState.Deleted;
        }

        /// <summary>
        /// Change the entity state all element in list to Deleted
        /// </summary>
        /// <param name="list">A collection that store all entity to delete</param>
        public void Delete(IEnumerable<T> list)
        {
            this._dbSet.RemoveRange(list);
        }

        /// <summary>
        /// Change the entity state all element match with a predicated to Deleted
        /// </summary>
        /// <param name="predicated">A function to test each element for a condition</param>
        public void Delete(Expression<Func<T, bool>> predicated)
        {
            this._dbSet.RemoveRange(this._dbSet.Where(predicated));
        }

        /// <summary>
        /// Filters a sequence of values based on a predicated
        /// </summary>
        /// <param name="predicated">A function to test each element for a condition</param>
        /// <returns>A collection that match with a predicated</returns>
        public IQueryable<T> Find(Expression<Func<T, bool>> predicated)
        {
            return this._dbSet.Where(predicated);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicated
        /// </summary>
        /// <param name="predicated">A function to test each element for a condition</param>
        /// <param name="includes">Include many lambda expression representing the path to include</param>
        /// <returns>A collection that match with a predicated</returns>
        public IQueryable<T> Find(Expression<Func<T, bool>> predicated, params Expression<Func<T, object>>[] includes)
        {
            return this.GetAll(includes).Where(predicated);
        }

        /// <summary>
        /// Finds an entity with the given primary key values
        /// </summary>
        /// <param name="keyValues">Primary key of the entity</param>
        /// <returns>If found, return an entity that match with primary key, otherwise is null</returns>
        public T Get(params object[] keyValues)
        {
            return this._dbSet.Find(keyValues);
        }

        /// <summary>
        /// Finds the first entity match with predicated
        /// </summary>
        /// <param name="predicated">A function to test each element for a condition</param>
        /// <returns>If found, return the first entity that match with predicated, otherwise is null</returns>
        public T Get(Expression<Func<T, bool>> predicated)
        {
            return this._dbSet.FirstOrDefault(predicated);
        }

        /// <summary>
        /// Finds the first entity match with predicated
        /// </summary>
        /// <param name="predicated">A function to test each element for a condition</param>
        /// <param name="includes">Include many lambda expression representing the path to include</param>
        /// <returns>If found, return the first entity that match with predicated, otherwise is null</returns>
        public T Get(Expression<Func<T, bool>> predicated, params Expression<Func<T, object>>[] includes)
        {
            return this.GetAll(includes).FirstOrDefault(predicated);
        }

        /// <summary>
        /// A collection that represents the collection of all entities in the context
        /// </summary>
        /// <returns>A sequence of values</returns>
        public IQueryable<T> GetAll()
        {
            return this._dbSet;
        }

        /// <summary>
        /// A collection that represents the collection of all entities in the context
        /// </summary>
        /// <param name="includes">Include many lambda expression representing the path to include</param>
        /// <returns>A sequence of values</returns>
        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> result = this._dbSet;

            foreach (var expression in includes)
            {
                result = result.Include(expression);
            }
            return result;
        }

        /// <summary>
        /// Projects each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the function represented by selector</typeparam>
        /// <param name="selector">A projection function to apply to each element.</param>
        /// <returns>An colection that store all elements are the result of invoking a projection 
        /// function on each element of source.</returns>
        public IQueryable<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
        {
            return this._dbSet.Select(selector);
        }

        /// <summary>
        /// Projects each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TResult">The type of the elements of source</typeparam>
        /// <param name="selector">A projection function to apply to each element</param>
        /// <param name="includes">Include many lambda expression representing the path to include</param>
        /// <returns>An colection that store all elements are the result of invoking a projection 
        /// function on each element of source.</returns>
        public IQueryable<TResult> Select<TResult>(Expression<Func<T, TResult>> selector, params Expression<Func<T, object>>[] includes)
        {
            return this.GetAll(includes).Select(selector);
        }

        /// <summary>
        /// Get date time from server using Entity Framework
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateTimeServer()
        {
            var dQuery = this._dbContext.Database.SqlQuery<DateTime>("SELECT GETDATE()");
            return dQuery.AsEnumerable().First();
        }
        #endregion
    }
}
