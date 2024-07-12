using Data_Access_Layer;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_logic_Layer
{
    public class BLLMissionTheme
    {
        private readonly DALMissionTheme _dalMissionTheme;
        public BLLMissionTheme(DALMissionTheme dalMissionTheme)
        {
            _dalMissionTheme = dalMissionTheme;
        }
        public async Task<List<MissionTheme>> GetMissionThemeList()
        {
            return await _dalMissionTheme.GetMissionThemeList();
        }
        public async Task<String> AddMissionTheme(MissionTheme missionTheme)
        {
            return await _dalMissionTheme.AddMissionTheme(missionTheme);
        }
        public async Task<MissionTheme> GetMissionThemeById(int id)
        {
            return await _dalMissionTheme.GetMissionThemeById(id);
        }
        public async Task<String> UpdateMissionTheme(MissionTheme missionTheme)
        {
            return await _dalMissionTheme.UpdateMissionTheme(missionTheme);
        }
        public async Task<String> DeleteMissionTheme(int id)
        {
            return await _dalMissionTheme.DeleteMissionTheme(id);
        }
    }
}
