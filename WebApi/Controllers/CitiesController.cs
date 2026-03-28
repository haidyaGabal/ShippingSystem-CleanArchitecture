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
    public class CitiesController : ControllerBase
    {
        ICity _city;
        public CitiesController(ICity city)
        {
            _city = city;
        
        
        }
        // GET: api/<ShippingTypesController>
        [HttpGet]
        public ActionResult<ApiResponse<List<CityDTO>>> Get()
        {
            try {
                var data = _city.GetAll();

                return Ok( ApiResponse<List<CityDTO>>.SuccessResponse(data));
            }
            catch(DataAccessException daEx)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<List<CityDTO>>.FailResponse(
                    "Data Access Exception ", new List<string>() {daEx.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
            catch (Exception ex)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<List<CityDTO>>.FailResponse(
                    "General Exception ", new List<string>() { ex.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
        }

        // GET api/<ShippingTypesController>/5
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<CityDTO>> Get(Guid id)
        {
            try
            {
                var data = _city.GetById(id);

                return Ok(ApiResponse<CityDTO>.SuccessResponse(data));
            }
            catch (DataAccessException daEx)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<CityDTO>.FailResponse(
                    "Data Access Exception ", new List<string>() { daEx.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
            catch (Exception ex)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<CityDTO>.FailResponse(
                    "General Exception ", new List<string>() { ex.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
        }


        ///get by country
        ///
        [HttpGet("GetByCountry{id}")]
        public ActionResult<ApiResponse<CityDTO>> GetByCountry(Guid id)
        {
            try
            {
                var data = _city.GetByCountryId(id);

                return Ok(ApiResponse<List<CityDTO>>.SuccessResponse(data));
            }
            catch (DataAccessException daEx)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<CityDTO>.FailResponse(
                    "Data Access Exception ", new List<string>() { daEx.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
            catch (Exception ex)
            {
                ///500 internal server error
                return StatusCode(500, ApiResponse<CityDTO>.FailResponse(
                    "General Exception ", new List<string>() { ex.Message }
                    ));
                /// add message , and list of error if i need return errors but this not secure ->  you must return log 
                /// new List<string>()->( list of error) this prefer if i have set of validation and if you need post

            }
        }


    }
}
