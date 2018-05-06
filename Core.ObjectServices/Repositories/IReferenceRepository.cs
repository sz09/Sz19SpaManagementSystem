using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
namespace Core.ObjectServices.Repositories
{
    public interface IReferenceRepository
    {
        Tuple<List<Address>, List<Bed>, List<Customer>, List<Service>, List<Staff>, List<Stock>, int> Search(string key);
    }
}
