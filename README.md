# Car Rental Web Application

Welcome to the **Car Rental Web Application**! This application allows users to sign up, sign in, and manage car rentals, including searching for available cars, making reservations, and managing user profiles.

## Overview

The **Car Rental Web Application** is built using **ASP.NET Core** and follows modern web development practices. The goal is to provide a smooth user experience for car rental management, offering customers the ability to search for cars based on their location, dates, and preferences.

## Features

### 1. **User Sign Up**
   - Users can register by providing their information:
     - **First Name, Last Name, Email Address, Password** (with strength indicator)
     - **Phone Number, Date of Birth** (optional)
     - **Address (Line 1, Line 2)**, **City**, **Country**
     - **Driver's License Number** for rental verification
   - **Error Handling**: Provides clear validation for incomplete or incorrect form submissions.

### 2. **User Sign In**
   - Registered users can log in using their **email** and **password**.
   - **Session Management**: Secure login with session handling and automatic logout after inactivity.
   - **Forgot Password**: Users can reset their password through a secure, email-linked process.

### 3. **Car Search and Reservation**
   - Users can search for available cars based on:
     - **Location, Dates**, and **Preferences**.
   - **Car Reservation**: Allows users to book cars and modify reservations as needed.
   - Users can view available cars, including details such as model, price per day, and availability.

### 4. **User Dashboard**
   - After logging in, users are directed to the dashboard, which includes:
     - A list of available cars for rent.
     - Quick access to **reservations**, **booking history**, and **profile settings**.

### 5. **Profile and Account Settings**
   - Users can view and update their **personal information** provided during sign-up.
   - Users can change their password, email, and other preferences.

### 6. **Admin Features**
   - Admin users can manage:
     - Car inventory.
     - User accounts and reservations.
   - Admin can view a **list of all reservations** and **manage** them.

## Technical Details

### Technologies Used
- **Frontend**: HTML, CSS, JavaScript, Razor Pages
- **Backend**: ASP.NET Core MVC
- **Database**: Entity Framework Core (SQL Server)
- **Authentication**: ASP.NET Core Identity for user management
- **Session Management**: Secure session handling for user login and registration

### Setup and Installation

To run this project locally, follow these steps:

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/your-username/car-rental-web-app.git
