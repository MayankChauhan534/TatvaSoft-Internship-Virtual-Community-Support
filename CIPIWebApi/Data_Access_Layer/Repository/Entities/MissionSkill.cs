using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Entities
{
    public class MissionSkill : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public String? SkillName { get; set; }
        public String? Status { get; set; }
    }
}
