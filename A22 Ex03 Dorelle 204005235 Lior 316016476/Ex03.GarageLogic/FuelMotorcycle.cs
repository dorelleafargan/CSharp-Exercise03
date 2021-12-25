using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        private const float k_MaxFuelAmount = 5.8f;
        private eFuelType m_FuelType = eFuelType.Octan98;
        private float m_CurrentFuelAmount = 0f;
        public FuelMotorcycle(string i_ModelName, string i_LicenseNumber)
            : base(i_ModelName, i_LicenseNumber)
        {

        }
        public float CurrentFuelAmount
        {
            get { return m_CurrentFuelAmount; }
            set { m_CurrentFuelAmount = value; }
        }
        public override float MaxEngineFuelCapcity()
        {
            return k_MaxFuelAmount;
        }
        public override void NewEngine(float i_CurrentAmoutOfEnergy)
        {
            SetEngine(eEngineType.FuelBased, i_CurrentAmoutOfEnergy, k_MaxFuelAmount);
        }
    }
}
