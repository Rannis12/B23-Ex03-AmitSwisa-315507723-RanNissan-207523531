using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic;
using EX03.GarageLogic.entities;
using EX03.GarageLogic.entities.EnergySourceTypes;
using EX03.GarageLogic.entities.Factory;
using EX03.GarageLogic.entities.VehicleModels;

namespace EX03.ConsoleUI
{
    class UserInterface
    {
        private GarageManager m_GarageManager;

        private enum eMenuOptions
        {
            InsertNewVehicleToTheGarage = 1,
            ShowAllLicenseNumbersInGarageWithFiltering,
            ChangeVehicleStatusInTheGarage,
            InflateVehicleWheelsToMaximum,
            RefuelRegularVehicle,
            RefuelElectricVehicle,
            ShowVehicleDetails,
            Exit
        }

        private void printMenu()
        {
            Console.WriteLine("Welcome to our Garage! Please select one of the following options:");
            Console.WriteLine("1. Insert A new vehicle to the garage: ");
            Console.WriteLine("2. Show list of all vehicle's license number in the garage: ");
            Console.WriteLine("3. Change vehicle current status in the garage: ");
            Console.WriteLine("4. Inflate vehicle's Wheels to maximum capacity: ");
            Console.WriteLine("5. Refuel vehicle: ");
            Console.WriteLine("6. Charge electric car: ");
            Console.WriteLine("7. Show vehicle's information: ");
            Console.WriteLine("8. Exit. ");
        }
        private void getUserSelectionFromMenu()
        {
            bool isValidSelectionFromMenu = false;
            int userSelection;

            do
            {
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out userSelection))
                {
                    if (userSelection < 1 || userSelection > 8)
                    {
                        Console.WriteLine("You entered an invalid selection. please enter a number between 1-7: ");
                    }
                    else
                    {
                        isValidSelectionFromMenu = true;
                    }
                }
                else
                {
                    Console.WriteLine("You entered an invalid input. please make sure you insert only number: ");
                }
            }
            while (!isValidSelectionFromMenu);

