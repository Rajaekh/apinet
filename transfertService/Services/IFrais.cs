using static LesApi.Services.FraisService;

namespace LesApi.Services
{
    public interface IFrais
    {
        public double CalculerFrais(double montantTransfert, string typeFrais, bool Notified);

    }
}
