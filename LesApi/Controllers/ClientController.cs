using LesApi.Models;
using LesApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClient _iClient;

        public ClientController(IClient iClient)
        {
            _iClient = iClient;
        }

        // GET api/<ClientController>/5
        [HttpGet("{phone}")]
        public ActionResult<Client> Get(string phone)
        {
            var client =_iClient.GetClientByPhone(phone);
            if(client == null)
            {
                return NotFound($"Client with Id={phone} not found");
            }
            return Ok(client);
        }
        // GET api/<ClientController>/5
        [HttpGet("beneficiaire/{IdClient}")]
        public ActionResult<List<Beneficiaire>> GetBeneficiaire(string IdClient)
        {
            if (_iClient.GetClientById(IdClient) == null)
            {
                return NotFound($"Client with Id={IdClient} not found");
            }
            return _iClient.GetClientBeneficiaire(IdClient);
        }



        // POST api/<ClientController>
        [HttpPost]
        public ActionResult<Client> Post([FromBody] Client client)
        {
            // Vérifier si le numéro de téléphone est déjà utilisé
            var existingClient = _iClient.GetClientByPhone(client.Phone);
            if (existingClient != null)
            {
                // Le numéro de téléphone est déjà utilisé, renvoyer une réponse d'erreur
                return Conflict("Le numéro de téléphone doit être unique.");
            }

            // Si le numéro de téléphone est unique, ajouter le client
            _iClient.AddClient(client);

            return CreatedAtAction(nameof(Get), new { phone = client.Phone }, client);
        }








        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
