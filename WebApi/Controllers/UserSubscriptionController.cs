using BL.Contracts;
using BL.DTOs;
using DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscriptionController : ControllerBase
    {
       IUserSubscription _userSubscription;
        public UserSubscriptionController(IUserSubscription userSubscription)
        {
            _userSubscription = userSubscription;
        
        
        }
        // GET: api/<UserSubscriptionController>
        [HttpGet]
        public ActionResult<ApiResponse<List<UserSubscriptionDTO>>> Get()
        {
            try {
                var data = _userSubscription.GetAll();

                return Ok( ApiResponse<List<UserSubscriptionDTO>>.SuccessResponse(data));
            }
            catch(DataAccessException daEx)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<List<UserSubscriptionDTO>>.FailResponse(
                    "Data Access Exception ", new List<string>() {daEx.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
            catch (Exception ex)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<List<UserSubscriptionDTO>>.FailResponse(
                    "General Exception ", new List<string>() { ex.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
        }



    
    }
}
