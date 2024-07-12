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
    public class DALMissionTheme
    {
        private readonly AppDbContext _appDbContext;

        public DALMissionTheme(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<MissionTheme>> GetMissionThemeList()
        {
            try
            {
                return await _appDbContext.MissionTheme.Where(x => !x.IsDeleted).ToListAsync();
            }catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<String> AddMissionTheme(MissionTheme missionTheme)
        {
            try
            {
                missionTheme.IsDeleted = false;
                missionTheme.CreatedDate = DateTime.UtcNow;
                await _appDbContext.MissionTheme.AddAsync(missionTheme);
                await _appDbContext.SaveChangesAsync();
                return "Mission Theme Added Successfully";
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<MissionTheme> GetMissionThemeById(int id)
        {
            try
            {
                return await _appDbContext.MissionTheme.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<String> UpdateMissionTheme(MissionTheme missionTheme)
        {
            try
            {
                var existedTheme = await _appDbContext.MissionTheme.Where(x => !x.IsDeleted && x.Id == missionTheme.Id).FirstOrDefaultAsync();
                if (existedTheme != null)
                {
                    existedTheme.ThemeName = missionTheme.ThemeName;
                    existedTheme.Status = missionTheme.Status;
                    existedTheme.ModifiedDate = DateTime.UtcNow;
                    await _appDbContext.SaveChangesAsync();
                    return "Mission theme updated successfully";
                }
                else
                {
                    throw new Exception("Mission theme doesnt exist");
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<String> DeleteMissionTheme(int id)
        {
            try
            {
                var existedTheme = await _appDbContext.MissionTheme.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
                if (existedTheme != null)
                {
                    existedTheme.IsDeleted = true;
                    existedTheme.ModifiedDate= DateTime.UtcNow;
                    await _appDbContext.SaveChangesAsync();
                    return "Mission Deleted Successfully";
                }
                else
                {
                    throw new Exception("Mission theme doesnt exist");
                }
            }catch (Exception ex) { throw ex; }
        }
    }
}
