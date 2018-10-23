using Mobile_Shared_Api.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace Mobile_Shared_Api.Controllers
{
    [RoutePrefix("api/profiles")]
    public class UserProfileController : ApiController
    {
        [HttpGet]
        [Route("v1/{userProfileId:Guid}")]
        public IHttpActionResult GetUserProfileById(Guid userProfileId)
        {
            try
            {
                var profile = DataRepository.Instance.GetUserProfileByProfileId(userProfileId);

                if (profile == null)
                {
                    return NotFound();
                }

                return Ok(profile);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("v1")]
        public IHttpActionResult GetAllProfiles()
        {
            try
            {
                var profiles = DataRepository.Instance.UserProfiles;

                if (!profiles.Any())
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

        [HttpPut]
        [Route("v1")]
        public IHttpActionResult UpdateUserProfile([FromBody] UserProfile profile)
        {
            try
            {
                var newProfile = DataRepository.Instance.UpdateUserProfile(profile.Id, profile.Client.Id, profile.EmailAddress, profile.Awards);

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

        [HttpDelete]
        [Route("v1/{profileId:Guid}/{userAccountId:Guid}")]
        public IHttpActionResult DeleteUserProfile(Guid profileId, Guid userAccountId)
        {
            try
            {
                DataRepository.Instance.DeleteProfile(profileId, userAccountId);

                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("v1/awards/active/{profileId:Guid}")]
        public IHttpActionResult GetActiveAwardsForUserProfile(Guid profileId)
        {
            try
            {
                var awards = DataRepository.Instance.GetActiveAwardsForUserProfile(profileId);

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
        [Route("v1/awards/redeemed/{profileId:Guid}")]
        public IHttpActionResult GetRedeemedAwardsForUserProfile(Guid profileId)
        {
            try
            {
                var awards = DataRepository.Instance.GetRedeemedAwardsForUserProfile(profileId);

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
        [Route("v1/awards/revoked/{profileId:Guid}")]
        public IHttpActionResult GetRevokedAwardsForUserProfile(Guid profileId)
        {
            try
            {
                var awards = DataRepository.Instance.GetRevokedAwardsForUserProfile(profileId);

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

        [HttpPost]
        [Route("v1/awards/{profileId:Guid}")]
        public IHttpActionResult CreateAwardForUserProfile(Guid userProfileId, [FromBody] AwardItem award)
        {
            try
            {
                var newAward = DataRepository.Instance.CreateAwardForUserProfile(userProfileId, award.AwardName, award.EarnedValue);

                if (newAward == null)
                {
                    return NotFound();
                }

                return Ok(newAward);
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
