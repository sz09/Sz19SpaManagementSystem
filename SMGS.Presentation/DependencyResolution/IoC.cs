// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using Core.ObjectModel.Entities;
using Core.ObjectServices;
using Core.ObjectServices.Repositories;
using Core.ObjectServices.UnitOfWork;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.UnitOfWork;
using Service.Business.IServices;
using Service.Business.Services;
using SPMS.ObjectModel.Entities;
using StructureMap;
using StructureMap.Graph;
using System.Data.Entity;
namespace SMGS.Presentation.DependencyResolution {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });

                            Connector connector = new Connector();
                            x.For<DbContext>().Use<SPMS.ObjectModel.Entities.SpaManagementEntities>().Ctor<string>("connectionString").Is(Connector._connectionString);
                            x.For<ISPMSContext>().Use<SPMSContext>();

                            x.For<IUnitOfWork>().Use<UnitOfWork>();
                            x.For<ISPMSContext>().Use<SPMSContext>();

                            //Staff 
                            x.For<IRepository<Staff>>().Use<Repository<Staff>>();
                            x.For<IStaffServices>().Use<StaffServices>();
                            x.For<IStaffRepository>().Use<StaffRepository>();

                            // ContactInformation
                            x.For<IRepository<ContactInformation>>().Use<Repository<ContactInformation>>();
                            x.For<IRepository<ContactFor>>().Use<Repository<ContactFor>>();
                            x.For<IRepository<ContactType>>().Use<Repository<ContactType>>();
                            x.For<IRepository<EAddress>>().Use<Repository<EAddress>>();
                            x.For<IRepository<District>>().Use<Repository<District>>();
                            x.For<IRepository<Address>>().Use<Repository<Address>>();
                            x.For<IRepository<Province>>().Use<Repository<Province>>();
                            x.For<IRepository<Country>>().Use<Repository<Country>>();
                            x.For<IContactInformationRepository>().Use<ContactInformationRepository>();
                            x.For<IContactInformationServices>().Use<ContactInformationServices>();

                            // Account
                            x.For<IRepository<Account>>().Use<Repository<Account>>();
                            x.For<IRepository<AccountFor>>().Use<Repository<AccountFor>>();
                            x.For<IRepository<AccountAutoLogin>>().Use<Repository<AccountAutoLogin>>();
                            x.For<IRepository<AccountMappingRole>>().Use<Repository<AccountMappingRole>>(); 
                            x.For<IAccountRepository>().Use<AccountRepository>();
                            x.For<IAccountServices>().Use<AccountServices>();

                            // Customer
                            x.For<IRepository<Customer>>().Use<Repository<Customer>>();

                            // Service
                            x.For<IRepository<SPMS.ObjectModel.Entities.Service>>().Use <Repository<SPMS.ObjectModel.Entities.Service>>();
                            x.For<IRepository<ServiceName>>().Use<Repository<ServiceName>>();
                            x.For<IServiceRepository>().Use<ServiceRepository>();
                            x.For<IServiceServices>().Use<ServiceServices>();

                            // Salary
                            x.For<IRepository<Salary>>().Use<Repository<Salary>>();
                            x.For<IRepository<Attendance>>().Use<Repository<Attendance>>();
                            x.For<ISalaryRepository>().Use<SalaryRepository>();
                            x.For<ISalaryServices>().Use<SalaryServices>();

                            // Bed
                            x.For<IRepository<Bed>>().Use<Repository<Bed>>();
                            x.For<IRepository<BedName>>().Use<Repository<BedName>>();
                            x.For<IBedRepository>().Use<BedRepository>();
                            x.For<IBedServices>().Use<BedServices>();

                            // Reference
                            x.For<IReferenceRepository>().Use<ReferenceRepository>();
                            x.For<IReferenceServices>().Use<ReferenceServices>();
                            x.For<IRepository<Language>>().Use<Repository<Language>>();

                            // Stock
                            x.For<IRepository<Stock>>().Use<Repository<Stock>>();
                            x.For<IRepository<StockName>>().Use<Repository<StockName>>();
                            x.For<IRepository<StockPackage>>().Use<Repository<StockPackage>>();
                            x.For<IRepository<StockPackageDetail>>().Use<Repository<StockPackageDetail>>();
                            x.For<IStockRepository>().Use<StockRepository>();
                            x.For<IStockServices>().Use<StockServices>();
                            
                            // Notifications
                            x.For<IRepository<Notification>>().Use<Repository<Notification>>();
                            x.For<IRepository<NotificationDetail>>().Use<Repository<NotificationDetail>>();
                            x.For<INotificationRepository>().Use<NotificationRepository>();
                            x.For<INotificationServices>().Use<NotificationServices>();

                            // Booking
                            x.For<IRepository<Bills>>().Use<Repository<Bills>>();
                            x.For<IRepository<DetailsBill>>().Use<Repository<DetailsBill>>();
                            x.For<IBookingRepository>().Use<BookingRepository>();
                            x.For<IBookingServices>().Use<BookingServices>();
                        });
            return ObjectFactory.Container;
        }
    }
}