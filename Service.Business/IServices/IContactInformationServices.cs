using SPMS.ObjectModel.Entities;
using System.Collections.Generic;

namespace Service.Business.IServices
{
    /// <summary>
    /// IContactInformationServices defines class to implement in class inherited
    ///     Using interface to resolve issue about ContactInformation
    ///         This service passes params into operations in IContactInformationRepository and retrive data
    /// </summary>
    public interface IContactInformationServices
    {
        /// <summary>
        /// Get all contact information by a person
        /// </summary>
        /// <param name="personId">Id of person, can be employee or customer</param>
        /// <returns>Contact information of that person</returns>
        ContactInformation Get(long personId);
        /// <summary>
        /// Get list ContactInformation of a list staff
        /// </summary>
        /// <param name="listStaffId">list Id of emp</param>
        /// <returns>List ContactInformation of list emp</returns>
        IEnumerable<ContactInformation> GetContactInformationFromListStaff(IEnumerable<long> listStaffId);
        /// <summary>
        /// Update ContactInformation for person
        /// </summary>
        /// <param name="contactInformation">ContactInformation</param>
        /// <returns>
        /// If update success, return true
        ///     Otherwise, return false
        /// </returns>
        bool UpdateContactInformation(ContactInformation contactInformation);

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
        /// Get Id of ContactType by name
        /// </summary>
        /// <param name="typeName">Value of type</param>
        /// <returns>Type Id</returns>
        int GetContactTypeId(string typeName);

        /// <summary>
        /// Get Id of ContactFor by name
        /// </summary>
        /// <param name="forName">Value of for</param>
        /// <returns>For Id</returns>
        int GetContactForId(string forName);

    }
}
