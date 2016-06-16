using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iasset.FlightDashBoard.Repository;
using iasset.FlightDashBoard.Repository.Interfaces;
using iasset.FlightDashBoard.Services;
using iasset.FlightDashBoard.Services.Interfaces;
using Ninject.Modules;

namespace iasset.FlightDashBoard
{
    public class FlightDashBoardWebModule : NinjectModule
    {
        public override void Load()
        {
            BindRepositories();
            BindServices();
            AutoMappings.ConfigureAutoMappings();
        }
        private void BindServices()
        {
            Bind<IGateService>().To<GateService>();
        }
        private void BindRepositories()
        {
            Bind<IGateRepository>().To<GateRepository>();
        }
    }
}