using SPMS.ObjectModel.Entities;
using System.Linq;

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
        /// <summary>
        /// Get service code by id 
        /// </summary>
        /// <param name="id">Id of service</param>
        /// <returns></returns>
        string GetServiceCodeById(int id);
        /// <summary>
        /// Get time using service by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int GetTotalTimeUseServices(int id);
        /// <summary>
        /// Generate code for bed
        /// </summary>
        /// <returns></returns>
        string CreateNewCode();
        /// <summary>
        /// Insert new service
        ///     If success, return Id
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        int CreateNewServiceReturnId(Service service);
        /// <summary>
        /// Add name for service name in language
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        bool AddNameForService(ServiceName serviceName);
    }
}
