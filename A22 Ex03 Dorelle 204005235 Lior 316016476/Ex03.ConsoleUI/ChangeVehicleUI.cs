using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class ChangeVehicleUI
    {
        private enum eChangeVehicleMenuOptions
        {
            ChangeStatus = 1,
            InflateTires = 2,
            Refuel = 3,
            Recharge = 4,
            MainMenu = 5,
        }

        private const string k_FuelMessage = "Fuel capacity (Liters)";
        private const string k_BatteryEnergyMessage = "Hours";
        private readonly GarageManager r_GarageManager;
        private readonly VehicleInfoUI r_InfoVehicleUi;

        internal ChangeVehicleUI(GarageManager i_GarageManager, VehicleInfoUI i_VehicleInfoUI)
        {
            r_GarageManager = i_GarageManager;
            r_InfoVehicleUi = i_VehicleInfoUI;
        }

        internal void ChangeVehicleMenu()
        {
            bool isChangeMenu = true;
            string licenseNumber;

            while (isChangeMenu)
            {
                Console.WriteLine(string.Format("Change Vehicle Information menu:{0}", Environment.NewLine));

                eChangeVehicleMenuOptions changeOptions = changeVehicleInfoMenu();

                switch (changeOptions)
                {
                    case eChangeVehicleMenuOptions.ChangeStatus:
                        {
                            try
                            {
                                licenseNumber = r_InfoVehicleUi.VehicleLicenseNumberInput();
                                eRepairStatus repairStatus = r_InfoVehicleUi.VehicleRepairStatusInput();
                                r_GarageManager.ChangeVehicleRepairStatus(licenseNumber, repairStatus);
                            }
                            catch (ArgumentNullException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (KeyNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;
                        }

                    case eChangeVehicleMenuOptions.InflateTires:
                        {
                            try
                            {
                                licenseNumber = r_InfoVehicleUi.VehicleLicenseNumberInput();
                                r_GarageManager.InflateTiresToMaxPressure(licenseNumber);
                            }
                            catch (ArgumentNullException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (ValueOutOfRangeException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (KeyNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;
                        }

                    case eChangeVehicleMenuOptions.Refuel:
                        {
                            try
                            {
                                licenseNumber = r_InfoVehicleUi.VehicleLicenseNumberInput();
                                eFuelType fuelType = fuelTypeInput();

                                float currentFillPercent = r_GarageManager.GetEnergyPrecnetage(licenseNumber);
                                float maxFillAmount = r_GarageManager.GetMaxAmountOfEnergyLevel(licenseNumber);
                                float amountOfEnergyToFill = amountOfEnergyToAddInput(k_FuelMessage, currentFillPercent, maxFillAmount);
                                r_GarageManager.FuelVehicle(licenseNumber, fuelType, amountOfEnergyToFill);
                            }
                            catch (ArgumentNullException ex)
                            {
                                Console.WriteLine(ex.Message);
                                throw;
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (ValueOutOfRangeException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (KeyNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;
                        }

                    case eChangeVehicleMenuOptions.Recharge:
                        {
                            try
                            {
                                licenseNumber = r_InfoVehicleUi.VehicleLicenseNumberInput();
                                float currentFillPercent = r_GarageManager.GetEnergyPrecnetage(licenseNumber);
                                float maxFillAmount = r_GarageManager.GetMaxAmountOfEnergyLevel(licenseNumber);

                                float amountOfEnergyToFill = amountOfEnergyToAddInput(k_BatteryEnergyMessage, currentFillPercent, maxFillAmount);
                                r_GarageManager.RechargeVehicle(licenseNumber, amountOfEnergyToFill / 60f);
                            }
                            catch (ArgumentNullException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (KeyNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (ValueOutOfRangeException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;
                        }

                    case eChangeVehicleMenuOptions.MainMenu:
                        {
                            isChangeMenu = false;
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
        }

        private eChangeVehicleMenuOptions changeVehicleInfoMenu()
        {
            eChangeVehicleMenuOptions changeOption;

            Console.WriteLine(string.Format(@"Please choose one of the following options:
1. Change Repair status
2. Inflate tires
3. Refuel engine
4. Recharge electric engine
5. Return to main menu"));

            string optionInput = Console.ReadLine();

            while (!Enum.TryParse(optionInput, out changeOption) ||
                   !Enum.IsDefined(typeof(eChangeVehicleMenuOptions), changeOption))
            {
                Console.WriteLine(string.Format(@"Invalid option, Please try again:
1. Change Repair status
2. Inflate tires
3. Refuel engine
4. Recharge electric engine
5. Return to main menu"));

                optionInput = Console.ReadLine();
            }

            return changeOption;
        }

        private eFuelType fuelTypeInput()
        {
            eFuelType fuelType;

            Console.WriteLine(string.Format(@"Please choose one of the below fuel types:
1. Octane98
2. Octane96
3. Octane95
4. Soler"));

            string fuelTypeInput = Console.ReadLine();

            while (!Enum.TryParse(fuelTypeInput, out fuelType) ||
                   !Enum.IsDefined(typeof(eFuelType), fuelType))
            {
                Console.WriteLine(string.Format(@"Invalid option fuel type, Please try again:
1. Octane98
2. Octane96
3. Octane95
4. Soler"));

                fuelTypeInput = Console.ReadLine();
            }

            return fuelType;
        }

        private float amountOfEnergyToAddInput(string i_ObjectToAddName, float i_CurrentFillPercent, float i_MaxAmount)
        {
            Console.WriteLine(string.Format("Current {0} percent: {1}%, Max {0} amount: {2}", i_ObjectToAddName, i_CurrentFillPercent, i_MaxAmount));

            if (i_ObjectToAddName == k_BatteryEnergyMessage)
            {
                i_ObjectToAddName = "minutes";
            }

            Console.WriteLine(string.Format("Please enter {0} to add:", i_ObjectToAddName));

            string amountInput = Console.ReadLine();
            float amountToAdd;

            try
            {
                if (!float.TryParse(amountInput, out amountToAdd))
                {
                    throw new FormatException(string.Format("Invalid amount of {0} entered, Please try again:", i_ObjectToAddName));
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                amountToAdd = amountOfEnergyToAddInput(i_ObjectToAddName, i_CurrentFillPercent, i_MaxAmount);
            }

            return amountToAdd;
        }
    }
}