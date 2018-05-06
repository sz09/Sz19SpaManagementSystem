using StructureMap.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolverProject.DependencyResolution
{
    public class ControllerRegistration: IRegistrationConvention
    {
        public ControllerRegistration()
        {

        }

        public void Process(Type type, StructureMap.Configuration.DSL.Registry registry)
        {
            
        }
    }
}
