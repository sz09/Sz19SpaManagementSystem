using SPMS.ObjectModel.Entities;
using System.Collections.Generic;

namespace Service.Business.IServices
{
    public interface IStockServices
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
        /// Get Stock by Id
        /// </summary>
        /// <param name="stockId">Id of stock</param>
        /// <returns>
        /// Stock if found
        ///     Otherwise, return null
        /// </returns>
        Stock GetStock(long stockId);
        /// <summary>
        /// Get collection of stock by name
        /// </summary>
        /// <param name="name">Name of stock</param>
        /// <returns>
        /// Collection of stock if found
        ///     Otherwise, return null
        /// </returns>
        IEnumerable<Stock> GetStockByName(string name);
        /// <summary>
        /// Get name of stock fit Id and LanguageId
        /// </summary>
        /// <param name="stockId">Id of stock</param>
        /// <param name="languageId">Id of language</param>
        /// <returns>
        /// Return name of stock if found
        ///     Otherwise, return empty string
        /// </returns>
        string GetStockName(long stockId, int languageId);
    }
}
