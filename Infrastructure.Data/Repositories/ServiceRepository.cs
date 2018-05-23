using Core.ObjectServices.Repositories;
using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Infrastructure.Logging;
using Core.ObjectServices.UnitOfWork;
using System.Configuration;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        #region Attributes
        private readonly IRepository<Service> _iServiceRepositories;
        private readonly IRepository<ServiceName> _iServiceNameRepositories;
        private readonly IUnitOfWork _iUnitOfWork;
        private static readonly ILog logger = LogManager.GetLogger(typeof(ServiceRepository));
        private readonly int servicesPerPage;
        private readonly int defaultServicePerpage = 20;
        #endregion

        #region Constructors
        public ServiceRepository(IRepository<Service> iServiceRepositories, IRepository<ServiceName> iServiceNameRepositories, IUnitOfWork iUnitOfWork)
        {
            logger.EnterMethod();
            this._iServiceRepositories = iServiceRepositories;
            this._iServiceNameRepositories = iServiceNameRepositories;
            this._iUnitOfWork = iUnitOfWork;
            try
            {
                this.servicesPerPage = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["servicesPerPage"]);
                logger.Info("Success setting value to servicesPerPage attribute value: [" + this.servicesPerPage.ToString() + "]");
            }
            catch (Exception ex)
            {
                this.servicesPerPage = defaultServicePerpage;
                logger.Error("Error:[" + ex.Message + "]. Setting default value: [" + this.servicesPerPage.ToString() + "]");
            }
            logger.Info("Setting value for attributes");
            logger.LeaveMethod();
        }
        #endregion

        #region Operations
        public IQueryable<Service> GetAll()
        {
            logger.EnterMethod();
            try
            {
                return this._iServiceRepositories.GetAll();
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

        public Service Get(int serviceId)
        {
            logger.EnterMethod();
            try
            {
                return this._iServiceRepositories.Get(_ => _.Id == serviceId);
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

        public string GetServiceNameByLanguage(int serviceId, string language = "English")
        {
            logger.EnterMethod();
            try
            {
                string serviceName = "";
                var service = this._iServiceNameRepositories.Find(_ => _.ServiceId == serviceId && _.Language.Value == language).FirstOrDefault();
                if(service != null)
                {
                    serviceName = service.Name;
                    logger.Info("Found service name with Id: [" + serviceId + "] and language: [" + language + "]");
                }
                else
                {
                    serviceName = "";
                    logger.Info("Couldn't found service name with Id: [" + serviceId + "] and language: [" + language + "]");
                }
                return serviceName;
            }
            catch (Exception ex)
            {
                logger.Error("Error: [" + ex.Message + "]");
                return "";
            }
            finally
            {
                logger.LeaveMethod();
            }
        }


        public IQueryable<Service> GetServiceByPage(int index)
        {
            logger.EnterMethod();
            try
            {
                var countAllServices = this._iServiceRepositories.Count();
                if (countAllServices <= this.servicesPerPage)
                {
                    logger.Info("All servicesfit a page. Get all services to a page");
                    return this.GetAll();
                }
                else
                {
                    var numberOfPages = CountTotalPages();
                    logger.Info("Found: [" + countAllServices.ToString() + "] services fit [" + numberOfPages.ToString() + "] pages");
                    if(index > numberOfPages)
                    {
                        logger.Error("Can't access page: [" + index.ToString() + "]. Out of range paging");
                        // Get all services, skip [servicesPerPage * (numberOfPages - 1)] records and take servicesPerPage records after
                        return (from services in this._iServiceRepositories.GetAll()
                                select services).OrderBy(_ => _.Id).Skip((numberOfPages - 1) * servicesPerPage).Take(servicesPerPage);
                    }
                    else
                    {
                        logger.Info("Getting query to showing services of page: [" + index.ToString() + "] in [" + numberOfPages.ToString() + "] pages");
                        // Get all services, skip [index - 1 * servicesPerPage] records and take servicesPerPage records after
                        return (from services in this._iServiceRepositories.GetAll()
                                select services).OrderBy(_ => _.Id).Skip((index -1) * servicesPerPage).Take(servicesPerPage);
                    }
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
        public int CountAllServices()
        {
            logger.EnterMethod();
            try
            {
                var count = this._iServiceRepositories.Count();
                if(count > 0)
                {
                    logger.Info("Found: [" + count.ToString() + "] servies");
                    return count;
                }
                else
                {
                    logger.Info("Can't find any service");
                    return 0;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return -1;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public int CountTotalPages()
        {
            logger.EnterMethod();
            try
            {
                var allPages = (int)(this.CountAllServices() / this.servicesPerPage);
                allPages = ((double)this.CountAllServices() / (double)this.servicesPerPage) != (allPages * 1.0) ? allPages +=1 : allPages;
                logger.Info("Calculate [" + allPages.ToString() + "] pages services");
                return allPages;
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return -1;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public string GetServiceCodeById(int id)
        {
            logger.EnterMethod();
            try
            {
                var service = this._iServiceRepositories.Get(_=>_.Id == id);
                if (service != null)
                {
                    logger.Info("Found code: [" + service.ServiceCode + "] for service: [" + id + "]");
                    return service.ServiceCode;
                }
                else
                {
                    logger.Info("");
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

        public int GetTotalTimeUseServices(int id)
        {
            logger.EnterMethod();
            try
            {
                var service = this._iServiceRepositories.Get(_ => _.Id == id);
                if (service != null)
                {
                    logger.Info("Found cost: [" + service.TimeCost +"] using for service with Id: ["  + id + "] ");
                    return service.TimeCost;
                }
                else
                {
                    logger.Info("Can't found any service with Id: [" + id + "]");
                    return 0;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return 0;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        #endregion


    }
}
