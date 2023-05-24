using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic.entities.Factory;
using EX03.GarageLogic.entities.VehicleModels;

namespace EX03.GarageLogic.DTO
{
    public class VehiclePossibleData
    {
        private readonly float m_WheelPressure;
        private readonly float m_AmountOfFuel;
        private readonly float m_BatteryPrecentage;
        private readonly Motorcycle.eLicenseType m_LicenseType;
        private readonly Car.eColor m_CarColor;
        private readonly bool m_isContainHazardousMaterials;
        private readonly string m_LicenseNumber;
        private readonly int m_EngineCapacity;
        private readonly float m_LoadCapacity;
        private readonly Car.eNumOfDoors m_NumOfDoors;

        public VehiclePossibleData(
            float i_WheelPressure,
            float i_AmountOfFuel,
            Car.eColor i_CarColor,
            VehicleFactory.eVehicleTypes i_VehicleType,
            float i_BatteryPrecentage,
            Motorcycle.eLicenseType i_LicenseType,
            string i_LicenseNumber,
            Car.eNumOfDoors i_NumOfDoors,
            int i_EngineCapacity,
            float i_LoadCapacity,
            bool i_IsContainHazardousMaterials = false)
        {
            m_WheelPressure = i_WheelPressure;
            m_LicenseNumber = i_LicenseNumber;

            if(i_VehicleType == VehicleFactory.eVehicleTypes.FuelCar
               || i_VehicleType == VehicleFactory.eVehicleTypes.FuelMotorcycle
               || i_VehicleType == VehicleFactory.eVehicleTypes.Truck)
            {
                m_AmountOfFuel = i_AmountOfFuel;
            }

            if(i_VehicleType == VehicleFactory.eVehicleTypes.FuelCar
               || i_VehicleType == VehicleFactory.eVehicleTypes.ElectricCar)
            {
                m_NumOfDoors = i_NumOfDoors;
            }

            if (i_VehicleType == VehicleFactory.eVehicleTypes.ElectricCar
               || i_VehicleType == VehicleFactory.eVehicleTypes.FuelCar)
            {
                m_CarColor = i_CarColor;
            }

            if(i_VehicleType == VehicleFactory.eVehicleTypes.ElectricCar
               || i_VehicleType == VehicleFactory.eVehicleTypes.ElectricMotorcycle)
            {
                m_BatteryPrecentage = i_BatteryPrecentage;
            }

            if(i_VehicleType == VehicleFactory.eVehicleTypes.Truck)
            {
                m_isContainHazardousMaterials = i_IsContainHazardousMaterials;
                m_LoadCapacity = i_LoadCapacity;
            }

            if(i_VehicleType == VehicleFactory.eVehicleTypes.FuelMotorcycle
               || i_VehicleType == VehicleFactory.eVehicleTypes.ElectricMotorcycle)
            {
                m_LicenseType = i_LicenseType;
                m_EngineCapacity = i_EngineCapacity;
            }
        }

        public float WheelPressure
        {
            get
            {
                return m_WheelPressure;
            }
        }
        public float AmountOfFuel
        {
            get
            {
                return m_AmountOfFuel;
            }
        }
        public float BatteryPrecentage
        {
            get
            {
                return m_BatteryPrecentage;
            }
        }
        public Motorcycle.eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
        }
        public Car.eColor CarColor
        {
            get
            {
              return m_CarColor;
            }
        }
        public bool IsContainHazardousMaterials
        {
            get
            {
                return m_isContainHazardousMaterials;
            }
        }
        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
        }
        public int EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }
        }
        public float LoadCapacity
        {
            get
            {
                return m_LoadCapacity;
            }
        }
        public Car.eNumOfDoors NumOfDoors
        {
            get
            {
                return m_NumOfDoors;
            }
        } 
    }
}
