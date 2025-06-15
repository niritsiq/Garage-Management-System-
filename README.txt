# 🚗 Garage Management System

## 👤 Author

- Nir Itzik  

---

## 📄 Overview

This C# solution implements a **Garage Management System** as part of the Object-Oriented Programming assignment (Exercise 03).  
It follows a clean, layered architecture by separating **logic** and **UI** into two distinct projects:

- 🧠 `Ex03.GarageLogic` – A class library (DLL) containing all object models and business logic.
- 🖥️ `Ex03.ConsoleUI` – A console application that serves as the user interface.

---

## 🚘 Features

- ✅ Support for **5 vehicle types**: fuel/electric motorcycles & cars, and fuel trucks.
- 📂 Load vehicles from file (`Vehicles.db`) using `File.ReadAllLines`.
- ➕ Add or update vehicles by license number.
- 📋 View vehicle license numbers (with optional filtering by garage status).
- 🔄 Change the garage status of a vehicle.
- 💨 Inflate all wheels to their **maximum air pressure**.
- ⛽ Refuel a fuel-based vehicle (**with validation** on type and quantity).
- 🔌 Recharge electric vehicles (**with validation** on duration).
- 🔍 View detailed information including tires, engine, owner, and garage status.

---

## 🧱 Object Model – Key Classes and Enums

| 🧩 Class / Enum        | 📝 Description |
|------------------------|----------------|
| `Vehicle`              | Abstract base class for all vehicles – defines core properties and behavior. |
| `Car`, `Motorcycle`, `Truck` | Inherit from `Vehicle`, add specific fields (color, license type, etc.). |
| `FuelCar`, `ElectricCar`, `FuelMotorcycle`, `ElectricMotorcycle`, `FuelTruck` | Concrete types with full specifications. |
| `Engine`               | Abstract class for engines. |
| `FuelEngine`, `ElectricEngine` | Handle refueling/charging logic. |
| `Wheel`                | Defines tire properties and air pressure behavior. |
| `Owner`                | Stores owner's name and phone. |
| `GarageManagement`     | Core logic – manages vehicles, actions, and statuses. |
| `VehicleCreator`       | Static factory – must be used to create vehicle objects. |
| `VehicleFileLoader`    | Loads and parses vehicle data from a file. |
| `ValueRangeException`  | Custom exception for validating input ranges. |
| `eVehicleStatus`       | Enum: `InRepair`, `Repaired`, `Paid`. |
| `eCarColor`            | Enum: `Yellow`, `White`, `Silver`, `Black`. |
| `eDoorsNumber`         | Enum: 2–5 doors. |
| `eLicenseType`         | Enum: `A`, `A2`, `AB`, `B2`. |
| `eFuelType`            | Enum: `Soler`, `Octan95`, `Octan96`, `Octan98`. |

---

## 🏗️ System Architecture

### 📚 Layered Design
- **Logic Layer (`Ex03.GarageLogic`)** – Contains core business logic, no `Console.WriteLine` or UI references.
- **UI Layer (`Ex03.ConsoleUI`)** – Handles user interaction and delegates tasks to the logic layer.

### 🔁 Extensibility
- Adding a new vehicle type only requires minimal changes.
- All creation logic is centralized in `VehicleCreator` to follow **OCP** (Open/Closed Principle).

---

## 🚨 Exception Handling

Robust validation is enforced using custom and built-in exceptions:

- `FormatException` – Invalid format (e.g., parsing input).
- `ArgumentException` – Logical errors (e.g., invalid fuel type).
- `ValueRangeException` – Out-of-range input (e.g., overfilling, overcharging).

---

## ▶️ How to Run

1. Open the solution in **Visual Studio**.
2. Set `Ex03.ConsoleUI` as the **Startup Project**.
3. Ensure `Vehicles.db` exists in the correct directory.
4. Run the application and follow the console menu.

---

## 📝 Notes

- All vehicle instances must be created **only via** `VehicleCreator`.
- Vehicle data is imported using `System.IO.File.ReadAllLines`.
- The Console and Logic layers are **fully decoupled**, following **SOLID** principles.
- This implementation strictly adheres to the requirements of the assignment.

---

