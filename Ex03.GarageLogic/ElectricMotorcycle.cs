namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxTimeOfRechargedBattery = 2.3f;

        internal ElectricMotorcycle(string i_ModelName, string i_LicenseNumber)
            : base(i_ModelName, i_LicenseNumber)
        {
        }

        public override float MaxEngineFuelCapcity()
        {
            return k_MaxTimeOfRechargedBattery;
        }

        internal override void NewEngine(float i_CurrentAmountOfEnergy)
        {
            SetEngine(eEngineType.ElectricBased, i_CurrentAmountOfEnergy, k_MaxTimeOfRechargedBattery);
        }
    }
}
