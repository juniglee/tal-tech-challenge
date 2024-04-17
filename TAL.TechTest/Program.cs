using Microsoft.Extensions.DependencyInjection;
using System.Text;
using TAL.TechTest.BLL.Interface;
using TAL.TechTest.BLL.Service;
using TAL.TechTest.DAL.Interface;
using TAL.TechTest.DAL.Repository;
using TAL.TechTest.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TAL.TechTest.DAL.Model;

namespace TAL.TechTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>()
                .AddScoped<IAppointmentService, AppointmentService>()
                .AddScoped<IAppointmentRepository, AppointmentRepository>()
                .AddScoped<IBlockoutRepository, BlockoutRepository>()
                .BuildServiceProvider();

            //get service
            var appointmentService = serviceProvider.GetService<IAppointmentService>();

            while (true)
            {
                Console.WriteLine("Please enter an input:");
                string rawInput = Console.ReadLine();

                if (rawInput == "EXIT" || string.IsNullOrEmpty(rawInput))
                {
                    return;
                }
                else
                {
                    string result = string.Empty;
                    string[] inputs = rawInput.Split(' ');

                    switch (inputs[0])
                    {
                        case "ADD":
                            string appointmentDate = inputs[1] + " " + inputs[2];

                            var appointment = new Appointment {
                                Date = DateTime.ParseExact(appointmentDate, "dd/MM HH:mm", null)
                            };

                            result = appointmentService.Create(appointment);

                            Console.WriteLine(result);
                            Console.WriteLine();
                            break;
                        case "DELETE":
                            string appointmentDateToDelete = inputs[1] + " " + inputs[2];

                            result = appointmentService.Delete(DateTime.ParseExact(appointmentDateToDelete, "dd/MM HH:mm", null));
                            Console.WriteLine(result);
                            Console.WriteLine();
                            break;
                        case "FIND":
                            DateOnly appointmentDateToFind = DateOnly.ParseExact(inputs[1], "dd/MM", null);

                            var timeslots = appointmentService.GetAvailableTimeslotsForDate(appointmentDateToFind);

                            Console.WriteLine($"Available timeslots for {appointmentDateToFind}:");
                            Console.WriteLine();

                            foreach (var timeslot in timeslots)
                            {
                                Console.WriteLine(timeslot);
                            }
                            Console.WriteLine();
                            break;
                        case "KEEP":
                            string blockoutTime = inputs[1];

                            var blockout = new Blockout
                            {
                                StartTime = TimeOnly.ParseExact(blockoutTime, "HH:mm", null)
                            };

                            result = appointmentService.Keep(blockout);
                            Console.WriteLine(result);
                            Console.WriteLine();
                            break;
                        default:
                            Console.WriteLine("Invalid command, please specify a valid input");
                            break;
                    }
                }
            }

        }
    }
}