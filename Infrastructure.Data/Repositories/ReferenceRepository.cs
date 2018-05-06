using Core.ObjectServices.Repositories;
using log4net;
using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Logging;

namespace Infrastructure.Data.Repositories
{
    public class ReferenceRepository : IReferenceRepository
    {
        #region Attributes
        private readonly IRepository<Staff> _staffRepostitories;
        private readonly IRepository<Service> _serviceRepostitories;
        private readonly IRepository<Customer> _customerRepostitories;
        private readonly IRepository<Stock> _stockRepostitories;
        private readonly IRepository<Bed> _bedRepostitories;
        private readonly IRepository<Address> _addressRepositories;
        private readonly IRepository<BedName> _bedNameRepositories;
        private readonly IRepository<ServiceName> _serviceNameRepositories;
        private readonly IRepository<StockName> _stockNameRepositories;
        private readonly IRepository<District> _districtRepositories;
        private readonly IRepository<Province> _proviceRepositories;
        private readonly IRepository<Country> _countryRepositories;

        private readonly int searchResult = 10;
        private readonly ILog logger = LogManager.GetLogger(typeof(ReferenceRepository));
        #endregion
        #region Constructors
        public ReferenceRepository(IRepository<Staff> staffRepostitories, 
            IRepository<Service> serviceRepostitories, 
            IRepository<Customer> customerRepostitories, 
            IRepository<Stock> stockRepostitories, 
            IRepository<Bed> bedRepostitories, 
            IRepository<Address> addressRepositories, 
            IRepository<BedName> bedNameRepositories, 
            IRepository<ServiceName> serviceNameRepositories,
            IRepository<StockName> stockNameRepositories,
            IRepository<District> districtRepositories,
            IRepository<Province> proviceRepositories,
            IRepository<Country> countryRepositories
            )
        {
            logger.EnterMethod();
            try
            {
                this._bedRepostitories = bedRepostitories;
                this._customerRepostitories = customerRepostitories;
                this._serviceRepostitories = serviceRepostitories;
                this._staffRepostitories = staffRepostitories;
                this._stockRepostitories = stockRepostitories;
                this._addressRepositories = addressRepositories;
                this._bedNameRepositories = bedNameRepositories;
                this._serviceNameRepositories = serviceNameRepositories;
                this._stockNameRepositories = stockNameRepositories;
                this._districtRepositories = districtRepositories;
                this._proviceRepositories = proviceRepositories;
                this._countryRepositories = countryRepositories;
                logger.Info("Success set values for attributes");
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        #endregion

        #region Operations
        public Tuple<List<Address>, List<Bed>, List<Customer>, List<Service>, List<Staff>, List<Stock>, int> Search(string key)
        {
            logger.EnterMethod();
            try
            {

                var bedSearch = (from bedName in this._bedNameRepositories.GetAll()
                                 join bed in this._bedRepostitories.GetAll() on
                                 bedName.BedId equals bed.Id
                                 where bedName.Name.Contains(key)
                                 select bed)
                                 .ToList();
                bedSearch = bedSearch.OrderBy(_ => _.Id).Take(searchResult).ToList();

                var customerSearch = (from customer in this._customerRepostitories.GetAll()
                                      where customer.LastMiddle.Contains(key) ||
                                      customer.FirstName.Contains(key) ||
                                      customer.Summary.Contains(key) ||
                                      customer.CustomerCode.Contains(key)
                                      select customer)
                                    .ToList();
                customerSearch = customerSearch.OrderBy(_ => _.Id).Take(searchResult).ToList();

                var staffSearch = (from staff in this._staffRepostitories.GetAll()
                                  where staff.StaffCode.Contains(key) ||
                                  staff.LastMiddle.Contains(key) ||
                                  staff.FirstName.Contains(key) ||
                                  staff.Summary.Contains(key)
                                 select staff)
                                 .ToList();
                staffSearch = staffSearch.OrderBy(_ => _.Id).Take(searchResult).ToList();
                
                var serviceSearch = (from service in this._serviceRepostitories.GetAll()
                                    join serviceName in this._serviceNameRepositories.GetAll() 
                                    on service.Id equals serviceName.ServiceId
                                    where serviceName.Name.Contains(key)
                                    select service)
                                    .ToList();
                serviceSearch = serviceSearch.OrderBy(_ => _.Id).Take(searchResult).ToList();

                var stockSearch = (from stock in this._stockRepostitories.GetAll()
                                  join stockName in this._stockNameRepositories.GetAll()
                                  on stock.Id equals stockName.StockId
                                  where stockName.Name.Contains(key)
                                  select stock)
                                  .ToList();
                stockSearch = stockSearch.OrderBy(_ => _.Id).Take(searchResult).ToList();

                var addressSearch = (from address in this._addressRepositories.GetAll()
                                     join district in this._districtRepositories.GetAll() 
                                            on address.DistrictId equals district.Id
                                     join province in this._proviceRepositories.GetAll()
                                            on district.ProvinceId equals province.Id
                                     join country in this._countryRepositories.GetAll()
                                            on province.CountryId equals country.Id
                                    where address.AddressNumberNoAndStreet.Contains(key) ||
                                          province.ProvinceName.Contains(key) ||
                                          district.DistrictName.Contains(key) ||
                                          country.CountryName.Contains(key)
                                    select address)
                                    .ToList();
                addressSearch = addressSearch.OrderBy(_ => _.Id).Take(searchResult).ToList();
               
                return Tuple.Create(addressSearch, bedSearch, customerSearch, serviceSearch, staffSearch, stockSearch, searchResult);
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
    }
}
