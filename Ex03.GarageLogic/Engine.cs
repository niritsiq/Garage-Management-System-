namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_CurrentEnergy;
        private readonly float r_MaximalEnergy;

        public float CurrentEnergy
        {
            get { return m_CurrentEnergy; }
            protected set { m_CurrentEnergy = value; }
        }

        public float MaximalEnergy
        {
            get { return r_MaximalEnergy; }
        }

        public float EnergyPercentage
        {
            get { return (m_CurrentEnergy / r_MaximalEnergy) * 100; }
        }

        protected Engine(float i_MaximalEnergy)
        {
            r_MaximalEnergy = i_MaximalEnergy;
            m_CurrentEnergy = 0;
        }

        public abstract void FillEnergy(params object[] i_Parameters);
        public abstract override string ToString();
    }
}