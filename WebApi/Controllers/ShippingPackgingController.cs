
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
    public class ShippingPackgingController : ControllerBase
    {
        IShipingPackages _packgingTypes;
        public ShippingPackgingController(IShipingPackages packgingTypes)
        {
            _packgingTypes = packgingTypes;
        }
        // GET: api/<ShippingTypesController>
        [HttpGet]
        public ActionResult<ApiResponse<List<ShipingPackageDTO>>> Get()
        {
            try
            {
                var data = _packgingTypes.GetAll();

                return Ok(ApiResponse<List<ShipingPackageDTO>>.SuccessResponse(data));
            }
            catch(DataAccessException daEx)
            {
                return StatusCode(500, ApiResponse<List<ShipingPackageDTO>>.FailResponse
                    ("data access exception", new List<string>() { daEx.Message }));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ApiResponse<List<ShipingPackageDTO>>.FailResponse
                    ("general exception", new List<string>() { ex.Message }));
            }

        }

        // GET api/<ShippingTypesController>/5
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<ShipingPackageDTO>> Get(Guid id)
        {
            try
            {
                var data = _packgingTypes.GetById(id);

                return Ok(ApiResponse<ShipingPackageDTO>.SuccessResponse(data));
            }
            catch (DataAccessException daEx)
            {
                return StatusCode(500, ApiResponse<ShipingPackageDTO>.FailResponse
                    ("data access exception", new List<string>() { daEx.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ShipingPackageDTO>.FailResponse
                    ("general exception", new List<string>() { ex.Message }));
            }
        }
    }
}
