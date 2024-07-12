using Data_Access_Layer;
using Data_Access_Layer.JWTService;
using Data_Access_Layer.Repository.Entities;

namespace Business_logic_Layer
{
    public class BALLogin
    {
        private readonly DALLogin _dalLogin;
        private readonly JwtService _jwtService;
        ResponseResult result = new ResponseResult();
        public BALLogin(DALLogin dalLogin, JwtService jwtService)
        {
            _dalLogin = dalLogin;
            _jwtService = jwtService;
        }
    
        public ResponseResult LoginUser(User user)
        {
            try
            {
                User userObj= new User();
                userObj = UserLogin(user);

                if(userObj != null)
                {
                    if(userObj.Message.ToString() == "Login Successfully")
                    {
                        result.Message = userObj.Message;
                        result.Result = ResponseStatus.Success;
                        result.Data = _jwtService.GenerateToken(userObj.Id.ToString(), userObj.FirstName, userObj.LastName, userObj.PhoneNumber, userObj.EmailAddress,userObj.UserType);
                    }
                    else
                    {
                        result.Message = userObj.Message;
                        result.Result = ResponseStatus.Error;
                    }
                }
                else
                {
                    result.Message = "Error in Login";
                    result.Result = ResponseStatus.Error;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public User  UserLogin(User user)
        {
            User userOb = new User()
            {
                EmailAddress = user.EmailAddress,
                Password = user.Password
            };

            return _dalLogin.LoginUser(user);
        }
        public String Register(User user)
        {
            return _dalLogin.Register(user);
        }

        public async Task<String> UpdateUserDetails(User user)
        {
            return await _dalLogin.UpdateUserDetails(user);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dalLogin.GetUserById(id);
        }
    }
}
