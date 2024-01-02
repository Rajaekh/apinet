namespace LesApi.Models
{
    public class TransfertDatabaseSettings : ITransfertDatabaseSettings
    {
        public string StudentCoursesCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string ClientCollectionName { get ; set ; } = string.Empty;
        public string BeneficiaireCollectionName { get ; set ; } = string.Empty;
        public string TransfertCollectionName { get ; set ; } = string.Empty;
        public string UsersCollectionName { get ; set ; } = string.Empty;
    }
}
