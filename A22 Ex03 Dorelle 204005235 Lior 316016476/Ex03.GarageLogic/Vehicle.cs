using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eRepairStatus
    {
        InRepair = 1,
        Repaired = 2,
        PaidFor = 3
    }

    public abstract class Vehicle
    {
        private readonly string r_ModelName = string.Empty;
        private readonly string r_LicensePlateNumber = string.Empty;
        private readonly List<Wheels> r_Wheels;
        private float m_EnergyLevelPercentage = 0f;
        private string m_OwnerName = string.Empty;
        private string m_OwnerPhoneNumber = string.Empty;
        private eRepairStatus m_VehicleRepairStatus = eRepairStatus.InRepair;
        private eVehicleType m_VehicleType;
        private Engine m_Engine;

        protected Vehicle(string i_ModelName, string i_LicenseNumber)
        {
            r_LicensePlateNumber = i_LicenseNumber;
            r_ModelName = i_ModelName;
            r_Wheels = new List<Wheels>();
        }

        internal eVehicleType VehicleType
        {
            get { return m_VehicleType; }
            set { m_VehicleType = value; }
        }

        internal string LicenseNumber
        {
            get { return r_LicensePlateNumber;}
        }

        internal eRepairStatus RepairStatus
        {
            get { return m_VehicleRepairStatus;}
            set { m_VehicleRepairStatus = value; }
        }

        internal string OwnerName
        {
            get { return m_OwnerName;}
            set { m_OwnerName= value;}
        }

        internal string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber;}
            set { m_OwnerPhoneNumber = value;}
        }

        internal float EnergyLevel
        {
            get { return m_EnergyLevelPercentage; }

        }

        internal abstract void NewEngine(float i_CurrentAmoutOfEnerg);

        protected void SetEngine(eEngineType i_EngineType, float i_CurrentAmountOfEnergy, float i_MaxAmountOfEnergy)
        {
            m_Engine = new Engine(i_EngineType, i_MaxAmountOfEnergy)
            { CurrentAmountOfEnergy = i_CurrentAmountOfEnergy };
            updateEngineEnergyLevel();
        }

        private void updateEngineEnergyLevel()
        {
            m_EnergyLevelPercentage = (m_Engine.CurrentAmountOfEnergy / m_Engine.MaxAmountOfEnergy)*100;
        }

        public abstract float MaxEngineFuelCapcity();

        internal abstract void NewWheels(string i_Manufacturer, float i_CurrentTirePressure);

        public abstract float MaxWheelTirePressure();

        protected void SetWheels(string i_Manufacturer, float i_CurrentTirePressure, float i_MaxTirePressure, int i_NumberOfWheels)
        {
            for(int i =0; i < i_NumberOfWheels; i++)
            {
                r_Wheels.Add(new Wheels(i_Manufacturer, i_MaxTirePressure) { TirePressure = i_CurrentTirePressure });
            }
        }

        protected void SetEngineFuelType(eFuelType i_FuelType)
        {
            if (m_Engine != null)
            {
                m_Engine.FuelType = i_FuelType;
            }
            else
            {
                throw new NullReferenceException("Fuel Type Cannot be set.");
            }
        }

        internal void Fuel(float i_FuelAmount, eFuelType i_FuelType)
        {
            m_Engine.Refuel(i_FuelAmount, i_FuelType);
            updateEngineEnergyLevel();
        }

        internal void Recharge(float i_HoursAmountToRecharge)
        {
            m_Engine.Recharge(i_HoursAmountToRecharge);
            updateEngineEnergyLevel();
        }

        internal void InflateTireToMaxPressure()
        {
            if (r_Wheels.Count > 0)
            {
                foreach (Wheels wheel in r_Wheels)
                {
                    if (wheel != null)
                    {
                        wheel.InfalteTires(wheel.MaxTirePressure - wheel.TirePressure);
                    }
                    else
                    {
                        throw new NullReferenceException("Vehicle has no wheels.");
                    }
                }
            }
            else
            {
                throw new NullReferenceException("Vehcile has no wheels.");
            }
        }

        internal string WheelsInfo()
        {
            if(r_Wheels.Count > 0 && r_Wheels[0] != null)
            {
                return r_Wheels[0].ToString();
            }
            else
            {
                throw new NullReferenceException("Vehicle Has No Wheels.");
            }
        }

        public override string ToString()
        {
            StringBuilder infoStringBuilder = new StringBuilder();
            infoStringBuilder.Append(string.Format("License Number : {0}{1}", LicenseNumber, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Model : {0}{1}", r_ModelName, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Owner Name : {0}{1}", m_OwnerName, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Owner Phone Number : {0}{1}", m_OwnerPhoneNumber, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Vehicle Type: {0}{1}", m_VehicleType, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Status : {0}{1}", RepairStatus, Environment.NewLine));
            infoStringBuilder.Append(WheelsInfo());
            infoStringBuilder.Append(m_Engine.ToString());
            return infoStringBuilder.ToString();
        }
    }
}
