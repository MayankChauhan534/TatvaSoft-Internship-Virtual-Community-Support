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
    public class DALCommon
    {
        private readonly AppDbContext _appDbContext;

        public DALCommon(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Country>> CountryList()
        {
            return await _appDbContext.Country.ToListAsync();
        }

        public async Task<List<City>> CityList(int id)
        {
            return await _appDbContext.City.Where(x => x.CountryId == id).ToListAsync();
        }
    }
}
