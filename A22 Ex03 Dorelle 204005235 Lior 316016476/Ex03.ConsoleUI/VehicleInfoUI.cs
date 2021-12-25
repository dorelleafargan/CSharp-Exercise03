using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class VehicleInfoUI
    {
        private enum eVehicleInfoOptions
        {
            GetVehicleInfo = 1,
            GetAllVehiclesLicensesWithRepairStatus = 2,
            GetAllVehiclesLicenses = 3,
            MainMenu = 4
        }

        private readonly GarageManager r_GarageManager;

        public VehicleInfoUI(GarageManager i_GarageManager)
        {
            r_GarageManager = i_GarageManager;
        }

        public void VehicleInfoMenu()
        {
            Console.WriteLine(string.Format("{0}Info menu:{1}", Environment.NewLine, Environment.NewLine));

            eVehicleInfoOptions infoOption = vehicleInfoOptionInput();

            switch (infoOption)
            {
                case eVehicleInfoOptions.GetVehicleInfo:
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

                case eVehicleInfoOptions.GetAllVehiclesLicensesWithRepairStatus:
                    {
                        eRepairStatus repairStatus = GetVehicleRepairStatusFromInput();

                        string vehiclesWithRepairStatusString = r_GarageManager.GetVehicleLicneseNumberByStatus(repairStatus);

                        Console.WriteLine(vehiclesWithRepairStatusString);

                        break;
                    }

                case eVehicleInfoOptions.GetAllVehiclesLicenses:
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

                case eVehicleInfoOptions.MainMenu:
                    {
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        public eRepairStatus GetVehicleRepairStatusFromInput()
        {
            eRepairStatus repairStatus;
            Console.WriteLine(string.Format(@"Please choose one of the following options:
1. In Repair
2. Repaired
3. Paid For"));

            string repairStatusInput = Console.ReadLine();

            while (!Enum.TryParse(repairStatusInput, out repairStatus) ||
                   !Enum.IsDefined(typeof(eRepairStatus), repairStatus))
            {
                Console.WriteLine(string.Format(@"Invalid status entered, Please try again:
1. In Repair
2. Repaired
3. Paid For"));

                repairStatusInput = Console.ReadLine();
            }

            return repairStatus;
        }

        public string VehicleLicenseNumberInput()
        {
            Console.WriteLine("Please enter the vehicle's license number");

            string vehicleLicenseNumberInput = Console.ReadLine();

            while (!r_GarageManager.DoesVehicleExist(vehicleLicenseNumberInput))
            {
                Console.WriteLine("The license number entered does not exists in the garage, Please try again:");
                vehicleLicenseNumberInput = Console.ReadLine();
            }

            return vehicleLicenseNumberInput;
        }

        private eVehicleInfoOptions vehicleInfoOptionInput()
        {
            eVehicleInfoOptions vehicleInfoOption;

            Console.WriteLine(string.Format(@"Please select one of the following options:
1. Get information about a vehicle.
2. Get all vehicle licenses with a repair status in the garage.
3. Get all vehicle licenses in the garage.
4. Return to main menu."));

            string optionInput = Console.ReadLine();

            while (!Enum.TryParse(optionInput, out vehicleInfoOption) ||
                   !Enum.IsDefined(typeof(eVehicleInfoOptions), vehicleInfoOption))
            {
                Console.WriteLine(string.Format(@"Invalid option entered, Please try again:
1. Get information about a vehicle.
2. Get all vehicle licenses with a repair status in the garage.
3. Get all vehicle licenses in the garage.
4. Return to main menu."));

                optionInput = Console.ReadLine();
            }

            return vehicleInfoOption;
        }
    }
}

