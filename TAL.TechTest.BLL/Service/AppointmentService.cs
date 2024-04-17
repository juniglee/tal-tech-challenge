using TAL.TechTest.BLL.Interface;
using TAL.TechTest.DAL.Interface;
using TAL.TechTest.DAL.Model;

namespace TAL.TechTest.BLL.Service
{
    public class AppointmentService : IAppointmentService
    {
        private IAppointmentRepository _appointmentRepository;
        private IBlockoutRepository _blockoutRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IBlockoutRepository blockoutRepository)
        {
            _appointmentRepository = appointmentRepository;
            _blockoutRepository = blockoutRepository;
        }

        public string Create(Appointment appointment)
        {
            //exists check
            var existingAppointment = _appointmentRepository.Get(appointment.Date);
            var blockouts = _blockoutRepository.Get();
            if (existingAppointment != null)
            {
                return $"Appointment already exists on {appointment.Date.ToString()}.";
            }

            if (blockouts.Any(x => x.StartTime == TimeOnly.FromDateTime(appointment.Date))) {
                return $"This timeslot has been blocked out from allowing any appointments to be made on this timeslot.";
            }

            //time check
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan secondDayOfThirdWeekStartTime = new TimeSpan(16, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            TimeSpan appointmentTime = appointment.Date.TimeOfDay;

            //second day of the third week of the month check
            DateTime firstDayOfMonth = new DateTime(appointment.Date.Year, appointment.Date.Month, 1);
            DayOfWeek firstDayOfWeek = firstDayOfMonth.DayOfWeek;
            int firstInstanceOfSecondDayOfTheWeek = 2 - (int)firstDayOfWeek + 1;

            if (firstInstanceOfSecondDayOfTheWeek < 0)
            {
                firstInstanceOfSecondDayOfTheWeek += 7;
            }

            int thirdInstanceOfSecondDayOfTheWeek = firstInstanceOfSecondDayOfTheWeek + 14;

            if (appointment.Date.Day == thirdInstanceOfSecondDayOfTheWeek )
            {
                return "Appointment could not be created, it is within 4PM to 5PM of the third Tuesday of the month.";
            }
            else if (!(startTime < appointmentTime && appointmentTime < endTime))
            {
                return "Appointment could not be created, it is outside of the available hours of 9AM to 5PM.";
            }
            else
            {
                var newAppointment = _appointmentRepository.Create(appointment);
                return $"Appointment successfully created on {newAppointment.Date.ToString()}.";
            }
        }

        public string Delete(DateTime appointmentDate)
        {
            try
            {
                var appointment = _appointmentRepository.Get(appointmentDate);

                if (appointment == null)
                {
                    return $"The appointment at the date {appointmentDate} could not be found.";
                }
                else
                {
                    _appointmentRepository.Delete(appointment.Id);
                    return $"The appointment at the date {appointmentDate} has been successfully deleted.";
                }
            }
            catch (Exception ex)
            {
                return $"There was an error deleting the appointment, please contact the administrator.";
            }
        }

        public List<string> GetAvailableTimeslotsForDate(DateOnly date)
        {
            //throw new NotImplementedException();

            var appointmentsForDay = _appointmentRepository.GetAppointments(date);
            var blockouts = _blockoutRepository.Get();

            TimeOnly startTime = new TimeOnly(9, 0, 0);
            TimeOnly endTime = new TimeOnly(17, 0, 0);

            var results = new List<string>();

            while (startTime < endTime)
            {
                if (appointmentsForDay.Where(x => TimeOnly.FromDateTime(x.Date)  == startTime).Count() == 0 && blockouts.Where(x => x.StartTime == startTime).Count() == 0)
                {
                    results.Add($"{startTime} - {startTime.AddMinutes(30)}");
                }
                startTime = startTime.AddMinutes(30);
            }

            return results;
        }

        public string Keep(Blockout blockout)
        {
            //for all intents and purposes, a blockout is basically a recurring appointment at a given fixed time daily with no end date
            //I decided to split it to make it easier to manage or scale in the future if required
            var existingBlockout = _blockoutRepository.Get(blockout.StartTime);
            if (existingBlockout != null)
            {
                return $"The time {blockout.StartTime} has already been added.";
            }
            _blockoutRepository.Create(blockout);
            return $"The time {blockout.StartTime} has been successfully added.";
        }
    }
}
