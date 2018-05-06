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


using Core.ObjectServices.Repositories;
using Infrastructure.Data.Repositories;
using Service.Business.IServices;
using Service.Business.Services;
using SPMS.ObjectModel.Entities;
using System.Data.Entity;
using StructureMap;
using StructureMap.Graph;
using System;
using Infrastructure.Data.UnitOfWork;
using Core.ObjectServices.UnitOfWork;
using Core.ObjectServices;
using Infrastructure.Data;
namespace DependencyResolverProject.DependencyResolution {
    public static class IoC {
        public static IContainer Initialize() {

            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });

                            //x.For<DbContext>().Use(() => new DbContext("LocalhostConnString"));


                            x.For<IUnitOfWork>().Use<UnitOfWork>();
                            x.For<IMNSPContext>().Use<MNSPContext>();

                            //Staff 
                            x.For<IRepository<Staff>>().Use<Repository<Staff>>();
                            x.For<IStaffRepository>().Use<StaffRepository>();
                            x.For<IStaffServices>().Use<StaffServices>();

                            // ContactInformation
                            x.For<IRepository<ContactInformation>>().Use<Repository<ContactInformation>>();
                            x.For<IContactInformationRepository>().Use<ContactInformationRepository>();
                            x.For<IContactInformationServices>().Use<ContactInformationServices>();


                            
                        });
            return ObjectFactory.Container;
        }
    }
}