using Data_Access_Layer.Repository.Entities;
using Data_Access_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer
{
    public class DALMission
    {
        private readonly AppDbContext _cIDbContext;

        public DALMission(AppDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }
        public List<Missions> MissionList()
        {
            return _cIDbContext.Missions.Where(x=> !x.IsDeleted).ToList();
        }

        public string AddMission(Missions mission)
        {
            string result = "";
            try
            {
                _cIDbContext.Missions.Add(mission);
                _cIDbContext.SaveChanges();
                result = "Mission added Successfully.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<Missions> MissionDetailById(int id)
        {
            return await _cIDbContext.Missions.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateMission(Missions mission)
        {
            try
            {
                var existed = await _cIDbContext.Missions.Where(x => x.Id == mission.Id && !x.IsDeleted).FirstOrDefaultAsync();
                if (existed != null)
                {
                    existed.CountryId = mission.CountryId;
                    existed.CityId = mission.CityId;
                    existed.EndDate = mission.EndDate;
                    existed.MissionDescription = mission.MissionDescription;
                    existed.MissionImages = mission.MissionImages;
                    existed.MissionSkillId = mission.MissionSkillId;
                    existed.MissionThemeId = mission.MissionThemeId;
                    existed.MissionTitle = mission.MissionTitle;
                    existed.StartDate = mission.StartDate;
                    existed.TotalSheets = mission.TotalSheets;
                    await _cIDbContext.SaveChangesAsync();
                    return "Updated Successfully";
                }
                else
                {
                    throw new Exception("Error while editing");
                }
            }catch (Exception ex)
            {
                throw ex;
            }        
        }
        public async Task<string> DeleteMission(int id)
        {
            try
            {
                var existed = await _cIDbContext.Missions.Where(x =>x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                if(existed != null)
                {
                    existed.IsDeleted = true;
                    await _cIDbContext.SaveChangesAsync();
                    return "Mission Deleted Successfully";
                }
                else
                {
                    throw new Exception("Mission with Id doesn't exist");
                }
            }catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Missions> ClientSideMissionList(int userId)
        {
            List<Missions> clientSideMissionList = new List<Missions>();
            try
            {
                clientSideMissionList = _cIDbContext.Missions
                    .Where(m => !m.IsDeleted)
                    .OrderBy(m => m.CreatedDate)
                    .Select(m => new Missions
                    {
                        Id = m.Id,
                        CountryId = m.CountryId,
                        CountryName = m.CountryName,
                        CityId = m.CityId,
                        CityName = m.CityName,
                        MissionTitle = m.MissionTitle,
                        MissionDescription = m.MissionDescription,
                        MissionOrganisationName = m.MissionOrganisationName,
                        MissionOrganisationDetail = m.MissionOrganisationDetail,
                        TotalSheets = m.TotalSheets,
                        RegistrationDeadLine = m.RegistrationDeadLine,
                        MissionThemeId = m.MissionThemeId,
                        MissionThemeName = m.MissionThemeName,
                        MissionImages = m.MissionImages,
                        MissionDocuments = m.MissionDocuments,
                        MissionSkillId = m.MissionSkillId,
                        MissionSkillName = string.Join(",", m.MissionSkillName),
                        MissionAvilability = m.MissionAvilability,
                        MissionVideoUrl = m.MissionVideoUrl,
                        MissionType = m.MissionType,
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        MissionStatus = m.RegistrationDeadLine < DateTime.Now.AddDays(-1) ? "Closed" : "Available",
                        MissionApplyStatus = _cIDbContext.MissionApplication.Any(ma => ma.MissionId == m.Id && ma.UserId == userId) ? "Applied" : "Apply",
                        MissionApproveStatus = _cIDbContext.MissionApplication.Any(ma => ma.MissionId == m.Id && ma.UserId == userId && ma.Status == true) ? "Approved" : "Applied",
                        MissionDateStatus = m.EndDate <= DateTime.Now.AddDays(-1) ? "MissionEnd" : "MissionRunning",
                        MissionDeadLineStatus = m.RegistrationDeadLine <= DateTime.Now.AddDays(-1) ? "Closed" : "Running",
                        MissionFavouriteStatus = _cIDbContext.MissionFavourites.Any(mf => mf.MissionId == m.Id && mf.UserId == userId) ? "1" : "0",
                        Rating = _cIDbContext.MissionRating.FirstOrDefault(mr => mr.MissionId == m.Id && mr.UserId == userId).Rating ?? 0
                    }).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return clientSideMissionList;
        }

        public async Task<List<MissionTheme>> GetMissionThemeList()
        {
            return await _cIDbContext.MissionTheme.Where(x=> !x.IsDeleted && x.Status == "active").ToListAsync();
        }

        public async Task<List<MissionSkill>> GetMissionSkillList()
        {
            return await _cIDbContext.MissionSkill.Where(x => !x.IsDeleted && x.Status == "active").ToListAsync();
        }

        public string ApplyMission(MissionApplication missionApplication)
        {
            string result = "";
            try
            {
                using (var transaction = _cIDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var mission = _cIDbContext.Missions.FirstOrDefault(m => m.Id == missionApplication.MissionId && m.IsDeleted == false);
                        var user = _cIDbContext.User.FirstOrDefault(u => u.Id == missionApplication.UserId && u.IsDeleted == false);
                        if (mission != null && user != null)
                        {
                            if (mission.TotalSheets > 0)
                            {
                                var newApplication = new MissionApplication
                                {
                                    MissionId = missionApplication.MissionId,
                                    UserId = missionApplication.UserId,
                                    AppliedDate = DateTime.UtcNow,
                                    Status = false,
                                    CreatedDate = DateTime.UtcNow,
                                    IsDeleted = false,
                                    MissionTitle = mission.MissionTitle,
                                    UserName = user.FirstName + " " + user.LastName,
                                };

                                _cIDbContext.MissionApplication.Add(newApplication);
                                _cIDbContext.SaveChanges();

                                mission.TotalSheets = mission.TotalSheets - 1;
                                _cIDbContext.SaveChanges();

                                result = "Mission Apply Successfully.";
                            }
                            else
                            {
                                result = "Mission Housefull";
                            }
                        }
                        else
                        {
                            result = "Mission or User Not Found.";
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public string MissionApplicationApprove(int id)
        {
            var result = "";
            try
            {
                var missionApplication = _cIDbContext.MissionApplication.FirstOrDefault(ma => ma.Id == id);
                if (missionApplication != null)
                {
                    missionApplication.Status = true;
                    _cIDbContext.SaveChanges();
                    result = "Mission is approved";
                }
                else
                {
                    result = "Mission Application is not found.";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public string MissionApplicationDelete(int id)
        {
            var result = "";
            try
            {
                var existed = _cIDbContext.MissionApplication.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
                if(existed != null)
                {
                    existed.IsDeleted = true;
                    _cIDbContext.SaveChanges();
                    result = "Mission Apllication Deleted";
                }
                else
                {
                    result = "Mission applicarion Doesnt found";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}
