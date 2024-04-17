using System.ComponentModel.DataAnnotations;

namespace TAL.TechTest.DAL.Model
{
    public class Appointment
    {
        [Key]
        public Guid Id  { get; set; }

        public DateTime Date { get; set; }
    }
}
