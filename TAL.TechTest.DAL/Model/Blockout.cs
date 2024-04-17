using System.ComponentModel.DataAnnotations;

namespace TAL.TechTest.DAL.Model
{
    public class Blockout
    {
        [Key]
        public Guid Id { get; set; }
        public TimeOnly StartTime { get; set; }
    }
}
