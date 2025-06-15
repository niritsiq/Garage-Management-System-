namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const float k_MaxFuelCapacity = 48f;

        public FuelCar(string i_LicensePlate, string i_ModelName) : base(i_LicensePlate, i_ModelName)
        {
            VehicleEngine = new FuelEngine(k_FuelType, k_MaxFuelCapacity);
        }
    }
}