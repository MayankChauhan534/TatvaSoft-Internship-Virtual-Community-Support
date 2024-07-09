using Data_Access_Layer.Repository.Entities;
using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_logic_Layer
{
    public class BALMission
    {
        private readonly DALMission _dalMission;

        public BALMission(DALMission dalMission)
        {
            _dalMission = dalMission;
        }

        public List<Missions> MissionList()
        {
            return _dalMission.MissionList();
        }

        public string AddMission(Missions mission)
        {
            return _dalMission.AddMission(mission);
        }

        public async Task<string> UpdateMission(Missions mission)
        {
            return await _dalMission.UpdateMission(mission);
        }

        public async Task<string> DeleteMission(int id)
        {
            return await (_dalMission.DeleteMission(id));
        }       
    }
}
