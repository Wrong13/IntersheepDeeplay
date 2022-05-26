using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersheepDeeplay.Model
{
    public class Worker
    {

        public int Id { get; set; }
        [MinLength(2)]
        [MaxLength(30)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public int DivisionId { get; set; }
        public int JobPositionId { get; set; }
        public Division Division { get; set; }
        public JobPosition JobPosition { get; set; }
    }
}
