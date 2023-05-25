using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic.entities.VehicleModels;

namespace EX03.GarageLogic.entities.VehicleModels
{
    public class Motorcycle : Vehicle
    {
       
        private const int k_NumberOfWheels = 2;
        private const int k_MaxWheelAirPressure = 31;
        private readonly eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public eLicenseType LicenseType 
        {
            get
            {
                return m_LicenseType;
            }
        }

        public enum eLicenseType
        {
            A1 = 1,
            A2,
            AA,
            B1
        }

        public Motorcycle(string i_LicenseNumber, int i_EngineCapacity, eLicenseType i_LicenseType) 
            : base(i_LicenseNumber, k_NumberOfWheels, k_MaxWheelAirPressure)
        {
            this.m_LicenseType = i_LicenseType;
            this.m_EngineCapacity = i_EngineCapacity;
        }

        public override List<string> GetSpecificInfo()
        {
            List<string> motorCycleInfoList = new List<string>();

            motorCycleInfoList.Add("License type: " + Enum.GetName(typeof(Motorcycle.eLicenseType), m_LicenseType));
            motorCycleInfoList.Add("Engine capacity: " + m_EngineCapacity);

            return motorCycleInfoList;
        }
    }
}