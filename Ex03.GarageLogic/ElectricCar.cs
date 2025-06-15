namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private const float k_MaxBatteryHours = 4.8f;

        public ElectricCar(string i_LicensePlate, string i_ModelName) : base(i_LicensePlate, i_ModelName)
        {
            VehicleEngine = new ElectricEngine(k_MaxBatteryHours);
        }
    }
}