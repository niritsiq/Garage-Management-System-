using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        private GarageManagement m_GarageManagement;

        public GarageUI()
        {
            m_GarageManagement = new GarageManagement();
        }

        public void Run()
        {
            bool continueRunning = true;

            while (continueRunning)
            {
                showMainMenu();

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    try
                    {
                        switch (choice)
                        {
                            case 1:
                                loadVehiclesFromFile();
                                break;
                            case 2:
                                insertNewVehicle();
                                break;
                            case 3:
                                displayLicensePlates();
                                break;
                            case 4:
                                changeVehicleStatus();
                                break;
                            case 5:
                                inflateWheelsToMaximum();
                                break;
                            case 6:
                                refuelVehicle();
                                break;
                            case 7:
                                rechargeVehicle();
                                break;
                            case 8:
                                displayVehicleDetails();
                                break;
                            case 0:
                                continueRunning = false;
                                Console.WriteLine("Goodbye!");
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("Error: {0}", ex.Message));
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }

                if (continueRunning)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private void showMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Garage Management System ===");
            Console.WriteLine("1. Load vehicles from file (Vehicles.db)");
            Console.WriteLine("2. Insert new vehicle");
            Console.WriteLine("3. Display license plates");
            Console.WriteLine("4. Change vehicle status");
            Console.WriteLine("5. Inflate wheels to maximum");
            Console.WriteLine("6. Refuel vehicle");
            Console.WriteLine("7. Recharge vehicle");
            Console.WriteLine("8. Display vehicle details");
            Console.WriteLine("0. Exit");
            Console.WriteLine(string.Format("\nCurrently {0} vehicles in garage.", m_GarageManagement.GetVehicleCount()));
            Console.Write("Enter your choice: ");
        }

        private void loadVehiclesFromFile()
        {
            Console.WriteLine("Loading vehicles from Vehicles.db...");

            try
            {
                int vehiclesLoaded = m_GarageManagement.LoadVehiclesFromFile();
                Console.WriteLine(string.Format("\nSuccessfully loaded {0} vehicles from Vehicles.db", vehiclesLoaded));

                if (vehiclesLoaded > 0)
                {
                    Console.WriteLine("\nLoaded vehicles:");
                    foreach (string licensePlate in m_GarageManagement.GetAllLicensePlates())
                    {
                        Console.WriteLine(string.Format("- {0}", licensePlate));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Failed to load vehicles: {0}", ex.Message));
                Console.WriteLine(string.Format("Stack trace: {0}", ex.StackTrace));
                if (ex.InnerException != null)
                {
                    Console.WriteLine(string.Format("Inner exception: {0}", ex.InnerException.Message));
                }
            }
        }

        private void insertNewVehicle()
        {
            string licensePlate = getLicensePlateFromUser(out bool isInGarage);

            if (isInGarage)
            {
                m_GarageManagement.ChangeVehicleStatus(licensePlate, eVehicleStatus.InRepair);
                Console.WriteLine("Vehicle found in garage. Status set to 'In Repair'.");
            }
            else
            {
                createNewVehicle(licensePlate);
            }
        }

        private string getLicensePlateFromUser(out bool o_IsInGarage)
        {
            Console.WriteLine("Enter the license plate of the vehicle:");
            string licensePlate = Console.ReadLine();
            o_IsInGarage = m_GarageManagement.IsVehicleInGarage(licensePlate);
            return licensePlate;
        }

        private void createNewVehicle(string i_LicensePlate)
        {
            Vehicle vehicle = getGeneralParametersOfVehicle(i_LicensePlate);

            foreach (string question in vehicle.QuestionsForUserToSetParams.Keys)
            {
                Console.WriteLine(question);
                while (true)
                {
                    try
                    {
                        vehicle.AddDetail(Console.ReadLine(), question); 
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("Invalid input: {0}. Please try again.", ex.Message));
                    }
                }
            }

            m_GarageManagement.AddVehicleToGarage(vehicle);

            if (vehicle.VehicleEngine is FuelEngine)
            {
                refuelVehicle(i_LicensePlate);
            }
            else if (vehicle.VehicleEngine is ElectricEngine)
            {
                rechargeVehicle(i_LicensePlate);
            }

            Console.WriteLine("Vehicle added to garage successfully!");
        }

        private Vehicle getGeneralParametersOfVehicle(string i_LicensePlate)
        {
            string vehicleType;
            string ownerName, ownerPhone, manufacturer;
            char inputtedLetter;
            Vehicle vehicle;

            Console.WriteLine("Enter vehicle type:");
            Console.WriteLine("1. FuelMotorcycle");
            Console.WriteLine("2. ElectricMotorcycle");
            Console.WriteLine("3. FuelCar");
            Console.WriteLine("4. ElectricCar");
            Console.WriteLine("5. Truck");

            while (true)
            {
                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            vehicleType = "FuelMotorcycle";
                            break;
                        case 2:
                            vehicleType = "ElectricMotorcycle";
                            break;
                        case 3:
                            vehicleType = "FuelCar";
                            break;
                        case 4:
                            vehicleType = "ElectricCar";
                            break;
                        case 5:
                            vehicleType = "Truck";
                            break;
                        default:
                            throw new ArgumentException("Invalid vehicle type");
                    }

                    Vehicle testVehicle = VehicleCreator.CreateVehicle(vehicleType, "TEST", "TEST");
                    if (testVehicle == null)
                    {
                        throw new ArgumentException("Vehicle type not supported");
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Please enter a valid vehicle type (1-5).");
                }
            }

            Console.WriteLine("Enter owner name:");
            ownerName = Console.ReadLine();
            Console.WriteLine("Enter owner phone number:");
            ownerPhone = Console.ReadLine();

            Console.WriteLine("Enter model name:");
            string modelName = Console.ReadLine();

            vehicle = VehicleCreator.CreateVehicle(vehicleType, i_LicensePlate, modelName);

            Owner owner = new Owner(ownerName, ownerPhone);
            vehicle.SetOwner(owner);

            Console.WriteLine("Enter the manufacturer for all wheels:");
            manufacturer = Console.ReadLine();
            Console.WriteLine("Do you wish to add air pressure for all wheels at once? (y/n)");

            while (!char.TryParse(Console.ReadLine(), out inputtedLetter) || (inputtedLetter != 'y' && inputtedLetter != 'n'))
            {
                Console.WriteLine("Please enter 'y' or 'n'.");
            }

            if (inputtedLetter == 'y')
            {
                addToAllAirPressure(vehicle, manufacturer);
            }
            else
            {
                addAirPressureOneByOne(vehicle, manufacturer);
            }

            return vehicle;
        }

        private void addToAllAirPressure(Vehicle i_Vehicle, string i_Manufacturer)
        {
            Console.WriteLine("Enter air pressure for all wheels:");
            while (true)
            {
                try
                {
                    float airPressureToAdd = float.Parse(Console.ReadLine());
                    foreach (Wheel wheel in i_Vehicle.VehicleWheels)
                    {
                        wheel.FillAirPressure(airPressureToAdd);
                        wheel.ManufacturerName = i_Manufacturer;
                    }
                    break;
                }
                catch (ValueRangeException ex)
                {
                    Console.WriteLine(string.Format("Please enter a value at most {0}.", ex.MaxValue));
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
        }

        private void addAirPressureOneByOne(Vehicle i_Vehicle, string i_Manufacturer)
        {
            for (int i = 0; i < i_Vehicle.VehicleWheels.Count; i++)
            {
                Console.WriteLine(string.Format("Enter air pressure for wheel {0}:", i + 1));
                while (true)
                {
                    try
                    {
                        float airPressureToAdd = float.Parse(Console.ReadLine());
                        i_Vehicle.VehicleWheels[i].FillAirPressure(airPressureToAdd);
                        i_Vehicle.VehicleWheels[i].ManufacturerName = i_Manufacturer;
                        break;
                    }
                    catch (ValueRangeException ex)
                    {
                        Console.WriteLine(string.Format("Please enter a value at most {0}.", ex.MaxValue));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please enter a valid number.");
                    }
                }
            }
        }

        private void displayLicensePlates()
        {
            char inputLetter;

            Console.WriteLine("Do you want to filter by vehicle status? (y/n)");
            while (!char.TryParse(Console.ReadLine(), out inputLetter) || (inputLetter != 'y' && inputLetter != 'n'))
            {
                Console.WriteLine("Please enter 'y' or 'n'.");
            }

            if (inputLetter == 'y')
            {
                eVehicleStatus status = getStatusFromUser();
                printLicensePlates(m_GarageManagement.GetLicensePlatesByStatus(status));
            }
            else
            {
                printLicensePlates(m_GarageManagement.GetAllLicensePlates());
            }
        }

        private eVehicleStatus getStatusFromUser()
        {
            eVehicleStatus status;

            Console.WriteLine("Enter vehicle status:");
            Console.WriteLine("1. InRepair");
            Console.WriteLine("2. Repaired");
            Console.WriteLine("3. Paid");

            while (true)
            {
                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    status = (eVehicleStatus)choice;
                    if (Enum.IsDefined(typeof(eVehicleStatus), status))
                    {
                        break;
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                catch
                {
                    Console.WriteLine("Please enter a valid status (1-3).");
                }
            }

            return status;
        }

        private void printLicensePlates(List<string> i_LicensePlates)
        {
            if (i_LicensePlates.Count == 0)
            {
                Console.WriteLine("No vehicles found.");
            }
            else
            {
                Console.WriteLine("License plates:");
                foreach (string license in i_LicensePlates)
                {
                    Console.WriteLine(string.Format("- {0}", license));
                }
            }
        }

        private void changeVehicleStatus()
        {
            string licensePlate = getLicensePlateFromUser(out bool isInGarage);

            if (!isInGarage)
            {
                Console.WriteLine("No vehicle with this license plate found in the garage.");
                return;
            }

            eVehicleStatus newStatus = getStatusFromUser();
            m_GarageManagement.ChangeVehicleStatus(licensePlate, newStatus);
            Console.WriteLine("Vehicle status updated successfully.");
        }

        private void inflateWheelsToMaximum()
        {
            string licensePlate = getLicensePlateFromUser(out bool isInGarage);

            if (!isInGarage)
            {
                Console.WriteLine("No vehicle with this license plate found in the garage.");
                return;
            }

            try
            {
                m_GarageManagement.InflateWheelsToMaximum(licensePlate);
                Console.WriteLine("Wheels inflated to maximum pressure.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error inflating wheels: {0}", ex.Message));
            }
        }

        private void refuelVehicle()
        {
            string licensePlate = getLicensePlateFromUser(out bool isInGarage);

            if (!isInGarage)
            {
                Console.WriteLine("No vehicle with this license plate found in the garage.");
                return;
            }

            refuelVehicle(licensePlate);
        }

        private void refuelVehicle(string i_LicensePlate)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter fuel type:");
                    Console.WriteLine("1. Soler");
                    Console.WriteLine("2. Octan95");
                    Console.WriteLine("3. Octan96");
                    Console.WriteLine("4. Octan98");

                    eFuelType fuelType = (eFuelType)int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the amount of fuel to add (liters):");
                    float fuelAmount = float.Parse(Console.ReadLine());

                    m_GarageManagement.RefuelVehicle(i_LicensePlate, fuelType, fuelAmount);
                    Console.WriteLine("Vehicle refueled successfully.");
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(string.Format("Error: {0}", ex.Message));
                    break;
                }
                catch (ValueRangeException ex)
                {
                    Console.WriteLine(string.Format("Please enter a fuel amount at most: {0}", ex.MaxValue));
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter valid values.");
                }
            }
        }

        private void rechargeVehicle()
        {
            string licensePlate = getLicensePlateFromUser(out bool isInGarage);

            if (!isInGarage)
            {
                Console.WriteLine("No vehicle with this license plate found in the garage.");
                return;
            }

            rechargeVehicle(licensePlate);
        }

        private void rechargeVehicle(string i_LicensePlate)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the number of minutes to charge:");
                    float minutesToCharge = float.Parse(Console.ReadLine());

                    float hoursToCharge = minutesToCharge / 60f;

                    m_GarageManagement.RechargeVehicle(i_LicensePlate, hoursToCharge);
                    Console.WriteLine("Vehicle charged successfully.");
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(string.Format("Error: {0}", ex.Message));
                    break;
                }
                catch (ValueRangeException ex)
                {
                    float maxMinutes = ex.MaxValue * 60f;
                    Console.WriteLine(string.Format("Invalid amount of minutes, maximum amount: {0:F1}", maxMinutes));
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a valid number of minutes.");
                }
            }
        }

        private void displayVehicleDetails()
        {
            string licensePlate = getLicensePlateFromUser(out bool isInGarage);

            if (!isInGarage)
            {
                Console.WriteLine("No vehicle with this license plate found in the garage.");
                return;
            }

            Console.WriteLine(m_GarageManagement.GetVehicleDetails(licensePlate));
        }
    }
}