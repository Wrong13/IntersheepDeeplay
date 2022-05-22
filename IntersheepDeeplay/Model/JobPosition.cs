using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersheepDeeplay.Model
{
    public class JobPosition
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }
        [MaxLength(120)]
        public string Description { get; set; }
        [MaxLength(120)]
        public string Description2 { get; set; }
        public List<Worker> Workers { get; set; }
    }
}
