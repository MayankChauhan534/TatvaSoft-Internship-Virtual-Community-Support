using Business_logic_Layer;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly BALCommon _balCommon;
        ResponseResult result = new ResponseResult();

        public CommonController(BALCommon balCommon)
        {
            _balCommon = balCommon;
        }

        [HttpGet("CountryList")]
        public async Task<ResponseResult> CountryList()
        {
            try
            {
                result.Data = await _balCommon.CountryList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Result =ResponseStatus.Error; 
            }
            return result;
        }

        [HttpGet("CityList/{id}")]
        public async Task<ResponseResult> CityList(int id)
        {
            try
            {
                result.Data = await _balCommon.CityList(id);
                result.Result = ResponseStatus.Success;
            }catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Result = ResponseStatus.Error;
            }
            return result;
        }
    }
}
