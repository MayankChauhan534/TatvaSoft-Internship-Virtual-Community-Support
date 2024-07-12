using Data_Access_Layer;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_logic_Layer
{
    public class BLLAdminUser
    {
        private readonly DALAdminUser _dalAdminUser;

        public BLLAdminUser(DALAdminUser dalAdminUser)
        {
            _dalAdminUser = dalAdminUser;
        }

        public async Task<List<UserDetail>> userDetailAsync()
        {
            return await _dalAdminUser.UserDetailListAsync();
        }

        public async Task<String> DelDeleteUserAndUserDetail(int id)
        {
            return await _dalAdminUser.DeleteUserAndUserDetail(id);
        }
        public List<MissionApplication> GetMissionApplicationList()
        {
            return _dalAdminUser.GetMissionApplicationList();
        }
    }
}
