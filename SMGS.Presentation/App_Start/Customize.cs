using System;
using System.Threading;
using System.Globalization;
using log4net;

[assembly: WebActivator.PreApplicationStartMethod(typeof(SMGS.Presentation.App_Start.Customize), "Start")]

namespace SMGS.Presentation.App_Start
{
    public static class Customize
    {
        private readonly static ILog logger = LogManager.GetLogger(typeof(Customize));
        private static CultureInfo _currentCultureUS = CultureInfo.CreateSpecificCulture("en-US");
        private static CultureInfo _currentCultureVN = CultureInfo.CreateSpecificCulture("vi-VN");

        private static CultureAndRegionInfoBuilder _cultureAndRegionInfoBuilder = new CultureAndRegionInfoBuilder("sz-VN", CultureAndRegionModifiers.None);
        public static void Start()
        {
            try
            {
                _cultureAndRegionInfoBuilder.LoadDataFromCultureInfo(_currentCultureUS);
                _cultureAndRegionInfoBuilder.NumberFormat.CurrencySymbol = _currentCultureVN.NumberFormat.CurrencySymbol;
                _cultureAndRegionInfoBuilder.Register();
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
            }

            CultureInfo ciNew = CultureInfo.CreateSpecificCulture("sz-VN");
            ciNew.NumberFormat.CurrencyNegativePattern = 3;
            ciNew.NumberFormat.CurrencyPositivePattern = 3;
            CultureInfo.DefaultThreadCurrentCulture = ciNew;
            CultureInfo.DefaultThreadCurrentUICulture = ciNew;
        }
    }
}