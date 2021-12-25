using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        private const float k_MaxFuelAmount = 48f;
        private eFuelType m_FuelType = eFuelType.Octan95;
        private float m_CurrentFuelAmount = 0f;

        public FuelCar(string i_ModelName, string i_LicenseNumber)
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
