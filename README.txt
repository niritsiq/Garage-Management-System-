# Garage Management System 

## Author

- Nir Itzik   


---

## Overview

This C# solution implements a Garage Management System as described in the Object-Oriented Programming assignment (Exercise 03). The system separates logic and UI layers across two projects:

- `Ex03.GarageLogic` – A class library (DLL) containing all object models and business logic.
- `Ex03.ConsoleUI` – A console application that serves as the user interface.

---

## Features

- Support for **5 vehicle types**: fuel/electric motorcycles and cars, and fuel trucks.
- Load vehicles from file (`Vehicles.db`) using `File.ReadAllLines`.
- Add or update vehicles by license number.
- View vehicle license numbers (with optional filtering by garage status).
- Change the garage status of a vehicle.
- Inflate all wheels to their maximum air pressure.
- Refuel a fuel-based vehicle (with validation on fuel type and quantity).
- Recharge electric vehicles (with validation on charge duration).
- View detailed info of a vehicle, including tires, engine, and owner details.

---

## Object Model – Key Classes and Enums

| Class/Enum              | Description |
|-------------------------|-------------|
| `Vehicle`               | Abstract base class for all vehicles – defines core properties and behavior. |
| `Car`, `Motorcycle`, `Truck` | Inherit from `Vehicle`, implement additional fields (e.g., color, license type, cargo, etc.). |
| `FuelCar`, `ElectricCar`, `FuelMotorcycle`, `ElectricMotorcycle`, `FuelTruck` | Concrete types defining full specifications of each vehicle category. |
| `Engine`               | Abstract base class for engines. |
| `FuelEngine`, `ElectricEngine` | Subclasses for fuel-based and electric engines with logic for refueling/charging. |
| `Wheel`                | Defines wheel properties and air pressure logic. |
| `Owner`                | Stores owner's name and phone number. |
| `GarageManagement`     | Manages the garage logic: holds vehicles, modifies states, and performs actions. |
| `VehicleCreator`       | A static factory class that creates vehicle objects based on type – **must be used exclusively**. |
| `VehicleFileLoader`    | Static class for loading and parsing vehicle data from a file. |
| `ValueRangeException`  | Custom exception for validating numerical input ranges. |
| `eVehicleStatus`       | Enum: `InRepair`, `Repaired`, `Paid`. |
| `eCarColor`            | Enum: `Yellow`, `White`, `Silver`, `Black`. |
| `eDoorsNumber`         | Enum: 2–5 doors. |
| `eLicenseType`         | Enum: `A`, `A2`, `AB`, `B2`. |
| `eFuelType`            | Enum: `Soler`, `Octan95`, `Octan96`, `Octan98`. |

---

## System Architecture

- **Layered Design**:
  - **Logic Layer (DLL)**: Contains no `Console.WriteLine` or direct UI interactions.
  - **UI Layer (Console)**: Handles all user interaction and delegates logic to the DLL.

- **Extensibility**:
  - Adding new vehicle types requires minimal change — logic relies on polymorphism and the factory method (`VehicleCreator`).

---

## Exception Handling

The system uses exception classes for robust input validation:
- `FormatException` – Invalid user input format (e.g., parsing errors).
- `ArgumentException` – Logical errors such as wrong fuel type.
- `ValueRangeException` – Custom exception for out-of-bound values (e.g., overfilling fuel or over-inflating tires).

---

## How to Run

1. Open the solution in Visual Studio.
2. Set `Ex03.ConsoleUI` as the startup project.
3. Make sure `Vehicles.db` exists in the correct path (if loading from file is enabled).
4. Run the project and follow the on-screen menu.

---

## Notes

- Object creation **must only be performed via** `VehicleCreator`.
- Vehicle data is loaded using `System.IO.File.ReadAllLines`.
- Console and Logic layers are completely separated, following SOLID principles.
- The program strictly follows the assignment instructions and constraints.

