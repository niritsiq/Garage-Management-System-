namespace Ex03.GarageLogic
{
    public class FuelTruck : Truck
    {
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const float k_MaxFuelCapacity = 135f;

        public FuelTruck(string i_LicensePlate, string i_ModelName) : base(i_LicensePlate, i_ModelName)
        {
            VehicleEngine = new FuelEngine(k_FuelType, k_MaxFuelCapacity);
        }
    }
}