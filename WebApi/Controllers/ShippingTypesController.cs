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
    public class ShippingTypesController : ControllerBase
    {
        IShipingType _shippingType;
        public ShippingTypesController(IShipingType shippingType)
        {
            _shippingType = shippingType;
        
        
        }
        // GET: api/<ShippingTypesController>
        [HttpGet]
        public ActionResult<ApiResponse<List<ShipingTypeDTO>>> Get()
        {
            try {
                var data = _shippingType.GetAll();

                return Ok( ApiResponse<List<ShipingTypeDTO>>.SuccessResponse(data));
            }
            catch(DataAccessException daEx)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<List<ShipingTypeDTO>>.FailResponse(
                    "Data Access Exception ", new List<string>() {daEx.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
            catch (Exception ex)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<List<ShipingTypeDTO>>.FailResponse(
                    "General Exception ", new List<string>() { ex.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
        }

        // GET api/<ShippingTypesController>/5
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<ShipingTypeDTO>> Get(Guid id)
        {
            try
            {
                var data = _shippingType.GetById(id);

                return Ok(ApiResponse<ShipingTypeDTO>.SuccessResponse(data));
            }
            catch (DataAccessException daEx)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<ShipingTypeDTO>.FailResponse(
                    "Data Access Exception ", new List<string>() { daEx.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
            catch (Exception ex)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<ShipingTypeDTO>.FailResponse(
                    "General Exception ", new List<string>() { ex.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
        }

    
    }
}
