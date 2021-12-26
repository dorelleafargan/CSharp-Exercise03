using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eLicenseType
    {
        A = 1,
        A2 = 2,
        AA = 3,
        B = 4,
    }

    public abstract class Motorcycle : Vehicle
    {
        private const int k_NumberOfWheels = 2;
        private const int k_MaxTirePressure = 30;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        protected Motorcycle(string i_ModelName, string i_LicenseNumber)
            : base(i_ModelName, i_LicenseNumber)
        {
        }

        internal eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        internal int EngineVolume
        {
            get { return m_EngineVolume; }
            set { m_EngineVolume = value; }
        }

        internal override void NewWheels(string i_Manufacturer, float i_CurrentTirePressure)
        {
            SetWheels(i_Manufacturer, i_CurrentTirePressure, k_MaxTirePressure, k_NumberOfWheels);
        }

        public override float MaxWheelTirePressure()
        {
            return k_MaxTirePressure;
        }

        public override string ToString()
        {
            StringBuilder infoStringBuilder = new StringBuilder(base.ToString());
            infoStringBuilder.Append(string.Format("License Type : {0}{1}", m_LicenseType, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Engine Volume : {0}{1}", m_EngineVolume, Environment.NewLine));
            return infoStringBuilder.ToString();
        }

    }
}
