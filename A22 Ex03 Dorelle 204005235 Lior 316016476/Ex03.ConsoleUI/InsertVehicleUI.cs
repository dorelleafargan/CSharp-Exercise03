using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class InsertVehicleUI
    {
        private const string k_LicenseNumberMessage = "License Number";
        private const string k_ModelMessage = "Model";
        private const string k_OwnerNameMessage = "Owner's name";
        private const string k_OwnerPhoneNumberMessage = "Owner's phone number";
        private const string k_WheelsManufacturerMessage = "Wheel's manufacturer";
        private const string k_FloatFormatExceptionMessage = "Invalid amount entered";
        private const string k_InvalidNumberMessage = "Amount entered cannot be larger than the maximum amount or negative, Please try again:";
        private const string k_FuelMessage = "Fuel capacity (Liters)";
        private const string k_BatteryEnergyMessage = "Recharge Left (Hours)";
        private const string k_WheelsPressureMessage = "Pressure in wheels (PSI)";
        private const string k_VolumeOfCargoMessage = "Truck's cargo volume";
        private readonly VehicleCreator r_VehicleCreator;
        private readonly GarageManager r_GarageManager;

        public InsertVehicleUI(VehicleCreator i_VehicleCreator, GarageManager i_GarageManager)
        {
            r_VehicleCreator = i_VehicleCreator;
            r_GarageManager = i_GarageManager;
        }

        public void InsertNewVehicle()
        {
            Console.WriteLine(string.Format("Vehicle add menu:{0}", Environment.NewLine));

            string licenseNumber = stringInputValidation(k_LicenseNumberMessage);

            if (r_GarageManager.DoesVehicleExist(licenseNumber))
            {
                Console.WriteLine("Vehicle already in garage, Changing status to \"In Repair\"");
                r_GarageManager.ChangeVehicleRepairStatus(licenseNumber, eRepairStatus.InRepair);
            }
            else
            {
                string ownerName = stringInputValidation(k_OwnerNameMessage);

                string ownersPhoneNumber = stringInputValidation(k_OwnerPhoneNumberMessage);

                string vehicleModel = stringInputValidation(k_ModelMessage);

                eVehicleType vehicleType = vehicleTypeInput();

                Vehicle newVehicle = null;

                eEngineType engineType = eEngineType.FuelBased;
                string energyNameString = k_FuelMessage;

                switch (vehicleType)
                {
                    case eVehicleType.Car:
                        {
                            engineType = engineTypeInput();
                            energyNameString = energySourceForMessage(engineType);
                            newVehicle = r_VehicleCreator.CreateVehicle(vehicleType, engineType, licenseNumber, vehicleModel);
                            ePaintJobColor carColor = colorInput();
                            eNumberOfDoors numberOfDoors = numberOfDoorsInput();
                            r_VehicleCreator.SetCarAttributes(newVehicle as Car, carColor, numberOfDoors);

                            break;
                        }

                    case eVehicleType.Motorcycle:
                        {
                            engineType = engineTypeInput();
                            energyNameString = energySourceForMessage(engineType);
                            newVehicle = r_VehicleCreator.CreateVehicle(vehicleType, engineType, licenseNumber, vehicleModel);
                            eLicenseType licenseType = licenseTypeInput();
                            int engineVolume = motorcyclEngineVolumeInput();
                            r_VehicleCreator.SetMotorcycleAttributes(newVehicle as Motorcycle, licenseType, engineVolume);
                            break;
                        }

                    case eVehicleType.Truck:
                        {
                            bool isRefrigirated = IsCargoRefrigirated();
                            float volumeOfCargo = floatInputValidation(k_VolumeOfCargoMessage);
                            newVehicle = r_VehicleCreator.CreateVehicle(vehicleType, engineType, licenseNumber, vehicleModel);
                            r_VehicleCreator.SetTruckAttributes(newVehicle as Truck, isRefrigirated, volumeOfCargo);
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }

                float currentAmountOfEnergy = floatInputValidation(energyNameString, newVehicle.MaxEngineFuelCapcity());
                string wheelsManufacturer = stringInputValidation(k_WheelsManufacturerMessage);
                float currentWheelsAirPressure = floatInputValidation(k_WheelsPressureMessage, newVehicle.MaxWheelTirePressure());

                try
                {
                    r_VehicleCreator.SetVehicleAttributes(newVehicle,
                        vehicleType,
                        currentAmountOfEnergy,
                        wheelsManufacturer,
                        currentWheelsAirPressure);

                    r_GarageManager.AddVehicle(ownerName, ownersPhoneNumber, newVehicle);
                    Console.WriteLine(string.Format("{0}, License Number {1} was added to the garage", vehicleModel ,licenseNumber));
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private string stringInputValidation(string i_StringName)
        {
            Console.WriteLine(string.Format("Please enter the {0} (not empty)", i_StringName));
            string stringInput = Console.ReadLine();
            while (string.IsNullOrEmpty(stringInput))
            {
                Console.WriteLine(string.Format("{0} cannot be empty, Please try again:", i_StringName));
                stringInput = Console.ReadLine();
            }
            return stringInput;
        }
        private float floatInputValidation(string i_NameOfObjectString, float i_MaxValue = 0)
        {
            string maxValueString = string.Empty;

            if (i_MaxValue > 0)
            {
                maxValueString = string.Format(" out of a maximum amount of {0}", i_MaxValue);
            }
            else
            {
                i_MaxValue = float.MaxValue;
            }

            Console.WriteLine(string.Format("Please enter the amount of {0}{1} (Non Negative):", i_NameOfObjectString, maxValueString));
            string floatInput = Console.ReadLine();
            float floatToReturn;

            try
            {
                while (!float.TryParse(floatInput, out floatToReturn) || !isInRange(floatToReturn, 0, i_MaxValue))
                {
                    if (!float.TryParse(floatInput, out floatToReturn))
                    {
                        throw new FormatException(k_FloatFormatExceptionMessage);
                    }
                    else
                    {
                        Console.WriteLine(k_InvalidNumberMessage);
                        floatInput = Console.ReadLine();
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                floatToReturn = floatInputValidation(i_NameOfObjectString, i_MaxValue);
            }

            return floatToReturn;
        }

        private eVehicleType vehicleTypeInput()
        {
            eVehicleType vehicleType;

            Console.WriteLine(string.Format(@"Please choose one of the following options:
1. Car
2. Motorcycle
3. Truck"));

            string vehicleTypeInput = Console.ReadLine();

            while (!Enum.TryParse(vehicleTypeInput, out vehicleType) ||
                   !Enum.IsDefined(typeof(eVehicleType), vehicleType))
            {
                Console.WriteLine("Invalid vehicle type entered, Please try again:");
                vehicleTypeInput = Console.ReadLine();
            }

            return vehicleType;
        }

        private eEngineType engineTypeInput()
        {
            eEngineType engineType;

            Console.WriteLine(string.Format(@"Please choose one of the following options:
1. Fuel
2. Electric"));

            string engineTypeInput = Console.ReadLine();

            while (!Enum.TryParse(engineTypeInput, out engineType) ||
                   !Enum.IsDefined(typeof(eEngineType), engineType))
            {
                Console.WriteLine("Invalid engine type entered, Please try again:");
                engineTypeInput = Console.ReadLine();
            }

            return engineType;
        }

        private string energySourceForMessage(eEngineType i_EngineType)
        {
            string energySource = string.Empty;

            switch (i_EngineType)
            {
                case eEngineType.FuelBased:
                    {
                        energySource = k_FuelMessage;
                        break;
                    }

                case eEngineType.ElectricBased:
                    {
                        energySource = k_BatteryEnergyMessage;
                        break;
                    }

                default:
                    {
                        break;
                    }
            }

            return energySource;
        }

        private ePaintJobColor colorInput()
        {
            ePaintJobColor carColor;

            Console.WriteLine(string.Format(@"Please enter a color from the following options:
1. Red
2. White
3. Black
4. Blue"));

            string carColorInput = Console.ReadLine();
            while (!Enum.TryParse(carColorInput, out carColor) ||
                   !Enum.IsDefined(typeof(ePaintJobColor), carColor))
            {
                Console.WriteLine("Invalid car color, Please try again:");
                carColorInput = Console.ReadLine();
            }

            return carColor;
        }

        private eNumberOfDoors numberOfDoorsInput()
        {
            eNumberOfDoors numberOfDoors;

            Console.WriteLine(string.Format(@"Please enter the number of doors from the following options:
2
3
4
5"));

            string numberOfDoorsInput = Console.ReadLine();

            while (!Enum.TryParse(numberOfDoorsInput, out numberOfDoors) ||
                   !Enum.IsDefined(typeof(eNumberOfDoors), numberOfDoors))
            {
                Console.WriteLine("Invalid number of doors, Please try again:");
                numberOfDoorsInput = Console.ReadLine();
            }

            return numberOfDoors;
        }

        private eLicenseType licenseTypeInput()
        {
            eLicenseType licenseType;

            Console.WriteLine(string.Format(@"Please enter the license type from the following options:
1. A
2. A2
3. AA
4. B"));

            string licenseTypeInput = Console.ReadLine();

            while (!Enum.TryParse(licenseTypeInput, out licenseType) ||
                   !Enum.IsDefined(typeof(eLicenseType), licenseType))
            {
                Console.WriteLine("Invalid license type, Please try again:");
                licenseTypeInput = Console.ReadLine();
            }

            return licenseType;
        }

        private int motorcyclEngineVolumeInput()
        {
            Console.WriteLine("Please enter engine volume (Non negative):");
            string engineVolumeInput = Console.ReadLine();
            int engineVolume;

            try
            {
                while (!int.TryParse(engineVolumeInput, out engineVolume) || !isInRange(engineVolume, 0, int.MaxValue))
                {
                    if (!int.TryParse(engineVolumeInput, out engineVolume))
                    {
                        throw new FormatException("Invalid engine volume entered");
                    }
                    else
                    {
                        Console.WriteLine("Invalid engine volume entered, Please try again:");
                        engineVolumeInput = Console.ReadLine();
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                engineVolume = motorcyclEngineVolumeInput();
            }

            return engineVolume;
        }

        private bool IsCargoRefrigirated()
        {
            Console.WriteLine(string.Format(@"Does the truck have a refrigrated cargo?
1. Yes
2. No"));
            string IsCargoRefrigiratedInput = Console.ReadLine();

            while (IsCargoRefrigiratedInput != "1" && IsCargoRefrigiratedInput != "2")
            {
                Console.WriteLine(string.Format(@"Invalid input, Please pick one of the following options:
1. Yes
2. No"));
                IsCargoRefrigiratedInput = Console.ReadLine();
            }

            return IsCargoRefrigiratedInput == "1";
        }

        private bool isInRange(float i_Number, float i_MinInRange, float i_MaxInRange)
        {
            return i_Number >= i_MinInRange && i_Number <= i_MaxInRange;
        }
    }
}

