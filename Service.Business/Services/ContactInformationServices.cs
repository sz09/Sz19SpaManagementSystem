using Core.ObjectServices.Repositories;
using Service.Business.IServices;
using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using log4net;
using Infrastructure.Logging;

namespace Service.Business.Services
{
    public class ContactInformationServices: IContactInformationServices
    {
        #region Attributes
        private readonly IContactInformationRepository _iContactInformationRepository;
        private static readonly ILog logger = LogManager.GetLogger(typeof(ContactInformationServices));
        #endregion

        #region Constructors
        public ContactInformationServices(IContactInformationRepository _iContactInformationRepository)
        {
            logger.EnterMethod();
            this._iContactInformationRepository = _iContactInformationRepository;
            logger.LeaveMethod();
        }
        #endregion

        #region Operations
        public ContactInformation Get(long personId)
        {
            logger.EnterMethod();
            try
            {
                return this._iContactInformationRepository.Get(personId);
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

        public IEnumerable<ContactInformation> GetContactInformationFromListStaff(IEnumerable<long> listStaffId)
        {
            logger.EnterMethod();
            try
            {
                return this._iContactInformationRepository.GetContactInformationFromListStaff(listStaffId);
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
        public bool UpdateContactInformation(ContactInformation contactInformation)
        {
            logger.EnterMethod();
            try
            {
                return this._iContactInformationRepository.UpdateContactInformationForStaff(contactInformation);
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

        public bool CheckContactInformationExistingAndUpdateForPerson(ContactInformation contactInformation)
        {
            logger.EnterMethod();
            try
            {
                return this._iContactInformationRepository.CheckContactInformationExistingAndUpdateForPerson(contactInformation);
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

        public int GetContactTypeId(string typeName)
        {
            logger.EnterMethod();
            try
            {
                return this._iContactInformationRepository.GetContactTypeId(typeName);
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

        public int GetContactForId(string forName)
        {
            logger.EnterMethod();
            try
            {
                return this._iContactInformationRepository.GetContactForId(forName);
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
        #endregion
    }
}
