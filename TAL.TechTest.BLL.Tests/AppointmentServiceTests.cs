using Microsoft.EntityFrameworkCore;
using Moq;
using TAL.TechTest.BLL.Interface;
using TAL.TechTest.BLL.Service;
using TAL.TechTest.DAL;
using TAL.TechTest.DAL.Interface;
using TAL.TechTest.DAL.Model;
using TAL.TechTest.DAL.Repository;

namespace TAL.TechTest.BLL.Tests
{
    public class AppointmentServiceTests
    {
        private Mock<IAppointmentRepository> _mockAppointmentRepository;
        private Mock<IBlockoutRepository> _mockBlockoutRepository;

        [SetUp]
        public void Setup()
        {
            _mockAppointmentRepository = new Mock<IAppointmentRepository>();
            _mockBlockoutRepository = new Mock<IBlockoutRepository>();
        }

        [Test]
        public void CreateAppointment_ShouldCreateAppointment_IfDoesNotExist()
        {
            //Arrange
            var appointmentService = new AppointmentService(_mockAppointmentRepository.Object, _mockBlockoutRepository.Object);
            var testAppointment = new Appointment
            {
                Id = Guid.NewGuid(),
                Date = DateTime.ParseExact("24/04 14:00", "dd/MM HH:mm", null)
            };
            _mockAppointmentRepository.Setup(x => x.Create(It.IsAny<Appointment>())).Returns(testAppointment);

            //Act
            var result = appointmentService.Create(testAppointment);

            //Assert
            StringAssert.AreEqualIgnoringCase($"Appointment successfully created on {testAppointment.Date}.", result);
        }

        [Test]
        public void CreateAppointment_ShouldNotCreateAppointment_IfExists()
        {
            //Arrange
            var appointmentService = new AppointmentService(_mockAppointmentRepository.Object, _mockBlockoutRepository.Object);
            var testAppointment = new Appointment
            {
                Id = Guid.NewGuid(),
                Date = DateTime.ParseExact("24/04 14:00", "dd/MM HH:mm", null)
            };

            _mockAppointmentRepository.Setup(x => x.Get(It.IsAny<DateTime>())).Returns(testAppointment);

            //Act
            var result = appointmentService.Create(testAppointment);

            //Assert
            StringAssert.AreEqualIgnoringCase($"Appointment already exists on {testAppointment.Date.ToString()}.", result);
        }

        [Test]
        public void CreateAppointment_ShouldNotCreateAppointment_OutsideOfHours()
        {
            //Arrange
            var appointmentService = new AppointmentService(_mockAppointmentRepository.Object, _mockBlockoutRepository.Object);
            var testAppointment = new Appointment
            {
                Id = Guid.NewGuid(),
                Date = DateTime.ParseExact("24/04 20:00", "dd/MM HH:mm", null)
            };

            //Act
            var result = appointmentService.Create(testAppointment);

            //Assert
            StringAssert.AreEqualIgnoringCase($"Appointment could not be created, it is outside of the available hours of 9AM to 5PM.", result);
        }

        [Test]
        public void CreateAppointment_ShouldNotCreateAppointment_OnSecondTuesdayOfTheMonthWithinHours()
        {
            //Arrange
            var appointmentService = new AppointmentService(_mockAppointmentRepository.Object, _mockBlockoutRepository.Object);
            var testAppointment = new Appointment
            {
                Id = Guid.NewGuid(),
                Date = DateTime.ParseExact("19/03 16:00", "dd/MM HH:mm", null)
            };

            //Act
            var result = appointmentService.Create(testAppointment);

            //Assert
            StringAssert.AreEqualIgnoringCase($"Appointment could not be created, it is within 4PM to 5PM of the third Tuesday of the month.", result);
        }

