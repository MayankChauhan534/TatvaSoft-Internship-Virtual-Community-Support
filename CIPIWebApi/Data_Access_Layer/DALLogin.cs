using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Data_Access_Layer
{
    public class DALLogin
    {
        private readonly AppDbContext _cIDbContext;
        public DALLogin(AppDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }

        public User LoginUser(User user)
        {
            User userObj = new User();
            try
            {
                    var query = from u in _cIDbContext.User
                                where u.EmailAddress == user.EmailAddress && u.IsDeleted == false
                                select new
                                {
                                    u.Id,
                                    u.FirstName,
                                    u.LastName,
                                    u.PhoneNumber,
                                    u.EmailAddress,
                                    u.UserType,
                                    u.Password
                                };

                    var userData = query.FirstOrDefault();

                    if (userData != null)
                    {
                        if (userData.Password == user.Password)
                        {
                            userObj.Id = userData.Id;
                            userObj.FirstName = userData.FirstName;
                            userObj.LastName = userData.LastName;
                            userObj.PhoneNumber = userData.PhoneNumber;
                            userObj.EmailAddress = userData.EmailAddress;
                            userObj.UserType = userData.UserType;
                            userObj.Message = "Login Successfully";
                        }
                        else
                        {
                            userObj.Message = "Incorrect Password.";
                        }
                    }
                    else
                    {
                        userObj.Message = "Email Address Not Found.";
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userObj;
        }

        public String Register(User user)
        {
            String result = String.Empty;
            try{
                bool emailExisted = _cIDbContext.User.Any(u => u.EmailAddress == user.EmailAddress && !u.IsDeleted);
                if (!emailExisted)
                {
                    String maxEmployedIdstr = _cIDbContext.UserDetail.Max(u => u.EmployeeId);
                    int maxEmployeeId = 0;
                    if (!String.IsNullOrEmpty(maxEmployedIdstr))
                    {
                        if (int.TryParse(maxEmployedIdstr, out int parsedEmployeeId))
                        {
                            maxEmployeeId = parsedEmployeeId;
                        }
                        else
                        {
                            throw new Exception("Error while converting string to int");
                        }
                    }
                    int newEmployeedId = maxEmployeeId + 1;
                    var newUser = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        Password = user.Password,
                        UserType = user.UserType,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                    };
                    _cIDbContext.User.Add(newUser);
                    _cIDbContext.SaveChanges();
                    var newUserDeatil = new UserDetail
                    {
                        UserId = newUser.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        UserType = user.UserType,
                        Name = user.FirstName,
                        Surname = user.LastName,
                        EmployeeId = newEmployeedId.ToString(),
                        Department = "IT",
                        Status = true
                    };
                    _cIDbContext.UserDetail.Add(newUserDeatil);
                    _cIDbContext.SaveChanges();
                    result = "User Registered Successfully";
                }
                else
                {
                    throw new Exception("Email already existed");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<String> UpdateUserDetails(User user)
        {
            try
            {
                var existedUser = await _cIDbContext.User.Where(u => u.Id == user.Id && !u.IsDeleted).FirstOrDefaultAsync();
                if (existedUser != null)
                {
                    existedUser.FirstName = user.FirstName;
                    existedUser.LastName = user.LastName;
                    existedUser.PhoneNumber = user.PhoneNumber;
                    existedUser.EmailAddress = user.EmailAddress;
                    existedUser.Password = user.Password;
                    existedUser.UserType = user.UserType;
                    existedUser.ModifiedDate = DateTime.UtcNow;
                    existedUser.UserType = "user";
                    await _cIDbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("user with given id doesnt exist or deleted");
                }
                var existedUserDetail = await _cIDbContext.UserDetail.Where(ud =>  ud.UserId == user.Id && !ud.IsDeleted).FirstOrDefaultAsync();
                if(existedUserDetail != null)
                {
                    existedUserDetail.FirstName = user.FirstName;
                    existedUserDetail.LastName = user.LastName;
                    existedUserDetail.PhoneNumber = user.PhoneNumber;
                    existedUserDetail.EmailAddress = user.EmailAddress;
                    existedUserDetail.Name= user.FirstName;
                    existedUserDetail.Surname = user.LastName;
                    await _cIDbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("user with given id doesnt exist or deleted");
                }
                return "User Updated Successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUserById(int id)
        {
            return await _cIDbContext.User.Where(u => !u.IsDeleted && u.Id == id).FirstOrDefaultAsync();
        }
    }
}
