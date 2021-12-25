using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eEngineType
    {
        FuelBased = 1,
        ElectricBased = 2
    }

    public class Engine 
    {
        private readonly float r_MaxAmountOfEnergy;
        private readonly eEngineType r_EngineType;
        private float m_CurrentAmountOfEnergy;
        private eFuelType m_FuelType;
        public Engine(eEngineType i_EngineType, float i_MaxAmountOfEnergy)
        {
            r_MaxAmountOfEnergy = i_MaxAmountOfEnergy;
            r_EngineType = i_EngineType;
        }

        public float MaxAmountOfEnergy
        {
            get { return r_MaxAmountOfEnergy;}
        }
        public eEngineType EngineType
        {
            get { return r_EngineType; }
        }
        public float CurrentAmountOfEnergy
        {
            get { return m_CurrentAmountOfEnergy; }
            set
            {
                if (r_MaxAmountOfEnergy >= value && value >= 0)
                {
                    m_CurrentAmountOfEnergy = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, r_MaxAmountOfEnergy);
                }
            }
        }
        public eFuelType FuelType
        {
            get 
            { 
                if(r_EngineType == eEngineType.FuelBased)
                {
                    return m_FuelType;
                }
                else
                {
                    throw new ArgumentException("Electric engine has no fuel type.");
                }
            }
            set {
                if (r_EngineType == eEngineType.FuelBased)
                {
                    m_FuelType = value;
                }
                else
                {
                    throw new ArgumentException("Cant assign fuel type to electric engine.");
                }
            }
        }

        public void ReEnergize(float i_UnitAmount)
        {
            if (m_CurrentAmountOfEnergy + i_UnitAmount <= r_MaxAmountOfEnergy && i_UnitAmount >= 0)
            {
                m_CurrentAmountOfEnergy += i_UnitAmount;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxAmountOfEnergy);
            }
        }
        public void Refuel(float i_FuelAmountToAdd, eFuelType i_FuelType)
        {
            if (r_EngineType != eEngineType.FuelBased)
            {
                throw new ArgumentException("Can't fuel electric based engine");
            }
            else if (m_FuelType != i_FuelType)
            {
                throw new ArgumentException(string.Format("Not the correct fuel type expected:{0} and got {1}", m_FuelType, i_FuelType));
            }
            else
            {
                ReEnergize(i_FuelAmountToAdd);
            }
        }

        public void Recharge(float i_HoursAmountTORecharge)
        {
            if (r_EngineType == eEngineType.ElectricBased)
            {
                ReEnergize(i_HoursAmountTORecharge);
            }
            else
            {
                throw new ArgumentException("Can't charge fuel based engine.");
            }
        }

        public override string ToString()
        {
            StringBuilder engineInfoStringBuilder = new StringBuilder();
            engineInfoStringBuilder.Append(string.Format("Engine type: {0}{1}", r_EngineType, Environment.NewLine));

            switch (r_EngineType)
            {
                case eEngineType.FuelBased:
                    {
                        engineInfoStringBuilder.Append(string.Format("Fuel Type: {0}{1}", m_FuelType, Environment.NewLine));
                        engineInfoStringBuilder.Append(string.Format("Max Fuel (Liters): {0}{1}", r_MaxAmountOfEnergy, Environment.NewLine));
                        break;
                    }

                case eEngineType.ElectricBased:
                    {
                        engineInfoStringBuilder.Append(string.Format("Max Battery (Hours): {0}{1}", m_CurrentAmountOfEnergy, Environment.NewLine));
                        break;
                    }
            }

            return engineInfoStringBuilder.ToString();
        }
    }

}
