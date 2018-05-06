using System;
using System.Collections.Generic;
using System.Linq;
using Core.ObjectServices.Repositories;
using SPMS.ObjectModel.Entities;
using System.Linq.Expressions;
using Core.ObjectServices.UnitOfWork;
using System.Transactions;
using log4net;
using Infrastructure.Logging;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// StaffRepository is implemented IStaffRepository
    /// This class provides all operations to access Staff Entity
    /// </summary>
    public class StaffRepository : IStaffRepository
    {
        #region Attributes
        private readonly IRepository<Staff> _staffRepository;
        private readonly IUnitOfWork _iUnitOfWork;
        private static readonly ILog logger = LogManager.GetLogger(typeof(StaffRepository));
        private const int codeEmpLength = 8; // StaffCode length is 8
        private readonly int empPerPage;
        private const int defaultEmpPerPage = 20;
        #endregion
        #region Constructors
        public StaffRepository(IRepository<Staff> _staffRepository, IUnitOfWork iUnitOfWork )
        {
            logger.EnterMethod();
            this._staffRepository = _staffRepository;
            this._iUnitOfWork = iUnitOfWork;
            try
            {
                this.empPerPage = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["empPerPage"]);
                logger.Info("Success setting value to empPerPage attribute value: [" + this.empPerPage.ToString() + "]");
            }
            catch (Exception ex)
            {
                this.empPerPage = defaultEmpPerPage;
                logger.Error("Error:[" + ex.Message + "]. Setting default value: [" + this.empPerPage.ToString() + "]");
            }
            logger.LeaveMethod();
        }
        #endregion

        #region Operations
        /// <summary>
        /// Insert Staff to Repository and save changes
        /// </summary>
        /// <param name="staff">Object staff</param>
        /// <returns>
        /// true: If insert Staff success
        /// Otherwise, false
        /// </returns>
        public bool InsertStaff(Staff staff)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._staffRepository.Add(staff);
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Success insert employee with Id: [" + staff.Id + "]  into database and save all changes");
                return true;
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

        /// <summary>
        /// Update staff in Repository and save all changes
        /// </summary>
        /// <param name="staff">Object staff</param>
        /// <returns>
        /// true: If update Staff success
        /// Otherwise, false
        /// </returns>
        public bool UpdateStaff(Staff staff)
        {
            logger.EnterMethod();
            try
            {
                var staffGot = this._staffRepository.Get(staff.Id);
                if (staffGot != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        staffGot.FirstName = staff.FirstName;
                        staffGot.LastMiddle = staff.LastMiddle;
                        staffGot.Salary = staff.Salary;
                        staffGot.Summary = staff.Summary;
                        staffGot.Image = staff.Image;
                        staffGot.IdentifierNumber = staff.IdentifierNumber;
                        staffGot.IsInUse = staff.IsInUse;
                        this._staffRepository.Update(staffGot);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Success update employee with Id: [" + staff.Id + "]in database and save all changes");
                    return true;
                }
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

        public bool DeleteStaff(int staffId)
        {
            logger.EnterMethod();
            try
            {
                var staffGot = this._staffRepository.Get(staffId);
                if (staffGot != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._staffRepository.Delete(staffGot);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Success detele employee with Id: [" + staffId + "] from database and save all changes");
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return false;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public bool DeleteStaff(params object[] keyValues)
        {
            logger.EnterMethod();
            try
            {
                var staffGot = this._staffRepository.Get(keyValues);
                if (staffGot != null) 
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._staffRepository.Delete(staffGot);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();

                    logger.Info("Success detele employee with keys: [" + keyValues + "] from database and save all changes");
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return false;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public bool Delete(IEnumerable<Staff> listStaff)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var item in listStaff)
                    {
                        var staffGot = this._staffRepository.Get(item);
                        if (staffGot != null)
                        {
                            this._staffRepository.Delete(staffGot);
                        }
                    }
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Success delete list employees: [" + listStaff.Count() + " employee(s)]");
                return true;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return false;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }


        public Staff Get(long staffId)
        {
            logger.EnterMethod();
            try
            {
                var emp = this._staffRepository.Get(_ => _.Id == staffId);
                if (emp != null)
                    logger.Info("Got employee with Id: [" + staffId + "]");
                return emp;
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex.Message);
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public Staff Get(params object[] keyValues)
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception ex)
            {

                logger.Error("Error: " + ex.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
            return this._staffRepository.Get(keyValues);
        }

        public Staff Get(Expression<Func<Staff, bool>> predicated)
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception ex)
            {

                logger.Error("Error: " + ex.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
            return this._staffRepository.Get(predicated);
        }

        public Staff Get(Expression<Func<Staff, bool>> predicated, params Expression<Func<Staff, object>>[] includes)
        {
            logger.EnterMethod();
            try
            {
                var emp = this._staffRepository.Get(predicated, includes);
                if (emp != null) 
                    logger.Info("Got employee with Id: [" + emp.Id + "]");
                return emp;
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex.Message);
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public IQueryable<Staff> GetAll()
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception ex)
            {

                logger.Error("Error: " + ex.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
            return this._staffRepository.GetAll();
        }

        public IQueryable<Staff> GetAll(bool isInUse = true)
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception ex)
            {

                logger.Error("Error: " + ex.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
            return this._staffRepository.Find(_ => _.IsInUse == isInUse);
        }

        public IQueryable<Staff> GetAll(params Expression<Func<Staff, object>>[] includes)
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception ex)
            {

                logger.Error("Error: " + ex.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
            return this._staffRepository.GetAll(includes);
        }

        public IQueryable<Staff> Find(Expression<Func<Staff, bool>> predicated)
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception ex)
            {

                logger.Error("Error: " + ex.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
            return this._staffRepository.Find(predicated);
        }

        public IQueryable<Staff> Find(Expression<Func<Staff, bool>> predicated, params Expression<Func<Staff, object>>[] includes)
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception ex)
            {

                logger.Error("Error: " + ex.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
            return this._staffRepository.Find(predicated, includes);

        }

        /// <summary>
        /// Create new Code for Employee
        ///     Random capitalization char 
        ///         With constant length of characters
        /// </summary>
        /// <returns>Code for Employee with length is constant variabble codeEmpLength</returns>
        public string CreateNewCode()
        {
            string code = "";
            bool coincidentCode = false;
            Random rdm = new Random();
            do{
                for (int i = 0; i < codeEmpLength; i++)
                {
                    code+= (char)rdm.Next(65, 90);
                }
                // Check if coincident
                coincidentCode = CheckCoincidentCode(code);
            } while (coincidentCode);
            return code;
        }

        private bool CheckCoincidentCode(string empCode)
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
            var findEmpByExistingCode = this._staffRepository.Find(_ => _.StaffCode == empCode).FirstOrDefault();
            if (findEmpByExistingCode != null)
                return true;
            return false;
        }

        public bool Delete(Expression<Func<Staff, bool>> predicated)
        {
            logger.EnterMethod();
            try
            {
                var emp = this._staffRepository.Get(predicated);
                if (emp != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._staffRepository.Delete(emp);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Success delete employee with Id: [" + emp.Id + "]");
                    return true;
                }
                else
                {
                    logger.Info("Couldn't found employee fit predicated");
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error: [" + ex.Message + "]");
                return false;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public IQueryable<Staff> GetStaffByPage(int index, bool inuse = true)
        {
            logger.EnterMethod();
            try
            {
                var countAllEmp = this._staffRepository.Count();
                if (countAllEmp <= this.empPerPage)
                {
                    logger.Info("All servicesfit a page. Get all services to a page");
                    return this.GetAll();
                }
                else
                {
                    var numberOfPages = CountTotalPages();
                    logger.Info("Found: [" + countAllEmp.ToString() + "] services fit [" + numberOfPages.ToString() + "] pages");
                    if (index > numberOfPages)
                    {
                        logger.Error("Can't access page: [" + index.ToString() + "]. Out of range paging");
                        // Get all services, skip [servicesPerPage * (numberOfPages - 1)] records and take servicesPerPage records after
                        if(inuse == true)
                            return (from emps in this._staffRepository.GetAll()
                                    where emps.IsInUse == true
                                    select emps).OrderBy(_ => _.Id).Skip((numberOfPages - 1) * empPerPage).Take(empPerPage);
                        else
                            return (from emps in this._staffRepository.GetAll()
                                    select emps).OrderBy(_ => _.Id).Skip((numberOfPages - 1) * empPerPage).Take(empPerPage);
                    }
                    else
                    {
                        logger.Info("Getting query to showing employee of page: [" + index.ToString() + "] in [" + numberOfPages.ToString() + "] pages");
                        // Get all services, skip [index - 1 * servicesPerPage] records and take servicesPerPage records after
                        if(inuse == true)
                            return (from emps in this._staffRepository.GetAll()
                                    where emps.IsInUse == true
                                    select emps).OrderBy(_ => _.Id).Skip((index - 1) * empPerPage).Take(empPerPage);
                        else
                            return (from emps in this._staffRepository.GetAll()
                                    select emps).OrderBy(_ => _.Id).Skip((index - 1) * empPerPage).Take(empPerPage);
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

        public int CountAllStaff()
        {
            logger.EnterMethod();
            try
            {
                var count = this._staffRepository.Count();
                if (count > 0)
                {
                    logger.Info("Found: [" + count.ToString() + "] employees");
                    return count;
                }
                else
                {
                    logger.Info("Can't find any employee");
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
                var allPages = (int)(this.CountAllStaff() / this.empPerPage);
                allPages = ((double)this.CountAllStaff() / (double)this.empPerPage) != (allPages * 1.0) ? allPages += 1 : allPages;
                logger.Info("Calculate [" + allPages.ToString() + "] pages employee");
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

        public Staff GetLast()
        {
            logger.EnterMethod();
            try
            {
                var emp = this.GetAll().OrderByDescending(_ => _.Id).FirstOrDefault();
                if(emp != null)
                    logger.Info("Found employee with Id: [" + emp.Id +"]");
                return emp;
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
