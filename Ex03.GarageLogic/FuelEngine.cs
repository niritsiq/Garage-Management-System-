using System;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private readonly eFuelType r_FuelType;

        public eFuelType FuelType
        {
            get { return r_FuelType; }
        }

        public FuelEngine(eFuelType i_FuelType, float i_MaxFuelLiters) : base(i_MaxFuelLiters)
        {
            r_FuelType = i_FuelType;
        }

        public override void FillEnergy(params object[] i_Parameters)
        {
            if (i_Parameters.Length != 2 ||
                !(i_Parameters[0] is eFuelType fuelType) ||
                !(i_Parameters[1] is float litersToAdd))
            {
                throw new ArgumentException("Fuel engine requires fuel type and liters (eFuelType, float)");
            }

            if (fuelType != r_FuelType)
            {
                throw new ArgumentException(string.Format("Wrong fuel type. Vehicle uses {0}", r_FuelType));
            }

            if (litersToAdd < 0 || m_CurrentEnergy + litersToAdd > MaximalEnergy)
            {
                throw new ValueRangeException(0, MaximalEnergy - m_CurrentEnergy);
            }

            m_CurrentEnergy += litersToAdd;
        }

        public void Refuel(float i_LitersToAdd, eFuelType i_FuelType)
        {
            FillEnergy(i_FuelType, i_LitersToAdd);
        }

        public override string ToString()
        {
            return string.Format("Fuel Engine - Current fuel: {0:F1} liters out of {1:F1} liters (Type: {2}) ({3:F1}%)",
                m_CurrentEnergy, MaximalEnergy, r_FuelType, EnergyPercentage);
        }
    }
}