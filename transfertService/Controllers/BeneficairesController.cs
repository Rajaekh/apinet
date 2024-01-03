using LesApi.Models;
using LesApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficairesController : ControllerBase
    {
        private readonly IBeneficiaire _beneficiaire;
        public BeneficairesController(IBeneficiaire beneficiaire)
        {
            _beneficiaire = beneficiaire;

        }
        [HttpGet("{gsm}")]
        public ActionResult<Beneficiaire> Get(string gsm)
        {
            var beneficiaire = _beneficiaire.GetBeneficiaireByGSM(gsm);
            if (beneficiaire == null)
            {
                return NotFound($"Client with GSM={gsm} not found");
            }
            return Ok(beneficiaire);
        }
        [HttpPost]
        public ActionResult<Beneficiaire> Post([FromBody] Beneficiaire beneficiaire)
        {
            // Vérifier si le numéro de téléphone est déjà utilisé
            var existingbenfecaire = _beneficiaire.GetBeneficiaireByGSM(beneficiaire.NumeroGsm);
            if (existingbenfecaire != null)
            {
                // Le numéro de téléphone est déjà utilisé, renvoyer une réponse d'erreur
                return Conflict("Le numéro de téléphone doit être unique.");
            }

            // Si le numéro de téléphone est unique, ajouter le client
            _beneficiaire.AddBeneficiaire(beneficiaire);

            return Ok(_beneficiaire.GetBeneficiaireByGSM(beneficiaire.NumeroGsm));
        }

    }
}
