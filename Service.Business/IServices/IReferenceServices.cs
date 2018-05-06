using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;

namespace Service.Business.IServices
{
    public interface IReferenceServices
    {
        Tuple<List<Address>, List<Bed>, List<Customer>, List<SPMS.ObjectModel.Entities.Service>, List<Staff>, List<Stock>, int> Search(string key);
    }
}
