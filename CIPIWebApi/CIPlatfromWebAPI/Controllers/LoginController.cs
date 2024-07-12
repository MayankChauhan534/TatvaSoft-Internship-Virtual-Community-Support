﻿using Business_logic_Layer;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {       
        private readonly BALLogin _balLogin;
        ResponseResult result = new ResponseResult();
        public LoginController(BALLogin balLogin)
        {           
            _balLogin = balLogin;
        }
            

        [HttpPost]
        [Route("LoginUser")]
        public ResponseResult LoginUser(User user)
        {
            try
            {                                
                result.Data = _balLogin.LoginUser(user);
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
        [Route("Register")]
        public ResponseResult Register(User user)
        {
            try
            {
                result.Data=_balLogin.Register(user);
                result.Result = ResponseStatus.Success;
            }
            catch(Exception ex) 
            {
                result.Result= ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ResponseResult> UpdateUser(User user)
        {
            try
            {
                result.Data = await _balLogin.UpdateUserDetails(user);
                result.Result = ResponseStatus.Success;
            }
            catch(Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message= ex.Message;
            }
            return result ;
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<ResponseResult> GetUserById(int id)
        {
            try
            {
                result.Data= await _balLogin.GetUserById(id);
                result.Result = ResponseStatus.Success;
            }
            catch(Exception ex)
            {
                result.Result= ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
