using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic;
using EX03.GarageLogic.entities;
using EX03.GarageLogic.entities.Factory;

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
                    
                    break;
                case eMenuOptions.RefuelRegularVehicle:
                    
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

        private void changeVehicleStatusInGarage()
        {
            string licenseNumber;

            Console.WriteLine(VehicleFactory.GeneralQuestions[0].Value);

            getLicenseNumber(out licenseNumber);

            Console.WriteLine(VehicleFactory.GeneralQuestions[1].Value);

            GarageForm.eServiceStatus serviceStatus = getVehicleStatusSelectionFromUser();



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
            string o_LicenseNumber;
            string o_PhoneNumber;
            string o_ClientName;
            
            Console.WriteLine("Please Enter License Number of your car: ");

            getLicenseNumber(out o_LicenseNumber);

            if(!m_GarageManager.IsVehicleExistsInTheGarage(o_LicenseNumber))
            {
                getClientNameAndPhoneNumber(out o_PhoneNumber, out o_ClientName);

                string vehicleType =  getVehicleInput();

                collectVehicleData(vehicleType);
            }
            else
            {
                Console.WriteLine("The vehicle already in the garage!");

                m_GarageManager.ChangeVehicleStatusInTheGarage(o_LicenseNumber, GarageForm.eServiceStatus.InRepair);
            }
        }

        private void collectVehicleData(string i_VehicleType)
        {
            VehicleFactory.eVehicleTypes vehicleType = (VehicleFactory.eVehicleTypes)Enum.ToObject(
                typeof(VehicleFactory.eVehicleTypes),
                i_VehicleType);

            float energyToDrive= 0;
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
            }


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
        private bool isElectricVehicle(string i_VehicleType)
        {
            bool isElectric = i_VehicleType.ToUpper().Equals("ElectricCar".ToUpper())
                              || i_VehicleType.ToUpper().Equals("ElectricMotorcycle".ToUpper());

            return isElectric;
        }
        private void getClientNameAndPhoneNumber(out string o_PhoneNumber, out string o_ClientName)
        {
            bool isValidNumber = false;

            Console.WriteLine("Please enter your name:");
            o_ClientName = Console.ReadLine();

            Console.WriteLine("Please enter your phone number: ");

            do
            {
                o_PhoneNumber = Console.ReadLine();

                if(!isValidPhoneNumber(o_PhoneNumber))
                {
                    Console.WriteLine("invalid phone number. Please try again: ");
                }
                else
                {
                    isValidNumber = true;
                }
            }
            while(!isValidNumber);
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

        private string getVehicleInput()
        {
            bool validInputFromUser = false;
            string vehicleType = null;

            Console.WriteLine("Please insert vehicle's type: FuelCar, ElectricCar, FuelMotorcycle, ElectricMotorcycle, Truck (without spaces!)");
            
            do
            {
                vehicleType = Console.ReadLine();

                if(!isOneOfVehicles(vehicleType))
                {
                    Console.WriteLine($@"There's no {vehicleType} exists. Please enter again: ");
                }
                else
                {
                    validInputFromUser = true;
                }
            }
            while(!validInputFromUser);

            return vehicleType;
        }

        private bool isOneOfVehicles(string i_VehicleType)
        {
            bool isValidVehicle = false;
            string[] arrayOfVehicles = VehicleFactory.GetVehiclesTypesAsArray();

            foreach(string vehicleType in arrayOfVehicles)
            {
                if(vehicleType.ToUpper().Equals(i_VehicleType.ToUpper()))
                {
                    isValidVehicle = true;
                    break;
                }
            }
            //return i_VehicleType.ToUpper().Equals("ELECTRICCAR") 
            //    || i_VehicleType.ToUpper().Equals("FUELCAR")                                            
            //    || i_VehicleType.ToUpper().Equals("TRUCK")         
            //    || i_VehicleType.ToUpper().Equals("ELECTRICMOTORCYCLE")                                         
            //    || i_VehicleType.ToUpper().Equals("FUELCAR");
            return isValidVehicle;
        }

        private void getLicenseNumber(out string o_LicenseNumber)
        {
            bool isValidNumber = false;
            do
            {
                o_LicenseNumber = Console.ReadLine();

                if (isValidLicenseNumber(o_LicenseNumber))
                {
                    Console.WriteLine("This is isn't a valid license number. please insert again.");
                }
                else
                {
                    isValidNumber = true;
                }
            } while (!isValidNumber);
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
