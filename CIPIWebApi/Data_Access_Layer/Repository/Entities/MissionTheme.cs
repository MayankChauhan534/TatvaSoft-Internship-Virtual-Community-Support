using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Entities
{
    public class MissionTheme : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public String ThemeName { get; set; }
        public String Status {  get; set; }
    }
}
