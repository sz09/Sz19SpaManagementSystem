namespace Infrastructure.Data.UnitOfWork
{
    using System;
    using Core.ObjectServices;
    using Core.ObjectServices.Repositories;
    using Core.ObjectServices.UnitOfWork;
    using System.Data.Entity;
    using System.Collections.Generic;
    using Infrastructure.Data.Repositories;
    using log4net;
    using Infrastructure.Logging;
    using System.Data.Entity.Validation;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {

       #region Properties
        private readonly DbContext _dbContext;
        private IDictionary<Type, object> _repositories;
        private bool IsDisposed;
        private readonly ILog logger = LogManager.GetLogger(typeof(UnitOfWork));
        #endregion

        #region Constructor
        /// <summary>
        /// Create a unit of work that can handle all repositories
        /// </summary>
        /// <param name="dbContext">A interface using to create instance of DbContext</param>
        public UnitOfWork(DbContext dbContext)
        {
            this._dbContext = dbContext;
            if (this._dbContext == null)
            {
                throw new ArgumentException("Please using Entity Framework to access this repository");
            }
            this._repositories = new Dictionary<Type, object>();
        }
        #endregion

        #region Operations
        /// <summary>
        /// Manage repositories by using UnitOfWork
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <returns>A repository map with the generic type</returns>
        public IRepository<T> GetRepository<T>() where T : class
        {
            IRepository<T> repository;
            if (!this._repositories.ContainsKey(typeof(T)))
            {
                repository = new Repository<T>(this._dbContext);
                this._repositories.Add(typeof(T), repository);
            }
            else
            {
                repository = this._repositories[typeof(T)] as Repository<T>;
            }
            return repository;
        }

        /// <summary>
        /// Saves all changes made in this context database.
        /// </summary>
        public bool Save()
        {
            logger.EnterMethod();
            try
            {
                this._dbContext.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.ErrorFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.ErrorFormat("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return false;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        /// <summary>
        /// Calls the protected Dispose method.
        /// </summary>
        /// <param name="disposing">Create a overload of Dispose method</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.IsDisposed)
            {
                if (disposing)
                {
                    this._dbContext.Dispose();
                }
            }
            this.IsDisposed = true;
        }

        /// <summary>
        ///  Call the Dispose method for DbContext and requests that the common language runtime 
        ///  not call the finalizer for this class
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
    #endregion
}
