using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Entities
{
    public class Country: BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class City : BaseEntity 
    {
        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
    }
}
