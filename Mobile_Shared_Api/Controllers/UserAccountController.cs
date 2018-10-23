using Mobile_Shared_Api.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace Mobile_Shared_Api.Controllers
{
    [RoutePrefix("api/users")]
    public class UserAccountController : ApiController
    {
        [HttpGet]
        [Route("v1/{id:Guid}")]
        public IHttpActionResult GetUserAccountById(Guid id)
        {
            try
            {
                var user = DataRepository.Instance.GetUserAccountById(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("v1")]
        public IHttpActionResult CreateUserAccount([FromBody] UserAccount userAccount, [FromBody] string password)
        {
            try
            {
                var user = DataRepository.Instance.AddUserAccount(userAccount.FirstName, userAccount.LastName, userAccount.Username, password);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("v1")]
        public IHttpActionResult UpdateUserAccount([FromBody] UserAccount userAccount, [FromBody] string password)
        {
            try
            {
                var user = DataRepository.Instance.UpdateUserAccount(userAccount.Id, userAccount.FirstName, userAccount.LastName, userAccount.Username, password);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("v1/{id:Guid}")]
        public IHttpActionResult DeleteUserAccount(Guid id)
        {
            try
            {
                DataRepository.Instance.DeleteUserAccount(id);

                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("v1/userProfiles/{userAccountId:Guid}")]
        public IHttpActionResult GetAllUserProfilesAccountById(Guid userAccountId)
        {
            try
            {
                var profiles = DataRepository.Instance.GetAllProfilesForUserAccount(userAccountId);

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

        [HttpPost]
        [Route("v1/userProfiles/{userAccountId:Guid}")]
        public IHttpActionResult AddnewUserProfileToAccount(Guid userAccountId, [FromBody] UserProfile profile)
        {
            try
            {
                var newProfile = DataRepository.Instance.AddNewUserProfileToAccount(userAccountId, profile.Client.Id, profile.EmailAddress, profile.Awards);

                if (newProfile == null)
                {
                    return NotFound();
                }

                return Ok(newProfile);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("v1/awards/active/{userAccountId:Guid}")]
        public IHttpActionResult GetActiveAwardsForUser(Guid userAccountId)
        {
            try
            {
                var awards = DataRepository.Instance.GetActiveAwardsForUser(userAccountId);

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
        [Route("v1/awards/redeemed/{userAccountId:Guid}")]
        public IHttpActionResult GetRedeemedAwardsForClient(Guid userAccountId)
        {
            try
            {
                var awards = DataRepository.Instance.GetRedeemedAwardsForUser(userAccountId);

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
        [Route("v1/awards/revoked/{userAccountId:Guid}")]
        public IHttpActionResult GetRevokedAwardsForClient(Guid userAccountId)
        {
            try
            {
                var awards = DataRepository.Instance.GetRevokedAwardsForUser(userAccountId);

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
        [Route("v1/awards/{userAccountId:Guid}")]
        public IHttpActionResult GetAllAwardsForClient(Guid userAccountId)
        {
            try
            {
                var awards = DataRepository.Instance.GetAwardsForUser(userAccountId);

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
