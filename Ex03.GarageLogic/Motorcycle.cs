using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;
        private const int k_NumberOfWheels = 2;
        private const float k_MaxWheelPressure = 30f;

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public int EngineVolume
        {
            get { return m_EngineVolume; }
            set { m_EngineVolume = value; }
        }

        public Motorcycle(string i_LicensePlate, string i_ModelName) : base(i_LicensePlate, i_ModelName)
        {
            AddWheels(k_NumberOfWheels, k_MaxWheelPressure);
            m_QuestionsForUserToSetParams["License type (A/A2/AB/B2):"] = typeof(eLicenseType);
            m_QuestionsForUserToSetParams["Engine volume (cc):"] = typeof(int);
        }

        public override void AddDetail(string i_UserInput, string i_QuestionAsked)
        {
            Type typeToConvert = m_QuestionsForUserToSetParams[i_QuestionAsked];

            if (typeToConvert == typeof(eLicenseType))
            {
                if (int.TryParse(i_UserInput, out _) || !Enum.TryParse(i_UserInput, true, out m_LicenseType))
                {
                    throw new FormatException("Invalid license type. Please enter: A, A2, AB, or B2");
                }
            }
            else if (typeToConvert == typeof(int))
            {
                if (!int.TryParse(i_UserInput, out m_EngineVolume) || m_EngineVolume <= 0)
                {
                    throw new FormatException("Invalid engine volume. Please enter a positive number");
                }
            }
            else
            {
                throw new ArgumentException("Unknown parameter type");
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.AppendLine(string.Format("License Type: {0}", m_LicenseType));
            sb.AppendLine(string.Format("Engine Volume: {0} cc", m_EngineVolume));
            return sb.ToString();
        }
    }
}