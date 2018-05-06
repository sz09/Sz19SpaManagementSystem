using Core.ObjectServices.Repositories;
using SPMS.ObjectModel.Entities;
using System.Collections.Generic;
using log4net;
using Infrastructure.Logging;
using System;
using Core.ObjectServices.UnitOfWork;
using System.Transactions;

namespace Infrastructure.Data.Repositories
{
    public class ContactInformationRepository: IContactInformationRepository
    {
        #region Attributes
        private readonly IRepository<ContactInformation> _contactInformationRepositories;
        private readonly IRepository<EAddress> _eaddressRepositories;
        private readonly IRepository<Address> _addressRepositories;
        private readonly IRepository<District> _districtRepositories;
        private readonly IRepository<Province> _provinceRepositories;
        private readonly IRepository<Country> _countryRepositories;
        private readonly IRepository<ContactFor> _contactForReppositories;
        private readonly IRepository<ContactType> _contactTypeReppositories;
        private static readonly ILog logger = LogManager.GetLogger(typeof(ContactInformationRepository));
        private readonly IUnitOfWork _iUnitOfWork;
        #endregion

        #region Contructors
        public ContactInformationRepository(IRepository<ContactInformation> contactInformationRepositories, IRepository<Address> addressRepositories, IRepository<EAddress> eaddressRepositories, IRepository<District> districtRepositories, IRepository<Province> provinceRepositories, IRepository<Country> countryRepositories, IRepository<ContactFor> contactForReppositories, IRepository<ContactType> contactTypeReppositories,IUnitOfWork iUnitOfWork)
        {
            logger.EnterMethod();
            try
            {
                this._contactInformationRepositories = contactInformationRepositories;
                this._addressRepositories = addressRepositories;
                this._eaddressRepositories = eaddressRepositories;
                this._districtRepositories = districtRepositories;
                this._provinceRepositories = provinceRepositories;
                this._countryRepositories = countryRepositories;
                this._contactForReppositories = contactForReppositories;
                this._contactTypeReppositories = contactTypeReppositories;
                this._iUnitOfWork = iUnitOfWork;
                logger.Info("Success setting values for attributes");
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
        } 
        #endregion

        #region Operations
        public ContactInformation Get(long personId)
        {
            logger.EnterMethod();
            try
            {
                var contactInformation = this._contactInformationRepositories.Get(_ => _.PersonId == personId);
                return contactInformation;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
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
            var listContactInformation = new List<ContactInformation>();
            try
            {
                foreach (var item in listStaffId)
                {
                    var contactInformation = (this.Get(item) == null ? new ContactInformation() { Id = -1 } : this.Get(item));
                    listContactInformation.Add(contactInformation);
                }
                logger.Info("Found [" + listContactInformation.Count + "] contact information for: [" + ((List<long>)listStaffId).Count + "] people");
                return listContactInformation;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public bool UpdateContactInformationForStaff(ContactInformation contactInformation)
        {
            logger.EnterMethod();
            try
            {
                var contactInformationFound = this._contactInformationRepositories.Get(_ => _.PersonId == contactInformation.PersonId);
                if (contactInformation != null)
                {
                    logger.Info("Person with Id:[" + contactInformation.PersonId + "] already has contact information. Update contact information");
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        contactInformationFound = contactInformation;
                        this._contactInformationRepositories.Update(contactInformationFound);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Updated contact information for peron[" + contactInformation.PersonId + "]");
                    return true;
                }
                else
                {
                    logger.Info("Person with Id: [" + contactInformation.PersonId + "] hasn't contact information. Add contact information");
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._contactInformationRepositories.Add(contactInformation);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Added contact information for peron[" + contactInformation.PersonId + "]");
                    return true;
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

        public bool CheckContactInformationExistingAndUpdateForPerson(ContactInformation contactInformation)
        {
            logger.EnterMethod();
            try
            {
                #region Create EAddress
                var createEAddress = this.CreateEAddress(contactInformation.EAddress);
                EAddress eaddress = new EAddress();
                if (createEAddress) // Inserted EAddress
                {
                    eaddress = this.Get(contactInformation.EAddress.NumberPhone, contactInformation.EAddress.Email, contactInformation.EAddress.Website);
                    logger.Info("Inserted EAddress: [" + eaddress.Id + "] for person: [" + eaddress.StaffId + "]");
                }
                else // Can't insert EAddress
                {
                    logger.Info("Can't insert EAddress");
                }
                #endregion

                var contactInforGot = this._contactInformationRepositories.Get(_ => _.PersonId == contactInformation.PersonId);
                string additionalInfo = "";
                #region Check and create address
                var checkAdress = CheckAddressExisting(contactInformation.Address);
                var checkDistrict = CheckDistrictExisting(contactInformation.Address.District);
                var checkProvince = CheckProvinceExisting(contactInformation.Address.District.Province);
                var checkCountry = CheckCountryExisting(contactInformation.Address.District.Province.Country);

                bool rt = true;

                if (checkCountry) // Found country
                {
                    if (checkProvince) // Found province
                    {
                        if (checkDistrict) // Found district
                        {
                            if (checkAdress) // Found address -> Existing adress in database. Using addressId for contactInforGot
                            {
                                additionalInfo += "Found address: [" + contactInformation.Address.AddressNumberNoAndStreet + "]. ";
                            }
                            else // Create address 
                            {
                                var insertedAdd = this.CreateAddress(contactInformation.Address);
                                if (insertedAdd) // Inserted address
                                {
                                    additionalInfo += "Create address [" + ((Address)Get("Address", contactInformation.Address.AddressNumberNoAndStreet)).Id + "]. ";
                                }
                                else // Can't insert address
                                {
                                    additionalInfo += "Can't create address. ";
                                    rt = false;
                                    goto RETURN_HERE;
                                }
                            }
                        }
                        else // Not found district. Create district, address fit district
                        {
                            var insertDistrict = this.CreateDistrict(contactInformation.Address.District);
                            if (insertDistrict) // Inserted district
                            {
                                var district = (District)this.Get("District", contactInformation.Address.District.DistrictName);
                                contactInforGot.Address.DistrictId = district.Id;
                                additionalInfo += "Create district [" + district.Id + "]. ";
                                var insertAddress = this.CreateAddress(contactInformation.Address);
                                if (insertAddress) // Inserted address
                                {
                                    additionalInfo += "Create address [" + ((Address)this.Get("Address", contactInformation.Address.AddressNumberNoAndStreet)).Id + "]";
                                }
                                else // Can't insert address
                                {
                                    additionalInfo += "Can't insert address";
                                    rt = false;
                                    goto RETURN_HERE;
                                }
                            }
                            else // Can't insert district
                            {
                                additionalInfo += "Can't insert district. ";
                                rt = false;
                                goto RETURN_HERE;
                            }
                        }
                    }
                    else // Not found province. Create province, district, address fit country
                    {
                        var insertProvince = this.CreateProvince(contactInformation.Address.District.Province);
                        if (insertProvince) // Inserted province
                        {
                            additionalInfo += "Create province [" + ((Province)this.Get("Province", contactInformation.Address.District.Province.ProvinceName)).Id + "]. ";

                            var insertDistrict = this.CreateDistrict(contactInformation.Address.District);
                            if (insertDistrict) // Inserted district
                            {
                                additionalInfo += "Create district [" + ((District)this.Get("District", contactInformation.Address.District.DistrictName)).Id + "]. ";

                                var insertAddress = this.CreateAddress(contactInformation.Address);
                                if (insertAddress) // Inserted address
                                {
                                    additionalInfo += "Create address [" + ((Address)this.Get("Address", contactInformation.Address.AddressNumberNoAndStreet)).Id + "]. ";
                                }
                                else // Can't insert address
                                {
                                    additionalInfo += "Can't create address. ";
                                    rt = false;
                                    goto RETURN_HERE;
                                }
                            }
                            else // Can't insert district
                            {
                                additionalInfo += "Can't create district. ";
                                rt = false;
                                goto RETURN_HERE;
                            }
                        }
                        else // Can't insert province
                        {
                            additionalInfo += "Can't create province. ";
                            rt = false;
                            goto RETURN_HERE;
                        }
                    }
                }
                else // Not found country. Create everything
                {
                    var createCountry = this.CreateCountry(contactInformation.Address.District.Province.Country);
                    var createProvince = this.CreateProvince(contactInformation.Address.District.Province);
                    var createDistrict = this.CreateDistrict(contactInformation.Address.District);
                    var createAddress = this.CreateAddress(contactInformation.Address);

                    if (createCountry) // Inserted country
                    {
                        if (createProvince) // Inserted province
                        {
                            if (createDistrict) // Inserted district
                            {
                                if (createAddress) // Inserted address
                                {
                                    additionalInfo += "Create address: [" + contactInformation.Address.AddressNumberNoAndStreet + "], district: [" + contactInformation.Address.District.DistrictName + "], province: [" + contactInformation.Address.District.Province.ProvinceName + "] and country: [" + contactInformation.Address.District.Province.Country.CountryName + "]. ";
                                }
                                else // Can't insert address
                                {
                                    additionalInfo += "Can't insert address. ";
                                    rt = false;
                                    goto RETURN_HERE;
                                }
                            }
                            else // Can't insert district
                            {
                                additionalInfo += "Can't insert district. ";
                                rt = false;
                                goto RETURN_HERE;
                            }
                        }
                        else // Can't insert province
                        {
                            additionalInfo += "Can't insert province. ";
                            rt = false;
                            goto RETURN_HERE;
                        }
                    }
                    else // Can't insert country
                    {
                        additionalInfo += "Can't insert country. ";
                        rt = false;
                        goto RETURN_HERE;
                    }
                }
                #endregion

                var addressInserted = this._addressRepositories.Get(_=>_.AddressNumberNoAndStreet == contactInformation.Address.AddressNumberNoAndStreet);
                #region Update contact information for person
                if (contactInforGot != null)
                {
                    contactInforGot.EAddress = eaddress;
                    contactInforGot.Address = addressInserted;
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        contactInforGot.Address = addressInserted;
                        contactInforGot.AddressId = addressInserted.Id;
                        contactInforGot.EAddress = eaddress;
                        contactInforGot.EAdressId = eaddress.Id;
                        contactInforGot.Address.District = addressInserted.District;
                        contactInforGot.Address.DistrictId = addressInserted.DistrictId;
                        contactInforGot.Address.District.Province = addressInserted.District.Province;
                        contactInforGot.Address.District.ProvinceId = addressInserted.District.ProvinceId;
                        contactInforGot.Address.District.Province.Country = addressInserted.District.Province.Country;
                        contactInforGot.Address.District.Province.CountryId = addressInserted.District.Province.CountryId;
                        contactInformation.IsInUse = true;
                        this._contactInformationRepositories.Update(contactInforGot);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                }
                else // Not found contact information for person
                {
                    ContactInformation contactInformationCreate = new ContactInformation() 
                    {
                        Address = addressInserted,
                        AddressId = addressInserted.Id,
                        PersonId = contactInformation.PersonId,
                        ContactForId = contactInformation.ContactForId,
                        ContactTypeId = contactInformation.ContactTypeId,
                        EAdressId = eaddress.Id,
                        IsInUse = true
                    };
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._contactInformationRepositories.Add(contactInformationCreate);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                }
                #endregion
            RETURN_HERE:
                {
                    logger.Info(additionalInfo);
                    return rt;
                };
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

        public bool CheckAddressExisting(Address address)
        {
            logger.EnterMethod();
            try
            {
                var addressFound = this._addressRepositories.Get(_ => _.AddressNumberNoAndStreet == address.AddressNumberNoAndStreet);
                if (addressFound != null)
                {
                    logger.Info("Found address: [" + address.AddressNumberNoAndStreet + "] is Id: [" + addressFound.Id + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find address: [" + address.AddressNumberNoAndStreet + "]");
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

        public bool CheckDistrictExisting(District district)
        {
            logger.EnterMethod();
            try
            {
                var districtFound = this._districtRepositories.Get(_ => _.DistrictName == district.DistrictName);
                if (districtFound != null)
                {
                    logger.Info("Found district: [" + district.DistrictName + "] is Id: [" + districtFound.Id + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find district: [" + district.DistrictName + "]");
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

        public bool CheckProvinceExisting(Province province)
        {
            logger.EnterMethod();
            try
            {
                var provinceFound = this._provinceRepositories.Get(_ =>_.ProvinceName == province.ProvinceName);
                if (provinceFound != null)
                {
                    logger.Info("Found province: [" + province.ProvinceName + "] is Id: [" + provinceFound.Id + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find province: [" + province.ProvinceName + "]");
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

        public bool CheckCountryExisting(Country country)
        {
            logger.EnterMethod();
            try
            {
                var countryFound = this._countryRepositories.Get(_ => _.CountryName == country.CountryName);
                if (countryFound != null)
                {
                    logger.Info("Found country: [" + country.CountryName + "] is Id: [" + countryFound.Id + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find country: [" + country.CountryName + "]");
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

        public bool CreateAddress(Address address)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._addressRepositories.Add(address);
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Added new address. Values: [" + address.AddressNumberNoAndStreet + "]");
                return true;
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

        public bool CreateDistrict(District district)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._districtRepositories.Add(district);
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Added new district. Values: [" + district.DistrictName + "]");
                return true;
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

        public bool CreateProvince(Province province)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._provinceRepositories.Add(province);
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Added new province. Values: [" + province.ProvinceName + "]");
                return true;
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

        public bool CreateCountry(Country country)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._countryRepositories.Add(country);
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Added new country. Values: [" + country.CountryName + "]");
                return true;
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

        public bool DeleteAddress(int addressId)
        {
            logger.EnterMethod();
            try
            {
                var addressFound = this._addressRepositories.Get(_ => _.Id == addressId);
                if (addressFound != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._addressRepositories.Delete(addressFound);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Deleted address with Id: [" + addressId + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find address with Id: [" + addressId + "]");
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

        public bool DeleteDistrict(int districtId)
        {
            logger.EnterMethod();
            try
            {
                var districtFound = this._districtRepositories.Get(_ => _.Id == districtId);
                if (districtFound != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._districtRepositories.Delete(districtFound);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Deleted district with Id: [" + districtId + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find district with Id: [" + districtId + "]");
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

        public bool DeleteProvince(int provinceId)
        {
            logger.EnterMethod();
            try
            {
                var provinceFound = this._provinceRepositories.Get(_ => _.Id == provinceId);
                if (provinceFound != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._provinceRepositories.Delete(provinceFound);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Deleted province with Id: [" + provinceId + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find province with Id: [" + provinceId + "]");
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

        public bool DeleteCountry(int countryId)
        {
            logger.EnterMethod();
            try
            {
                var countryFound = this._countryRepositories.Get(_ => _.Id == countryId);
                if (countryFound != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._addressRepositories.Delete(countryFound);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Deleted country with Id: [" + countryId + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find country with Id: [" + countryId + "]");
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

        public object Get(string type, int id)
        {
            logger.EnterMethod();
            try
            {
                type = type.ToLower();
                if (type == "address")
                {
                    var address = this._addressRepositories.Get(_ => _.Id == id);
                    if (address != null)
                    {
                        logger.Info("Found address with Id: [" + id + "]");
                        return address;
                    }
                    else
                    {
                        logger.Info("Can't find any address with Id: [" + id + "]");
                        return null;
                    }
                }
                else if(type == "district")
                {
                    var district = this._districtRepositories.Get(_ => _.Id == id);
                    if (district != null)
                    {
                        logger.Info("Found district with Id: [" + id + "]");
                        return district;
                    }
                    else
                    {
                        logger.Info("Can't find any district with Id: [" + id + "]");
                        return null;
                    }
                }
                else if (type == "province")
                {
                    var province = this._provinceRepositories.Get(_ => _.Id == id);
                    if (province != null)
                    {
                        logger.Info("Found province with Id: [" + id + "]");
                        return province;
                    }
                    else
                    {
                        logger.Info("Can't find any province with Id: [" + id + "]");
                        return null;
                    }
                }
                else if (type == "country")
                {
                    var county = this._countryRepositories.Get(_ => _.Id == id);
                    if (county != null)
                    {
                        logger.Info("Found country with Id: [" + id + "]");
                        return county;
                    }
                    else
                    {
                        logger.Info("Can't find any country with Id: [" + id + "]");
                        return null;
                    }
                }
                else
                {
                    logger.Error("Type inputed is incorrect");
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

        public object Get(string type, string value)
        {
            logger.EnterMethod();
            try
            {
                type = type.ToLower();
                if (type == "address")
                {
                    var address = this._addressRepositories.Get(_ => _.AddressNumberNoAndStreet == value);
                    if (address != null)
                    {
                        logger.Info("Found address with value: [" + value + "]");
                        return address;
                    }
                    else
                    {
                        logger.Info("Can't find any address with value: [" + value + "]");
                        return null;
                    }
                }
                else if (type == "district")
                {
                    var district = this._districtRepositories.Get(_ => _.DistrictName == value);
                    if (district != null)
                    {
                        logger.Info("Found district with value: [" + value + "]");
                        return district;
                    }
                    else
                    {
                        logger.Info("Can't find any district with value: [" + value + "]");
                        return null;
                    }
                }
                else if (type == "province")
                {
                    var province = this._provinceRepositories.Get(_ => _.ProvinceName == value);
                    if (province != null)
                    {
                        logger.Info("Found province with value: [" + value + "]");
                        return province;
                    }
                    else
                    {
                        logger.Info("Can't find any province with value: [" + value + "]");
                        return null;
                    }
                }
                else if (type == "country")
                {
                    var county = this._countryRepositories.Get(_ => _.CountryName == value);
                    if (county != null)
                    {
                        logger.Info("Found country with value: [" + value + "]");
                        return county;
                    }
                    else
                    {
                        logger.Info("Can't find any country with value: [" + value + "]");
                        return null;
                    }
                }
                else
                {
                    logger.Error("Type inputed is incorrect");
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

        public int Count(string type)
        {
            logger.EnterMethod();
            try
            {
                type = type.ToLower();
                if (type == "address")
                {
                    var countAddress = this._addressRepositories.Count();
                    logger.Info("Found [" + countAddress + "] adddresses");
                    return countAddress;
                }
                else if (type == "district")
                {
                    var countDistrict = this._districtRepositories.Count();
                    logger.Info("Found [" + countDistrict + "] districts");
                    return countDistrict;
                }
                else if (type == "province")
                {
                    var countProvince = this._provinceRepositories.Count();
                    logger.Info("Found [" + countProvince + "] provinces");
                    return countProvince;
                }
                else if (type == "country")
                {
                    var countCountry = this._countryRepositories.Count();
                    logger.Info("Found [" + countCountry + "] countries");
                    return countCountry;
                }
                else
                {
                    logger.Error("Type inputed is incorrect");
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

        public bool DeleteAddress(string noAndStreetName)
        {
            logger.EnterMethod();
            try
            {
                var address = this._addressRepositories.Get(_ => _.AddressNumberNoAndStreet == noAndStreetName);
                if (address != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._addressRepositories.Delete(address);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Deleted address with value: [" + noAndStreetName + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find address with vaue: [" + noAndStreetName +"]");
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

        public bool DeleteDistrict(string districtName)
        {
            logger.EnterMethod();
            try
            {
                var district = this._districtRepositories.Get(_ => _.DistrictName == districtName);
                if (district != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._districtRepositories.Delete(district);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Deleted address with value: [" + districtName + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find address with vaue: [" + districtName + "]");
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

        public bool DeleteProvince(string provinceName)
        {
            logger.EnterMethod();
            try
            {
                var provice = this._provinceRepositories.Get(_ => _.ProvinceName == provinceName);
                if (provice != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._provinceRepositories.Delete(provice);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Deleted address with value: [" + provinceName + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find address with vaue: [" + provinceName + "]");
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

        public bool DeleteCountry(string countryName)
        {
            logger.EnterMethod();
            try
            {
                var country = this._countryRepositories.Get(_ => _.CountryName == countryName);
                if (country != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._countryRepositories.Delete(country);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Deleted address with value: [" + countryName + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find address with vaue: [" + countryName + "]");
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

        public bool CreateEAddress(EAddress eAddress)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._eaddressRepositories.Add(eAddress);
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Create EAddress with email: [" + eAddress.Email + "], phone: [" + eAddress.NumberPhone + "], website: [" + eAddress.Website + "] for [" + eAddress.StaffId + "]");
                return true;
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

        public EAddress Get(string phone, string email, string website)
        {
            logger.EnterMethod();
            try
            {
                var eAddress = this._eaddressRepositories.Get(_ => _.Email == email &&
                                                                   _.NumberPhone == phone &&
                                                                   _.Website == website);
                if (eAddress != null)
                {
                    logger.Info("Found EAddress: [" + eAddress.Id + "] for params passed");
                    return eAddress;
                }
                else
                {
                    logger.Info("Can't find EAddress for params passed");
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
        #endregion


        public int GetContactForId(string forName)
        {
            logger.EnterMethod();
            try
            {
                var contactFor = this._contactForReppositories.Get(_ => _.StaffOrCustomer == forName);
                if (contactFor != null)
                {
                    logger.Info("Found ContactForId: [" + contactFor.Id + "] for: [" + forName + "] inputed");
                    return contactFor.Id;
                }
                else
                {
                    logger.Info("Can't find ContactFor for :[" + forName + "] inputed");
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

        public int GetContactTypeId(string typeName)
        {
            logger.EnterMethod();
            try
            {
                var contactType = this._contactTypeReppositories.Get(_ => _.ContactTypeName == typeName);
                if (contactType != null)
                {
                    logger.Info("Found ContactTypeId: [" + contactType.Id + "] for [" + typeName + "] inputed");
                    return contactType.Id;
                }
                else
                {
                    logger.Info("Can't find ContactType for [" + typeName + "] inputed");
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
    }
}
