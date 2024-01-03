namespace LesApi.Models
{
    public class TransferModel
    {
      
            public bool Notified { get; set; }
            public string TypeTransfert { get; set; }
            public string id { get; set; }
            public string? otp { get; set; }
            public string? IdClient { get; set; }
            public string? IdBeneficiaire { get; set; }
            public string Frais { get; set; }
            public double? ValFrais { get; set; }
            public string MotifsTransfert { get; set; }
            public DateTime DataeTransfert { get; set; }
            public DateTime DataeExpiration { get; set; }
            public double Montant { get; set; }
            public string NomBeneficiaire { get; set; }
            public string NomClient { get; set; }
            public string Status { get; set; }
            public string Idagent { get; set; }
            public string? MotifRestitution { get; set; }
            public string? MotifBlicage { get; set; }
            public string? AutreMotif { get; set; }
           public string? Reference { get; set; }

    }
}
