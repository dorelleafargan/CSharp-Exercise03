using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxTimeOfRechargedBattery = 2.3f;
        private float m_TimeOfRechargeLeft = 0f;

        public ElectricMotorcycle(string i_ModelName, string i_LicenseNumber)
            : base(i_ModelName, i_LicenseNumber)
        {

        }
        public float TimeOfRechargeLeft
        {
            get { return m_TimeOfRechargeLeft; }
            set { m_TimeOfRechargeLeft = value; }
        }
        public override float MaxEngineFuelCapcity()
        {
            return k_MaxTimeOfRechargedBattery;
        }
        public override void NewEngine(float i_CurrentAmoutOfEnergy)
        {
            SetEngine(eEngineType.ElectricBased, i_CurrentAmoutOfEnergy, k_MaxTimeOfRechargedBattery);
        }
        public void RechargeBattery(float i_NumberOfHoursToAdd)
        {
            if (m_TimeOfRechargeLeft + i_NumberOfHoursToAdd <= k_MaxTimeOfRechargedBattery && m_TimeOfRechargeLeft + i_NumberOfHoursToAdd >= 0)
            {
                m_TimeOfRechargeLeft += i_NumberOfHoursToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, k_MaxTimeOfRechargedBattery);
            }
        }
    }
}
