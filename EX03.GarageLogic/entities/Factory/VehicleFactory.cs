using EX03.GarageLogic.entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic.entities.VehicleModels;
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

        public static List<KeyValuePair<Type, string>> GetSpecificVehicleQuestions(eVehicleTypes i_VehicleType)
        {
            List<KeyValuePair<Type, string>> questionsList = new List<KeyValuePair<Type, string>>(GeneralQuestions);

            switch(i_VehicleType)
            {
                case eVehicleTypes.ElectricCar:
                    addGenericCarQuestions(ref questionsList);
                    break;
                case eVehicleTypes.FuelCar:
                    addGenericCarQuestions(ref questionsList);
                    break;
                case eVehicleTypes.ElectricMotorcycle:
                    addGenericMotorcycleQuestions(ref questionsList);
                    break;
                case eVehicleTypes.FuelMotorcycle:
                    addGenericMotorcycleQuestions(ref questionsList);
                    break;
                case eVehicleTypes.Truck:
                    
                    break;
                default:
                    throw new ArgumentException("This type of car doesnt exist.");
            }

            return questionsList;
        }

        private static void addGenericMotorcycleQuestions(ref List<KeyValuePair<Type, string>> i_QuestionsList)
        {
            string[] licenseTypes = Enum.GetNames(typeof(Motorcycle.eLicenseType));
            i_QuestionsList.Add(new KeyValuePair<Type, string>(typeof(Motorcycle.eLicenseType), 
                "What is the license type of the motorcycle? " + licenseTypes));
            
            string[] numOfDoorsOptions = Enum.GetNames(typeof(eNumOfDoors));
            i_QuestionsList.Add(new KeyValuePair<Type, string>(typeof(int), 
                "What is the engine capacity of the motorcycle? "));
        }

        private static void addGenericCarQuestions(ref List<KeyValuePair<Type, string>> i_QuestionsList)
        {
            string[] carColors = Enum.GetNames(typeof(eColor));
            i_QuestionsList.Add(new KeyValuePair<Type, string>(typeof(eColor), 
                "What is the color of the car? " + carColors.ToString()));
            
            string[] numOfDoorsOptions = Enum.GetNames(typeof(eNumOfDoors));
            i_QuestionsList.Add(new KeyValuePair<Type, string>(typeof(eNumOfDoors), 
                "How many doors do the car have? " + numOfDoorsOptions.ToString()));
        }
        
        /*private static void addElectricCarQuestions(ref List<KeyValuePair<Type, string>> i_QuestionsList)
        {
            addGenericCarQuestions(ref i_QuestionsList);
        }*/

        //public static Vehicle BuildElectricCar(string i_LicenseNumber, eColor i_Color, 
        //        eNumOfDoors i_NumOfDoors)
        //{/*
        //    public ElectricCar(string i_LicenseNumber, eColor i_Color, eNumOfDoors i_NumOfDoors,
        //         float i_CurrentBatteryTimeLeft) : base(i_LicenseNumber, i_Color, i_NumOfDoors)*/
        //    return new Vehicle();
        //}

    }
}
