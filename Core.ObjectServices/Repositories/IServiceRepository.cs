using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ObjectServices.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IServiceRepository
    {
        /// <summary>
        /// Get all services 
        /// </summary>
        /// <returns>
        /// IQueryable
        ///     Lazy loading
        /// </returns>
        IQueryable<Service> GetAll();
        /// <summary>
        /// Get service by Id
        /// </summary>
        /// <param name="serviceId">Id of service</param>
        /// <returns>Service</returns>
        Service Get(int serviceId);

        /// <summary>
        /// Get service name by language
        /// </summary>
        /// <param name="serviceId">Id of service</param>
        /// <param name="language">Name of string</param>
        /// <returns>Name of service fit language</returns>
        string GetServiceNameByLanguage(int serviceId, string language = "en");

        /// <summary>
        /// Paging feature
        ///     Using parcel page to get services by page
        ///         Get query, lazy loading
        /// </summary>
        /// <param name="index">Index choosen</param>
        /// <returns>Query, lazy loading</returns>
        IQueryable<Service> GetServiceByPage(int index);

        /// <summary>
        /// Get number of all services existing in database
        /// </summary>
        /// <returns>Number of services</returns>
        int CountAllServices();

        /// <summary>
        /// Get all pages paging
        /// </summary>
        /// <returns>Total pages</returns>
        int CountTotalPages();
    }
}
