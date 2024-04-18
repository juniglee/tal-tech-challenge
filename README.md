# tal-tech-challenge

Basic console application demonstrating an API layer with basic database functionality. This console application is meant to simulate an appointment system by being able to add an appointment, and block out timeslots as required.

All appointments and blockouts are fixed 30 minute durations, and can be assigned any day except outside 9AM - 5PM. There is also an additional constraint that appointments also cannot be created from 4PM to 5PM on each second day of the third week of any month (I have interpreted this requirement as being every third Tuesday of the month), this time will be reserved and unavailable.

In the interest of scalability, and clearer data, I decided to split out the definition of an Appointment, and a Blockout. For this application's purpose, an Appointment is a 30 minute timeslot booked out within the day, to prevent another Appointment being added on the same date and timeslot. A Blockout is essentially a recurring Appointment every day, that prevents any new Appointments from being added in its timeslot every day. Another way to look at it is a Blockout can also be an Appointment without a defined Date component, and thus can be coded to treated as continually recurring. This aspect can most certainly be refined further, which would lead to removing the Blockout component entirely, and thus reducing the number of repositories by 1. This would also mean that the AppointmentService will only have require access to an AppointmentRepository, and this keeps it rather clean as we don't have one designated service writing to two different repositories.

The application has been designed with the fact of being able to making appointments in 30 minute intervals (e.g. minutes are either 0 or 30). This hasn't been enforced in code (ie. KEEP 15:52 is a legal input), but the display for the available timeslots will be displayed as such (i.e. if an appointment is listed for 09:00 and 09:01, the one with 09:00 will cause the timeslot "09:00 AM - 09:30 AM" to not display when using FIND, but the one with 09:01 will cause that same timeslot to appear in FIND).

Database is handled via migration (code-first approach).

The console application accepts 4 different commands (all commands are case insensitive):

# Setup

There isn't much setup that needs to be done, except to get the database up and running.

To create the database, an Initial_Create migration has already been created. You will simply need to run:

```
Update-Database -s "TAL.TechTest.DAL"
```

Ensure that in Package Manager Console, the Default project is set to TAL.TechTest.DAL.

Once the database has been created with the tables, you can simply execute the console app via Visual Studio.

## 1. ADD 

Input format: DD/MM hh:mm

Example: ADD 24/04 15:00

Adds an appointment to the database. This locks out 30 minutes access to that timeslot in the specified day.

## 2. DELETE

Input format: DD/MM hh:mm

Example: DELETE 24/04 15:00

Removes the appointment in that time slot from the database.

## 3. FIND

Input format: DD/MM

Example: FIND 24/04

Find all available timeslots for the given day, taking into factor existing appointments and blockouts.

## 4. KEEP

Input format: hh:mm

Example: KEEP 15:00

Allows the user to block out the timeslot specified in the input from allowing an appointment to be added to that timeslot at any date. Blocks out 30 minutes from the specified time.

## 5. EXIT

No input required

Example: EXIT

Kills the console application. The user can also choose to input an empty command, and it will work exactly the same as EXIT. 

## Areas for Improvement

1. For time input, allow smart conversion of time based on AM or PM. For example, since the app's restriction is that appointments cannot be added outside of 9AM to 5PM, therefore putting in 01:00 should be automatically read as 01:00PM, rather than AM.
2. Deleting a Blockout. As it is now, you can add a Blockout, but you can't Delete one. Given there are a finite amount of timeslots in a day with zero restrictions (16 timeslots maximum), adding 16 blockouts essentially means you can never add any appointments to the app ever.
3. Better defined timeslots. For example, actually enforcing the 30 minute input, or even extending it to allow 15 minute intervals instead.
4. Allow the addition of an assigned user. For example, if this was a medical appointment system, we should be able to add a doctor.
5. Allows the functionality of adding multiple appointments to a single timeslot. This can only work if there was a user added as per above.
6. Email functionality. To send a confirmation email to the user that their appointment was successfully booked in the timeslot. Will also require an email field input.
7. Defining the connection string via an appsettings.json file in the console application level, then have it be passed down to the DbContext via dependency injection. Currently, the connection string is hard defined in the DbContext level.
8. Adding logging.
9. Integration tests. It will be good to ensure that the correct data operations are taking place.
10. Containerize the application. This is not really all that required, but it would be nice to have to show that we can containerize the app for better deployment too.
11. Convert the application into a proper executable. At the current point of time, it cannot be run without either access to Visual Studio, or VS command lines.