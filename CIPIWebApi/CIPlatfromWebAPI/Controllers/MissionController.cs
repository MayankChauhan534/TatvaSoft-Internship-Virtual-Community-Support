using Business_logic_Layer;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Reflection;
namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly BALMission _balMission;
        private readonly ResponseResult result = new ResponseResult();
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly ILogger<MissionController> _logger;

        public MissionController(BALMission balMission, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, ILogger<MissionController> logger)
        {
            _balMission = balMission;
            _environment = environment;
            _logger = logger;
        }

        [HttpGet]
        [Route("MissionList")]
        public ResponseResult MissionList()
        {
            try
            {
                result.Data = _balMission.MissionList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("AddMission")]
        public ResponseResult AddMission(Missions mission)
        {
            try
            {
                result.Data = _balMission.AddMission(mission);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] List<IFormFile> upload)
        {
            try
            {
                string filePath = "";
                string fullPath = "";
                var files = Request.Form.Files;
                List<string> fileList = new List<string>();
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        //filePath = Path.Combine("wwwroot", "UploadMissionImage", "Mission");
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                        string fileRootPath = Path.Combine(_environment.ContentRootPath, filePath);
                       
                        if (!Directory.Exists(fileRootPath))
                        {
                            Directory.CreateDirectory(fileRootPath);
                        }

                        string name = Path.GetFileNameWithoutExtension(fileName);
                        string extension = Path.GetExtension(fileName);
                        string fullFileName = name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                        fullPath = Path.Combine(fileRootPath, fullFileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        fileList.Add(Path.Combine("UploadMissionImage", "Mission", fullFileName));
                    }
                }
                return Ok(fileList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while uploading images.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("MissionDetailById/{id}")]
        public async Task<ResponseResult> MissionDetailById(int id)
        {
            try
            {
                result.Data = await _balMission.MissionDetailById(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPut("UpdateMission")]
        public async Task<ResponseResult> UpdateMission(Missions mission)
        {
            try
            {
                result.Data = await _balMission.UpdateMission(mission);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpDelete("DeleteMission/{id}")]
        public async Task<ResponseResult> DeleteMission(int id)
        {
            try
            {
                result.Data = await _balMission.DeleteMission(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpGet("GetMissionThemeList")]
        public async Task<ResponseResult> GetMissionThemeList()
        {
            try
            {
                result.Data = await _balMission.GetMissionThemeList();
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
                result.Data = await _balMission.GetMissionSkillList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("MissionApplicationApprove")]
        public ResponseResult MissionApplicationApprove(MissionApplication missionApplication)
        {
            try
            {
                result.Data = _balMission.MissionApplicationApprove(missionApplication);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("MissionApplicationDelete")]
        public ResponseResult MissionApplicationDelete(MissionApplication missionApplication)
        {
            try
            {
                result.Data = _balMission.MissionApplicationDelete(missionApplication);
                result.Result = ResponseStatus.Success;
            }catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
