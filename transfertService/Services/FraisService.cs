namespace LesApi.Services
{
    public class FraisService : IFrais
    {
        public double CalculerFrais(double montantTransfert, string typeFrais, bool Notified)
        {
            double fraisProportionnels = montantTransfert *0.02;
            var prixNot = 2;
            switch (typeFrais)
            {
                case "donneur":
                    if (Notified)
                    {
                        // Add notification service fees to donneur fees
                        return montantTransfert + fraisProportionnels + prixNot;
                    }
                    return montantTransfert + fraisProportionnels;

                case "beneficiaire":
                   
                    if (Notified)
                    {
                        // Add notification service fees to donneur fees
                        return montantTransfert  + prixNot;
                    }
                    return montantTransfert;


                case "partages":
                    double fraisPartage = fraisProportionnels * 0.5;
                    if (Notified)
                    {
                        // Add notification service fees to partages fees
                        return montantTransfert + fraisPartage + prixNot;
                    }
                    return montantTransfert + fraisPartage;

                default:
                    return 0;
            }
        }

    }
  
    }

