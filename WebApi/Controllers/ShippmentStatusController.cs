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
    public class ShippmentStatusController : ControllerBase
    {
        IShippmentStatus _shippmentStatus;
        public ShippmentStatusController(IShippmentStatus shippmentStatus)
        {
            _shippmentStatus= shippmentStatus;
        }
        // GET: api/<ShippmentStatusController>
        [HttpGet]
        public ActionResult<ApiResponse<List<ShippmentStatusDTO>>> Get()
        {
            try {
                var data = _shippmentStatus.GetAll();

                return Ok( ApiResponse<List<ShippmentStatusDTO>>.SuccessResponse(data));
            }
            catch(DataAccessException daEx)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<List<ShippmentStatusDTO>>.FailResponse(
                    "Data Access Exception ", new List<string>() {daEx.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
            catch (Exception ex)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<List<ShippmentStatusDTO>>.FailResponse(
                    "General Exception ", new List<string>() { ex.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
        }

    

    
    }
}
