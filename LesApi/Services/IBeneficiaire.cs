using LesApi.Models;

namespace LesApi.Services
{
    public interface IBeneficiaire
    {
        Beneficiaire GetBeneficiaireById(string IdBeneficiaire);
        Beneficiaire AddBeneficiaire(Beneficiaire beneficiaire);
        Beneficiaire GetBeneficiaireByGSM(string gsm);

    }
}
