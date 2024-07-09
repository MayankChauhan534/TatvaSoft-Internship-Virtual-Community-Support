using Data_Access_Layer.Repository.Entities;
using Data_Access_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data_Logic_Layer.Entity;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;

namespace Data_Access_Layer
{
    public class DALMission
    {
        private readonly AppDbContext _context;

        public DALMission(AppDbContext cIDbContext)
        {
            _context = cIDbContext;
        }

        public List<Missions> MissionList()
        {
            return _context.Missions.Where(x => !x.IsDeleted).ToList();
        }

        public string AddMission(Missions mission)
        {
            string result = "";
            try
            {
                mission.Id = null;
                mission.IsDeleted = false;
                mission.CreatedDate = DateTime.UtcNow;
                _context.Missions.Add(mission);
                _context.SaveChanges();
                result = "Mission added Successfully.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> UpdateMission(Missions mission)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var existed = await _context.Missions.Where(x => x.Id == mission.Id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (existed != null)
                    {
                        existed.MissionTitle = mission.MissionTitle;
                        existed.MissionDescription = mission.MissionDescription;
                        existed.CountryId = mission.CountryId;
                        existed.CityId = mission.CityId;
                        existed.StartDate = mission.StartDate;
                        existed.EndDate = mission.EndDate;
                        existed.MissionImages = mission.MissionImages;
                        existed.MissionSkillId = mission.MissionSkillId;
                        existed.MissionThemeId = mission.MissionThemeId;
                        existed.TotalSheets = mission.TotalSheets;
                        existed.ModifiedDate = DateTime.UtcNow;

                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();

                        return "Updated Successfully";
                    }
                    else
                    {
                        throw new Exception("Can't find the mission");
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task<string> DeleteMission(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    var existed = await _context.Missions.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (existed != null)
                    {
                        existed.IsDeleted = true;
                        existed.ModifiedDate = DateTime.UtcNow;

                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();

                        return "Mission Deleted Successfully";
                    }
                    else
                    {
                        throw new Exception("Mission with Id doesn't exist");
                    }

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}
