using Ex03.GarageLogic;
using System;

namespace Ex03.ConsoleUI
{
    internal class GarageHandler
    {
        private readonly VehicleCreator r_VehicleCreator;
        private readonly GarageManager r_GarageManager;
        private readonly InsertVehicleUI r_InsertVehicleUI;
        private readonly ChangeVehicleUI r_ChangeVehicleUI;
        private readonly VehicleInfoUI r_VehicleInfoUI;

        private enum eOptions
        {
            InsertVehicle = 1,
            ChangeVehicle = 2,
            VehicleInfo = 3,
        }

        public GarageHandler()
        {
            r_VehicleCreator = new VehicleCreator();
            r_GarageManager = new GarageManager();
            r_InsertVehicleUI = new InsertVehicleUI(r_VehicleCreator, r_GarageManager);
            r_VehicleInfoUI = new VehicleInfoUI(r_GarageManager);
            r_ChangeVehicleUI = new ChangeVehicleUI(r_GarageManager, r_VehicleInfoUI);
        }

        public void Run()
        {
            Console.WriteLine(string.Format("Welcome to Elayev-Afargan and Sons. {0}", Environment.NewLine));
            garageMenu();
        }

        private eOptions getInput()
        {
            eOptions garageOption;
            Console.WriteLine(string.Format(@"Choose one of the following options:
1. Enter a new Vehicle.
2. Change Vehicle Information.
3. Get vehicle information. (By license number)
"));
            string optionsInput = Console.ReadLine();
            while (!Enum.TryParse(optionsInput, out garageOption) || !Enum.IsDefined(typeof(eOptions), garageOption))
            {
                Console.WriteLine(string.Format(@"Invalid option, choose again from the following:
1. Enter a new Vehicle.
2. Change Vehicle Information.
3. Get vehicle information. (By license number)
"));
                optionsInput = Console.ReadLine();
            }

            return garageOption;
        }

        private void garageMenu()
        {
            while (true)
            {
                Console.WriteLine(string.Format("Main Menu:{0}", Environment.NewLine));
                eOptions garageOption = getInput();
                switch (garageOption)
                {
                    case eOptions.InsertVehicle:
                        {
                            r_InsertVehicleUI.InsertNewVehicle();
                            break;
                        }

                    case eOptions.ChangeVehicle:
                        {
                            r_ChangeVehicleUI.ChangeVehicleMenu();
                            break;
                        }

                    case eOptions.VehicleInfo:
                        {
                            r_VehicleInfoUI.VehicleInfoMenu();
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
        }

        private eOptions validOptionInput()
        {
            eOptions option;
            Console.WriteLine(string.Format(@"Please choose one of the following options:
1. Enter a new vehicle
2. Change a vehicle attribute
3. Get vehicle information"));

            string optioninput = Console.ReadLine();

            while (!Enum.TryParse(optioninput, out option) || !Enum.IsDefined(typeof(eOptions), option))
            {
                Console.WriteLine(string.Format(@"Invalid option, please try again:
1. Enter a new vehicle
2. Change a vehicle attribute
3. Get vehicle information"));
                optioninput = Console.ReadLine();

            }

            return option;
        }

    }
}
