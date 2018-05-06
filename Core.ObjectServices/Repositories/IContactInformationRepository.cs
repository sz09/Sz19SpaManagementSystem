using SPMS.ObjectModel.Entities;
using System.Collections.Generic;

namespace Core.ObjectServices.Repositories
{
    public interface IContactInformationRepository
    {
        /// <summary>
        /// Get Contact Information of a person
        /// </summary>
        /// <param name="personId">Id of person</param>
        /// <returns>
        /// Contact Information type if found
        ///     Otherwise, return null
        /// </returns>
        ContactInformation Get(long personId);
        /// <summary>
        /// Get Address, District, Province, Country type fit param[type] inputed
        /// </summary>
        /// <param name="type">Name of type</param>
        /// <param name="id">Id of type inputed</param>
        /// <returns>
        /// Object fit type if found
        ///     Otherwise, return null
        /// </returns>
        object Get(string type, int id);
        /// <summary>
        /// Get Address, District, Province, Country type fit param[type] inputed
        /// </summary>
        /// <param name="type">Name of type</param>
        /// <param name="value">
        /// No. and street with Address
        ///     Name with Distict, Province, Country
        /// </param>
        /// <returns>
        /// Object fit type if found
        ///     Otherwise, return null
        /// </returns>
        object Get(string type, string value);
        /// <summary>
        /// Get EAddress by values of person
        /// </summary>
        /// <param name="phone">Number phone for person</param>
        /// <param name="email">Email address</param>
        /// <param name="website">Website</param>
        /// <returns>EAddress type</returns>
        EAddress Get(string phone, string email, string website);
        /// <summary>
        /// Get Id of ContactFor by name
        /// </summary>
        /// <param name="forName">Value of for</param>
        /// <returns>For Id</returns>
        int GetContactForId(string forName);
        /// <summary>
        /// Get Id of ContactType by name
        /// </summary>
        /// <param name="typeName">Value of type</param>
        /// <returns>Type Id</returns>
        int GetContactTypeId(string typeName);
        /// <summary>
        /// Count all records of type inputed
        /// </summary>
        /// <param name="type">Name of type</param>
        /// <returns>Total records</returns>
        int Count(string type);
        /// <summary>
        /// Get all ContactInformation of list employees 
        ///     If not found ContactInformation, create new ContactInformation with Id = -1 and still add to list
        /// </summary>
        /// <param name="listStaffId">List personId passed</param>
        /// <returns>List ContactInformation</returns>
        IEnumerable<ContactInformation> GetContactInformationFromListStaff(IEnumerable<long> listStaffId);
        /// <summary>
        /// Update ContactInformation for a person
        ///     Check if person already has contact information, updated
        ///         Otherwise, add for that person
        /// </summary>
        /// <param name="contactInformation">Type ContactInformation</param>
        /// <returns>
        /// Success update, return true
        ///     Otherwise, return false
        /// </returns>
        bool UpdateContactInformationForStaff(ContactInformation contactInformation);
        /// <summary>
        /// Check each object in ContactInformation existing.
        ///     If not found, create and using Id for update.
        ///         Otherwise, using Id for create.
        /// </summary>
        /// <param name="contactInformation">ContactInformation type</param>
        /// <returns>
        /// If update success, return true.
        ///     Otherwise, return false.
        /// </returns>
        bool CheckContactInformationExistingAndUpdateForPerson(ContactInformation contactInformation);
        /// <summary>
        /// Check existing Address in database
        /// </summary>
        /// <param name="address">Type Address</param>
        /// <returns>
        /// If found, return true
        ///     Otherwise, return false
        /// </returns>
        bool CheckAddressExisting(Address address);
        /// <summary>
        /// Check existing District in database
        /// </summary>
        /// <param name="district">Type District</param>
        /// <returns>
        /// If found, return true
        ///     Otherwise, return false
        /// </returns>
        bool CheckDistrictExisting(District district);
        /// <summary>
        /// Check existing Province in database
        /// </summary>
        /// <param name="province">Type Province</param>
        /// <returns>
        /// If found, return true
        ///     Otherwise, return false
        /// </returns>
        bool CheckProvinceExisting(Province province);
        /// <summary>
        /// Check existing Country in database
        /// </summary>
        /// <param name="country">Type Country</param>
        /// <returns>
        /// If found, return true
        ///     Otherwise, return false
        /// </returns>
        bool CheckCountryExisting(Country country);
        /// <summary>
        /// Added new Address to database 
        ///     Save all changes
        /// </summary>
        /// <param name="address">Type Address</param>
        /// <returns></returns>
        bool CreateAddress(Address address);
        /// <summary>
        /// Added new District to database 
        ///     Save all changes
        /// </summary>
        /// <param name="district">Type District</param>
        /// <returns></returns>
        bool CreateDistrict(District district);
        /// <summary>
        /// Added new Province to database 
        ///     Save all changes
        /// </summary>
        /// <param name="province">Type Province</param>
        /// <returns></returns>
        bool CreateProvince(Province province);
        /// <summary>
        /// Added new Country to database 
        ///     Save all changes
        /// </summary>
        /// <param name="country">Type Country</param>
        /// <returns></returns>
        bool CreateCountry(Country country);

        bool CreateEAddress(EAddress eAddress);
        /// <summary>
        /// Find and delete Address
        ///     Save all changes
        /// </summary>
        /// <param name="addressId">Id of address</param>
        /// <returns>
        /// If success, return true
        ///     Otherwise, return false
        /// </returns>
        bool DeleteAddress(int addressId);
        /// <summary>
        /// Find and delete District
        ///     Save all changes
        /// </summary>
        /// <param name="districtId">Id of district</param>
        /// <returns>
        /// If success, return true
        ///     Otherwise, return false
        /// </returns>
        bool DeleteDistrict(int districtId);
        /// <summary>
        /// Find and delete Province
        ///     Save all changes
        /// </summary>
        /// <param name="provinceId">Id of province</param>
        /// <returns>
        /// If success, return true
        ///     Otherwise, return false
        /// </returns>
        bool DeleteProvince(int provinceId);
        /// <summary>
        /// Find and delete Country
        ///     Save all changes
        /// </summary>
        /// <param name="countryId">Id of country</param>
        /// <returns>
        /// If success, return true
        ///     Otherwise, return false
        /// </returns>
        bool DeleteCountry(int countryId);

        /// <summary>
        /// Find and delete Address
        ///     Save all changes
        /// </summary>
        /// <param name="noAndStreetName">No. and street name</param>
        /// <returns>
        /// If success, return true
        ///     Otherwise, return false
        /// </returns>
        bool DeleteAddress(string noAndStreetName);
        /// <summary>
        /// Find and delete District
        ///     Save all changes
        /// </summary>
        /// <param name="districtName">District's name</param>
        /// <returns>
        /// If success, return true
        ///     Otherwise, return false
        /// </returns>
        bool DeleteDistrict(string districtName);
        /// <summary>
        /// Find and delete Province
        ///     Save all changes
        /// </summary>
        /// <param name="provinceName">Province's name,</param>
        /// <returns>
        /// If success, return true
        ///     Otherwise, return false
        /// </returns>
        bool DeleteProvince(string provinceName);
        /// <summary>
        /// Find and delete Country
        ///     Save all changes
        /// </summary>
        /// <param name="countryName">Country's name</param>
        /// <returns>
        /// If success, return true
        ///     Otherwise, return false
        /// </returns>
        bool DeleteCountry(string countryName);
    }
}
