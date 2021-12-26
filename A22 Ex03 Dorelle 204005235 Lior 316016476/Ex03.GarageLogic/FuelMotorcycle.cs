namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle
    {
        private const float k_MaxFuelAmount = 5.8f;
        private const eFuelType k_FuelType = eFuelType.Octane98;

        internal FuelMotorcycle(string i_ModelName, string i_LicenseNumber)
            : base(i_ModelName, i_LicenseNumber)
        {
        }

        public override float MaxEngineFuelCapcity()
        {
            return k_MaxFuelAmount;
        }

        internal override void NewEngine(float i_CurrentAmoutOfEnergy)
        {
            SetEngine(eEngineType.FuelBased, i_CurrentAmoutOfEnergy, k_MaxFuelAmount);
            SetEngineFuelType(k_FuelType);
        }
    }
}
