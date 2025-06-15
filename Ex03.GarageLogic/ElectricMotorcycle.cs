namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxBatteryHours = 3.2f;

        public ElectricMotorcycle(string i_LicensePlate, string i_ModelName) : base(i_LicensePlate, i_ModelName)
        {
            VehicleEngine = new ElectricEngine(k_MaxBatteryHours);
        }
    }
}