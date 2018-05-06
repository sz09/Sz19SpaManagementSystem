using Core.ObjectServices.Repositories;
using Core.ObjectServices.UnitOfWork;
using log4net;
using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Logging;
using System.Transactions;

namespace Infrastructure.Data.Repositories
{
    public class StockRepository : IStockRepository
    {
        #region Attributes
        private readonly IRepository<Stock> _stockRepositories;
        private readonly IRepository<StockName> _stockNameRepositories;
        private readonly IRepository<StockPackage> _stockPackageRepositories;
        private readonly IRepository<StockPackageDetail> _stockPackageDetailRepositories;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly int _stockInPage = 10;
        private static readonly ILog logger = LogManager.GetLogger(typeof(StockRepository));
        private int totalPages;
        #endregion
        #region Constructors
        public StockRepository(IRepository<Stock> stockRepositories, IRepository<StockName> stockNameRepositories, IRepository<StockPackage> stockPackageRepositories, IRepository<StockPackageDetail> stockPackageDetailRepositories, IUnitOfWork iUnitOfWork)
        {
            logger.EnterMethod();
            try
            {
                this._iUnitOfWork = iUnitOfWork;
                this._stockNameRepositories = stockNameRepositories;
                this._stockPackageDetailRepositories = stockPackageDetailRepositories;
                this._stockPackageRepositories = stockPackageRepositories;
                this._stockRepositories = stockRepositories;
                logger.Info("Set value for attributes");
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
            }
            finally
            {
                logger.LeaveMethod();
            }

        }
        #endregion

        
        #region Operations
        public Stock GetStock(long stockId)
        {
            logger.EnterMethod();
            try
            {
                var stock = this._stockRepositories.Get(_ => _.Id == stockId);
                if (stock != null)
                {
                    logger.Info("Found stock by Id: [" + stockId + "]");
                    return stock;
                }
                else
                {
                    logger.Info("Can't find stock by Id: [" + stockId + "]");
                    return null;
                }
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

        public IEnumerable<Stock> Get(string name)
        {
            logger.EnterMethod();
            try
            {
                var stockName = this._stockNameRepositories.Find(_ => _.Name == name);
                if (stockName.ToList() != null)
                {
                    var listStock = new List<Stock>();
                    var stockNameList = stockName.ToList();
                    for(int i = 0; i < stockNameList.Count; i++)
                    {
                        var stock = this.GetStock(stockNameList[i].StockId);
                        if(!listStock.Contains(stock))
                            listStock.Add(stock);
                    }
                    logger.Info("Found [" + stockNameList.Count + "] stocks fit name: " + name + "]");
                    return listStock;
                }
                else
                {
                    logger.Info("Can't find any stock fit name: [" + name + "]");
                    return null;
                }
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

        public string GetName(long stockId, int language)
        {
            logger.EnterMethod();
            try
            {
                var stock = this._stockNameRepositories.Get(_ => _.StockId == stockId &&
                                                               _.LanguageId == language);
                if (stock != null)
                {
                    logger.Info("Found stock name [" + stock.Name + "] fit Id: [" + stockId + "] in languageId: [" + language + "]");
                    return stock.Name;
                }
                else
                {
                    logger.Info("Can't find name of [" + stockId + "] in languageId: [" + language + "]");
                    return "";
                }
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return "";
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        
        public IQueryable<StockPackageDetail> Get(long stockPackageId)
        {
            logger.EnterMethod();
            try
            {
                return this._stockPackageDetailRepositories.Find(_ => _.StockPackageId == stockPackageId);
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

        public StockPackage GetStockPackage(long stockPackageId)
        {
            logger.EnterMethod();
            try
            {
                var stockPackage = this._stockPackageRepositories.Get(_ => _.Id == stockPackageId);
                if (stockPackage != null)
                    logger.Info("Found StockPackage with Id: [" + stockPackageId +  "]");
                else
                    logger.Info("Can't find StockPackage with Id: [" + stockPackageId + "]");                    
                return stockPackage;
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

        public bool Create(Stock stock)
        {
            logger.EnterMethod();
            try
            {
                using(TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._stockRepositories.Add(stock);
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Inserted new Stock to database and save all changes");
                return true;
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

        public bool Create(StockPackage stockPackage)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._stockPackageRepositories.Add(stockPackage);
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Inserted new StockPackage to database and save all changes");
                return true;
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

        public bool Create(StockPackageDetail stockPackageDetail)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._stockPackageDetailRepositories.Add(stockPackageDetail);
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Inserted new StockPackageDetail to database and save all changes");
                return true;
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

        public bool Update(Stock stock)
        {
            logger.EnterMethod();
            try
            {
                var stockGot = this._stockRepositories.Get(stock.Id);
                if (stockGot != null)
                {
                    stockGot.Cost = stock.Cost;
                    stockGot.Unit = stock.Unit;
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._stockRepositories.Update(stockGot);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Updated stock values and save all changes");
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool Delete(long stockId)
        {
            logger.EnterMethod();
            try
            {
                var stockGot = this._stockRepositories.Get(stockId);
                if (stockGot != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._stockRepositories.Delete(stockGot);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Delete stock with Id: [" + stockId + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find any stock with Id: [" + stockId + "]");
                    return false;
                }
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
        
        public IQueryable<StockPackage> Get(DateTime date, string getBy = "date")
        {
            logger.EnterMethod();
            try
            {
                if (getBy.ToLower() == "date")
                    return this._stockPackageRepositories.Find(_ => _.DateAdd == date);
                else if (getBy.ToLower() == "month")
                    return this._stockPackageRepositories.Find(_ => _.DateAdd.Month == date.Month &&
                                                                  _.DateAdd.Year == date.Year);
                logger.Info("GetBy string is not correct");
                return null;
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
                var allStocks = this._stockRepositories.GetAll().ToList();
                if (allStocks.Count > 0)
                    logger.Info("Found [" + allStocks.Count + "] stocks in database");
                else
                    logger.Info("Can't find any stock in database");
                return allStocks;
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
                if (index < 1)
                    index = 1;
                if (index > totalPages)
                    index = totalPages;
                var stocks = this._stockRepositories.GetAll().
                             OrderBy(_ => _.Id).
                             Skip((index - 1) * _stockInPage).
                             Take(_stockInPage).ToList();
                if (stocks.Count > 0)
                    logger.Info("Found [" + stocks.Count + "] stocks in database");
                else
                    logger.Info("Can't find any stocks in database");
                return stocks;
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
