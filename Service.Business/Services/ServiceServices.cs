using Core.ObjectServices.Repositories;
using Service.Business.IServices;
using System.Linq;
using log4net;
using Infrastructure.Logging;
using System;
using SPMS.ObjectModel.Entities;

namespace Service.Business.Services
{
    public class ServiceServices: IServiceServices
    {
        #region Attributes
        private readonly IServiceRepository _iServiceRepositories;
        private static readonly ILog logger = LogManager.GetLogger(typeof(ServiceServices));
        #endregion
        #region Constructors
        public ServiceServices(IServiceRepository iServiceRepositories)
        {
            logger.EnterMethod();
            this._iServiceRepositories = iServiceRepositories;
            logger.Info("Success setting values for attributes");
            logger.LeaveMethod();
        }
        #endregion

        #region Operations
        public IQueryable<SPMS.ObjectModel.Entities.Service> GetAll()
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

        public SPMS.ObjectModel.Entities.Service Get(int serviceId)
        {
            logger.EnterMethod();
            try
            {
                return this._iServiceRepositories.Get(serviceId);
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
                return this._iServiceRepositories.GetServiceNameByLanguage(serviceId, language);
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

        public IQueryable<SPMS.ObjectModel.Entities.Service> GetServiceByPage(int index)
        {
            logger.EnterMethod();
            try
            {
                return this._iServiceRepositories.GetServiceByPage(index);
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
        public int CountTotalPages()
        {
            logger.EnterMethod();
            try
            {
                return this._iServiceRepositories.CountTotalPages();
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
                return this._iServiceRepositories.GetServiceCodeById(id);
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
                return this._iServiceRepositories.GetTotalTimeUseServices(id);
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

        public string CreateNewCode()
        {
            logger.EnterMethod();
            try
            {
                return this._iServiceRepositories.CreateNewCode();
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return string.Empty;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public int CreateNewServiceReturnId(SPMS.ObjectModel.Entities.Service service)
        {
            logger.EnterMethod();
            try
            {
                return this._iServiceRepositories.CreateNewServiceReturnId(service);
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

        public bool AddNameForService(ServiceName serviceName)
        {
            logger.EnterMethod();
            try
            {
                return this._iServiceRepositories.AddNameForService(serviceName);
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
        #endregion
    }
}
