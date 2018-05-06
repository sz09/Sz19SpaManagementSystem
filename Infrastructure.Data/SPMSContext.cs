namespace Infrastructure.Data
{
    using Core.ObjectServices;
    using System.Data.Entity;

    public class SPMSContext : ISPMSContext
    {
        public SPMSContext()
        {

        }
        public object GetContext()
        {
            return new DbContext("SpaManagementEntities");
        }
    }
}
