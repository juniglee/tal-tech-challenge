using TAL.TechTest.DAL.Interface;
using TAL.TechTest.DAL.Model;

namespace TAL.TechTest.DAL.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private AppDbContext _db;

        public AppointmentRepository(AppDbContext db)
        {
            _db = db;
        }

        public Appointment Get(DateTime appointmentDate)
        {
            return _db.Appointments.FirstOrDefault(x => x.Date == appointmentDate);
        }

        public IEnumerable<Appointment> GetAppointments(DateOnly appointmentDate)
        {
            return _db.Appointments.Where(x => DateOnly.FromDateTime(x.Date) == appointmentDate);
        }

        public Appointment Create(Appointment appointment)
        {
            _db.Appointments.Add(appointment);
            _db.SaveChanges();
            return appointment;
        }

        public void Delete(Guid id)
        {
            var appointment = _db.Appointments.FirstOrDefault(x => x.Id == id);
            _db.Appointments.Remove(appointment);
            _db.SaveChanges();
        }

        public Appointment Update(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}
