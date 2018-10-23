using Mobile_Shared_Api.Models;
using System;
using System.Web.Http;

namespace Mobile_Shared_Api.Controllers
{
    [RoutePrefix("api/awards")]
    public class AwardController : ApiController
    {
        [HttpPut]
        [Route("v1/redeem/{id:Guid}")]
        public IHttpActionResult RedeemAward(Guid id, [FromBody] AwardInputItem awardItem)
        {
            try
            {
                DataRepository.Instance.RedeemAward(id, awardItem.AdjustmentAmount);

                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("v1/revoke/{id:Guid}")]
        public IHttpActionResult RevokeAward(Guid id, [FromBody] AwardInputItem awardItem)
        {
            try
            {
                DataRepository.Instance.RevokeAward(id, awardItem.Status, awardItem.AdjustmentAmount);

                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
