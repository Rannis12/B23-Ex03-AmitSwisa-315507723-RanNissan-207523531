using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic.entities;
using EX03.GarageLogic.entities.EnergySourceTypes;

namespace EX03.GarageLogic
{
    class GarageManager
    {
        private readonly Dictionary<string, GarageForm> r_GarageForms = new Dictionary<string, GarageForm>();

        public void InsertVehicleToGarage(string i_LicenseNumber, Vehicle i_Vehicle, Client i_Client)
        {
            GarageForm garageForm = new GarageForm(i_Vehicle, i_Client);
            r_GarageForms.Add(i_LicenseNumber, garageForm);
        }

        public bool IsVehicleExistsInTheGarage(string i_LicenseNumber)
        {
            return r_GarageForms.ContainsKey(i_LicenseNumber);
        }

        public void ChangeVehicleStatusInTheGarage(string i_LicenseNumber, GarageForm.eServiceStatus i_eServiceStatus)
        {
            //if its the same status - throw exception.
            //else
            r_GarageForms[i_LicenseNumber].ServiceStatus = i_eServiceStatus;
        }

        public void RefuelRegularVehicle(string i_LicenseNumber, FuelEnergy.eFuelType i_eFuelType, float i_AmountToFill)
        {
            //r_GarageForms[i_LicenseNumber].Vehicle.
        }

        public void RefuelElectricVehicle(string i_LicenseNumber, float i_AmountToFill)
        {
            //r_GarageForms[i_LicenseNumber].Vehicle.
        }


        /*
        public VehicleDetails GetVehicleDetails(string i_LicenseNumber)
        {

        } */
    }
}
