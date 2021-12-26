using System;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Wheels
    {
        private readonly string r_Manufacturer = "0";
        private readonly float r_MaxTirePressure = 0f;
        private float m_CurrentTirePressure = 0f;

        internal Wheels(string i_Manufacturer, float i_MaxTirePressure)
        {
            r_Manufacturer = i_Manufacturer;
            r_MaxTirePressure = i_MaxTirePressure;
        }

        internal float TirePressure
        {
            get
            {
                return m_CurrentTirePressure;
            }

            set
            {
                if(r_MaxTirePressure >= value && value >= 0)
                    {
                        m_CurrentTirePressure = value;
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(0, r_MaxTirePressure);
                    }
            }
        }

        internal float MaxTirePressure
        {
            get { return r_MaxTirePressure; }
        }

        internal void InfalteTires(float i_TirePressureToAdd)
        {
            if (m_CurrentTirePressure + i_TirePressureToAdd <= r_MaxTirePressure && m_CurrentTirePressure + i_TirePressureToAdd >= 0)
            {
                m_CurrentTirePressure += i_TirePressureToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxTirePressure);
            }
        }

        public override string ToString()
        {
            StringBuilder wheelInfoStringBuilder = new StringBuilder();
            wheelInfoStringBuilder.Append(string.Format("Wheel manufacturer: {0}{1}", r_Manufacturer, Environment.NewLine));
            wheelInfoStringBuilder.Append(string.Format("Wheel current air pressure (PSI): {0}/{1}{2}", m_CurrentTirePressure, r_MaxTirePressure, Environment.NewLine));
            return wheelInfoStringBuilder.ToString();
        }
    }
}
