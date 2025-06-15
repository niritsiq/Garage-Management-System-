using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_CarriesDangerousMaterials;
        private float m_CargoVolume;
        private const int k_NumberOfWheels = 12;
        private const float k_MaxWheelPressure = 27f;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const float k_MaxFuelCapacity = 135f;

        public bool CarriesDangerousMaterials
        {
            get
            {
                return m_CarriesDangerousMaterials;
            }
            set
            {
                m_CarriesDangerousMaterials = value;
            }
        }

        public float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }
            set
            {
                m_CargoVolume = value;
            }
        }

        public Truck(string i_LicensePlate, string i_ModelName) : base(i_LicensePlate, i_ModelName)
        {
            AddWheels(k_NumberOfWheels, k_MaxWheelPressure);

            VehicleEngine = new FuelEngine(k_FuelType, k_MaxFuelCapacity);

            m_QuestionsForUserToSetParams["Carries dangerous materials (true/false):"] = typeof(bool);
            m_QuestionsForUserToSetParams["Cargo volume:"] = typeof(float);
        }

        public override void AddDetail(string i_UserInput, string i_QuestionAsked)
        {
            Type typeToConvert = m_QuestionsForUserToSetParams[i_QuestionAsked];

            if (typeToConvert == typeof(bool))
            {
                const bool v_TrueValue = true;
                const bool v_FalseValue = false;

                if (i_UserInput.ToLower() == "true" || i_UserInput.ToLower() == "yes" || i_UserInput.ToLower() == "y")
                {
                    m_CarriesDangerousMaterials = v_TrueValue;
                }
                else if (i_UserInput.ToLower() == "false" || i_UserInput.ToLower() == "no" || i_UserInput.ToLower() == "n")
                {
                    m_CarriesDangerousMaterials = v_FalseValue;
                }
                else if (!bool.TryParse(i_UserInput, out m_CarriesDangerousMaterials))
                {
                    throw new FormatException("Invalid boolean value. Please enter: true/false, yes/no, or y/n");
                }
            }
            else if (typeToConvert == typeof(float))
            {
                if (!float.TryParse(i_UserInput, out m_CargoVolume) || m_CargoVolume < 0)
                {
                    throw new FormatException("Invalid cargo volume. Please enter a non-negative number");
                }
            }
            else
            {
                throw new ArgumentException("Unknown parameter type");
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(base.ToString());
            stringBuilder.AppendLine(string.Format("Carries Dangerous Materials: {0}", m_CarriesDangerousMaterials));
            stringBuilder.AppendLine(string.Format("Cargo Volume: {0:F1}", m_CargoVolume));

            return stringBuilder.ToString();
        }
    }
}