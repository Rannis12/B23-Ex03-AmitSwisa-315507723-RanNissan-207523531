using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic.entities.EnergySourceTypes;
using EX03.GarageLogic.entities.VehicleModels;

namespace EX03.GarageLogic.entities.Vehicles
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxBatteryTime = 2.6f;

        public ElectricMotorcycle(string i_LicenseNumber, int i_EngineCapacity, 
            eLicenseType i_LicenseType, float i_CurrentBatteryTimeLeft) 
                : base(i_LicenseNumber, i_EngineCapacity, i_LicenseType)
        {
            this.m_EnergySource = new ElectricEnergy(k_MaxBatteryTime, i_CurrentBatteryTimeLeft);
        }
    }
}
