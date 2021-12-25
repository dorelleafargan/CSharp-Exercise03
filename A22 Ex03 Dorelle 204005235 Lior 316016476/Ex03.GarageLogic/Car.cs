using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum ePaintJobColor
    {
        Red = 1,
        White = 2,
        Black = 3,
        Blue = 4
    }
    
    public enum eNumberOfDoors
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
    }

    public abstract class Car : Vehicle
    {
        private const int k_NumberOfWheels = 4;
        private const int k_MaxTirePressure = 29;
        private eNumberOfDoors m_Doors;
        private ePaintJobColor m_PaintJobColor;

        public Car(string i_ModelName, string i_LicenseNumber)
            :base(i_ModelName, i_LicenseNumber)
        {
            
        }

        public ePaintJobColor Color
        {
            get { return m_PaintJobColor; }
            set { m_PaintJobColor = value; }
        }public eNumberOfDoors NumberOfDoors
        {
            get { return m_Doors; }
            set { m_Doors = value; }
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
            infoStringBuilder.Append(string.Format("Car Color : {0}{1}", m_PaintJobColor, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Number Of Doors : {0}{1}", m_Doors, Environment.NewLine));
            return infoStringBuilder.ToString();
        }
    }
}