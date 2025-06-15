namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaximalAirPressure;

        public string ManufacturerName
        {
            get { return m_ManufacturerName; }
            set { m_ManufacturerName = value; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        public float MaximalAirPressure
        {
            get { return r_MaximalAirPressure; }
        }

        public Wheel(float i_MaximalAirPressure)
        {
            r_MaximalAirPressure = i_MaximalAirPressure;
            m_CurrentAirPressure = 0;
            m_ManufacturerName = string.Empty;
        }

        public void FillAirPressure(float i_AirPressureToAdd)
        {
            if (i_AirPressureToAdd < 0 || m_CurrentAirPressure + i_AirPressureToAdd > r_MaximalAirPressure)
            {
                throw new ValueRangeException(0, r_MaximalAirPressure - m_CurrentAirPressure);
            }

            m_CurrentAirPressure += i_AirPressureToAdd;
        }

        public override string ToString()
        {
            return string.Format("Manufacturer: {0}, Pressure: {1:F1}/{2:F1}",
                m_ManufacturerName, m_CurrentAirPressure, r_MaximalAirPressure);
        }
    }
}