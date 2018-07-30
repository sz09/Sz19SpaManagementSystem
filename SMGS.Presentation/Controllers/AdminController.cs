using Service.Business.IServices;
using SMGS.Presentation.ViewModel.VM;
using SMGS.Presentation.ViewModel.VM_Convert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using log4net;
using System.Web.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Infrastructure.Logging;
using System.Web.Services;
using System.Globalization;
using SMGS.Presentation.Classes;
using SMGS.Presentation.CustomAttribute;

namespace SMGS.Presentation.Controllers
{
    [AllowAnonymous]
    [AuthorizeRoles(nameof(Enums.Account.Admin)), AuthorizeRoles(nameof(Enums.Account.SupperAdmin))]
    public class AdminController : Controller
    {
        #region Attributes
        private readonly IStaffServices _iStaffServices;
        private readonly IContactInformationServices _iContactInformationServices;
        private readonly IServiceServices _iServiceServices;
        private readonly ISalaryServices _iSalaryServices;
        private readonly ICustomerServices _iCustomerServices;
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
            IStockServices iStockServices,
            ICustomerServices iCustomerServices)
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
            this._iCustomerServices = iCustomerServices;
            logger.Info("Success set value to attributes");

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
                return View();
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
                return View(newEmp);
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
        public ActionResult CreateCustomer()
        {
            logger.EnterMethod();
            var newCus = new VM_Customer();
            try
            {
                newCus.CustomerCode = this._iCustomerServices.CreateNewCode();
                return View(newCus);
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
        [WebMethod]
        public ActionResult GetCustomerCode()
        {
            var code = this._iCustomerServices.CreateNewCode();
            return Json(code);
        }
        
        [WebMethod]
        public ActionResult CreateNewCustomerFromBooking(string code, string firstname, string lastname, string summary)
        {
            var vM_Customer = new VM_Customer
            {
                CustomerCode = code,
                FirstName = firstname,
                LastMiddle = lastname,
                Summary = summary,
                Image = "[nullimage]"
            };

            var id = this._iCustomerServices.InsertCustomerReturnId(ConvertVM.VMCustomer_To_Customer(vM_Customer));
            if (id > 0)
            {
                return Json(new
                {
                    id = id,
                    name = vM_Customer.FullName
                });
            }
            return Json(-1);
        }



        /// <summary>
            /// Go to Service for admin
            /// </summary>
            /// <param name="language">Language pass by user, default is English</param>
            /// <returns>
            /// Page fit language.
            ///     If not found langue in settings, return page Error.
            /// </returns>
        public ActionResult AdminServices(string language = "en", int page = 1)
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

        public ActionResult CreateService()
        {
            VM_CreateService vM_CreateService = new VM_CreateService();
            vM_CreateService.ServiceCode = this._iServiceServices.CreateNewCode();
            var languges = this._iReferenceServices.GetAllLanguage();
            vM_CreateService.Languages = ConvertVM.Language_To_VMLanguage(languges);
            return View(vM_CreateService);
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
                var emp = new VM_StaffInformation() {
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

                if (insertSuccess) // Insert new employee success
                {
                    var contactForId = this._iContactInformationServices.GetContactForId(Enums.ContactFor.Staff.ToString());
                    var contactTypeId = this._iContactInformationServices.GetContactTypeId(Enums.ContactType.Address.ToString());
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
        /// This fucntion call by Javascript to update salary for an employee
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="salary"></param>
        /// <returns></returns>
        [WebMethod]
        public ActionResult UpdateSalaryForEmployee(long empId, decimal salary)
        {
            var isOk = this._iStaffServices.UpdateSalaryForStaff(empId, salary);
            return Json(isOk);
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
                    var allSalaries = ConvertVM.Salary_To_VMEmpSalaryPaying(this._iSalaryServices.GetSalariesForEmployee(empId).ToList());

                    foreach (var salary in allSalaries.ListEmpPaying)
                    {
                        var isPaid = salary.IsPaid;
                        rsl += "<tr>" +
                                    "<td>" +
                                        "<select class='input-sm disabled' data-val='true' data-val-required='The IsPaid field is required.' id='SalaryPaying_ListEmpPaying_0__IsPaid' name='SalaryPaying.ListEmpPaying[0].IsPaid'>" +
                                            "<option " + (isPaid ? "selected = 'selected'" : "") + "  value='true'>Paid</option>" +
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

        /// <summary>
        /// Get history paid salary for an employee
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get all bookings 
        /// </summary>
        /// <returns></returns>
        public ActionResult Bookings(string lang = "en")
        {
            logger.EnterMethod();
            string language = WebConfigurationManager.AppSettings[lang];
            try
            {
                VM_ListBooking vM_ListBooking = new VM_ListBooking();
                List<VM_Booking> vM_Bookings = new List<VM_Booking>();
                List<VM_Bed> vM_Beds = ConvertVM.Bed_To_VMBed(this._iBedServices.GetAll());
                foreach (var item in vM_Beds)
                {
                    item.TimePeriod = ConvertVM.TimePeriod_To_VMTimePeriod(this._iBedServices.GetTimePeriodsForBed(item.Id));
                    item.Name = this._iBedServices.GetBedName(item.Id, language);
                }
                vM_ListBooking.Bookings = ConvertVM.Bills_To_VMBookings(this._iBookingServices.GetAll());
                vM_ListBooking.Bookings = ConvertVM.Bills_To_VMBookings(this._iBookingServices.GetAll());
                vM_ListBooking.Beds = vM_Beds;
                return View(vM_ListBooking);
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
        
        [WebMethod]
        public ActionResult PerformBooking(int bedId, long customerId, long staffId,int yearfrom, int monthfrom, int dayfrom, int hoursfrom, int minutesfrom, int yearto, int monthto, int dayto, int hoursto,int minutesto, decimal cost)
        {
            var vM_Book = new VM_Book()
            {
                BedId = bedId,
                CustomerId = customerId,
                PeriodFrom = new DateTime(yearfrom, monthfrom, dayfrom, hoursfrom, minutesfrom, 0, 0),
                PeriodTo = new DateTime(yearto, monthto, dayto, hoursto, minutesto, 0, 0),
                StaffId = staffId,
                TotalCost = cost
            };
            var check = this._iBookingServices.Booking(ConvertVM.VMBook_To_Bill(vM_Book));
            return Json(check);
        }
        public ActionResult Booking(int bedId)
        {
            var bookings = this._iBookingServices.GetBillBed(bedId, false).ToList();
            VM_BookingByBed vM_BookingByBed = new VM_BookingByBed();
            vM_BookingByBed.BedId = bedId;
            vM_BookingByBed.Bookings = ConvertVM.Bill_To_VMBookingByBedInformationRow(bookings);
          
            return View(vM_BookingByBed);
        }

        public ActionResult Beds()
        {
            return View();
        }
        public ActionResult CreateBed()
        {
            VM_CreateBed vM_CreateBed = new VM_CreateBed();
            vM_CreateBed.BedCode = this._iBedServices.CreateNewCode();
            var languges = this._iReferenceServices.GetAllLanguage();
            vM_CreateBed.Languages = ConvertVM.Language_To_VMLanguage(languges);
            return View(vM_CreateBed);
        }
        [WebMethod]
        public ActionResult PerformCreateBed(string code)
        {
            VM_Bed bed = new VM_Bed
            {
                BedCode = code
            };
            var id = this._iBedServices.CreateNewBedReturnId(ConvertVM.VMBed_To_Bed(bed));

            return Json(id);
        }

        [WebMethod]
        public ActionResult PerformCreateService(string code, int hours, int minutes, decimal cost, bool isActive)
        {
            VM_Service service = new VM_Service
            {
               ServiceCode = code,
               TimeCost = new VM_Time(hours, minutes),
               Cost = cost,
               IsInUse = isActive
            };
            var id = this._iServiceServices.CreateNewServiceReturnId(ConvertVM.VMService_To_Service(service));
            return Json(id);
        }

        [WebMethod]
        public ActionResult PerformAddBedNameInLanguage(int bedId, string value, int languageId)
        {
            VM_BedName bedName = new VM_BedName
            {
                Name = value,
                LanguageId = languageId,
                BedId = bedId
            };
            var id = this._iBedServices.AddNameForBed(ConvertVM.VMBedName_To_BedName(bedName));

            return Json(id);
        }

        [WebMethod]
        public ActionResult PerformAddServiceNameInLanguage(int serviceId, string value, int languageId)
        {
            VM_ServiceName serviceName = new VM_ServiceName
            {
                Name = value,
                LanguageId = languageId,
                ServiceId = serviceId
            };
            var id = this._iServiceServices.AddNameForService(ConvertVM.VMServiceName_To_ServiceName(serviceName));
        
            return Json(id);
        }
        [WebMethod]
        public ActionResult GetAllLanguages()
        {
            var languages = this._iReferenceServices.GetAllLanguage();
            var rtJSon = "[";
            foreach (var language in languages)
            {
                rtJSon += "{\"id\": " + language.Id + ",\"value\": \"" + language.Value + "\"}";
                if (language != languages[languages.Count - 1])
                    rtJSon += ",";
            }

            rtJSon += "]";
    
            return Json(rtJSon);
        }
        [WebMethod]
        public ActionResult GetOthersLanguages(int[] ids)
        {
            var languages = this._iReferenceServices.GetAllLanguage();
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var language = languages.Where(_ => _.Id == id).FirstOrDefault();
                    if (language != null)
                        languages.Remove(language);
                }
            }

            var rtJSon = "[";
            foreach (var language in languages)
            {
                rtJSon += "{\"id\": " + language.Id + ",\"value\": \"" + language.Value + "\"}";
                if (language != languages[languages.Count - 1])
                    rtJSon += ",";
            }

            rtJSon += "]";
    
            return Json(rtJSon);
        }
        public ActionResult BookNew(int bedId, string language = "en")
        {
            logger.EnterMethod();
            try
            {
                language = WebConfigurationManager.AppSettings[language];
                var newBooking = new VM_NewBooking();
                var services = ConvertVM.Service_To_VMService(this._iServiceServices.GetAll());
                foreach (var item in services)
                {
                    item.Name = this._iServiceServices.GetServiceNameByLanguage(item.Id, language);
                }
                newBooking.Services = services;
                newBooking.Customers = ConvertVM.Customer_To_VMCustomer(this._iCustomerServices.GetAll().ToList());
                newBooking.BedId = bedId;
                newBooking.BedName = this._iBedServices.GetBedName(bedId, language);
                return View(newBooking);
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
        [WebMethod]
        public JsonResult GetCodeForService(string ids)
        {
            var codes = "";
            var listIds = ids.Split(',').ToList<string>();
            foreach (var id in listIds)
            {
                int idSer;
                if (int.TryParse(id, out idSer))
                    codes += "[" + this._iServiceServices.GetServiceCodeById(idSer).Trim() + "]";
            }
            return Json(codes);
        }
        [WebMethod]
        public JsonResult GetTimeForService(string ids)
        {
            var time = "";
            var timeCost = 0;
            var listIds = ids.Split(',').ToList<string>();
            foreach (var id in listIds)
            {
                int idSer;
                if (int.TryParse(id, out idSer))
                    timeCost += this._iServiceServices.GetTotalTimeUseServices(idSer);
            }
            int hours = 0, minutes = 0;
            hours = timeCost / 60;
            minutes = timeCost % 60;
            time = hours + "h " + minutes + "m";
            return Json(time);
        }
        [WebMethod]
        public JsonResult GetCostForService(string ids)
        {
            var vnd = "";
            decimal cost = 0;
            var listIds = ids.Split(',').ToList<string>();
            foreach (var id in listIds)
            {
                int idSer;
                if (int.TryParse(id, out idSer))
                    cost += this._iServiceServices.GetTotalCostServices(idSer);
            }
            vnd = cost + "VND";
            return Json(vnd);
        }
        
        [WebMethod]
        public JsonResult CheckBedFree(int bedId, DateTime from, DateTime to)
        {
            var check = false;
            this._iBedServices.CheckBedFree(bedId, from, to);
            return Json(check);
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