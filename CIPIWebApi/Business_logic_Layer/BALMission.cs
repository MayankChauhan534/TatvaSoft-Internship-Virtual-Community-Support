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

        public async Task<Missions> MissionDetailById(int id)
        {
            return await _dalMission.MissionDetailById(id);
        }
        public async Task<string> UpdateMission(Missions mission)
        {
            return await _dalMission.UpdateMission(mission);
        }
        public async Task<string> DeleteMission(int id)
        {
            return await (_dalMission.DeleteMission(id));
        }
        public List<Missions> ClientSideMissionList(int userId)
        {
            return _dalMission.ClientSideMissionList(userId);
        }
        public async Task<List<MissionTheme>> GetMissionThemeList()
        {
            return await _dalMission.GetMissionThemeList();
        }
        public async Task<List<MissionSkill>> GetMissionSkillList()
        {
            return await _dalMission.GetMissionSkillList();
        }
        public string ApplyMission(MissionApplication missionApplication)
        {
            return _dalMission.ApplyMission(missionApplication);
        }
        public string MissionApplicationApprove(MissionApplication missionApplication)
        {
            return _dalMission.MissionApplicationApprove(missionApplication.Id);
        }
        public string MissionApplicationDelete(MissionApplication missionApplication)
        {
            return _dalMission.MissionApplicationDelete(missionApplication.Id);
        }
    }
}
