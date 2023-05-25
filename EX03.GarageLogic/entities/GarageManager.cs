﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic.entities;
using EX03.GarageLogic.entities.EnergySourceTypes;
using EX03.GarageLogic.entities.Factory;

namespace EX03.GarageLogic
{
    public class GarageManager
    {
        private readonly Dictionary<string, GarageForm> r_GarageForms = new Dictionary<string, GarageForm>();

        public void InsertVehicleToGarage(string i_LicenseNumber, Vehicle i_Vehicle, GarageClient i_Client)
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
        
        public string[] ShowAllLicensesWithFilterOption(int i_ServiceStatus) //returns only arrays after filtering.
        {
           List<string> licenseArray = new List<string>();
           GarageForm.eServiceStatus serviceStatus = (GarageForm.eServiceStatus)Enum.
               ToObject(typeof(GarageForm.eServiceStatus), i_ServiceStatus);

           foreach(KeyValuePair<string, GarageForm> form in r_GarageForms)
           {
               if(form.Value.ServiceStatus == serviceStatus)
               {
                   licenseArray.Add(form.Value.Vehicle.LicenseNumber);
               }
           }

           return licenseArray.ToArray();
        }
        
        public string[] GetSupportedVehiclesTypes()
        {
            return VehicleFactory.GetVehiclesTypesAsArray();
        }

        public bool IsVehicleTypeExist(string i_VehicleType)
        {
            VehicleFactory.eVehicleTypes enumValue;
            return Enum.TryParse(i_VehicleType, out enumValue);
        }

        public List<KeyValuePair<Type, string>> GetClientVehicleForm(string i_eVehicleType)
        {
            return VehicleFactory.GetSpecificVehicleQuestions(i_eVehicleType);
        }
    }
    
}
