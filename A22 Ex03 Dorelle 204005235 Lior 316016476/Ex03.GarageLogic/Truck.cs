using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Truck : Vehicle
    {
        private const int k_NumberOfWheels = 16;
        private const int k_MaxTirePressure = 25;
        private bool m_IsCargoRefrigerated = false;
        private float m_CargoVolume = 0f;

        public Truck(string i_ModelName, string i_LicenseNumber)
            : base(i_ModelName, i_LicenseNumber)
        {

        }
        public bool IsContentRefgirated
        {
            get { return m_IsCargoRefrigerated; }
            set { m_IsCargoRefrigerated = value; }
        }

        public float CargoVolume
        {
            get { return m_CargoVolume; }
            set { m_CargoVolume = value; }
        }
        public override void NewWheels(string i_Manufacturer, float i_CurrentTirePressure)
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
            infoStringBuilder.Append(string.Format("Cargo Refrigirated : {0}{1}", m_IsCargoRefrigerated, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Volume of Cargo : {0} in KG{1} ", m_CargoVolume, Environment.NewLine));
            return infoStringBuilder.ToString();
        }
    }
}
