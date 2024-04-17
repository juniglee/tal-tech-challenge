using TAL.TechTest.DAL.Model;

namespace TAL.TechTest.BLL.Interface
{
    public interface IAppointmentService
    {
        public List<string> GetAvailableTimeslotsForDate(DateOnly date);
        public string Create(Appointment appointment);
        public string Delete(DateTime appointmentDate);

        public string Keep(Blockout blockout);
    }
}
