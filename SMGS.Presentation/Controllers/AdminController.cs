﻿using Service.Business.IServices;
using SMGS.Presentation.ViewModel.VM;
using SMGS.Presentation.ViewModel.VM_Convert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using System.Web.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Infrastructure.Logging;
using System.Web.Services;
using System.Globalization;

namespace SMGS.Presentation.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        #region Attributes
        private readonly IStaffServices _iStaffServices;
        private readonly IContactInformationServices _iContactInformationServices;
        private readonly IServiceServices _iServiceServices;
        private readonly ISalaryServices _iSalaryServices;
        private readonly IReferenceServices _iReferenceServices;
        private readonly INotificationServices _iNotificationServices;
        private readonly IBedServices _iBedServices;
        private readonly IBookingServices _iBookingServices;
        private readonly IStockServices _iStockServices;
        private static long accountLoggedIn = -1;
        private static readonly ILog logger = LogManager.GetLogger(typeof(AdminController));
        #endregion

        #region Constructors
        public AdminController(IStaffServices iStaffServices, 
            IContactInformationServices iContactInformationServices, 
            IServiceServices iServiceServices, 
            ISalaryServices iSalaryServices, 
            IReferenceServices iReferenceServices, 
            INotificationServices iNotificationServices,
            IBookingServices iBookingServices,
            IBedServices iBedServices,
            IStockServices iStockServices)
        {
            logger.EnterMethod();
            this._iStaffServices = iStaffServices;
            this._iContactInformationServices = iContactInformationServices;
            this._iServiceServices = iServiceServices;
            this._iSalaryServices = iSalaryServices;
            this._iReferenceServices = iReferenceServices;
            this._iNotificationServices = iNotificationServices;
            this._iBookingServices = iBookingServices;
            this._iBedServices = iBedServices;
            this._iStockServices = iStockServices;
            logger.Info("Success set value to attributes");


            //VM_Booking vM_Booking = new VM_Booking
            //{
            //    BedId = 1,
            //    CustomerId = 1,
            //    IsPaid = false,
            //    PeriodFrom = new DateTime(),
            //    PeriodTo = new DateTime(),
            //    StaffId = 1,
            //    Time = new DateTime(),
            //    TimePaid = null,
            //    TotalCost = 1000
            //};
            //bool a = this._iBookingServices.Booking(ConvertVM.VMBooking_To_Bill(vM_Booking));

            logger.LeaveMethod();
        }
        #endregion

        #region Operations
        /// <summary>
        /// Index view
        /// Main view for admin access
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return View("ErrorAdminPage");
            }
            finally
            {
                logger.LeaveMethod();
            }
            return View();
        }

        /// <summary>
        /// Go to user page
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Employee(int id = 1) 
        {
            logger.EnterMethod();
            try
            {
                var vM_Staff = this._iStaffServices.Get(id);
                var vM_ContactInformation = this._iContactInformationServices.Get(id);
                return PartialView("User", ConvertVM.VMStaff_Add_VM_ContactInformation_To_VMStaffInformation(vM_Staff, vM_ContactInformation));

            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return View("ErrorAdminPage");
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        /// <summary>
        /// Get all staff
        /// Convert to ListStaff in VM
        /// </summary>
        /// <param name="isInUse"></param>
        /// <returns></returns>
        public ActionResult Employees(int index = 1, bool inuse = true)
        {
            logger.EnterMethod();
            try
            {
                var allStaffes = this._iStaffServices.GetStaffByPage(index, inuse);
                var vM_ListStaff = ConvertVM.ListStaff_To_VM_ListStaff(allStaffes.ToList());

                List<long> listStaffId = new List<long>();
                foreach (var item in allStaffes)
                {
                    listStaffId.Add(item.Id);
                }
                var listContactInformation = ConvertVM.ListContactInformation_To_ListVMContactInformation(
                                                        this._iContactInformationServices.GetContactInformationFromListStaff(listStaffId));
                var allPages = this._iStaffServices.CountTotalPages();
                return View(ConvertVM.VM_ListStaff_Add_Paging(SelfConvertVM.VM_ListStaffAddContactInformation(listContactInformation, vM_ListStaff), index, allPages));
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");                
                return View("ErrorAdminPage");  
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        /// <summary>
        /// Go to CreateEmployee view
        ///     Generate employee code automatically
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateEmployee()
        {
            logger.EnterMethod();
            var newEmp = new VM_StaffInformation();
            try
            {
                newEmp.StaffCode = this._iStaffServices.CreateNewCode();
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return View("ErrorAdminPage");
            }
            finally
            {
                logger.LeaveMethod();
            }
            return View(newEmp);
        }

        /// <summary>
        /// Go to Service for admin
        /// </summary>
        /// <param name="language">Language pass by user, default is English</param>
        /// <returns>
        /// Page fit language.
        ///     If not found langue in settings, return page Error.
        /// </returns>
        public ActionResult ServicesAdmin(string language = "en", int page = 1)
        {
            logger.EnterMethod();
            try
            {
                string lang = WebConfigurationManager.AppSettings[language];
                if (String.IsNullOrEmpty(lang))
                {
                    logger.Error("Can't find value: [" + language + "] in Web.config");
                    return View("ErrorAdminPage");
                }
                else
                    logger.Info("Found value: [" + lang + "] for: [" + language + "] input");
                var allServices = this._iServiceServices.GetServiceByPage(page).ToList();
                var nameOfServices = new List<string>();
                foreach (var item in allServices)
	            {
                    nameOfServices.Add(this._iServiceServices.GetServiceNameByLanguage(item.Id, lang));
	            }
                logger.Info("Found [" + nameOfServices.Count() + "] names for [" + allServices.Count() + "] services");
                var listServices = SelfConvertVM.VM_ServiceAddName(ConvertVM.Service_To_VMService(allServices), nameOfServices);
                var ttPage = this._iServiceServices.CountTotalPages();
                return View(ConvertVM.ListServices_To_VMListServices(listServices, page, this._iServiceServices.CountTotalPages()));
            }
            catch (Exception ex)
            {
                logger.Error("Error: [" + ex.Message + "]");
                return View("ErrorAdminPage");
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        /// <summary>
        /// Submit add employee to database
        ///     Using ajax calling
        /// </summary>
        /// <param name="empCode">Employee's code</param>
        /// <param name="empFirstName">Employee's FirstName</param>
        /// <param name="empLastMiddle">Employee's Last and Middle Name</param>
        /// <param name="empIdentifierNumber">Employee's Identifier Number</param>
        /// <param name="empAddress">Employee's Address, st. name and number</param>
        /// <param name="empDistrict">Employee's District</param>
        /// <param name="empCity">Employee's City or Province</param>
        /// <param name="empCountry">Employee's Nation</param>
        /// <param name="empPhone">Employee's contact: Number phone</param>
        /// <param name="empEmail">Employee's contact: Email</param>
        /// <param name="empWebsite">Employee's contact: Website</param>
        /// <param name="empSummary">Summary about employee</param>
        /// <returns>Json result to ajax</returns>
        [HttpPost]
        public ActionResult SumitAddEmployee(string empCode, string empFirstName, string empLastMiddle, string empIdentifierNumber, string empAddress, string empDistrict, string empCity, string empCountry, string empPhone, string empEmail, string empWebsite, string empSummary)
        {
            logger.EnterMethod();
            try
            {
                var emp = new VM_StaffInformation(){
                    StaffCode = empCode,
                    FirstName = empFirstName,
                    LastMiddle = empLastMiddle,
                    IdentifierNumber = empIdentifierNumber,
                    Summary = empSummary,
                    IsInUse = true,
                    Image = "[nullimage]",
                    Salary = (decimal)0
                };
                var insertSuccess = this._iStaffServices.InsertEmployee(ConvertVM.VMStaffInformation_To_Staff(emp));

                if(insertSuccess) // Insert new employee success
                {
                    var contactForId = this._iContactInformationServices.GetContactForId("Staff");
                    var contactTypeId = this._iContactInformationServices.GetContactTypeId("Address");
                    var empId = this._iStaffServices.GetLast();
                    var contactInformation = new VM_ContactInformation()
                    {
                        Address = new VM_Address()
                        {
                            AddressNumberNoAndStreet = empAddress,
                            District = new VM_District()
                            {
                                DistrictName = empDistrict,
                                Province = new VM_Province()
                                {
                                    ProvinceName = empCity,
                                    Country = new VM_Country()
                                    {
                                        CountryName = empCountry
                                    }
                                }
                            }
                        },
                        ContactForId = contactForId,
                        ContactTypeId = contactTypeId,
                        EAddress = new VM_EAddress()
                        {
                            Email = empEmail,
                            NumberPhone = empPhone,
                            Website = empWebsite,
                            StaffId = empId.Id
                        },
                        IsInUse = true,
                        PersonId = empId.Id
                    };
                    var updateContactInformation = this._iContactInformationServices.CheckContactInformationExistingAndUpdateForPerson(ConvertVM.VMContactInformation_To_ContactInformation(contactInformation));
                    if(updateContactInformation)
                        return Json(JsonConvert.SerializeObject(JObject.Parse(@"{insertEmp: true, updateContactInformation: true, personid: '" + empId.Id + "'}")));
                    else
                    {
                        return Json(JsonConvert.SerializeObject(JObject.Parse(@"{insertEmp: true, updateContactInformation: false, personid: '" + empId.Id + "'}")));
                    }
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(JObject.Parse(@"{insertEmp: false}")));
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error: [" + ex.Message + "]");
                return Json(@"{insertEmp: false}");
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        /// <summary>
        /// Submit update employee to database
        ///     Using ajax calling
        /// </summary>
        /// <param name="empCode">Employee's code</param>
        /// <param name="empFirstName">Employee's FirstName</param>
        /// <param name="empLastMiddle">Employee's Last and Middle Name</param>
        /// <param name="empIdentifierNumber">Employee's Identifier Number</param>
        /// <param name="empAddress">Employee's Address, st. name and number</param>
        /// <param name="empDistrict">Employee's District</param>
        /// <param name="empCity">Employee's City or Province</param>
        /// <param name="empCountry">Employee's Nation</param>
        /// <param name="empPhone">Employee's contact: Number phone</param>
        /// <param name="empEmail">Employee's contact: Email</param>
        /// <param name="empWebsite">Employee's contact: Website</param>
        /// <param name="empSummary">Summary about employee</param>
        /// <returns>Json result to ajax</returns>
        [HttpPost]
        public ActionResult SubmitUpdateEmployee(int id, string empCode, string empFirstName, string empLastMiddle, string empIdentifierNumber, string empAddress, string empDistrict, string empCity, string empCountry, string empPhone, string empEmail, string empWebsite, string empSummary)
        {
            logger.EnterMethod();
            try
            {
                var emp = new VM_StaffInformation()
                {
                    Id = id,
                    StaffCode = empCode,
                    FirstName = empFirstName,
                    LastMiddle = empLastMiddle,
                    IdentifierNumber = empIdentifierNumber,
                    Summary = empSummary,
                    IsInUse = true,
                    Image = "[nullimage]",
                    Salary = (decimal)0
                };
                var insertSuccess = this._iStaffServices.UpdateEmployee(ConvertVM.VMStaffInformation_To_Staff(emp));

                if (insertSuccess) // Insert new employee success
                {
                    var contactForId = this._iContactInformationServices.GetContactForId("Staff");
                    var contactTypeId = this._iContactInformationServices.GetContactTypeId("Address");
                    var empId = this._iStaffServices.GetLast();
                    var contactInformation = new VM_ContactInformation()
                    {
                        Address = new VM_Address()
                        {
                            AddressNumberNoAndStreet = empAddress,
                            District = new VM_District()
                            {
                                DistrictName = empDistrict,
                                Province = new VM_Province()
                                {
                                    ProvinceName = empCity,
                                    Country = new VM_Country()
                                    {
                                        CountryName = empCountry
                                    }
                                }
                            }
                        },
                        ContactForId = contactForId,
                        ContactTypeId = contactTypeId,
                        EAddress = new VM_EAddress()
                        {
                            Email = empEmail,
                            NumberPhone = empPhone,
                            Website = empWebsite,
                            StaffId = empId.Id
                        },
                        IsInUse = true,
                        PersonId = empId.Id
                    };
                    var updateContactInformation = this._iContactInformationServices.CheckContactInformationExistingAndUpdateForPerson(ConvertVM.VMContactInformation_To_ContactInformation(contactInformation));
                    if (updateContactInformation)
                        return Json(JsonConvert.SerializeObject(JObject.Parse(@"{insertEmp: true, updateContactInformation: true, personid: '" + empId.Id + "'}")));
                    else
                    {
                        return Json(JsonConvert.SerializeObject(JObject.Parse(@"{insertEmp: true, updateContactInformation: false, personid: '" + empId.Id + "'}")));
                    }
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(JObject.Parse(@"{insertEmp: false}")));
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error: [" + ex.Message + "]");
                return Json(@"{insertEmp: false}");
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public ActionResult Salaries()
        {
            
            var vM_SalariesInMonth = new VM_SalariesInMonth();
            var months = this._iSalaryServices.GetCollectionMonthPaid();
            foreach (var item in months)
            {
                var salary = new VM_Salaries
                {
                    Time = item,
                    Salaries = ConvertVM.Salary_To_VMSalaryInfo(this._iSalaryServices.GetAllSalariesPaidInMonth(item))
                };
                vM_SalariesInMonth.MonthSalaries.Add(salary);
            }
            var a = vM_SalariesInMonth.MonthSalaries.Select(_ => _.Time.Year).Distinct();
            return View(vM_SalariesInMonth);
        }
        public ActionResult Salary(long empId, int month = -1, int year = -1)
        {
            logger.EnterMethod();
            try
            {
                if (month > 0 && month <= 12 && year > 0)
                {
                    DateTime date = new DateTime(year, month, 1);
                    var empSalaries = this._iSalaryServices.GetSalariesForStaffPreviewNearest(empId, date).ToList();
                    if (empSalaries != null)
                    {
                        var emp = this._iStaffServices.Get(empId);
                        var salaries = empSalaries.ToList();
                        var empCountAllSalaries = this._iSalaryServices.TotalSalariesInPast(empId);
                        var model = ConvertVM.VMSalary_Add_VMEmpSalaryPaying_Add_History(ConvertVM.Staff_To_VMSalary(emp),
                                                                                         new VM_EmpSalaryPaying()
                                                                                         {
                                                                                             ListEmpPaying = ConvertVM.Salary_To_VMSalaryPaying(salaries)
                                                                                         },
                                                                                         empCountAllSalaries);
                        return View(model);
                    }
                }
                else
                {
                    var empSalaries = this._iSalaryServices.GetSalariesForStaffPreviewNearest(empId);
                    if (empSalaries.ToList() != null)
                    {
                        var emp = this._iStaffServices.Get(empId);
                        var salaries = empSalaries.ToList();
                        var empCountAllSalaries = this._iSalaryServices.TotalSalariesInPast(empId);
                        var model = ConvertVM.VMSalary_Add_VMEmpSalaryPaying_Add_History(ConvertVM.Staff_To_VMSalary(emp),
                                                                                         new VM_EmpSalaryPaying()
                                                                                         {
                                                                                             ListEmpPaying = ConvertVM.Salary_To_VMSalaryPaying(salaries)
                                                                                         },
                                                                                         empCountAllSalaries);
                        return View(model);
                    }
                }
                return View("ErrorAdminPage");
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return View("ErrorAdminPage");
            }
            finally
            {
                logger.LeaveMethod();
            }

        }

        /// <summary>
        /// Get all information salary for employee
        /// </summary>
        /// <param name="empId">Employee Id</param>
        /// <param name="month">Month when get salaries</param>
        /// <param name="year">Year when get salaries</param>
        /// <returns></returns>
        [WebMethod]
        public ActionResult GetSalaryForEmployeeInMonth(long empId, int month = -1, int year = -1)
        {
            try
            {
                var rsl = "";
                if (month < 0 || month > 12 || year < 0)
                {
                    var allSalaries = ConvertVM.Salary_To_VMEmpSalaryPaying( this._iSalaryServices.GetSalariesForEmployee(empId).ToList());
                    
                    foreach (var salary in allSalaries.ListEmpPaying)
                    {
                        var isPaid = salary.IsPaid;
                        rsl += "<tr>" +
                                    "<td>" +
                                        "<select class='input-sm disabled' data-val='true' data-val-required='The IsPaid field is required.' id='SalaryPaying_ListEmpPaying_0__IsPaid' name='SalaryPaying.ListEmpPaying[0].IsPaid'>" +
                                            "<option " + (isPaid ? "selected = 'selected'": "" ) + "  value='true'>Paid</option>" +
                                            "<option " + (!isPaid ? "selected = 'selected'" : "") + "  value='false'>Un-paid</option>" +
                                        "</select>" +
                                    "</td>" +
                                    "< td >" + salary.TimePay + "</td>" +
                                    "<td>" + salary.TotalSalary + "</td>" +
                                    "<td>" + salary.Description + "</td>" +
                                "</tr>";
                    }
                }
                else
                {
                    var salaries = ConvertVM.Salary_To_VMEmpSalaryPaying(this._iSalaryServices.GetAllSalariesForEmployee(empId, month, year).ToList());
                    foreach (var salary in salaries.ListEmpPaying)
                    {
                        var isPaid = salary.IsPaid;
                        var d = decimal.Parse(salary.TotalSalary.ToString(), CultureInfo.InvariantCulture);
                        rsl += "<tr>" +
                                    "<td>" +
                                        "<select class='input-sm disabled' data-val='true' data-val-required='The IsPaid field is required.' id='" + salary.Id + "' name='" + salary.Id + "'>" +
                                            "<option " + (isPaid ? "selected = 'selected'" : "") + "  value='true'>Paid</option>" +
                                            "<option " + (!isPaid ? "selected = 'selected'" : "") + "  value='false'>Un-paid</option>" +
                                        "</select>" +
                                    "</td>" +
                                    "<td>" + salary.TimePay.Value.ToString("yyyy MMM") + "</td>" +
                                    "<td>" + d.ToString(CultureInfo.InvariantCulture) + "</td>" +
                                    "<td>" + salary.Description + "</td>" +
                                "</tr>";
                    }
                }
                return Json(rsl);
            }
            catch (Exception ex)
            {
                logger.Error("Error: [" + ex.Message + "]");
                return View("ErrorAdminPage");
            }
        }

        public ActionResult SalaryHistory(long empId)
        {
            logger.EnterMethod();
            try
            {
                var history = new VM_EmpSalaryPaying()
                {
                    ListEmpPaying = ConvertVM.Salary_To_VMSalaryPaying(this._iSalaryServices.GetSalariesForEmployee(empId))
                };
                foreach (var item in history.ListEmpPaying)
                {
                    item.SumDays = this._iSalaryServices.AttendanceInMonth(empId, item.Time);
                }
                var emp = this._iStaffServices.Get(empId);

                return View(history);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return View("ErrorAdminPage");
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public ActionResult FirstCountNotification()
        {
            logger.EnterMethod();
            try
            {
                int count = 0;
                count = this._iNotificationServices.CountNotificationForAccount(accountLoggedIn);
                return Json(count.ToString());
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

        public ActionResult Bookings()
        {
            logger.EnterMethod();
            try
            {
                VM_ListBooking vM_ListBooking = new VM_ListBooking();  
                List<VM_Booking> vM_Bookings = new List<VM_Booking>();
                List<VM_Bed> vM_Beds = ConvertVM.Bed_To_VMBed(this._iBedServices.GetAll());
                foreach (var item in vM_Beds)
                {
                    item.TimePeriod = ConvertVM.TimePeriod_To_VMTimePeriod(this._iBedServices.GetTimePeriodsForBed(item.Id));
                }
                vM_ListBooking.Bookings = ConvertVM.Bills_To_VMBookings(this._iBookingServices.GetAll());
                vM_ListBooking.Beds = vM_Beds;
                return View(vM_ListBooking);
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

        public  ActionResult CreateNewBooking(int bedId)
        {
            return View();
        }

        public  ActionResult UpdateBooking(int bookingId)
        {
            return View();
        }

        public ActionResult Stocks(int index = 1)
        {
            VM_ListStock vM_ListStock = new VM_ListStock();
            var listStock = this._iStockServices.GetStockPaging(index);
            vM_ListStock = new VM_ListStock { Stocks = ConvertVM.Stock_To_VMStock(listStock) };
            return View(vM_ListStock);
        }

        public ActionResult Search(string key)
        {
            var result = this._iReferenceServices.Search(key);
            VM_Search vM_Search = ConvertVM.SearchResult_To_VMSearch(result);
            return View(vM_Search);
        }

        public ActionResult Paging(int page = 1)
        {
            return View();
        }

        public ActionResult Original()
        {
            return View();
        }
        
        public ActionResult notifications() 
        {
            return View();
        }
        
        public ActionResult Update()
        {
            return View();
        }
        #endregion
	}   
}