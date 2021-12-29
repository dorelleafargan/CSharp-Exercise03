using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eFuelType
    {
        Octane98 = 1,
        Octane96 = 2,
        Octan95 = 3,
        Soler = 4,
    }

    public enum eEngineType
    {
        FuelBased = 1,
        ElectricBased = 2,
    }

    internal class Engine 
    {
        private readonly float r_MaxAmountOfEnergy;
        private readonly eEngineType r_EngineType;
        private float m_CurrentAmountOfEnergy;
        private eFuelType m_FuelType;

        internal Engine(eEngineType i_EngineType, float i_MaxAmountOfEnergy)
        {
            r_MaxAmountOfEnergy = i_MaxAmountOfEnergy;
            r_EngineType = i_EngineType;
        }

        internal float MaxAmountOfEnergy
        {
            get { return r_MaxAmountOfEnergy;}
        }

        internal float CurrentAmountOfEnergy
        {
            get
            {
                return m_CurrentAmountOfEnergy;
            }

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

        internal eFuelType FuelType
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

            set
            {
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

        private void reEnergize(float i_UnitAmount)
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

        internal void Refuel(float i_FuelAmountToAdd, eFuelType i_FuelType)
        {
            if (r_EngineType != eEngineType.FuelBased)
            {
                throw new ArgumentException("Can't fuel electric based engine.");
            }
            else if (m_FuelType != i_FuelType)
            {
                throw new ArgumentException(string.Format("Not the correct fuel type expected:{0} and got {1}", m_FuelType, i_FuelType));
            }
            else
            {
                reEnergize(i_FuelAmountToAdd);
            }
        }

        internal void Recharge(float i_HoursAmountToRecharge)
        {
            if (r_EngineType == eEngineType.ElectricBased)
            {
                reEnergize(i_HoursAmountToRecharge);
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
                        engineInfoStringBuilder.Append(string.Format("Fuel left (Liters): {0}{1}", m_CurrentAmountOfEnergy, Environment.NewLine));
                        break;
                    }

                case eEngineType.ElectricBased:
                    {
                        engineInfoStringBuilder.Append(string.Format("Max Battery (Hours): {0}{1}", r_MaxAmountOfEnergy, Environment.NewLine));
                        engineInfoStringBuilder.Append(string.Format("Recharge Left in Battery (Hours): {0}{1}", m_CurrentAmountOfEnergy, Environment.NewLine));

                        break;
                    }
            }

            return engineInfoStringBuilder.ToString();
        }
    }

}
