using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class VehicleFileLoader
    {
        private const string k_DefaultFileName = "Vehicles.db";

        public static List<Vehicle> LoadVehiclesFromFile(string i_FilePath = k_DefaultFileName)
        {
            List<Vehicle> loadedVehicles = new List<Vehicle>();

            try
            {
                if (!File.Exists(i_FilePath))
                {
                    throw new FileNotFoundException(string.Format("Vehicle database file '{0}' not found.", i_FilePath));
                }

                string[] lines = File.ReadAllLines(i_FilePath);

                foreach (string line in lines)
                {
                    // Skip empty lines, comments, and format description lines
                    if (string.IsNullOrWhiteSpace(line) ||
                        line.StartsWith("*") ||
                        line.StartsWith("THE FORMAT") ||
                        line.StartsWith("VehicleType") ||
                        line.Contains("[SPECIFIC") ||
                        line.Contains("LicensePlate") ||
                        line.StartsWith("//") ||
                        line.StartsWith("#"))
                    {
                        continue;
                    }

                    try
                    {
                        Vehicle vehicle = parseVehicleFromLine(line);
                        if (vehicle != null)
                        {
                            loadedVehicles.Add(vehicle);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("Error parsing line '{0}': {1}", line, ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Error loading vehicles from file: {0}", ex.Message), ex);
            }

            return loadedVehicles;
        }

        private static Vehicle parseVehicleFromLine(string i_Line)
        {
            string[] parts = i_Line.Split(',').Select(part => part.Trim()).ToArray();

            if (parts.Length < 8)
            {
                throw new FormatException(string.Format("Invalid line format - expected at least 8 parts, got {0}", parts.Length));
            }

            // Format: VehicleType, LicensePlate, ModelName, EnergyPercentage, TireModel, CurrAirPressure, OwnerName, OwnerPhone, [SPECIFIC]
            string vehicleTypeStr = parts[0];
            string licensePlate = parts[1];
            string modelName = parts[2];
            float energyPercentage = float.Parse(parts[3]);
            string tireModel = parts[4];
            float currentAirPressure = float.Parse(parts[5]);
            string ownerName = parts[6];
            string ownerPhone = parts[7];

            string vehicleType = convertVehicleTypeToString(vehicleTypeStr);

            Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleType, licensePlate, modelName);

            Owner owner = new Owner(ownerName, ownerPhone);
            vehicle.SetOwner(owner);

            foreach (Wheel wheel in vehicle.VehicleWheels)
            {
                wheel.ManufacturerName = tireModel;
                if (currentAirPressure > 0)
                {
                    wheel.FillAirPressure(currentAirPressure);
                }
            }

            setVehicleEnergyFromPercentage(vehicle, energyPercentage);

            // Parse vehicle-specific parameters starting from index 8
            parseVehicleSpecificParameters(vehicle, parts, 8);

            return vehicle;
        }

        private static string convertVehicleTypeToString(string i_VehicleTypeStr)
        {
            switch (i_VehicleTypeStr.ToLower())
            {
                case "fuelmotorcycle":
                    return "FuelMotorcycle";
                case "electricmotorcycle":
                    return "ElectricMotorcycle";
                case "fuelcar":
                    return "FuelCar";
                case "electriccar":
                    return "ElectricCar";
                case "truck":
                    return "Truck";
                default:
                    throw new ArgumentException(string.Format("Unknown vehicle type: {0}", i_VehicleTypeStr));
            }
        }

        private static void setVehicleEnergyFromPercentage(Vehicle i_Vehicle, float i_EnergyPercentage)
        {
            if (i_EnergyPercentage < 0 || i_EnergyPercentage > 100)
            {
                throw new ArgumentOutOfRangeException("i_EnergyPercentage", "Energy percentage must be between 0 and 100");
            }

            Engine engine = i_Vehicle.VehicleEngine;
            float energyToAdd = (i_EnergyPercentage / 100f) * engine.MaximalEnergy;

            if (engine is ElectricEngine)
            {
                engine.FillEnergy(energyToAdd);
            }
            else if (engine is FuelEngine fuelEngine)
            {
                engine.FillEnergy(fuelEngine.FuelType, energyToAdd);
            }
        }

        private static void parseVehicleSpecificParameters(Vehicle i_Vehicle, string[] i_Parts, int i_StartIndex)
        {
            if (i_Vehicle is Car car)
            {
                if (i_Parts.Length > i_StartIndex + 1)
                {
                    eCarColor color = parseCarColor(i_Parts[i_StartIndex]);
                    car.CarColor = color;

                    if (int.TryParse(i_Parts[i_StartIndex + 1], out int doorCount))
                    {
                        car.NumberOfDoors = convertIntToDoorsEnum(doorCount);
                    }
                }
            }
            else if (i_Vehicle is Motorcycle motorcycle)
            {
                if (i_Parts.Length > i_StartIndex + 1)
                {
                    if (Enum.TryParse(i_Parts[i_StartIndex], true, out eLicenseType license))
                    {
                        motorcycle.LicenseType = license;
                    }

                    if (int.TryParse(i_Parts[i_StartIndex + 1], out int engineVolume))
                    {
                        motorcycle.EngineVolume = engineVolume;
                    }
                }
            }
            else if (i_Vehicle is Truck truck)
            {
                if (i_Parts.Length > i_StartIndex + 1)
                {
                    if (bool.TryParse(i_Parts[i_StartIndex], out bool dangerous))
                    {
                        truck.CarriesDangerousMaterials = dangerous;
                    }

                    if (float.TryParse(i_Parts[i_StartIndex + 1], out float cargoVolume))
                    {
                        truck.CargoVolume = cargoVolume;
                    }
                }
            }
        }

        private static eCarColor parseCarColor(string i_ColorStr)
        {
            switch (i_ColorStr.ToLower())
            {
                case "black":
                    return eCarColor.Black;
                case "white":
                    return eCarColor.White;
                case "silver":
                    return eCarColor.Silver;
                case "yellow":
                    return eCarColor.Yellow;
                default:
                    return eCarColor.Black; // Default fallback
            }
        }

        private static eDoorsNumber convertIntToDoorsEnum(int i_DoorCount)
        {
            switch (i_DoorCount)
            {
                case 2:
                    return eDoorsNumber.TwoDoors;
                case 3:
                    return eDoorsNumber.ThreeDoors;
                case 4:
                    return eDoorsNumber.FourDoors;
                case 5:
                    return eDoorsNumber.FiveDoors;
                default:
                    return eDoorsNumber.FourDoors; // Default fallback
            }
        }
    }
}