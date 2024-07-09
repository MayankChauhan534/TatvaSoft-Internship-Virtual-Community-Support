using Data_Logic_Layer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Logic_Layer
{
    public class DALAdminUser
    {
        private readonly AppDbContext _context;

        public DALAdminUser(AppDbContext context)
        {
            _context = context;
        }

        public string AddUser(User user)
        {
            var result = "";
            try
            {
                var userEmailExsits = _context.User.FirstOrDefault(x => !x.IsDeleted && x.EmailAddress == user.EmailAddress);
                if( userEmailExsits == null)
                {
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
                    _context.User.Add(newUser);
                    _context.SaveChanges();
                    var maxEmployeeId = 0;
                    var lastUserDetail = _context.UserDetail.ToList().LastOrDefault();

                    if(lastUserDetail != null)
                    {
                        maxEmployeeId = Convert.ToInt32(lastUserDetail.EmployeeId);
                    }
                    var newEmployeeId = maxEmployeeId + 1;
                    var newUserDetail = new UserDetail
                    {
                        UserId = newUser.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        UserType = user.UserType,
                        Name = user.FirstName,
                        Surname = user.LastName,
                        EmployeeId = newEmployeeId.ToString(),
                        Department = "IT",
                        Status = true,
                        CreatedDate = DateTime.UtcNow,
                    };
                    _context.UserDetail.Add(newUserDetail);
                    _context.SaveChanges();

                    result = "User added successfully.";
                }
                else
                {
                    result = "Email already exist.";
                }
            }
            catch(Exception ex) 
            {
                result = ex.Message;
            }
            return result;
        }

        public List<UserDetail> GetUserList()
        {
            var userDetailList = from u in _context.User
                                 join ud in _context.UserDetail on u.Id equals ud.UserId into userDetailGroup
                                 from userDetail in userDetailGroup.DefaultIfEmpty()
                                 where !u.IsDeleted && u.UserType == "user" && !userDetail.IsDeleted
                                 select new UserDetail
                                 {
                                     Id = u.Id,
                                     FirstName = u.FirstName,
                                     LastName = u.LastName,
                                     PhoneNumber = u.PhoneNumber,
                                     EmployeeId = userDetail.EmployeeId,
                                     Department = userDetail.Department,
                                     Status = userDetail.Status,
                                 };

            return userDetailList.ToList();
        }

        public async Task<string> DeleteUser(int userId)
        {
            try
            {
                var result = string.Empty;
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var userDetail = await _context.UserDetail.FirstOrDefaultAsync(x => x.UserId == userId);
                        if (userDetail != null)
                        {
                            userDetail.IsDeleted = true;
                        }
                        var user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);
                        if (user != null)
                        {
                            user.IsDeleted = true;
                        }

                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();

                        result = "Delete user successfully";
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw ex;
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<string> UpdateUser(User updatedUser)
        {
            try
            {
                var result = string.Empty;
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    { 
                        var user = await _context.User.FirstOrDefaultAsync(x => x.Id == updatedUser.Id);
                        var userDetail = await _context.UserDetail.FirstOrDefaultAsync(x => x.UserId == user.Id);
                        if (user != null && userDetail != null)
                        {
                            user.FirstName = updatedUser.FirstName;
                            user.LastName = updatedUser.LastName;
                            user.PhoneNumber = updatedUser.PhoneNumber;
                            user.EmailAddress = updatedUser.EmailAddress;
                            user.Password = updatedUser.Password;
                            user.UserType = updatedUser.UserType;
                            user.ModifiedDate = DateTime.UtcNow;


                            userDetail.FirstName = updatedUser.FirstName;
                            userDetail.LastName = updatedUser.LastName;
                            userDetail.PhoneNumber = updatedUser.PhoneNumber;
                            userDetail.EmailAddress = updatedUser.EmailAddress;
                            userDetail.UserType = updatedUser.UserType;
                            userDetail.Name = updatedUser.FirstName;
                            userDetail.Surname = updatedUser.LastName;
                            user.ModifiedDate = DateTime.UtcNow;
                        }
                        else
                        {
                            result = "User not found.";
                        }

                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();

                        result = "User updated successfully.";

                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw ex;
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