        [Test]
        public void DeleteAppointment_ShouldDelete_IfExists()
        {
            //Arrange
            var appointmentService = new AppointmentService(_mockAppointmentRepository.Object, _mockBlockoutRepository.Object);
            var testAppointmentDate = DateTime.ParseExact("24/04 14:00", "dd/MM HH:mm", null);
            var testAppointment = new Appointment
            {
                Id = Guid.NewGuid(),
                Date = testAppointmentDate
            };
            _mockAppointmentRepository.Setup(x => x.Get(It.IsAny<DateTime>())).Returns(testAppointment);

            //Act
            var result = appointmentService.Delete(testAppointmentDate);

            //Assert
            StringAssert.AreEqualIgnoringCase($"The appointment at the date {testAppointmentDate} has been successfully deleted.", result);
        }

        [Test]
        public void DeleteAppointment_ShouldNotDelete_IfDoesNotExists()
        {
            //Arrange
            var appointmentService = new AppointmentService(_mockAppointmentRepository.Object, _mockBlockoutRepository.Object);
            var testAppointmentDate = DateTime.ParseExact("24/04 14:00", "dd/MM HH:mm", null);

            //Act
            var result = appointmentService.Delete(testAppointmentDate);

            //Assert
            StringAssert.AreEqualIgnoringCase($"The appointment at the date {testAppointmentDate} could not be found.", result);
        }

        [Test]
        public void KeepAppointment_ShouldAddBlockout_IfDoesNotExist()
        {
            //Arrange
            var appointmentService = new AppointmentService(_mockAppointmentRepository.Object, _mockBlockoutRepository.Object);
            var testBlockoutDate = TimeOnly.ParseExact("14:00", "HH:mm", null);
            var testBlockout = new Blockout { 
                Id = Guid.NewGuid(),
                StartTime = testBlockoutDate
            };

            //Act
            var result = appointmentService.Keep(testBlockout);

            //Assert
            StringAssert.AreEqualIgnoringCase($"The time {testBlockout.StartTime} has been successfully added.", result);
        }

        [Test]
        public void KeepAppointment_ShouldNotAddBlockout_IfExists()
        {
            //Arrange
            var appointmentService = new AppointmentService(_mockAppointmentRepository.Object, _mockBlockoutRepository.Object);
            var testBlockoutDate = TimeOnly.ParseExact("14:00", "HH:mm", null);
            var testBlockout = new Blockout
            {
                Id = Guid.NewGuid(),
                StartTime = testBlockoutDate
            };
            _mockBlockoutRepository.Setup(x => x.Get(It.IsAny<TimeOnly>())).Returns(testBlockout);

            //Act
            var result = appointmentService.Keep(testBlockout);

            //Assert
            StringAssert.AreEqualIgnoringCase($"The time {testBlockout.StartTime} has already been added.", result);
        }

        [Test]
        public void GetAvailableTimeslotsForDate_ShouldListAllAvailableTimeslots()
        {
            //Arrange
            var appointmentService = new AppointmentService(_mockAppointmentRepository.Object, _mockBlockoutRepository.Object);

            var testAppointments = new List<Appointment> {
                new Appointment
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.ParseExact("24/04 11:30", "dd/MM HH:mm", null)
                },
                new Appointment
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.ParseExact("24/04 14:00", "dd/MM HH:mm", null)
                }
            };
            _mockAppointmentRepository.Setup(x => x.GetAppointments(It.IsAny<DateOnly>())).Returns(testAppointments);

            var testBlockouts = new List<Blockout>
            {
                new Blockout { Id = Guid.NewGuid(), StartTime = TimeOnly.ParseExact("12:00", "HH:mm", null) },
                new Blockout { Id = Guid.NewGuid(), StartTime = TimeOnly.ParseExact("15:30", "HH:mm", null) }
            };
            _mockBlockoutRepository.Setup(x => x.Get()).Returns(testBlockouts);

            //Act
            var result = appointmentService.GetAvailableTimeslotsForDate(DateOnly.ParseExact("24/04", "dd/MM", null));

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.IsNotEmpty(result);
            CollectionAssert.DoesNotContain(result, "03:30 PM - 04:00 PM");
            Assert.AreEqual(12, result.Count);
        }
    }
}