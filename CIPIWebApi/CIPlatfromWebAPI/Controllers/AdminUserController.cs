using Business_logic_Layer;
using Data_Access_Layer;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly BLLAdminUser _bllAdminUser;

        public AdminUserController(BLLAdminUser bllAdminUser)
        {
            _bllAdminUser = bllAdminUser;
        }

        [HttpGet("UserDetailList")]
        public async Task<IActionResult> GetUserDetailList()
        {
            try
            {
                var userDetail = await _bllAdminUser.userDetailAsync();
                return Ok(new ResponseResult { Data = userDetail, Result = ResponseStatus.Success });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new ResponseResult { Result = ResponseStatus.Error, Message = ex.Message });
            }
        }
        [HttpDelete("DeleteUserAndUserDetail/{id}")]
        public async Task<IActionResult> DeleteUserAndUserDetail(int id)
        {
            try
            {
                var result = await _bllAdminUser.DelDeleteUserAndUserDetail(id);
                return Ok(new ResponseResult { Data = result, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseResult { Data = ex.Message, Result = ResponseStatus.Error });
            }
        }
        [HttpGet]
        [Route("MissionApplicationList")]
        public ResponseResult MissionApplicationList()
        {
            ResponseResult result = new ResponseResult();
            try
            {
                result.Data = _bllAdminUser.GetMissionApplicationList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Result = ResponseStatus.Error;
            }
            return result;
        }
    }
}
