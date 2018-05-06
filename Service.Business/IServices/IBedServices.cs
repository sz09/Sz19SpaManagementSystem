using SPMS.ObjectModel.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Service.Business.IServices
{
    public interface IBedServices
    {
        /// <summary>
        /// Get bed by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Bed GetBed(int id);
        string GetBedName(int id, string language = "English");
        IEnumerable<Bed> GetAll();
        int CountAllBed();
        IQueryable<Bed> GetBedByPage(int index, string language = "English");
        bool CreateNewBed(Bed bed);
        bool UpdateBed(Bed bed);
        bool DeleteBed(int id);
        List<TimePeriod> GetTimePeriodsForBed(int bedId);
    }
}
