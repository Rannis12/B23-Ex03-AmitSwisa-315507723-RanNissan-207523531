using EX03.GarageLogic.entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static EX03.GarageLogic.entities.VehicleModels.Car;

namespace EX03.GarageLogic.entities.Factory
{
    public static class VehicleFactory
    {
        public enum eVehicleTypes
        {
            ElectricCar = 1,
            ElectricMotorcycle = 2,
            FuelCar = 3,
            FuelMotorcycle = 4,
            Truck = 5
        }

        private readonly static List<KeyValuePair<Type, string>> s_DataMembersInfo;

        public static List<KeyValuePair<Type, string>> GeneralQuestions
        {
            get
            {
                return s_DataMembersInfo;
            }
        }
        
        static VehicleFactory()
        {
            s_DataMembersInfo = new List<KeyValuePair<Type, string>>();
            initDataMembersInfo();
        }

        private static void initDataMembersInfo()
        {
            s_DataMembersInfo.Add(new KeyValuePair<Type, string>(typeof(string), "License number: "));
            s_DataMembersInfo.Add(new KeyValuePair<Type, string>(typeof(int), "Choose vehicle type: "));
            s_DataMembersInfo.Add(new KeyValuePair<Type, string>(typeof(float), "Vehicle current energy amount: "));
            s_DataMembersInfo.Add(new KeyValuePair<Type, string>(typeof(float), "What is your current air pressure in wheel number [number]? "));
        }

        public static string[] GetVehiclesTypesAsArray()
        {
            return Enum.GetNames(typeof(eVehicleTypes));
        }

        //public static Vehicle BuildElectricCar(string i_LicenseNumber, eColor i_Color, 
        //        eNumOfDoors i_NumOfDoors)
        //{/*
        //    public ElectricCar(string i_LicenseNumber, eColor i_Color, eNumOfDoors i_NumOfDoors,
        //         float i_CurrentBatteryTimeLeft) : base(i_LicenseNumber, i_Color, i_NumOfDoors)*/
        //    return new Vehicle();
        //}

    }
}
