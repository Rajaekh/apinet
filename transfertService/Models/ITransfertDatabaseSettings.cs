namespace LesApi.Models
{
    public interface ITransfertDatabaseSettings
    {
        string StudentCoursesCollectionName { get; set; }
        string ClientCollectionName { get; set; }
        string BeneficiaireCollectionName { get; set; }
        string TransfertCollectionName { get; set; }
        string UsersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
