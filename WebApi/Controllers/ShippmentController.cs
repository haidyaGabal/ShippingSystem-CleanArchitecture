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
    public class ShippmentController : ControllerBase
    {
        IShippment _shippment;
        public ShippmentController(IShippment shippment)
        {
            _shippment = shippment;
        }
        // GET: api/<ShippmentController>
        [HttpGet]
        public ActionResult<ApiResponse<List<ShippmentDTO>>> Get()
        {
            try {
                var data = _shippment.GetAll();

                return Ok( ApiResponse<List<ShippmentDTO>>.SuccessResponse(data));
            }
            catch(DataAccessException daEx)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<List<ShippmentDTO>>.FailResponse(
                    "Data Access Exception ", new List<string>() {daEx.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
            catch (Exception ex)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<List<ShippmentDTO>>.FailResponse(
                    "General Exception ", new List<string>() { ex.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
        }

        // GET api/<ShippmentController>/5
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<ShippmentDTO>> Get(Guid id)
        {
            try
            {
                var data = _shippment.GetById(id);

                return Ok(ApiResponse<ShippmentDTO>.SuccessResponse(data));
            }
            catch (DataAccessException daEx)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<ShippmentDTO>.FailResponse(
                    "Data Access Exception ", new List<string>() { daEx.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
            catch (Exception ex)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<ShippmentDTO>.FailResponse(
                    "General Exception ", new List<string>() { ex.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
        }

    
    }
}
