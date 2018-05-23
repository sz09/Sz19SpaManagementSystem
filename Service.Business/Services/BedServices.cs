using Core.ObjectServices.Repositories;
using log4net;
using Service.Business.IServices;
using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Logging;

namespace Service.Business.Services
{
    public class BedServices : IBedServices
    {
        #region Attributes
        private readonly ILog logger = LogManager.GetLogger(typeof(BedServices));
        private readonly IBedRepository _iBedRepositories;
        #endregion

        #region Constructors
        public BedServices(IBedRepository iBedRepositories)
        {
            this._iBedRepositories = iBedRepositories;
        }
        #endregion

        #region Operations
        public int CountAllBed()
        {
            logger.EnterMethod();
            try
            {
                return this._iBedRepositories.CountAllBed();
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

        public bool CreateNewBed(Bed bed)
        {
            logger.EnterMethod();
            try
            {
                return this._iBedRepositories.CreateNewBed(bed);
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

        public bool DeleteBed(int id)
        {
            logger.EnterMethod();
            try
            {
                return this._iBedRepositories.DeleteBed(id);
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

        public string CreateNewCode()
        {
            logger.EnterMethod();
            try
            {
                return this._iBedRepositories.CreateNewCode();
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

        public IEnumerable<Bed> GetAll()
        {
            logger.EnterMethod();
            try
            {
                return this._iBedRepositories.GetAll();
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

        public Bed GetBed(int id)
        {
            logger.EnterMethod();
            try
            {
                return this._iBedRepositories.GetBed(id);
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

        public IQueryable<Bed> GetBedByPage(int index, string language = "English")
        {
            logger.EnterMethod();
            try
            {
                return this._iBedRepositories.GetBedByPage(index, language);
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

        public string GetBedName(int id, string language = "English")
        {
            logger.EnterMethod();
            try
            {
                return this._iBedRepositories.GetBedName(id, language);
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

        public List<TimePeriod> GetTimePeriodsForBed(int bedId)
        {
            logger.EnterMethod();
            try
            {

                return this._iBedRepositories.GetTimePeriodsForBed(bedId);
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

        public bool UpdateBed(Bed bed)
        {
            logger.EnterMethod();
            try
            {
                return this._iBedRepositories.UpdateBed(bed);
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

        public int CreateNewBedReturnId(Bed bed)
        {
            logger.EnterMethod();
            try
            {
                return this._iBedRepositories.CreateNewBedReturnId(bed);
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

        public bool AddNameForBed(BedName bedName)
        {
            logger.EnterMethod();
            try
            {
                return this._iBedRepositories.AddNameForBed(bedName);
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