            this.handleUserSelection((eMenuOptions)Enum.ToObject(typeof(eMenuOptions), userSelection));
        }

        private void handleUserSelection(eMenuOptions i_UserSelection)
        {
            switch(i_UserSelection)
            {
                case eMenuOptions.InsertNewVehicleToTheGarage:
                    insertNewVehicleToTheGarage();
                    break;
                case eMenuOptions.ShowAllLicenseNumbersInGarageWithFiltering:
                    showAllLicensesWithFilterOption();
                    break;
                case eMenuOptions.ChangeVehicleStatusInTheGarage:
                    changeVehicleStatusInGarage();
                    break;
                case eMenuOptions.InflateVehicleWheelsToMaximum:
                    inflateVehicleWheelsToMaximum();
                    break;
                case eMenuOptions.RefuelRegularVehicle:
                    refuelRegularVehicle();
                    break;
                case eMenuOptions.RefuelElectricVehicle:
                    
                    break;
                case eMenuOptions.ShowVehicleDetails:
                    
                    break;
                case eMenuOptions.Exit:
                    Console.WriteLine("GoodBye!");
                    Environment.Exit(0);
                    break;
            }
        }

        private void refuelRegularVehicle()
        {
            Console.WriteLine("Please enter car's license number: ");

            string licenseNumber = getLicenseNumberFromClient();
            string[] fuelTypes = FuelEnergy.getFuelTypes();
            int i = 1;
            bool isValidSelection = false;
            int fuelTypeSelection;

            Console.WriteLine("Please enter the fuel type you would like to fill with: ");
            
            foreach(string fuelType in fuelTypes)
            {
                Console.WriteLine($"{i}. {fuelType}");
            }

            do
            {
                fuelTypeSelection = readIntegerInputFromUser();

                if (!(fuelTypeSelection >= 1 && fuelTypeSelection <= 4))
                {
                    Console.WriteLine("Please enter a valid option from the menu. ");
                }
                else
                {
                    isValidSelection = true;
                }
            }
            while(!isValidSelection);

            Console.WriteLine("Please enter amount to fill: ");

            float amountToFill = readFloatInputFromUser();

            try
            {
                m_GarageManager.RefuelRegularVehicle(
                    licenseNumber,
                    (FuelEnergy.eFuelType)Enum.ToObject(typeof(FuelEnergy.eFuelType), fuelTypeSelection),
                    amountToFill);
            }
            catch(ArgumentException ae)
            {

            }
            
            



        }

        private void inflateVehicleWheelsToMaximum()
        {
            Console.WriteLine("Please enter your car's license number: ");
            string licenseNumber = getLicenseNumberFromClient();

            try
            {
                m_GarageManager.InflateWheelsToMaximum(licenseNumber);
            }
            catch(KeyNotFoundException knfe)
            {
                Console.WriteLine(knfe.Message);
            }
        }

        private void changeVehicleStatusInGarage()
        {
            
            Console.WriteLine(VehicleFactory.GeneralQuestions[0].Value);

            string licenseNumber = getLicenseNumberFromClient();

            Console.WriteLine(VehicleFactory.GeneralQuestions[1].Value);

            GarageForm.eServiceStatus serviceStatus = getVehicleStatusSelectionFromUser();

            // TODO - FINISH

        }

        private GarageForm.eServiceStatus getVehicleStatusSelectionFromUser()
        {
            bool isValidSelection = false;
            GarageForm.eServiceStatus serviceStatus = GarageForm.eServiceStatus.InRepair;

            Console.WriteLine("Please select one of the following: ");
            Console.WriteLine("1. InRepair. ");
            Console.WriteLine("2. Repaired. ");
            Console.WriteLine("3. Payed. ");

            do
            {
                int userSelection = readIntegerInputFromUser();
                

                if (!(userSelection >= 1 && userSelection <= 3))
                {
                    Console.WriteLine("Please make sure you select option from the menu.");
                }
                else
                {
                    serviceStatus = getAsEServiceStatus(userSelection);
                    isValidSelection = true;
                }
            }
            while(!isValidSelection);

            return serviceStatus;
        }

        private GarageForm.eServiceStatus getAsEServiceStatus(int i_Selection)
        {
            return (GarageForm.eServiceStatus)Enum.
                ToObject(typeof(GarageForm.eServiceStatus), i_Selection);
        }

        private void showAllLicensesWithFilterOption()
        {
            int selection;

            do
            {
                selection = readIntegerInputFromUser();
                selection += 1;
            }
            while(selection >= 1 || selection <= 4);
            
            string[] filteredLicenseArray = m_GarageManager.ShowAllLicensesWithFilterOption(selection);

            GarageForm.eServiceStatus serviceStatus = (GarageForm.eServiceStatus)Enum.
                ToObject(typeof(GarageForm.eServiceStatus), selection);

            Console.WriteLine($@"The licenses after the filter {serviceStatus} is:");
            foreach(string license in filteredLicenseArray)
            {
                Console.WriteLine(license);
            }
        }

        private void insertNewVehicleToTheGarage()
        {
            Console.WriteLine("Please Enter License Number of your car: ");
            string licenseNumber = getLicenseNumberFromClient();
            
            if(!m_GarageManager.IsVehicleExistsInTheGarage(licenseNumber))
            {
                try
                {
                    FillClientForm();
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
            }
            else
            {
                Console.WriteLine("The vehicle already in the garage!");

                m_GarageManager.ChangeVehicleStatusInTheGarage(licenseNumber, GarageForm.eServiceStatus.InRepair);
            }
        }

        // TODO - i_VehicleType should be int.
        private void FillClientForm()
        {
            Console.WriteLine("Whats your name?");
            string clientName = getClientResponse();
                
            Console.WriteLine("What is your phone number?");
            string phoneNumber = getClientResponse();

            Console.WriteLine("Choose your vehicle type " + this.m_GarageManager.GetSupportedVehiclesTypes() + " (without spaces!): ");
            string vehicleType = getClientResponse();

            List<KeyValuePair<Type, string>> clientForm 
                = this.m_GarageManager.GetClientVehicleForm(vehicleType);
            
            /*
             * TODO 1. for each clientForm element, receive input from user to object element.
             * TODO 2. Make sure constructor of element is in same order as questions.
             * TODO 3. Insert new vehicle using GameManager function (That will call VehicleFactory).
             * TODO -  Insert client (if not already exist).
             */

            
            
            /*float energyToDrive= 0;
            bool isValidInput = false;

            if(isElectricVehicle(i_VehicleType))
            {
                Console.WriteLine("Please enter battery percentage amount: ");
            }
            else
            {
                Console.WriteLine("Please enter fuel amount: ");
            }

            energyToDrive = readFloatInputFromUser();

            Console.WriteLine("Please enter wheel pressure: ");

            float wheelsPressure = readFloatInputFromUser();

            if(vehicleType == VehicleFactory.eVehicleTypes.ElectricCar || vehicleType == VehicleFactory.eVehicleTypes.FuelCar)
            {
                Console.WriteLine("Please enter car color: ");


                //handle errors, and get input (there a method named getFloatInputFromUser)

            }

            else if(vehicleType == VehicleFactory.eVehicleTypes.Truck)
            {
                Console.WriteLine("Is your truck delivers hazardous materials? (answer yes/no)");

                //handle errors, and get input (there a method named getFloatInputFromUser)
            }
            else 
            {
                Console.WriteLine("Please enter motorcycle licenseType: ");

                //handle errors, and get input (there a method named getFloatInputFromUser)
            }*/


            //creating the vehicle.

        }
        private int readIntegerInputFromUser()
        {
            bool isValidInput = false;
            int integerInput;

            do
            {
                string userInput = Console.ReadLine();

                if (!int.TryParse(userInput, out integerInput))
                {
                    Console.WriteLine("Please make sure you insert a number.");
                }
                else
                {
                    isValidInput = true;
                }
            }
            while (!isValidInput);

            return integerInput;
        }

        private float readFloatInputFromUser()
        {
            bool isValidInput = false;
            float floatInput;

            do
            {
                string userInput = Console.ReadLine();

                if (!float.TryParse(userInput, out floatInput))
                {
                    Console.WriteLine("Please make sure you insert a number.");
                }
                else
                {
                    isValidInput = true;
                }
            }
            while (!isValidInput);

            return floatInput;
            ;
        }
        
        private string getClientResponse()
        {
            string res = Console.ReadLine();
            return res;
        }

        private bool isValidPhoneNumber(string i_PhoneNumber)
        {
            bool isValidNumber = true;

            foreach(char character in i_PhoneNumber)
            {
                if(!char.IsDigit(character))
                {
                    isValidNumber = false;
                    break;
                }
            }

            return isValidNumber;
        }
        
        private int getVehicleTypeFromClient()
        {
            bool validInputFromUser = false;
            int vehicleType = -1;

            Console.WriteLine("Choose your vehicle type " + this.m_GarageManager.GetSupportedVehiclesTypes() + " (without spaces!): ");
            
            do
            {
                string vehicleTypeAsString = Console.ReadLine();

                try
                {
                    if(vehicleTypeAsString != null)
                    {
                        if(!this.m_GarageManager.IsVehicleTypeExist(vehicleTypeAsString))
                        {
                            throw new ArgumentOutOfRangeException("Vehicle type doesnt exist.");
                        }
                        else
                        {
                            validInputFromUser = true;
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($@"There's no {vehicleTypeAsString} exists. Please enter again: ");
                }
            }
            while(!validInputFromUser);

            return vehicleType;
        }
        
        private string getLicenseNumberFromClient()
        {
            string licenseNumberResponse = "";
            bool isValidNumber = false;
            do
            {
                licenseNumberResponse = Console.ReadLine();

                if (isValidLicenseNumber(licenseNumberResponse))
                {
                    Console.WriteLine("This is isn't a valid license number. please insert again.");
                }
                else
                {
                    isValidNumber = true;
                }
            } while (!isValidNumber);

            return licenseNumberResponse;
        }

        private bool isValidLicenseNumber(string i_licenseNumber)
        {
            bool isLetter = false;

            foreach(char character in i_licenseNumber)
            {
                if(char.IsLetter(character))
                {
                    isLetter = true;
                    break;
                }   
            }

            return isLetter;
        }
    }
}
