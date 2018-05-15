using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
namespace Core.ObjectServices.Repositories
{
    public interface IReferenceRepository
    {
        Tuple<List<Address>, List<Bed>, List<Customer>, List<Service>, List<Staff>, List<Stock>, int> Search(string key);
        /// <summary>
        /// Get language by Id if found
        ///     Otherwise, return null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Language GetLanguageById(int id);
        /// <summary>
        /// Get language by name if found
        ///     Otherwise, return null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Language GetLanguageByName(string value);
        /// <summary>
        /// Get all languges
        /// </summary>
        /// <returns></returns>
        List<Language> GetAllLanguage();
    }
}
