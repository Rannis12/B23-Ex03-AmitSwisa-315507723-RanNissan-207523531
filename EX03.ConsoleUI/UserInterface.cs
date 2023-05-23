using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic;
using EX03.GarageLogic.entities;

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
                    
                    break;
                case eMenuOptions.ChangeVehicleStatusInTheGarage:
                    
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
                enterVehicleData();
            }
            else
            {
                Console.WriteLine("The vehicle already in the garage!");

                m_GarageManager.ChangeVehicleStatusInTheGarage(o_LicenseNumber, GarageForm.eServiceStatus.InRepair);
            }



            //VehicleFactory.Get
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
            return i_VehicleType.ToUpper().Equals("ELECTRICCAR") || i_VehicleType.ToUpper().Equals("FUELCAR")
                                                                 || i_VehicleType.ToUpper().Equals("TRUCK")
                                                                 || i_VehicleType.ToUpper().Equals("ELECTRICMOTORCYCLE")
                                                                 || i_VehicleType.ToUpper().Equals("FUELCAR");
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
