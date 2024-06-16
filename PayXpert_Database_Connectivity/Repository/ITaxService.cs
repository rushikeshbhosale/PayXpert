using PayXpert_Database_Connectivity.Models;

namespace PayXpert_Database_Connectivity.Repository
{
    public interface ITaxService
    {
        void CalculateTax(int employeeId, int taxYear);
        Tax GetTaxById(int taxId);
        List<Tax> GetTaxesForEmployee(int employeeId);
        List<Tax> GetTaxesForYear(int taxYear);

    }
}
