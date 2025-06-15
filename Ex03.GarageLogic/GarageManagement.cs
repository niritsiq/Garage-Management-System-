using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class GarageManagement
    {
        private Dictionary<string, Vehicle> m_VehiclesInGarage;

        public GarageManagement()
        {
            m_VehiclesInGarage = new Dictionary<string, Vehicle>();
        }

        public int LoadVehiclesFromFile(string i_FilePath = "Vehicles.db")
        {
            List<Vehicle> loadedVehicles = VehicleFileLoader.LoadVehiclesFromFile(i_FilePath);
            int vehiclesAdded = 0;

            foreach (Vehicle vehicle in loadedVehicles)
            {
                if (!IsVehicleInGarage(vehicle.LicensePlate))
                {
                    AddVehicleToGarage(vehicle);
                    vehiclesAdded++;
                }
            }

            return vehiclesAdded;
        }

        public bool IsVehicleInGarage(string i_LicensePlate)
        {
            return m_VehiclesInGarage.ContainsKey(i_LicensePlate);
        }

        public void AddVehicleToGarage(Vehicle i_Vehicle)
        {
            m_VehiclesInGarage[i_Vehicle.LicensePlate] = i_Vehicle;
        }

        public List<string> GetAllLicensePlates()
        {
            return new List<string>(m_VehiclesInGarage.Keys);
        }

        public List<string> GetLicensePlatesByStatus(eVehicleStatus i_Status)
        {
            return m_VehiclesInGarage.Values
                .Where(v => v.VehicleStatus == i_Status)
                .Select(v => v.LicensePlate)
                .ToList();
        }

        public void ChangeVehicleStatus(string i_LicensePlate, eVehicleStatus i_NewStatus)
        {
            if (!IsVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            m_VehiclesInGarage[i_LicensePlate].VehicleStatus = i_NewStatus;
        }

        public void InflateWheelsToMaximum(string i_LicensePlate)
        {
            if (!IsVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            Vehicle vehicle = m_VehiclesInGarage[i_LicensePlate];
            foreach (Wheel wheel in vehicle.VehicleWheels)
            {
                float pressureToAdd = wheel.MaximalAirPressure - wheel.CurrentAirPressure;
                if (pressureToAdd > 0)
                {
                    wheel.FillAirPressure(pressureToAdd);
                }
            }
        }

        public void RefuelVehicle(string i_LicensePlate, eFuelType i_FuelType, float i_FuelToAdd)
        {
            if (!IsVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            Vehicle vehicle = m_VehiclesInGarage[i_LicensePlate];
            FuelEngine fuelEngine = vehicle.VehicleEngine as FuelEngine;

            if (fuelEngine == null)
            {
                throw new ArgumentException("Vehicle does not have a fuel engine");
            }

            fuelEngine.FillEnergy(i_FuelType, i_FuelToAdd);
        }

        public void RechargeVehicle(string i_LicensePlate, float i_HoursToAdd)
        {
            if (!IsVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            Vehicle vehicle = m_VehiclesInGarage[i_LicensePlate];
            ElectricEngine electricEngine = vehicle.VehicleEngine as ElectricEngine;

            if (electricEngine == null)
            {
                throw new ArgumentException("Vehicle does not have an electric engine");
            }

            electricEngine.FillEnergy(i_HoursToAdd);
        }

        public string GetVehicleDetails(string i_LicensePlate)
        {
            if (!IsVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            return m_VehiclesInGarage[i_LicensePlate].ToString();
        }

        public int GetVehicleCount()
        {
            return m_VehiclesInGarage.Count;
        }

        public Vehicle GetVehicle(string i_LicensePlate)
        {
            if (!IsVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            return m_VehiclesInGarage[i_LicensePlate];
        }
    }
}