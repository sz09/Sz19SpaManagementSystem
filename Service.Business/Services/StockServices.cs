using Core.ObjectServices.Repositories;
using log4net;
using Service.Business.IServices;
using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using Infrastructure.Logging;

namespace Service.Business.Services
{
    public class StockServices : IStockServices
    {
        #region Attributes
        private readonly IStockRepository _iStockRepositories;
        private static readonly ILog logger = LogManager.GetLogger(typeof(StockServices));
        #endregion
        
        #region Constructors
        public StockServices(IStockRepository iStockRepositories)
        {
            logger.EnterMethod();
            this._iStockRepositories = iStockRepositories;
            logger.LeaveMethod();
        }
        #endregion


        #region Operations
        public Stock GetStock(long stockId)
        {
            logger.EnterMethod();
            try
            {
                return this._iStockRepositories.GetStock(stockId);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public IEnumerable<Stock> GetStockByName(string name)
        {
            logger.EnterMethod();
            try
            {
                return this._iStockRepositories.Get(name);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public string GetStockName(long stockId, int languageId)
        {
            logger.EnterMethod();
            try
            {
                return this._iStockRepositories.GetName(stockId, languageId);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public List<Stock> GetAll()
        {
            logger.EnterMethod();
            try
            {
                return this._iStockRepositories.GetAll();
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public List<Stock> GetStockPaging(int index)
        {
            logger.EnterMethod();
            try
            {
                return this._iStockRepositories.GetStockPaging(index);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        #endregion
    }
}
