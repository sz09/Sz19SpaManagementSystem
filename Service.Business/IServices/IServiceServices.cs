using SPMS.ObjectModel.Entities;
using System.Linq;

namespace Service.Business.IServices
{
    public interface IServiceServices
        { 
        /// <summary>
        /// Call method in IServiceRepository
        ///     Get all services 
        /// </summary>
        /// <returns>
        /// IQueryable
        ///     Lazy loading
        /// </returns>
        IQueryable<SPMS.ObjectModel.Entities.Service> GetAll();
        /// <summary>
        /// Get service by Id
        /// </summary>
        /// <param name="serviceId">Id of service</param>
        /// <returns>Service</returns>
        SPMS.ObjectModel.Entities.Service Get(int serviceId);
        /// <summary>
        /// Get name of service by Id and language
        ///     By default, language is English
        /// </summary>
        /// <param name="serviceId">Id of service</param>
        /// <param name="language">Name of language</param>
        /// <returns>string name of language</returns>
        string GetServiceNameByLanguage(int serviceId, string language = "English");
        /// <summary>
        /// Paging feature
        ///     Using parcel page to get services by page
        ///         Get query, lazy loading
        /// </summary>
        /// <param name="index">Index choosen</param>
        /// <returns>Query, lazy loading</returns>
        IQueryable<SPMS.ObjectModel.Entities.Service> GetServiceByPage(int index);
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
        /// Get time using service by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        decimal GetTotalCostServices(int id);
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
        int CreateNewServiceReturnId(SPMS.ObjectModel.Entities.Service service);
        /// <summary>
        /// Add name for service name in language
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        bool AddNameForService(ServiceName serviceName);

        int GetServiceIdByCode(string code);
    }
}
