using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class VehicleInfoUI
    {
        private enum eVehicleInfoMenuOptions
        {
            GetVehicleInfo = 1,
            GetAllVehiclesLicensesWithRepairStatus = 2,
            GetAllVehiclesLicenses = 3,
            MainMenu = 4,
        }

        private readonly GarageManager r_GarageManager;

        internal VehicleInfoUI(GarageManager i_GarageManager)
        {
            r_GarageManager = i_GarageManager;
        }

        internal void VehicleInfoMenu()
        {
            Console.WriteLine(string.Format(@"{0}Info menu:{1}", Environment.NewLine, Environment.NewLine));

            eVehicleInfoMenuOptions infoOption = vehicleInfoOptionInput();

            switch (infoOption)
            {
                case eVehicleInfoMenuOptions.GetVehicleInfo:
                    {
                        try
                        {
                            string licenseNumber = VehicleLicenseNumberInput();
                            Console.WriteLine(r_GarageManager.GetVehicleInfoString(licenseNumber).ToString());
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

                case eVehicleInfoMenuOptions.GetAllVehiclesLicensesWithRepairStatus:
                    {
                        eRepairStatus repairStatus = VehicleRepairStatusInput();

                        string vehiclesWithRepairStatusString = r_GarageManager.GetVehicleLicneseNumberByStatus(repairStatus);

                        Console.WriteLine(vehiclesWithRepairStatusString);

                        break;
                    }

                case eVehicleInfoMenuOptions.GetAllVehiclesLicenses:
                    {
                        StringBuilder allVehiclesLicensesStringBuilder = new StringBuilder();

                        foreach (eRepairStatus repairStatus in Enum.GetValues(typeof(eRepairStatus)))
                        {
                            string vehiclesWithRepairStatusString = r_GarageManager.GetVehicleLicneseNumberByStatus(repairStatus);
                            allVehiclesLicensesStringBuilder.Append(vehiclesWithRepairStatusString);
                        }

                        Console.WriteLine(allVehiclesLicensesStringBuilder.ToString());
                        break;
                    }

                case eVehicleInfoMenuOptions.MainMenu:
                    {
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        internal eRepairStatus VehicleRepairStatusInput()
        {
            eRepairStatus repairStatus;
            Console.WriteLine(string.Format(
                @"{0}Please choose one of the following options:
1. In Repair
2. Repaired
3. Paid For", Environment.NewLine));

            string repairStatusInput = Console.ReadLine();

            while (!Enum.TryParse(repairStatusInput, out repairStatus) ||
                   !Enum.IsDefined(typeof(eRepairStatus), repairStatus))
            {
                Console.WriteLine(string.Format(
                    @"{0}Invalid status entered, Please try again:
1. In Repair
2. Repaired
3. Paid For",Environment.NewLine));

                repairStatusInput = Console.ReadLine();
            }

            return repairStatus;
        }

        internal string VehicleLicenseNumberInput()
        {
            Console.WriteLine("Please enter the vehicle's license number:");

            string vehicleLicenseNumberInput = Console.ReadLine();

            while (!r_GarageManager.DoesVehicleExist(vehicleLicenseNumberInput))
            {
                Console.WriteLine("The license number entered does not exists in the garage, Please try again:");
                vehicleLicenseNumberInput = Console.ReadLine();
            }

            return vehicleLicenseNumberInput;
        }

        private eVehicleInfoMenuOptions vehicleInfoOptionInput()
        {
            eVehicleInfoMenuOptions vehicleInfoOption;

            Console.WriteLine(string.Format(
                @"{0}Please select one of the following options:
1. Get information about a vehicle.
2. Get all vehicle licenses with a repair status in the garage.
3. Get all vehicle licenses in the garage.
4. Return to main menu.",Environment.NewLine));

            string optionInput = Console.ReadLine();

            while (!Enum.TryParse(optionInput, out vehicleInfoOption) ||
                   !Enum.IsDefined(typeof(eVehicleInfoMenuOptions), vehicleInfoOption))
            {
                Console.WriteLine(string.Format(
                    @"{0}Invalid option entered, Please try again:
1. Get information about a vehicle.
2. Get all vehicle licenses with a repair status in the garage.
3. Get all vehicle licenses in the garage.
4. Return to main menu.", Environment.NewLine));

                optionInput = Console.ReadLine();
            }

            return vehicleInfoOption;
        }
    }
}