using TAL.TechTest.DAL.Model;

namespace TAL.TechTest.DAL.Interface
{
    public interface IAppointmentRepository
    {
        public Appointment Get(DateTime appointmentDate);
        IEnumerable<Appointment> GetAppointments(DateOnly appointmentDate);
        public Appointment Create(Appointment appointment);
        public Appointment Update(Appointment appointment);
        public void Delete(Guid id);
    }
}
