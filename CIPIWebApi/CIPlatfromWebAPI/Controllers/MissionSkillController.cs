using Business_logic_Layer;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionSkillController : ControllerBase
    {
        private readonly BLLMissoinSkill _bllMissionSkill;
        ResponseResult result = new ResponseResult();

        public MissionSkillController(BLLMissoinSkill bllMissionSkill)
        {
            _bllMissionSkill = bllMissionSkill;
        }

        [HttpPost("AddMissionSkill")]
        public async Task<ResponseResult> AddMissionSkill(MissionSkill missionSkill)
        {
            try
            {
                result.Data = await _bllMissionSkill.AddMissionSkill(missionSkill);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpGet("GetMissionSkillList")]
        public async Task<ResponseResult> GetMissionSkillList()
        {
            try
            {
                result.Data = await _bllMissionSkill.GetMissionSkillList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet("GetMissionSkillById/{id}")]
        public async Task<ResponseResult> GetMissionSkillById(int id)
        {
            try
            {
                result.Data = await _bllMissionSkill.GetMissionSkillById(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpDelete("DeleteMissionSkill/{id}")]
        public async Task<ResponseResult> DeleteMissionSkill(int id)
        {
            try
            {
                result.Data =await _bllMissionSkill.DeleteMissionSkill(id);
                result.Result = ResponseStatus.Success;
            }catch(Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPut("UpdateMissionSkill")]
        public async Task<ResponseResult> UpdateMissionSkill(MissionSkill missionSkill)
        {
            try
            {
                result.Data = await _bllMissionSkill.UpdateMissionSkill(missionSkill);
                result.Result = ResponseStatus.Success;
            }catch(Exception ex)
            {
                result.Result= ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
