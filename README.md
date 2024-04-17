# tal-tech-challenge

Basic console application demonstrating an API layer with basic database functionality. This console application is meant to simulate an appointment system by being able to add an appointment, and block out timeslots as required.

All appointments and blockouts are fixed 30 minute durations, and can be assigned any day except outside 9AM - 5PM. There is also an additional constraint that except from 4PM to 5PM on each second day of the third week of any month (i.e. every third Tuesday of the month), this time will be reserved and unavailable.

In the interest of scalability, and clearer data, I decided to split out the definition of an Appointment, and a Blockout. For this application's purpose, an Appointment is a 30 minute timeslot booked out within the day, to prevent another Appointment being added on the same date and timeslot. A Blockout is essentially a recurring Appointment every day, that prevents any new Appointments from being added in its timeslot every day. 

The application has been designed with the fact of being able to making appointments in 30 minute intervals (e.g. minutes are either 0 or 30). This hasn't been enforced in code (ie. KEEP 15:52 is a legal input), but the display for the available timeslots will be displayed as such.

The console application accepts 4 different commands:

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

## Areas for Improvement

1. For time input, allow smart conversion of time based on AM or PM. For example, since the app's restriction is that appointments cannot be added outside of 9AM to 5PM, therefore putting in 01:00 should be automatically read as 01:00PM, rather than AM.
2. Allow the usage of mixed or lower case commands (e.g. add instead of ADD). Probably just a simple .ToLowerCase() check addition.
3. Deleting a Blockout. As it is now, you can add a Blockout, but you can't Delete one. Given there are a finite amount of timeslots in a day with zero restrictions (16 timeslots maximum), adding 16 blockouts essentially means you can never add any appointments to the app ever.
4. Better defined timeslots. For example, actually enforcing the 30 minute input, or even extending it to allow 15 minute intervals instead.
4. Allow the addition of an assigned user. For example, if this was a medical appointment system, we should be able to add a doctor.
5. Allows the functionality of adding multiple appointments to a single timeslot. This can only work if there was a user added as per above.
6. Email functionality. To send a confirmation email to the user that their appointment was successfully booked in the timeslot. Will also require an email field input.
7. Integration tests. It will be good to ensure that the correct data operations are taking place.
8. Containerize the application. This is not really all that required, but it would be nice to have to show that we can containerize the app for better deployment too.