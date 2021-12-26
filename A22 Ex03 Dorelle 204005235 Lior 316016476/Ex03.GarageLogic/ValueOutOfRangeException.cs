using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
            : base(string.Format("Float entered exceeded the limits of the range {0}-{1}", i_MinValue, i_MaxValue))
        {
        }
    }
}
