using LesApi.Models;
using LesApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }
        // GET api/<ClientController>/5
        [HttpGet("{gsm}")]
        public ActionResult<User> Get(string gsm)
        {
            var user = _user.GetUserByGSM(gsm);
            if (user == null)
            {
                return NotFound($"Client with GSM={gsm} not found");
            }
            return Ok(user);
        }
        // recuperer user by Identity:
        // recuperer user by Identity:
        [HttpGet("UserByIdentity/{Nid}")]
        public ActionResult<User> GetUserById(string Nid)
        {
            var user = _user.GetUserByIdentity(Nid);
            if (user == null)
            {
                return NotFound($"User with Identity={Nid} not found");
            }
            return Ok(user);
        }

        // GET api/<ClientController>/5
        [HttpGet("beneficiaire/{IdUser}")]
        public ActionResult<List<Beneficiaire>> GetBeneficiaire(string IdUser)
        {
            if (_user.GetUserById(IdUser)== null)
            {
                return NotFound($"Client with Id={IdUser} not found");
            }
            return _user.GetUserBeneficiaire(IdUser);
        }

        // POST api/<ClientController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            // Vérifier si le numéro de téléphone est déjà utilisé
            var existingUser = _user.GetUserByGSM(user.Gsm);
            if (existingUser != null)
            {
                // Le numéro de téléphone est déjà utilisé, renvoyer une réponse d'erreur
                return Conflict("Le numéro de téléphone doit être unique.");
            }

            // Convertir le rôle de la chaîne en enum
            if (Enum.TryParse<Role>(user.Role.ToString(),out Role userRole))
            {
                // La conversion a réussi, vous pouvez utiliser la variable "userRole" ici
                user.Role = userRole;

                // Si le numéro de téléphone est unique, ajouter l'utilisateur
                _user.AddUser(user);

                return Ok(user);
            }
            else
            {
                // La conversion a échoué
                return BadRequest("Le rôle spécifié n'est pas valide.");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<User> EditUser(string id, [FromBody] User user)
        {
            var existingUser = _user.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound($"User with ID={id} not found");
            }
            _user.EditUser(user);
            return Ok(_user.GetUserByGSM(user.Gsm));
            
        }


    }
}
