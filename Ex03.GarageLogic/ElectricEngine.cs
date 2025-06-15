using System;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxBatteryHours) : base(i_MaxBatteryHours)
        {
        }

        public override void FillEnergy(params object[] i_Parameters)
        {
            if (i_Parameters.Length != 1 || !(i_Parameters[0] is float hoursToAdd))
            {
                throw new ArgumentException("Electric engine requires hours to charge (float)");
            }

            if (hoursToAdd < 0 || m_CurrentEnergy + hoursToAdd > MaximalEnergy)
            {
                throw new ValueRangeException(0, MaximalEnergy - m_CurrentEnergy);
            }

            m_CurrentEnergy += hoursToAdd;
        }

        public void Recharge(float i_HoursToAdd)
        {
            FillEnergy(i_HoursToAdd);
        }

        public override string ToString()
        {
            return string.Format("Electric Engine - Current charge: {0:F1} hours out of {1:F1} hours ({2:F1}%)",
                m_CurrentEnergy, MaximalEnergy, EnergyPercentage);
        }
    }
}