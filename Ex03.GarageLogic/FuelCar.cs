namespace Ex03.GarageLogic
{
    internal class FuelCar : Car
    {
        private const float k_MaxFuelAmount = 48f;
        private const eFuelType k_FuelType = eFuelType.Octan95;

        internal FuelCar(string i_ModelName, string i_LicenseNumber)
            : base(i_ModelName, i_LicenseNumber)
        {
        }

        public override float MaxEngineFuelCapcity()
        {
            return k_MaxFuelAmount;
        }

        internal override void NewEngine(float i_CurrentAmountOfEnergy)
        {
            SetEngine(eEngineType.FuelBased, i_CurrentAmountOfEnergy, k_MaxFuelAmount);
            SetEngineFuelType(k_FuelType);
        }
    }
}
