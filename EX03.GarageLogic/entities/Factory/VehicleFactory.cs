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

        public static string[] GetVehiclesTypesAsArray()
        {
            return Enum.GetNames(typeof(eVehicleTypes));
        }

        private static eVehicleTypes getVehicleType(int i_VehicleType)
        {
            eVehicleTypes enumValue;
            
            try
            {
                enumValue = (eVehicleTypes)Enum.ToObject(typeof(eVehicleTypes), i_VehicleType);
            }
            catch (Exception)
            {
                throw new ArgumentException("Vehicle type doesn't exist in our system.");
            }

            return enumValue;
        }

        public static List<string> GetSpecificVehicleQuestions(int i_VehicleType)
        {
            List<string> questionsList = new List<string>();

            eVehicleTypes enumValue = getVehicleType(i_VehicleType);

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

        private static void addTruckQuestions(ref List<string> io_QuestionsList)
        {
            io_QuestionsList.Add("Does the truck loads hazard materials?\n 1. Yes.\n2. No. ");
            
            io_QuestionsList.Add("What is the cargo capacity? ");
        }

        private static void addGenericMotorcycleQuestions(ref List<string> io_QuestionsList)
        {
            io_QuestionsList.Add("What is the engine capacity of the motorcycle?");
            
            string[] licenseTypes = Enum.GetNames(typeof(Motorcycle.eLicenseType));
            io_QuestionsList.Add(buildOptionQuestion("What is the license type of the motorcycle?", 
                licenseTypes));
        }

        private static string buildOptionQuestion(string i_Question, string[] i_Options)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(i_Question);
            
            for(int i = 1; i <= i_Options.Length; i++)
            {
                stringBuilder.Append(i + ". " + i_Options[i - 1]);
            }

            return stringBuilder.ToString();
        }

        private static void addGenericCarQuestions(ref List<string> io_QuestionsList)
        {
            string[] carColors = Enum.GetNames(typeof(eColor));
            io_QuestionsList.Add(buildOptionQuestion("What is the color of the car?",
                carColors));
            
            string[] numOfDoorsOptions = Enum.GetNames(typeof(eNumOfDoors));
            io_QuestionsList.Add(buildOptionQuestion("How many doors do the car have?",
                numOfDoorsOptions));
        }

        public static Vehicle CreateVehicle(string i_LicenseNumber, int i_VehicleType, 
            string i_ModelName, float i_RemainingEnergyInEnergySource, 
                Wheel i_WheelModel, List<object> i_ClientAnswers)
        {
            Vehicle newVehicle;
            
            eVehicleTypes vehicleType = getVehicleType(i_VehicleType);

            switch(vehicleType)
            {
                case eVehicleTypes.ElectricCar:
                    eColor electricCarColor = getVehicleColor(i_ClientAnswers[0]);
                    eNumOfDoors electricCarNumOfDoors = getVehicleNumOfDoorsAsEnum(i_ClientAnswers[1]);
                    newVehicle = new ElectricCar(i_LicenseNumber, electricCarColor, 
                        electricCarNumOfDoors, i_RemainingEnergyInEnergySource);
                    break;
                case eVehicleTypes.FuelCar:
                    eColor fuelCarColor = getVehicleColor(i_ClientAnswers[0]);
                    eNumOfDoors fuelCarNumOfDoors = getVehicleNumOfDoorsAsEnum(i_ClientAnswers[1]);
                    newVehicle = new FuelCar(i_LicenseNumber, fuelCarColor, 
                        fuelCarNumOfDoors, i_RemainingEnergyInEnergySource);
                    break;
                case eVehicleTypes.ElectricMotorcycle:
                    Motorcycle.eLicenseType eMLicenseType = getMotorcycleLicenseType(i_ClientAnswers[1]);
                    int eMEngineCapacity = GetMotorcycleEngineCapacity(i_ClientAnswers[0]);
                    newVehicle = new ElectricMotorcycle(i_LicenseNumber, eMEngineCapacity,
                        eMLicenseType, i_RemainingEnergyInEnergySource);
                    break;
                case eVehicleTypes.FuelMotorcycle:
                    Motorcycle.eLicenseType eFLicenseType = getMotorcycleLicenseType(i_ClientAnswers[1]);
                    int eFEngineCapacity = GetMotorcycleEngineCapacity(i_ClientAnswers[0]);
                    newVehicle = new FuelMotorcycle(i_LicenseNumber, eFEngineCapacity,
                        eFLicenseType, i_RemainingEnergyInEnergySource);
                    break;
                case eVehicleTypes.Truck:
                    bool isLoadHazardousMaterials = getIsHavingHazardMaterials(i_ClientAnswers[0]);
                    float loadAmount = getLoadingAmount(i_ClientAnswers[1]);
                    newVehicle = new Truck(i_LicenseNumber, i_RemainingEnergyInEnergySource, 
                        isLoadHazardousMaterials, loadAmount);
                    break;
                default:
                    throw new ArgumentException("Vehicle type doesn't exists in our system.");
            }

            newVehicle.UpdateWheels(i_WheelModel);
            newVehicle.UpdateModel(i_ModelName);

            return newVehicle;
        }

        private static float getLoadingAmount(object i_ClientAnswer)
        {
            try
            {
                return (float)i_ClientAnswer;
            }
            catch (Exception)
            {
                throw new ArgumentException("Please enter valid Loading amount.");
            }
        }

        private static bool getIsHavingHazardMaterials(object i_ClientAnswer)
        {
            try
            {
                int choice = (int)i_ClientAnswer;

                if(choice == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Answer to is truck loaded with hazard materials isn't valid.");
            }
        }

        private static int GetMotorcycleEngineCapacity(object i_EngineCapacity)
        {
            try
            {
                return (int)i_EngineCapacity;
            }
            catch (Exception)
            {
                throw new ArgumentException("Please enter valid engine capacity.");
            }
        }
        
        private static Motorcycle.eLicenseType getMotorcycleLicenseType(object i_ClientAnswer)
        {
            int licenseChosen = (int)i_ClientAnswer;

            Motorcycle.eLicenseType enumValue;
        
            try
            {
                enumValue = (Motorcycle.eLicenseType)Enum.ToObject(typeof(Motorcycle.eLicenseType), i_ClientAnswer);
            }
            catch (Exception)
            {
                throw new ArgumentException("Motorcycle license doesn't supported by our system.");
            }

            return enumValue;
        }

        private static eNumOfDoors getVehicleNumOfDoorsAsEnum(object i_ClientAnswer)
        {
            int numOfDoors = (int)i_ClientAnswer;

            eNumOfDoors enumValue;
        
            try
            {
                enumValue = (eNumOfDoors)Enum.ToObject(typeof(eNumOfDoors), numOfDoors);
            }
            catch (Exception)
            {
                throw new ArgumentException("Number of doors doesn't supported by our system.");
            }

            return enumValue;
        }

        private static eColor getVehicleColor(object i_ClientAnswer)
        {
            int vehicleColorValue = (int)i_ClientAnswer;

            eColor enumValue;
        
            try
            {
                enumValue = (eColor)Enum.ToObject(typeof(eColor), vehicleColorValue);
            }
            catch (Exception)
            {
                throw new ArgumentException("Car color doesn't exist in our system.");
            }

            return enumValue;
        }
    }
}
