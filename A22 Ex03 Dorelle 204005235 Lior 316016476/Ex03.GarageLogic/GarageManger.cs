using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private readonly Dictionary<string, Vehicle> r_GargeVehiclesDicitonary;
        public GarageManager()
        {
            r_GargeVehiclesDicitonary = new Dictionary<string, Vehicle>();
        }

        public bool DoesVehicleExist(string i_LicenseNumber)
        {
            if (i_LicenseNumber != null)
            {
                return !string.IsNullOrEmpty(i_LicenseNumber) && r_GargeVehiclesDicitonary.ContainsKey(i_LicenseNumber);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void AddVehicle(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            if (i_Vehicle != null)
            {
                i_Vehicle.OwnerName = i_OwnerName;
                i_Vehicle.OwnerPhoneNumber = i_OwnerPhoneNumber;
                r_GargeVehiclesDicitonary.Add(i_Vehicle.LicenseNumber, i_Vehicle);
            }
            else
            {
                throw new ArgumentNullException("Vehicle Provided is empty");
            }
        }

        public string GetVehicleLicneseNumberByStatus(eRepairStatus i_RepairStatus)
        {
            StringBuilder LicenseNumberStringBuilder = new StringBuilder(string.Format(@"Vehicles With Status:
{0} {1}", i_RepairStatus, Environment.NewLine));
            List<string> VehiclesWithRepairStatus = r_GargeVehiclesDicitonary.Values.
                Where(vehicle => vehicle.RepairStatus == i_RepairStatus)
                .Select(vehicle => vehicle.LicenseNumber).ToList();

            LicenseNumberStringBuilder.Append(string.Format("{0}{1}", string.Join(Environment.NewLine, VehiclesWithRepairStatus), Environment.NewLine));
            return LicenseNumberStringBuilder.ToString();
        }

        public float GetEnergyPrecnetage(string i_LicenseNumber)
        {
            return r_GargeVehiclesDicitonary[i_LicenseNumber].EnergyLevel;
        }
        public float GetMaxAmountOfEnergyLevel(string i_LicenseNumber)
        {
            return r_GargeVehiclesDicitonary[i_LicenseNumber].MaxEngineFuelCapcity();
        }

        public string GetVehicleInfoString(string i_LicenseNumber)
        {
            return r_GargeVehiclesDicitonary[i_LicenseNumber].ToString();
        }
        public void ChangeVehicleRepairStatus(string i_LicenseNumber, eRepairStatus i_RepairStatus)
        {
            r_GargeVehiclesDicitonary[i_LicenseNumber].RepairStatus = i_RepairStatus;
        }

        public void InflateTiresToMaxPressure(string i_LicenseNumber)
        {
            r_GargeVehiclesDicitonary[i_LicenseNumber].InflateTireToMaxPressure();
        }

        public void FuelVehicle(string i_LicenseNumber , eFuelType i_FuelType, float i_FuelAmount)
        {
            r_GargeVehiclesDicitonary[i_LicenseNumber].Fuel(i_FuelAmount, i_FuelType);
        }

        public void RechargeVehicle(string i_LicenseNumber, float i_HoursAmountToRecharge)
        {
            r_GargeVehiclesDicitonary[i_LicenseNumber].Recharge(i_HoursAmountToRecharge);
        }

    }
}
