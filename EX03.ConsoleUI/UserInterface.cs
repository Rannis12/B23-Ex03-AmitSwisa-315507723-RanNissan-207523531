using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic;
using EX03.GarageLogic.entities;
using EX03.GarageLogic.entities.EnergySourceTypes;
using EX03.GarageLogic.entities.Factory;
using EX03.GarageLogic.entities.VehicleModels;
using EX03.GarageLogic.Exceptions;

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
                    refuelElectricVehicle();
                    break;
                case eMenuOptions.ShowVehicleDetails:
                    showVehicleDetails();
                    break;
                case eMenuOptions.Exit:
                    Console.WriteLine("GoodBye!");
                    Environment.Exit(0);
                    break;
            }
        }

        private void showVehicleDetails()
        {
            Console.WriteLine("Please enter car's license number: ");
            string licenseNumber = getLicenseNumberFromClient();

            displayFormInfo(licenseNumber);
            displaySpecificInfo(licenseNumber);

        }

        private void displaySpecificInfo(string i_LicenseNumber)
        {
            try
            {
                GarageForm garageForm = m_GarageManager.GetGarageForm(i_LicenseNumber);
                List<string> relevantDetails = garageForm.Vehicle.GetSpecificInfo();

                foreach(string relevantDetail in relevantDetails)
                {
                    Console.WriteLine(relevantDetail);
                }
            }
            catch(KeyNotFoundException knf)
            {
                Console.WriteLine(knf.Message);
            }
        }

        private void displayFormInfo(string i_LicenseNumber)
        {
            try
            {
                GarageForm garageForm = m_GarageManager.GetGarageForm(i_LicenseNumber);

                Console.WriteLine($"License number: {i_LicenseNumber}");
                Console.WriteLine($"Manufacturer name: {garageForm.Vehicle.Model}");
                Console.WriteLine($"Owner name: {garageForm.Client.ClientName}");
                Console.WriteLine($"Current status service: {garageForm.ServiceStatus}");

                foreach(Wheel wheel in garageForm.Vehicle.Wheels)
                {
                    Console.WriteLine($"Manufacturer name: {wheel.Manufacturer}");
                    Console.WriteLine($"Current pressure: {wheel.CurrentTierPressure}");
                }

                if(garageForm.Vehicle.EnergySource is FuelEnergy)
                {
                    FuelEnergy fuelEnergy = garageForm.Vehicle.EnergySource as FuelEnergy;
                    Console.WriteLine($"Current amount of fuel: {fuelEnergy.CurrentEnergyAmount}");
                    Console.WriteLine($"Fuel type: {fuelEnergy.FuelType}");
                }
                else
                {
                    ElectricEnergy electricEnergy = garageForm.Vehicle.EnergySource as ElectricEnergy;
                    Console.WriteLine($"Battery percentage: {electricEnergy.CurrentEnergyAmount}");
                }
            }
            catch (KeyNotFoundException knf)
            {
                Console.WriteLine(knf.Message);
            }
        }

        private void refuelElectricVehicle()
        {
            Console.WriteLine("Please enter your car's license number: ");

            string licenseNumber = getLicenseNumberFromClient();

            Console.WriteLine("Please enter the amount of time you would like to charge the battery: ");

            float chargeTime = readFloatInputFromUser();

            try
            {
                m_GarageManager.RefuelElectricVehicle(licenseNumber, chargeTime);
            }
            catch(KeyNotFoundException knf)
            {
                Console.WriteLine(knf.Message);
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            catch(ValueOutOfRangeException voor)
            {
                Console.WriteLine(voor.Message);
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
                Console.WriteLine(ae.Message);
            }
            catch(ValueOutOfRangeException voor)
            {
                Console.WriteLine(voor.Message);
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
            string licenseNumber = getLicenseNumberFromClient();

            string clientName = getClientName();

            GarageForm.eServiceStatus serviceStatus = getVehicleStatusSelectionFromUser();

            try
            {
                m_GarageManager.ChangeVehicleStatusInTheGarage(licenseNumber, serviceStatus);
            }
            catch(KeyNotFoundException knf)
            {
                Console.WriteLine(knf.Message);
            }
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

        private string getClientName()
        {
            Console.WriteLine("Whats your name?");
            string clientName = getClientResponse();
            return clientName;
        }

        // TODO - i_VehicleType should be int.
        private void FillClientForm()
        {
            string clientName = getClientName();
                
            Console.WriteLine("What is your phone number?");
            string phoneNumber = getPhoneNumber();

            Console.WriteLine("Choose your vehicle type " + this.m_GarageManager.GetSupportedVehiclesTypes() + " (without spaces!): ");
            int vehicleType = getVehicleTypeFromClient();
            
            Console.WriteLine("Model name:");
            string modelName = getClientResponse();
            
            Console.WriteLine("Remaining energy in you vehicle?");
            float remainingEnergyInEnergySource = readFloatInputFromUser();
            
            Console.WriteLine("Wheels manifacturer name?");
            string wheelsManufacturerName = getClientResponse();

            Console.WriteLine("Wheels air pressure?");
            float wheelsCurrentAirPressure = readFloatInputFromUser();
            
            List<string> vehicleQuestions 
                = this.m_GarageManager.GetClientVehicleForm(vehicleType);

            List<Object> clientAnswers = new List<Object>();

            foreach(string question in vehicleQuestions)
            {
                Console.WriteLine(question);
                string res = getClientResponse();
                
                clientAnswers.Add(res);
            }
            
            // TODO - make car.


        }

        private string getPhoneNumber()
        {
            string phoneNumber = getClientResponse();

            if(!isValidPhoneNumber(phoneNumber))
            {
                throw new ArgumentException("Phone number is invalid.");
            }

            return phoneNumber;
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
            string userInput = Console.ReadLine();
            
            float floatInput;

            if (!float.TryParse(userInput, out floatInput))
            {
                throw new ArgumentException("Please enter a valid air pressure.");
            }

            return floatInput;
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
            Console.WriteLine("Choose your vehicle type: ");

            string[] vehicleTypes = this.m_GarageManager.GetSupportedVehiclesTypes();

            for(int i = 1; i <= vehicleTypes.Length; i++)
            {
                Console.WriteLine(i + ". " + vehicleTypes[i-1]);
            }
            
            string vehicleTypeAsString = Console.ReadLine();

            int vehicleType;
           
            try
            {
                vehicleType = int.Parse(vehicleTypeAsString);
            }
            catch (Exception)
            {
                throw new ArgumentException("Vehicle type doesn't exist in our system.");
            }

            return vehicleType;
        }
        
        private string getLicenseNumberFromClient()
        {
            Console.WriteLine("Please Enter License Number of your car: ");
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
