# tal-tech-challenge

Basic console application demonstrating an API layer with basic database functionality. This console application is meant to simulate an appointment system by being able to add an appointment, and block out timeslots as required.

Appointments are fixed 30 minute durations, and can be assigned any day except outside 9AM - 5PM. There is also an additional constraint that except from 4PM to 5PM on each second day of the third week of any month, this time will be reserved and unavailable.

I have also added the liberty of only allowing 30 minute intervals for booking an appointment (all mm parameters only accept 00 or 30. for easier display purposes).

The console application accepts 4 different commands:

## 1. ADD 

Input format: DD/MM hh:mm

Adds an appointment to the database. This locks out 30 minutes access to that timeslot in the specified day.

## 2. DELETE

Input format: DD/MM hh:mm

Removes the appointment in that time slot from the database.

## 3. FIND

Input format: DD/MM

Find all available timeslots for the given day.

## 4. KEEP

Input format: hh:mm

Allows the user to block out the timeslot specified in the input from allowing an appointment to be added to that timeslot.

## Areas for improvement

1. Allow the addition of an assigned user. For example, if this was a medical appointment system, we should be able to add a doctor.
2. Allows the functionality of adding multiple appointments to a single timeslot. This can only work if there was a user added as per above.
3. Email functionality. To send a confirmation email to the user that their appointment was successfully booked in the timeslot. Will also require an email field input.
