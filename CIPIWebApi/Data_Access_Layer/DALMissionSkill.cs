using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class DALMissionSkill
    {
        private readonly AppDbContext _appDbContext;

        public DALMissionSkill(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<MissionSkill>> getmissionskilllist()
        {
            return await _appDbContext.MissionSkill.Where(ms => !ms.IsDeleted).ToListAsync();
        }

        public async Task<String> AddMissionSkill(MissionSkill missionSkill)
        {
            missionSkill.IsDeleted = false;
            missionSkill.CreatedDate = DateTime.UtcNow;
            await _appDbContext.MissionSkill.AddAsync(missionSkill);
            await _appDbContext.SaveChangesAsync();
            return "Mission added successfully";
        }

        public async Task<MissionSkill> GetMissionSkillById(int id)
        {
            return await _appDbContext.MissionSkill.Where(X => X.Id == id && !X.IsDeleted).FirstOrDefaultAsync();
        }
           
        public async Task<String> DeleteMissionSkill(int id)
        {
            try
            {
                var existedSkill = await _appDbContext.MissionSkill.Where(m =>  m.Id == id && !m.IsDeleted).FirstOrDefaultAsync();
                if (existedSkill != null)
                {
                    existedSkill.IsDeleted = true;
                    existedSkill.ModifiedDate = DateTime.UtcNow;
                    await _appDbContext.SaveChangesAsync();
                    return "Deleted MIssionSkill Suceessfully";
                }
                else
                {
                    throw new Exception("Mission Skill is not found");
                }
            }catch(Exception ex)
            {
                throw new Exception("Eroor in deleting mission skill",ex);
            }
        }

        public async Task<String> UpdateMissionSkill(MissionSkill missionSkill)
        {
            try
            {
                var existedMissionSkill = await _appDbContext.MissionSkill.Where(x => !x.IsDeleted && x.Id == missionSkill.Id).FirstOrDefaultAsync();
                if (existedMissionSkill != null)
                {
                    existedMissionSkill.SkillName = missionSkill.SkillName;
                    existedMissionSkill.Status = missionSkill.Status;
                    existedMissionSkill.ModifiedDate = DateTime.UtcNow;
                    await _appDbContext.SaveChangesAsync();
                    return "Mission skill Updated Succcessfully";
                }
                else
                {
                    throw new Exception("Mission skill doesn't found");
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
