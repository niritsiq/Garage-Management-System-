using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private eDoorsNumber m_NumberOfDoors;
        private const int k_NumberOfWheels = 5;
        private const float k_MaxWheelPressure = 32f;

        public eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public eDoorsNumber NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }

        public Car(string i_LicensePlate, string i_ModelName) : base(i_LicensePlate, i_ModelName)
        {
            AddWheels(k_NumberOfWheels, k_MaxWheelPressure);
            m_QuestionsForUserToSetParams["Car color (Yellow/White/Silver/Black):"] = typeof(eCarColor);
            m_QuestionsForUserToSetParams["Number of doors (TwoDoors/ThreeDoors/FourDoors/FiveDoors):"] = typeof(eDoorsNumber);
        }

        public override void AddDetail(string i_UserInput, string i_QuestionAsked)
        {
            Type typeToConvert = m_QuestionsForUserToSetParams[i_QuestionAsked];

            if (typeToConvert == typeof(eCarColor))
            {
                if (int.TryParse(i_UserInput, out _) || !Enum.TryParse(i_UserInput, true, out m_CarColor))
                {
                    throw new FormatException("Invalid car color. Please enter: Yellow, White, Silver, or Black");
                }
            }
            else if (typeToConvert == typeof(eDoorsNumber))
            {
                if (int.TryParse(i_UserInput, out int doorNumber))
                {
                    if (Enum.IsDefined(typeof(eDoorsNumber), doorNumber))
                    {
                        m_NumberOfDoors = (eDoorsNumber)doorNumber;
                    }
                    else
                    {
                        throw new FormatException("Invalid number of doors. Please enter: 2, 3, 4, or 5");
                    }
                }
                else if (!Enum.TryParse(i_UserInput, true, out m_NumberOfDoors))
                {
                    throw new FormatException("Invalid number of doors. Please enter: TwoDoors, ThreeDoors, FourDoors, or FiveDoors");
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
            sb.AppendLine(string.Format("Car Color: {0}", m_CarColor));
            sb.AppendLine(string.Format("Number of Doors: {0}", m_NumberOfDoors));
            return sb.ToString();
        }
    }
}