using Core.ObjectServices.Repositories;
using Core.ObjectServices.UnitOfWork;
using Infrastructure.Logging;
using log4net;
using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Infrastructure.Data.Repositories
{
    public class BedRepository : IBedRepository
    {
        #region Attributes
        private readonly IRepository<Bed> _iBedRepositories;
        private readonly IRepository<BedName> _iBedNameRepositories;
        private readonly IRepository<Language> _iLanguageRepostories;
        private readonly IRepository<Bills> _iBillRepostories;

        private readonly IUnitOfWork _iUnitOfWork;
        private int bedPerPage;
        private int defaultCountBedPerPage = 20;
        private int codeBedLength = 8;
        private static readonly ILog logger = LogManager.GetLogger(typeof(BedRepository));
        #endregion

        #region Constructors
        public BedRepository(IRepository<Bed> iBedRepositories, 
            IRepository<BedName> iBedNameRepositories, 
            IRepository<Language> iLanguageRepostories,
            IRepository<Bills> iBillRepostories,
            IUnitOfWork iUnitOfWork)
        {
            logger.EnterMethod();
            this._iBedRepositories = iBedRepositories;
            this._iBedNameRepositories = iBedNameRepositories;
            this._iLanguageRepostories = iLanguageRepostories;
            this._iBillRepostories = iBillRepostories;
            this._iUnitOfWork = iUnitOfWork;
            try
            {
                this.bedPerPage = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["bedPerPage"]);
                logger.Info("Success setting value to bedPerPage attribute value: [" + this.bedPerPage.ToString() + "]");
            }
            catch (Exception ex)
            {
                this.bedPerPage = defaultCountBedPerPage;
                logger.Error("Error:[" + ex.Message + "]. Setting default value: [" + this.bedPerPage.ToString() + "]");
            }
            logger.Info("Success setting values for attribbutes");
            logger.LeaveMethod();
        }
        #endregion

        #region Operations
        public Bed GetBed(int id)
        {
            logger.EnterMethod();
            try
            {
                var bed = this._iBedRepositories.Get(_ => _.Id == id);
                if (bed != null)
                    logger.Info("Found bed match Id: [" + id + "]");
                else
                    logger.Info("Can't find any bed match Id: [" + id + "]");
                return bed;
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
                string name = "";
                var lang = this._iLanguageRepostories.Get(_=>_.Value == language);
                if(lang != null)
                {
                    var bed = this._iBedNameRepositories.Get(_ => (_.BedId == id &&
                                                               _.LanguageId == lang.Id));
                    if (bed != null)
                    {
                        logger.Info("Found name for bed with Id: [" + id + "] and language: [" + language + "] passed");
                        name = bed.Name;
                    }
                    else
                        logger.Info("Can't find name for bed with Id: [" + id + "] and language: [" + language + "] passed");
                }
                else
                    logger.Info("Language can't be found");
                return name;
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

        public IQueryable<Bed> GetBedByPage(int index, string language = "en")
        {
            logger.EnterMethod();
            try
            {
                int allPages = GetAllPageForBed();
                if (index > allPages)
                {
                    logger.Info("Can't access page: [" + index.ToString() + "]. Out of range paging");
                    return (from bed in this._iBedRepositories.GetAll()
                             select bed).OrderBy(_ => _.Id).Skip((allPages - 1) * bedPerPage).Take(bedPerPage);
                }
                else
                {
                    logger.Info("Getting query to showing bed of page: [" + index.ToString() + "] in [" + allPages.ToString() + "] pages");
                    return (from bed in this._iBedRepositories.GetAll()
                            select bed).OrderBy(_ => _.Id).Skip((index - 1) * bedPerPage).Take(bedPerPage);
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

        private int GetAllPageForBed()
        {
            var allPages = (int)(this.CountAllBed() / this.bedPerPage);
            allPages = ((double)this.CountAllBed() / (double)this.bedPerPage) != (allPages * 1.0) ? allPages += 1 : allPages;
            logger.Info("Calculate [" + allPages.ToString() + "] pages beds");
            return allPages;
        }

        public int CountAllBed()
        {
            logger.EnterMethod();
            try
            {
                var count = this._iBedRepositories.Count();
                logger.Info("Repository has [" + count + "] beds");
                return count;
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
                if (bed != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._iBedRepositories.Add(bed);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Success add new bed and save all changes");
                    return true;
                }
                else
                {
                    logger.Info("Null bed can't not be insert");
                    return false;
                }
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

        public bool UpdateBed(Bed bed)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBed(int id)
        {
            throw new NotImplementedException();
        }

        public List<TimePeriod> GetTimePeriodsForBed(int bedId)
        {
            logger.EnterMethod();
            try
            {
                List<TimePeriod> timePeriods = new List<TimePeriod>();
                var allBills = this._iBillRepostories.Find(_ => _.BedId == bedId);

                foreach (var item in allBills)
                {
                    timePeriods.Add(new TimePeriod
                    {
                        From = item.PeriodFrom ?? new DateTime(1111, 1, 1),
                        To = item.PeriodTo ?? new DateTime(1111, 1, 1)
                    });
                }
                logger.Info("Found " + timePeriods.Count + " periods");
                return timePeriods;
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

        /// <summary>
        /// Create new Code for Bed
        ///     Random capitalization char 
        ///         With constant length of characters
        /// </summary>
        /// <returns>Code for Employee with length is constant variabble codeEmpLength</returns>
        public string CreateNewCode()
        {
            string code = "";
            Random rdm = new Random();
            do
            {
                for (int i = 0; i < codeBedLength; i++)
                {
                    code += (char)rdm.Next(65, 90);
                }
                // Check if coincident
            } while (CheckCoincidentCode(code));
            return code;
        }

        private bool CheckCoincidentCode(string bedCode)
        {
            logger.EnterMethod();
            try
            {
                var findEmpByExistingCode = this._iBedRepositories.Find(_ => _.BedCode == bedCode).FirstOrDefault();
                if (findEmpByExistingCode != null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex.Message);
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
                if (bed != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._iBedRepositories.Add(bed);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Success add new bed and save all changes");
                    return bed.Id;
                }
                else
                {
                    logger.Info("Null bed can't not be insert");
                    return -1;
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

        public bool AddNameForBed(BedName bedName)
        {
            logger.EnterMethod();
            try
            {
                var existBedName = this._iBedNameRepositories.Get(_ => (
                                                                _.BedId == bedName.BedId &&
                                                                _.LanguageId == bedName.LanguageId
                                                                ));
                if (existBedName != null)
                {
                    logger.Info("Exist bed with Id: [" + bedName.BedId + "] and LanguageId: [" + bedName.LanguageId  + "]");
                    return false;
                }
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._iBedNameRepositories.Add(bedName);
                    transactionScope.Complete();
                    this._iUnitOfWork.Save();
                    logger.Info("Insert new bed name for bed with Id: [" + bedName.BedId + "], name value: [" + bedName.Name + "] in language with Id: [" + bedName.LanguageId + "]");
                    return true;
                }
                logger.Info("Can't save bed name");
                return false;
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
