using Data_Access_Layer;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_logic_Layer
{
    public class BLLMissoinSkill
    {
        private readonly DALMissionSkill _dalMissionSkill;

        public BLLMissoinSkill(DALMissionSkill dalMissionSkill)
        {
            _dalMissionSkill = dalMissionSkill;
        }

        public async Task<List<MissionSkill>> GetMissionSkillList()
        {
            return await _dalMissionSkill.getmissionskilllist();
        }

        public async Task<String> AddMissionSkill(MissionSkill missoinSkill)
        {
            return await _dalMissionSkill.AddMissionSkill(missoinSkill);
        }

        public async Task<MissionSkill> GetMissionSkillById(int id)
        {
            return await _dalMissionSkill.GetMissionSkillById(id);
        }
        public async Task<String> DeleteMissionSkill(int id)
        {
            return await _dalMissionSkill.DeleteMissionSkill(id);
        }
        public async Task<String> UpdateMissionSkill(MissionSkill missionSkill)
        {
            return await _dalMissionSkill.UpdateMissionSkill(missionSkill);
        }
    }
}
