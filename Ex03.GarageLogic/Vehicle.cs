using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_LicensePlate;
        private string m_ModelName;
        private Engine m_Engine;
        private List<Wheel> m_Wheels;
        private eVehicleStatus m_Status;
        private Owner m_Owner;
        protected Dictionary<string, Type> m_QuestionsForUserToSetParams;

        public string LicensePlate
        {
            get { return r_LicensePlate; }
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public Engine VehicleEngine
        {
            get { return m_Engine; }
            set { m_Engine = value; }
        }

        public List<Wheel> VehicleWheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public Owner VehicleOwner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
        }

        public Dictionary<string, Type> QuestionsForUserToSetParams
        {
            get { return m_QuestionsForUserToSetParams; }
        }

        public float EnergyPercentage
        {
            get { return m_Engine?.EnergyPercentage ?? 0; }
        }

        protected Vehicle(string i_LicensePlate, string i_ModelName)
        {
            r_LicensePlate = i_LicensePlate;
            m_ModelName = i_ModelName;
            m_Owner = null; // Will be set later when adding to garage
            m_Status = eVehicleStatus.InRepair;
            m_QuestionsForUserToSetParams = new Dictionary<string, Type>();
        }

        public void SetOwner(Owner i_Owner)
        {
            m_Owner = i_Owner;
        }

        protected void AddWheels(int i_NumberOfWheels, float i_MaxPressure)
        {
            m_Wheels = new List<Wheel>();
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_MaxPressure));
            }
        }

        public abstract void AddDetail(string i_UserInput, string i_QuestionAsked);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("License Plate: {0}", r_LicensePlate));
            sb.AppendLine(string.Format("Model: {0}", m_ModelName));
            sb.AppendLine(m_Owner.ToString());
            sb.AppendLine(string.Format("Status: {0}", m_Status));
            sb.AppendLine(string.Format("Energy: {0:F1}%", EnergyPercentage));
            sb.AppendLine("Wheels:");

            for (int i = 0; i < m_Wheels.Count; i++)
            {
                sb.AppendLine(string.Format("  Wheel {0}: {1}", i + 1, m_Wheels[i]));
            }

            sb.AppendLine(string.Format("Engine: {0}", m_Engine));

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return r_LicensePlate.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Vehicle other = obj as Vehicle;
            return other != null && r_LicensePlate.Equals(other.r_LicensePlate);
        }
    }
}