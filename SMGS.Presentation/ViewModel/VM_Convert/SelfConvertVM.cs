using SMGS.Presentation.ViewModel.VM;
using System.Collections.Generic;
using System.Linq;

namespace SMGS.Presentation.ViewModel.VM_Convert
{
    /// <summary>
    /// Convert to VM type 
    ///     Return is type in among of which input
    /// </summary>
    public class SelfConvertVM
    {
        internal static VM_ListStaff VM_ListStaffAddContactInformation(IEnumerable<VM_ContactInformation> listContactInformation, VM_ListStaff listStaff)
        {
            if (!listStaff.AllStaff.Count().Equals(listContactInformation.Count()))
                return listStaff;
            for (int i = 0; i < listStaff.AllStaff.Count(); i++)
            {
                listStaff.AllStaff[i].ContactInformation = listContactInformation.ToList()[i];
            }
            return listStaff;
        }

        internal static void VM_StaffAddContactInformation(ref VM_Staff VM_Staff, VM_ContactInformation VM_ContactInformation)
        {
            VM_Staff.ContactInformation = VM_ContactInformation;
        }

        internal static void VM_ServiceAddName(ref VM_Service vM_Service, string name)
        {
            vM_Service.Name = name;
        }

        internal static IEnumerable<VM_Service> VM_ServiceAddName(IEnumerable<VM_Service> listService, IEnumerable<string> listString)
        {
            if (listService.Count() != listString.Count())
                return listService;
            for (int i = 0; i < listService.Count(); i++)
            {
                listService.ToList()[i].Name = listString.ToList()[i];
            }
            return listService;
        }

        internal static void VMAnEmpSalaries_Add_CountHistory(ref VM_AnEmpSalaries vM_AnEmpSalaries, int count)
        {
            vM_AnEmpSalaries.History = count;
        }
        internal static void VMEmpSalaryPaying_Add_Paging(ref VM_EmpSalaryPaying vM_EmpSalaryPaying, int totals, int thisPage)
        {
            vM_EmpSalaryPaying.ThisPage = thisPage;
            vM_EmpSalaryPaying.ToltalPages = totals;
        }
        internal static void VM_Bed_Add_TimePeriod(VM_Bed vM_Bed, List<VM_TimePeriod> vM_TimePeriods)
        {
            vM_Bed.TimePeriod = vM_TimePeriods;
        }
    }
}