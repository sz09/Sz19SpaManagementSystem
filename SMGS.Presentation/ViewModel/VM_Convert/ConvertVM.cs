using Service.Business.IServices;
using SMGS.Presentation.ViewModel.VM;
using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using Infrastructure.Logging;

namespace SMGS.Presentation.ViewModel.VM_Convert
{
    /// <summary>
    /// Convert type Entities to ViewModel 
    ///     And Convert type ViewModel to Entities
    /// </summary>
    internal class ConvertVM
    {
       
        private static readonly ILog logger = LogManager.GetLogger(typeof(ConvertVM));
        internal static VM_StaffInformation Staff_To_VMStaffInformation(Staff staff)
        {
            logger.EnterMethod();
            try
            {
                return new VM_StaffInformation()
                {
                    Id = staff.Id,
                    FirstName = staff.FirstName,
                    LastMiddle = staff.LastMiddle,
                    IdentifierNumber = staff.IdentifierNumber,
                    Image = staff.Image,
                    IsInUse = staff.IsInUse,
                    Salary = staff.Salary,
                    StaffCode = staff.StaffCode,
                    Summary = staff.Summary
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new VM_StaffInformation();
            }
            finally
            {
                logger.LeaveMethod();
            }

        }

        internal static Staff VMStaffInformation_To_Staff(VM_StaffInformation vM_StaffInformation) 
        {
            logger.EnterMethod();
            try
            {
                return new Staff()
                {
                    Id = vM_StaffInformation.Id,
                    FirstName = vM_StaffInformation.FirstName,
                    LastMiddle = vM_StaffInformation.LastMiddle,
                    IdentifierNumber = vM_StaffInformation.IdentifierNumber,
                    Image = vM_StaffInformation.Image,
                    IsInUse = vM_StaffInformation.IsInUse,
                    Salary = vM_StaffInformation.Salary,
                    StaffCode = vM_StaffInformation.StaffCode,
                    Summary = vM_StaffInformation.Summary
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new Staff();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        internal static VM_ContactInformation ContactInformation_To_VMContactInformation(ContactInformation contactInformation) 
        {
            try
            {
                return new VM_ContactInformation()
                {
                    Id = contactInformation.Id,
                    AddressId = contactInformation.AddressId,
                    ContactForId = contactInformation.ContactForId,
                    ContactTypeId = contactInformation.ContactTypeId,
                    EAdressId = contactInformation.EAdressId,
                    IsInUse = contactInformation.IsInUse,
                    PersonId = contactInformation.PersonId,
                    Address = Address_To_VMAddress(contactInformation.Address),
                    ContactFor = ContactFor_To_VMContactFor(contactInformation.ContactFor),
                    ContactType = ContactType_To_VMContactType(contactInformation.ContactType),
                    EAddress = EAddress_To_VMEAddress(contactInformation.EAddress)
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new VM_ContactInformation() {
                    Address = new VM_Address()
                    {
                        AddressNumberNoAndStreet = "[Not set]",
                        District = new VM_District()
                        {
                            DistrictName = "[Not set]",
                            Province = new VM_Province() { 
                                ProvinceName = "[Not set]",
                                Country = new VM_Country()
                                {
                                    CountryName = "[Not set]"
                                }
                            }
                        }
                    }
                };
            }
        }

        internal static VM_Address Address_To_VMAddress(Address address)
        {
            logger.EnterMethod();
            try
            {
                return new VM_Address()
                {
                    Id = address.Id,
                    AddressNumberNoAndStreet = address.AddressNumberNoAndStreet,
                    District = District_To_VMDistrict(address.District),
                    DistrictId = address.DistrictId
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new VM_Address();
            }
            finally
            {
                logger.LeaveMethod();
            }

        }
        internal static List<VM_Address> Address_To_VMAddress(List<Address> addresses)
        {
            logger.EnterMethod();
            try
            {
                List<VM_Address> vM_Addresses = new List<VM_Address>();
                foreach (var address in addresses)
                {
                    vM_Addresses.Add(Address_To_VMAddress(address));
                };
                return vM_Addresses;
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new List<VM_Address>();
            }
            finally
            {
                logger.LeaveMethod();
            }

        }

        internal static VM_District District_To_VMDistrict(District district)
        {
            logger.EnterMethod();
            try
            {
                return new VM_District()
                {
                    DistrictName = district.DistrictName,
                    Id = district.Id,
                    Province = Province_To_VMProvince(district.Province),
                    ProvinceId = district.ProvinceId
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new VM_District();
            }
            finally
            {
                logger.LeaveMethod();
            }

        }

        internal static VM_Province Province_To_VMProvince(Province province)
        {
            logger.EnterMethod();
            try
            {
                return new VM_Province()
                {
                    Id = province.Id,
                    ProvinceName = province.ProvinceName,
                    Country = Country_To_VMCountry(province.Country),
                    CountryId = province.CountryId
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new VM_Province();
            }
            finally
            {
                logger.LeaveMethod();
            }

        }

        internal static VM_Country Country_To_VMCountry(Country country)
        {
            logger.EnterMethod();
            try
            {
                return new VM_Country()
                {
                    Id = country.Id,
                    CountryName = country.CountryName
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new VM_Country();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }


        internal static VM_EAddress EAddress_To_VMEAddress(EAddress eAddress)
        {
            logger.EnterMethod();
            try
            {
                return new VM_EAddress()
                {
                    Id = eAddress.Id,
                    Email = eAddress.Email,
                    NumberPhone = eAddress.NumberPhone,
                    StaffId = eAddress.StaffId,
                    Website = eAddress.Website
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new VM_EAddress();
            }
            finally
            {
                logger.LeaveMethod();
            }

        }

        internal static EAddress VMEAddress_To_EAddress(VM_EAddress eAddress)
        {
            logger.EnterMethod();
            try
            {
                return new EAddress()
                {
                    Id = eAddress.Id,
                    Email = eAddress.Email,
                    NumberPhone = eAddress.NumberPhone,
                    StaffId = eAddress.StaffId,
                    Website = eAddress.Website
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new EAddress();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        internal static VM_ContactFor ContactFor_To_VMContactFor(ContactFor contactFor)
        {
            logger.EnterMethod();
            try
            {
                return new VM_ContactFor()
                {
                    Id = contactFor.Id,
                    StaffOrCustomer = contactFor.StaffOrCustomer
                };
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

        internal static VM_ContactType ContactType_To_VMContactType (ContactType contactType)
        {
            logger.EnterMethod();
            try
            {
                return new VM_ContactType()
                {
                    Id = contactType.Id,
                    ContactTypeName = contactType.ContactTypeName
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new VM_ContactType();
            }
            finally
            {
                logger.LeaveMethod();
            }

        }

        internal static ContactInformation VMContactInformation_To_ContactInformation(VM_ContactInformation vM_ContactInformation)
        {
            logger.EnterMethod();
            try
            {
                return new ContactInformation()
                {
                    Id = vM_ContactInformation.Id,
                    Address = VMAddress_To_Address(vM_ContactInformation.Address),
                    AddressId = vM_ContactInformation.AddressId,
                    EAdressId = vM_ContactInformation.EAdressId,
                    ContactForId = vM_ContactInformation.ContactForId,
                    ContactTypeId = vM_ContactInformation.ContactTypeId,
                    IsInUse = vM_ContactInformation.IsInUse,
                    PersonId = vM_ContactInformation.PersonId,
                    EAddress = VMEAddress_To_EAddress(vM_ContactInformation.EAddress)
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new ContactInformation();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

       internal static Address VMAddress_To_Address(VM_Address vM_Address)
        {
            logger.EnterMethod();
            try
            {
                return new Address()
                {
                    AddressNumberNoAndStreet = vM_Address.AddressNumberNoAndStreet,
                    Id = vM_Address.Id,
                    District = VMDistrict_To_District(vM_Address.District),
                    DistrictId = vM_Address.DistrictId
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new Address();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        internal static District VMDistrict_To_District(VM_District vM_District)
        {
            logger.EnterMethod();
            try
            {
                return new District()
                {
                    DistrictName = vM_District.DistrictName,
                    Id = vM_District.Id,
                    Province = VMProvince_To_Province(vM_District.Province),
                    ProvinceId = vM_District.ProvinceId
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new District();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        internal static Province VMProvince_To_Province(VM_Province vM_Province)
        {
            logger.EnterMethod();
            try
            {
                return new Province()
                {
                    ProvinceName = vM_Province.ProvinceName,
                    Id = vM_Province.Id,
                    Country = VMCountry_To_Country(vM_Province.Country),
                    CountryId = vM_Province.CountryId
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new Province();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        internal static Country VMCountry_To_Country(VM_Country vM_Country)
        {
            logger.EnterMethod();
            try
            {
                return new Country()
                {
                    Id = vM_Country.Id,
                    CountryName = vM_Country.CountryName
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new Country();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        internal static IEnumerable<VM_ContactInformation> ListContactInformation_To_ListVMContactInformation(IEnumerable<ContactInformation> listContactInformation)
        {
            logger.EnterMethod();
            try
            {
                var listVMContactInformation = new List<VM_ContactInformation>();
                foreach (var item in listContactInformation)
                {
                    listVMContactInformation.Add(ContactInformation_To_VMContactInformation(item));
                }
                return listVMContactInformation;
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new List<VM_ContactInformation>();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        internal static VM_Staff Staff_To_VMStaff(Staff staff)
        {
            logger.EnterMethod();
            try
            {
                return new VM_Staff()
                {
                    Id = staff.Id,
                    FirstName = staff.FirstName,
                    LastMiddle = staff.LastMiddle,
                    StaffCode = staff.StaffCode,
                    Salary = staff.Salary
                };
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
        internal static List<VM_Staff> Staff_To_VMStaff(List<Staff> staffes)
        {
            List<VM_Staff> vM_Staffs = new List<VM_Staff>();
            foreach (var emp in staffes)
            {
                vM_Staffs.Add(Staff_To_VMStaff(emp));
            }
            return vM_Staffs;
        }

        internal static VM_ListStaff ListStaff_To_VM_ListStaff(IEnumerable<Staff> listStaff)
        {
            logger.EnterMethod();
            try
            {
                VM_ListStaff vM_ListStaff = new VM_ListStaff();
                foreach (var item in listStaff)
                {
                    var staff = Staff_To_VMStaff(item);
                    vM_ListStaff.AllStaff.Add(staff);
                }
                return vM_ListStaff;
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new VM_ListStaff();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }


        internal static VM_StaffInformation VMStaff_Add_VM_ContactInformation_To_VMStaffInformation(Staff staff, ContactInformation contactInformation)
        {
            logger.EnterMethod();
            try
            {
                return new VM_StaffInformation()
                {
                    Id = staff.Id,
                    ContactInformation = ContactInformation_To_VMContactInformation(contactInformation),
                    FirstName = staff.FirstName,
                    LastMiddle = staff.LastMiddle,
                    IdentifierNumber = staff.IdentifierNumber,
                    Image = staff.Image,
                    IsInUse = staff.IsInUse,
                    Salary = staff.Salary,
                    StaffCode = staff.StaffCode,
                    Summary = staff.Summary

                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new VM_StaffInformation();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        internal static VM_Service Service_To_VMService(SPMS.ObjectModel.Entities.Service service)
        {
            logger.EnterMethod();
            try
            {
                return new VM_Service()
                {
                    Id = service.Id,
                    Cost = service.Cost,
                    ServiceCode = service.ServiceCode,
                    TimeCost = new VM_Time(service.TimeCost / 60, service.TimeCost % 60),
                    IsInUse = service.IsInUse
                };
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new VM_Service();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        internal static List<VM_Service> Service_To_VMService(IEnumerable<SPMS.ObjectModel.Entities.Service> listServices)
        {
            logger.EnterMethod();
            try
            {
                var listVMServices = new List<VM_Service>();
                foreach (var item in listServices)
                {
                    listVMServices.Add(Service_To_VMService(item));
                }
                return listVMServices;
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new List<VM_Service>();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        internal static SPMS.ObjectModel.Entities.Service VMService_To_Service(VM_Service vM_Service)
        {
            logger.EnterMethod();
            try
            {
                return new SPMS.ObjectModel.Entities.Service()
                {
                    Id = vM_Service.Id,
                    Cost = vM_Service.Cost,
                    ServiceCode = vM_Service.ServiceCode.Trim(),
                    TimeCost = vM_Service.TimeCost.Hour * 60 + vM_Service.TimeCost.minute,
                    IsInUse = vM_Service.IsInUse
                };
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

        internal static IEnumerable<SPMS.ObjectModel.Entities.Service> VMService_To_Service(IEnumerable<VM_Service> vM_Service)
        {
            logger.EnterMethod();
            try
            {
                var listService = new List<SPMS.ObjectModel.Entities.Service>();
                foreach (var item in vM_Service)
                {
                    listService.Add(VMService_To_Service(item));
                }
                return listService;
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new List<SPMS.ObjectModel.Entities.Service>();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        internal static VM_ListServices ListServices_To_VMListServices(IEnumerable<VM_Service> ieServices, int thisPage, int totalPage)
        {
            logger.EnterMethod();
            try
            {
                return new VM_ListServices()
                {
                    ListServices = ieServices,
                    ThisPage = thisPage,
                    TotalPages = totalPage
                }; 
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new VM_ListServices();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        internal static VM_ListStaff VM_ListStaff_Add_Paging(VM_ListStaff vM_ListStaff, int thisPage, int totalPages)
        {
            logger.EnterMethod();
            try
            {
                vM_ListStaff.ThisPage = thisPage;
                vM_ListStaff.TotalPages = totalPages;
                return vM_ListStaff;
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return vM_ListStaff;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        internal static VM_Salary Staff_To_VMSalary(Staff staff)
        {
            logger.EnterMethod();
            try
            {
                return new VM_Salary()
                {
                    Id = staff.Id,
                    EmpCode = staff.StaffCode,
                    FirstName = staff.FirstName,
                    LastMiddle = staff.LastMiddle,
                    Salary = staff.Salary
                };
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
        internal static List<VM_Salary> Staff_To_VMSalary(IEnumerable<Staff> listStaff)
        {
            logger.EnterMethod();
            try
            {
                var listVMSalary = new List<VM_Salary>();
                foreach (var item in listStaff)
                {
                    listVMSalary.Add(Staff_To_VMSalary(item));
                }
                return listVMSalary;
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


        internal static VM_ListSalary ListVMSalaries_To_VM_ListSalary_Add_Paging(List<VM_Salary> listVMSalary, int thisPage, int totalPages)
        {
            return new VM_ListSalary()
            {
                ListSalary = listVMSalary,
                ThisPage = thisPage,
                TotalPages = totalPages
            };
        }

        internal static VM_SalaryPaying Salary_To_VMSalaryPaying(Salary salary)
        {
            logger.EnterMethod();
            try
            {

                return new VM_SalaryPaying()
                {
                    Id = salary.Id,
                    StaffId = salary.StaffId,
                    Time = salary.Time,
                    TimePay = salary.TimePay,
                    TotalSalary = salary.TotalSalary,
                    Description = salary.Description,
                    IsPaid = salary.IsPaid
                };
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

        internal static List<VM_SalaryPaying> Salary_To_VMSalaryPaying(IEnumerable<Salary> listSalaries)
        {
            logger.EnterMethod();
            try
            {
                List<VM_SalaryPaying> listVMSalaries = new List<VM_SalaryPaying>();
                foreach (var item in listSalaries)
                {
                    listVMSalaries.Add(Salary_To_VMSalaryPaying(item));
                }
                return listVMSalaries;
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

        internal static VM_AnEmpSalaries VMSalary_Add_VMEmpSalaryPaying_Add_History(VM_Salary vM_Salary, VM_EmpSalaryPaying vM_EmpSalaryPaying, int history)
        {
            logger.EnterMethod();
            try
            {
                return new VM_AnEmpSalaries()
                {
                    Emp_Salary = vM_Salary,
                    SalaryPaying = vM_EmpSalaryPaying,
                    History = history
                };
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
        internal static VM_EmpSalaryPaying Salary_To_VMEmpSalaryPaying(List<Salary> salaries)
        {
            logger.EnterMethod();
            try
            {
                VM_EmpSalaryPaying vM_EmpSalaryPaying = new VM_EmpSalaryPaying();
                foreach (var salary in salaries)
                {
                    vM_EmpSalaryPaying.ListEmpPaying.Add(new VM_SalaryPaying
                    {
                        Id = salary.Id,
                        Description = salary.Description,
                        IsPaid = salary.IsPaid,
                        StaffId = salary.StaffId,
                        Time = salary.Time,
                        TimePay = salary.TimePay,
                        TotalSalary = salary.TotalSalary
                    });
                }
                return vM_EmpSalaryPaying;
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new VM_EmpSalaryPaying();
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        internal static VM_Stock Stock_To_VMStock(Stock stock)
        {
            logger.EnterMethod();
            try
            {
                return new VM_Stock()
                {
                    Id = stock.Id,
                    Cost = stock.Cost
                };
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
        internal static List<VM_Stock> Stock_To_VMStock(List<Stock> stocks)
        {
            List<VM_Stock> vM_Stocks = new List<VM_Stock>();
            if (stocks == null) return vM_Stocks;
            foreach (var stock in stocks)
            {
                vM_Stocks.Add(Stock_To_VMStock(stock));
            }
            return vM_Stocks;
        }

        internal static Stock VMStock_To_Stock(VM_Stock vM_Stock)
        {
            logger.EnterMethod();
            try
            {
                return new Stock()
                {
                    Id = vM_Stock.Id,
                    Cost = vM_Stock.Cost
                };
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

        internal static VM_Booking Bill_To_VMBooking(Bills bill)
        {
            logger.EnterMethod();
            try
            {
                return new VM_Booking()
                {
                    Id = bill.Id,
                    BedId = bill.BedId,
                    CustomerId = bill.CustomerId,
                    IsPaid = bill.IsPaid,
                    PeriodFrom = bill.PeriodFrom,
                    PeriodTo = bill.PeriodTo,
                    StaffId = bill.StaffId,
                    Time = bill.Time,
                    TimePaid = bill.TimePaid,
                    TotalCost = bill.TotalCost
                };
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

        internal static IEnumerable<VM_Booking> Bills_To_VMBookings(IEnumerable<Bills> bills)
        {
            logger.EnterMethod();
            try
            {
                List<VM_Booking> VM_Booking = new List<VM_Booking>();
                foreach (var bill in bills)
                {

                    VM_Booking.Add(new VM_Booking()
                    {
                        Id = bill.Id,
                        BedId = bill.BedId,
                        CustomerId = bill.CustomerId,
                        IsPaid = bill.IsPaid,
                        PeriodFrom = bill.PeriodFrom,
                        PeriodTo = bill.PeriodTo,
                        StaffId = bill.StaffId,
                        Time = bill.Time,
                        TimePaid = bill.TimePaid,
                        TotalCost = bill.TotalCost
                    });
                }
                return VM_Booking;
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
        internal static Bills Bill_To_VMBooking(VM_Booking vM_Booking)
        {
            logger.EnterMethod();
            try
            {
                return new Bills()
                {
                    Id = vM_Booking.Id,
                    BedId = vM_Booking.BedId,
                    CustomerId = vM_Booking.CustomerId,
                    IsPaid = vM_Booking.IsPaid,
                    PeriodFrom = vM_Booking.PeriodFrom,
                    PeriodTo = vM_Booking.PeriodTo,
                    StaffId = vM_Booking.StaffId,
                    Time = vM_Booking.Time,
                    TimePaid = vM_Booking.TimePaid,
                    TotalCost = vM_Booking.TotalCost
                };
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

        internal static Bills  VMBooking_To_Bill(VM_Booking bill)
        {
            logger.EnterMethod();
            try
            {
                return new Bills()
                {
                    Id = bill.Id,
                    BedId = bill.BedId,
                    CustomerId = bill.CustomerId,
                    IsPaid = bill.IsPaid,
                    PeriodFrom = bill.PeriodFrom,
                    PeriodTo = bill.PeriodTo,
                    StaffId = bill.StaffId,
                    Time = bill.Time,
                    TimePaid = bill.TimePaid,
                    TotalCost = bill.TotalCost
                };
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

        internal static IEnumerable<Bills> VMBills_To_Bookings(IEnumerable<VM_Booking> vM_Bookings)
        {
            logger.EnterMethod();
            try
            {
                List<Bills> bills = new List<Bills>();
                foreach (var vM_Booking in vM_Bookings)
                {

                    bills.Add( new Bills()
                    {
                        Id = vM_Booking.Id,
                        BedId = vM_Booking.BedId,
                        CustomerId = vM_Booking.CustomerId,
                        IsPaid = vM_Booking.IsPaid,
                        PeriodFrom = vM_Booking.PeriodFrom,
                        PeriodTo = vM_Booking.PeriodTo,
                        StaffId = vM_Booking.StaffId,
                        Time = vM_Booking.Time,
                        TimePaid = vM_Booking.TimePaid,
                        TotalCost = vM_Booking.TotalCost
                    });
                }
                return bills;
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

        internal static VM_Bed Bed_To_VMBed(Bed bed)
        {
            logger.EnterMethod();
            try
            {
                return new VM_Bed()
                {
                    BedCode = bed.BedCode,
                    Id = bed.Id
                };
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
        internal static Bed VMBed_To_Bed(VM_Bed vM_Bed)
        {
            logger.EnterMethod();
            try
            {
                return new Bed()
                {
                    BedCode = vM_Bed.BedCode,
                    Id = vM_Bed.Id
                };
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
        internal static BedName VMBedName_To_BedName(VM_BedName vM_BedName)
        {
            logger.EnterMethod();
            try
            {
                return new BedName()
                {
                    Name = vM_BedName.Name,
                    LanguageId = vM_BedName.LanguageId,
                    BedId = vM_BedName.BedId
                };
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
        internal static VM_BedName BedName_To_VMBedName(BedName bedName)
        {
            logger.EnterMethod();
            try
            {
                return new VM_BedName()
                {
                    Name = bedName.Name,
                    LanguageId = bedName.LanguageId,
                    BedId = bedName.BedId
                };
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

        internal static List<VM_Bed> Bed_To_VMBed(IEnumerable<Bed> beds)
        {
            logger.EnterMethod();
            try
            {
                List<VM_Bed> vM_Beds = new List<VM_Bed>();
                foreach (var bed in beds)
                {
                    vM_Beds.Add(
                    new VM_Bed()
                    {
                        BedCode = bed.BedCode,
                        Id = bed.Id
                    });
                }
                return vM_Beds;
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
        internal static VM_TimePeriod TimePeriod_To_VMTimePeriod(TimePeriod timePeriod)
        {
            return new VM_TimePeriod
            {
                From = timePeriod.From,
                To = timePeriod.To
            };
        }
        internal static List<VM_TimePeriod> TimePeriod_To_VMTimePeriod(List<TimePeriod> timePeriods)
        {
            List<VM_TimePeriod> vM_TimePeriods = new List<VM_TimePeriod>();

            foreach (var timePeriod in timePeriods)
            {
                vM_TimePeriods.Add(new VM_TimePeriod
                {
                    From = timePeriod.From,
                    To = timePeriod.To
                });
            }
            return vM_TimePeriods;
        }

        internal static VM_SalaryInfo Salary_To_VMSalaryInfo(Salary salary)
        {
            return new VM_SalaryInfo
            {
                Id = salary.Id,
                Salary = salary.TotalSalary,
                TimePaid = salary.TimePay ?? new DateTime(),
                Description = salary.Description,
                FirstName = salary.Staff.FirstName,
                EmpCode = salary.Staff.StaffCode,
                LastMiddle = salary.Staff.LastMiddle,
                CurrencyCode = "VND"
            };
        }
        internal static List<VM_SalaryInfo> Salary_To_VMSalaryInfo(List<Salary> salaries)
        {
            var vM_SalaryInfos = new List<VM_SalaryInfo>();
            foreach (var item in salaries)
            {
                vM_SalaryInfos.Add(Salary_To_VMSalaryInfo(item));
            }
            return vM_SalaryInfos;
        }

        internal static VM_Customer Customer_To_VMCustomer(Customer customer)
        {
            return new VM_Customer
            {
                CustomerCode = customer.CustomerCode,
                FirstName = customer.FirstName,
                Id = customer.Id,
                Image = customer.Image,
                LastMiddle = customer.LastMiddle,
                Summary = customer.Summary
            };
        }
        internal static Customer VMCustomer_To_Customer(VM_Customer vM_Customer)
        {
            return new Customer
            {
                CustomerCode = vM_Customer.CustomerCode,
                FirstName = vM_Customer.FirstName,
                Id = vM_Customer.Id,
                Image = vM_Customer.Image,
                LastMiddle = vM_Customer.LastMiddle,
                Summary = vM_Customer.Summary
            };
        }

        internal static List<VM_Customer> Customer_To_VMCustomer(List<Customer> customers)
        {
            List<VM_Customer> vM_Customers = new List<VM_Customer>();
            foreach (var customer in customers)
            {
                vM_Customers.Add(Customer_To_VMCustomer(customer));
            }
            return vM_Customers;
        }

        internal static VM_Search SearchResult_To_VMSearch(Tuple<List<Address>, List<Bed>, List<Customer>, List<SPMS.ObjectModel.Entities.Service>, List<Staff>, List<Stock>, int> searchResult)
        {
            return new VM_Search
            {
                ListAddress = Address_To_VMAddress(searchResult.Item1),
                ListBed = Bed_To_VMBed(searchResult.Item2),
                ListCustomer = Customer_To_VMCustomer(searchResult.Item3),
                ListEmployee = Staff_To_VMStaff(searchResult.Item5),
                ListService = Service_To_VMService(searchResult.Item4),
                ListStock = Stock_To_VMStock(searchResult.Item6),
                SearchResultsInSearch = searchResult.Item7
            };
        }
        internal static VM_Language Language_To_VMLanguage(Language language)
        {
            return new VM_Language
            {
                Id = language.Id,
                Value = language.Value
            };
        }
        internal static VM_ServiceName ServiceName_To_VMServiceName(ServiceName serviceName)
        {
            return new VM_ServiceName
            {
                Id = serviceName.Id,
                LanguageId = serviceName.LanguageId,
                Name = serviceName.Name,
                ServiceId = serviceName.ServiceId
            };
        }
        internal static List<VM_Language> Language_To_VMLanguage(List<Language> languages)
        {
            List<VM_Language> vM_Languages = new List<VM_Language>();
            foreach (var language in languages)
            {
                vM_Languages.Add(new VM_Language
                {
                    Id = language.Id,
                    Value = language.Value
                });
            }
            return vM_Languages;
        }
        internal static List<VM_ServiceName> ServiceName_To_VMServiceName(List<ServiceName> services)
        {
            List<VM_ServiceName> vM_ServiceNames = new List<VM_ServiceName>();
            foreach (var service in services)
            {
                vM_ServiceNames.Add(ServiceName_To_VMServiceName(service));
            }
            return vM_ServiceNames;
        }

        internal static ServiceName VMServiceName_To_ServiceName(VM_ServiceName service)
        {
            return new ServiceName
            {
                Id = service.Id,
                LanguageId = service.LanguageId,
                Name = service.Name,
                ServiceId = service.ServiceId
            };
        }
        internal static List<ServiceName> VMServiceName_To_ServiceName(List<VM_ServiceName> services)
        {
            List<ServiceName> vM_ServiceNames = new List<ServiceName>();
            foreach (var service in services)
            {
                vM_ServiceNames.Add(VMServiceName_To_ServiceName(service));
            }
            return vM_ServiceNames;
        }

        internal static Bills VMBook_To_Bill(VM_Book book)
        {
            return new Bills()
            {
                BedId = book.BedId,
                PeriodFrom = book.PeriodFrom,
                PeriodTo = book.PeriodTo,
                CustomerId = book.CustomerId,
                IsPaid = false,
                DetailsBills = new List<DetailsBill>(),
                StaffId = book.StaffId, 
                TotalCost = book.TotalCost
            };
        }

        internal static VM_BookingByBedInformationRow Bill_To_VMBookingByBedInformationRow(Bills bill)
        {
            return new VM_BookingByBedInformationRow
            {
                Id = bill.Id, 
                From = bill.PeriodFrom.HasValue ? bill.PeriodFrom.Value : DateTime.MinValue,
                To = bill.PeriodTo.HasValue ? bill.PeriodTo.Value : DateTime.MinValue,
                Cost = bill.TotalCost,
                IsPaid = bill.IsPaid,
                CustomerName = bill.Customer.LastMiddle + " " + bill.Customer.FirstName
            };
        }

        internal static VM_Booked Bill_To_VMooked(Bills bill)
        {
            return new VM_Booked
            {
                Id = bill.Id,
                PeriodFrom = bill.PeriodFrom,
                PeriodTo = bill.PeriodTo,
                Time = bill.Time,
                TotalCost = bill.TotalCost
            };
        }

        internal static List<VM_BookingByBedInformationRow> Bill_To_VMBookingByBedInformationRow(List<Bills> bills)
        {
            var rows = new List<VM_BookingByBedInformationRow>();
            foreach (var bill in bills)
            {
                rows.Add(Bill_To_VMBookingByBedInformationRow(bill));
            }
            return rows;
        }
    }
}