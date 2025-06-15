namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        private const eFuelType k_FuelType = eFuelType.Octan98;
        private const float k_MaxFuelCapacity = 5.8f;

        public FuelMotorcycle(string i_LicensePlate, string i_ModelName) : base(i_LicensePlate, i_ModelName)
        {
            VehicleEngine = new FuelEngine(k_FuelType, k_MaxFuelCapacity);
        }
    }
}