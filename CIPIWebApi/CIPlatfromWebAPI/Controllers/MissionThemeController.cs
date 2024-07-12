using Business_logic_Layer;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionThemeController : ControllerBase
    {
        private readonly BLLMissionTheme _bllMissionTheme;
        ResponseResult result = new ResponseResult();
        public MissionThemeController(BLLMissionTheme bllMissionTheme)
        {
            _bllMissionTheme = bllMissionTheme;
        }
        [HttpGet("GetMissionThemeList")]
        public async Task<ResponseResult> GetMissionThemeList()
        {
            try
            {
                result.Data = await _bllMissionTheme.GetMissionThemeList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost("AddMissionTheme")]
        public async Task<ResponseResult> AddMissionTheme(MissionTheme missionTheme)
        {
            try
            {
                result.Data = await _bllMissionTheme.AddMissionTheme(missionTheme);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpGet("GetMissionThemeById/{id}")]
        public async Task<ResponseResult> GetMissionThemeById(int id)
        {
            try
            {
                result.Data = await _bllMissionTheme.GetMissionThemeById(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPut("UpdateMissionTheme")]
        public async Task<ResponseResult> UpdateMissionTheme(MissionTheme missionTheme)
        {
            try
            {
                result.Data = await _bllMissionTheme.UpdateMissionTheme(missionTheme);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpDelete("DeleteMissionTheme/{id}")]
        public async Task<ResponseResult> DeleteMissionTheme(int id)
        {
            try
            {
                result.Data = await _bllMissionTheme.DeleteMissionTheme(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
