using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.ObjectServices.Repositories
{
    public interface IStockRepository
    {
        /// <summary>
        /// Get all stocks in database
        /// </summary>
        /// <returns>List of stock</returns>
        List<Stock> GetAll();
        /// <summary>
        /// Get stock by paging
        /// </summary>
        /// <param name="index">Index of page selected</param>
        /// <returns></returns>
        List<Stock> GetStockPaging(int index);
        /// <summary>
        /// Get stock by Id
        /// </summary>
        /// <param name="id">Id of stock</param>
        /// <returns>
        /// Found, return stock type
        /// Otherwise, return null
        /// </returns>
        Stock GetStock(long id);
        /// <summary>
        /// Get collection of Stock type by name
        /// </summary>
        /// <param name="name">Name of stock</param>
        /// <returns>
        /// List Stock fit name
        ///     Just has distinct stocks
        /// </returns>
        IEnumerable<Stock> Get(string name);
        /// <summary>
        /// Get name of Stock fit Id and LanguageId
        /// </summary>
        /// <param name="id">Id of stock</param>
        /// <param name="language">Id of language</param>
        /// <returns>
        /// Name of stock if found
        ///     Otherwise, return empty string
        /// </returns>
        string GetName(long id, int language);
        /// <summary>
        /// Get StockPackage by Id
        /// </summary>
        /// <param name="id">Id of stock</param>
        /// <returns>
        /// StockPackage if found
        ///     Otherwise, return null
        /// </returns>
        StockPackage GetStockPackage(long id);
        /// <summary>
        /// Build query to search StockPackages in database in time
        /// </summary>
        /// <param name="date">Date to search</param>
        /// <param name="getBy">Type of search</param>
        /// <returns></returns>
        IQueryable<StockPackage> Get(DateTime date, string getBy = "date");
        /// <summary>
        /// Build query to get StockPackageDetail by StockPackageId
        /// </summary>
        /// <param name="stockPackageId">Id of StockPackage</param>
        /// <returns>
        /// Query built, lazy loading
        /// </returns>
        IQueryable<StockPackageDetail> Get(long stockPackageId);
        /// <summary>
        /// Create a stock and save all changes
        /// </summary>
        /// <param name="stock">Stock type</param>
        /// <returns>
        /// If insert success, return true
        ///     Otherwise, return false
        /// </returns>
        bool Create(Stock stock);
        /// <summary>
        /// Create a StockPackage and save all changes
        /// </summary>
        /// <param name="stockPackage">StockPackage type</param>
        /// <returns>
        /// If insert success, return true
        ///     Otherwise, return false
        /// </returns>
        bool Create(StockPackage stockPackage);
        /// <summary>
        /// Create a StockPackageDetail and save all changes
        /// </summary>
        /// <param name="stockPackageDetail">StockPackageDetail type</param>
        /// <returns>
        /// If insert success, return true
        ///     Otherwise, return false
        /// </returns>
        bool Create(StockPackageDetail stockPackageDetail);
        /// <summary>
        /// Update information for stock
        /// </summary>
        /// <param name="stock">Stock type</param>
        /// <returns>
        /// If update success, return true
        ///     Otherwise, return false
        /// </returns>
        bool Update(Stock stock);
    }
}
