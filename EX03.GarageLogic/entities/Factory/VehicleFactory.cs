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

        private static readonly List<KeyValuePair<Type, string>> sr_DataMembersInfo;

        public static List<KeyValuePair<Type, string>> GeneralQuestions
        {
            get
            {
                return sr_DataMembersInfo;
            }
        }
        
        static VehicleFactory()
        {
            sr_DataMembersInfo = new List<KeyValuePair<Type, string>>();
            initDataMembersInfo();
        }

        private static void initDataMembersInfo()
        {
            sr_DataMembersInfo.Add(new KeyValuePair<Type, string>(typeof(float), "Vehicle current energy amount: "));
            sr_DataMembersInfo.Add(new KeyValuePair<Type, string>(typeof(float), "What is your current air pressure in wheel number [number]? "));
        }

        public static string[] GetVehiclesTypesAsArray()
        {
            return Enum.GetNames(typeof(eVehicleTypes));
        }

        public static List<KeyValuePair<Type, string>> GetSpecificVehicleQuestions(string i_VehicleType)
        {
            List<KeyValuePair<Type, string>> questionsList = new List<KeyValuePair<Type, string>>();
            
            questionsList.Add(new KeyValuePair<Type, string>(typeof(float), 
                "How much air pressure does your tiers have? "));

            eVehicleTypes enumValue = (eVehicleTypes)Enum.ToObject(typeof(eVehicleTypes), i_VehicleType);

            switch(enumValue)
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
                    addTruckQuestions(ref questionsList);
                    break;
                default:
                    throw new ArgumentException("This type of car doesnt exist.");
            }

            return questionsList;
        }

        private static void addTruckQuestions(ref List<KeyValuePair<Type, string>> io_QuestionsList)
        {
            io_QuestionsList.Add(new KeyValuePair<Type, string>(typeof(bool), 
                "Does the truck loads hazard materials? "));
            
            io_QuestionsList.Add(new KeyValuePair<Type, string>(typeof(float), 
                "What is the cargo capacity? "));
        }

        private static void addGenericMotorcycleQuestions(ref List<KeyValuePair<Type, string>> io_QuestionsList)
        {
            string[] licenseTypes = Enum.GetNames(typeof(Motorcycle.eLicenseType));
            io_QuestionsList.Add(new KeyValuePair<Type, string>(typeof(Motorcycle.eLicenseType), 
                "What is the license type of the motorcycle? " + licenseTypes));
            
            string[] numOfDoorsOptions = Enum.GetNames(typeof(eNumOfDoors));
            io_QuestionsList.Add(new KeyValuePair<Type, string>(typeof(int), 
                "What is the engine capacity of the motorcycle? "));
        }

        private static void addGenericCarQuestions(ref List<KeyValuePair<Type, string>> io_QuestionsList)
        {
            string[] carColors = Enum.GetNames(typeof(eColor));
            io_QuestionsList.Add(new KeyValuePair<Type, string>(typeof(eColor), 
                "What is the color of the car? " + carColors.ToString()));
            
            string[] numOfDoorsOptions = Enum.GetNames(typeof(eNumOfDoors));
            io_QuestionsList.Add(new KeyValuePair<Type, string>(typeof(eNumOfDoors), 
                "How many doors do the car have? " + numOfDoorsOptions.ToString()));
        }

        public static bool isVehicleTypeExist(string i_VehicleType)
        {
            eVehicleTypes enumValue;
            try
            {
                return Enum.TryParse(i_VehicleType, out enumValue);
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public static Vehicle CreateVehicle(eVehicleTypes i_VehicleType, List<Object> i_Params)
        {
            // TODO - Switch(i_VehicleType).
            // TODO - same case for *both* car types.
            // TODO - same case for *both* motorcycle types.
            // TODO - case for truck car type.
            return null;
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
