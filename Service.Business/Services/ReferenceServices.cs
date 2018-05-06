using Core.ObjectServices.Repositories;
using log4net;
using Service.Business.IServices;
using System;
using System.Collections.Generic;
using Infrastructure.Logging;
using SPMS.ObjectModel.Entities;

namespace Service.Business.Services
{
    public class ReferenceServices : IReferenceServices
    {
        #region Attributes
        private readonly IReferenceRepository _iReferenceRepositories;
        private readonly ILog logger = LogManager.GetLogger(typeof(ReferenceServices));
        #endregion
        #region Constructors
        public ReferenceServices(IReferenceRepository iReferenceRepositories)
        {
            logger.EnterMethod();
            try
            {
                this._iReferenceRepositories = iReferenceRepositories;
                logger.Info("Success set value for attributes");
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
        public Tuple<List<Address>, List<Bed>, List<Customer>, List<SPMS.ObjectModel.Entities.Service>, List<Staff>, List<Stock>, int> Search(string key)
        {
            logger.EnterMethod();
            try
            {
                return this._iReferenceRepositories.Search(key);
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
