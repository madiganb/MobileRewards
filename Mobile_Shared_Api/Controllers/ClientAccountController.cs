using Mobile_Shared_Api.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Http;

namespace Mobile_Shared_Api.Controllers
{
    [RoutePrefix("api/clients")]
    public class ClientAccountController : ApiController
    {
        [HttpGet]
        [Route("v1/details/{id:Guid}")]
        public IHttpActionResult GetClientAccountById(Guid id)
        {
            try
            {
                var client = DataRepository.Instance.GetClientAccountById(id);

                if (client == null)
                {
                    return NotFound();
                }

                return Ok(client);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("v1")]
        public IHttpActionResult GetAllClients()
        {
            try
            {
                var clients = DataRepository.Instance.ClientAccounts.Values.ToList();

                if (!clients.Any())
                {
                    return NotFound();
                }

                return Ok(clients);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("v1")]
        public IHttpActionResult CreateClientAccount([FromBody] string client)
        {
            try
            {
                var clientObj = JsonConvert.DeserializeObject<ClientAccount>(client);

                var result = DataRepository.Instance.AddClientAccount(clientObj.ClientName);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch 
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("v1")]
        public IHttpActionResult UpdateClientAccount([FromBody] string client)
        {
            try
            {
                var clientObj = JsonConvert.DeserializeObject<ClientAccount>(client);

                var clientAccount = DataRepository.Instance.UpdateClientAccount(clientObj.Id, clientObj.ClientName);

                if (clientAccount == null)
                {
                    return NotFound();
                }

                return Ok(clientAccount);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("v1/{id:Guid}")]
        public IHttpActionResult DeleteClientAccount(Guid id)
        {
            try
            {
                DataRepository.Instance.DeleteClientAccount(id);

                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }

        [Route("v1/userProfiles/{clientAccountId:Guid}")]
        public IHttpActionResult GetAllUserProfilesByClientAccountId(Guid clientAccountId)
        {
            try
            {
                var profiles = DataRepository.Instance.GetProfilesForClientAccount(clientAccountId);

                if (profiles == null || !profiles.Any())
                {
                    return NotFound();
                }

                return Ok(profiles);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("v1/awards/active/{clientAccountId:Guid}")]
        public IHttpActionResult GetActiveAwardsForClient(Guid clientAccountId)
        {
            try
            {
                var awards = DataRepository.Instance.GetActiveAwardsForClient(clientAccountId);

                if (awards == null || !awards.Any())
                {
                    return NotFound();
                }

                return Ok(awards);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("v1/awards/redeemed/{clientAccountId:Guid}")]
        public IHttpActionResult GetRedeemedAwardsForClient(Guid clientAccountId)
        {
            try
            {
                var awards = DataRepository.Instance.GetRedeemedAwardsForClient(clientAccountId);

                if (awards == null || !awards.Any())
                {
                    return NotFound();
                }

                return Ok(awards);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("v1/awards/revoked/{clientAccountId:Guid}")]
        public IHttpActionResult GetRevokedAwardsForClient(Guid clientAccountId)
        {
            try
            {
                var awards = DataRepository.Instance.GetRevokedAwardsForClient(clientAccountId);

                if (awards == null || !awards.Any())
                {
                    return NotFound();
                }

                return Ok(awards);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("v1/awards/{clientAccountId:Guid}")]
        public IHttpActionResult GetAllAwardsForClient(Guid clientAccountId)
        {
            try
            {
                var awards = DataRepository.Instance.GetAwardsForClient(clientAccountId);

                if (awards == null || !awards.Any())
                {
                    return NotFound();
                }

                return Ok(awards);
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
