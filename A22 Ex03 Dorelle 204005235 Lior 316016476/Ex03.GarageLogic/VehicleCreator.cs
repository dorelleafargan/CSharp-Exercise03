namespace Ex03.GarageLogic
{
    public enum eVehicleType
    {
        Car = 1,
        Motorcycle = 2,
        Truck = 3,
    }

    public class VehicleCreator
    {
        public Vehicle CreateVehicle(eVehicleType i_VehcileType, eEngineType i_EngineType, string i_LicenseNumber, string i_ModelName)
        {
            Vehicle newVehicle = null;
            switch (i_VehcileType)
            {
                case eVehicleType.Car:
                    {
                        switch (i_EngineType)
                        {
                            case eEngineType.FuelBased:
                                {
                                    newVehicle = new FuelCar(i_ModelName, i_LicenseNumber);
                                    break;
                                }

                            case eEngineType.ElectricBased:
                                {
                                    newVehicle = new ElectricCar(i_ModelName, i_LicenseNumber);
                                    break;
                                }

                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }

                case eVehicleType.Motorcycle:
                    {
                        switch (i_EngineType)
                        {
                            case eEngineType.FuelBased:
                                {
                                    newVehicle = new FuelMotorcycle(i_ModelName, i_LicenseNumber);
                                    break;
                                }

                            case eEngineType.ElectricBased:
                                {
                                    newVehicle = new ElectricMotorcycle(i_ModelName, i_LicenseNumber);
                                    break;
                                }

                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }

                case eVehicleType.Truck:
                    {
                        switch (i_EngineType)
                        {
                            case eEngineType.FuelBased:
                                {
                                    newVehicle = new FuelTruck(i_ModelName, i_LicenseNumber);
                                    break;
                                }

                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }
            }

            return newVehicle;
        }

        public void SetCarAttributes(Car i_Car, ePaintJobColor i_Color, eNumberOfDoors i_NumberOfDoors)
        {
            i_Car.Color = i_Color;
            i_Car.NumberOfDoors = i_NumberOfDoors;
        }

        public void SetMotorcycleAttributes(Motorcycle i_Motorcycle, eLicenseType i_LicenseType, int i_EngineVolume)
        {
            i_Motorcycle.LicenseType = i_LicenseType;
            i_Motorcycle.EngineVolume = i_EngineVolume;
        }

        public void SetTruckAttributes(Truck i_Truck, bool i_isCargoRefrigirated, float i_CargoVolume)
        {
            i_Truck.IsContentRefgirated = i_isCargoRefrigirated;
            i_Truck.CargoVolume = i_CargoVolume;
        }

        public void SetVehicleAttributes(Vehicle i_Vehicle, eVehicleType i_VehicleType, float i_CurrentAmountOfEnergy, string i_WheelManufacturer, float i_CurrentTirePressure)
        {
            i_Vehicle.VehicleType = i_VehicleType;
            i_Vehicle.NewEngine(i_CurrentAmountOfEnergy);
            i_Vehicle.NewWheels(i_WheelManufacturer, i_CurrentTirePressure);
        }
    }
}
