using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic.entities.EnergySourceTypes;
using EX03.GarageLogic.entities.VehicleModels;

namespace EX03.GarageLogic.entities.Vehicles
{
    class ElectricCar : Car
    {
        private const float k_MaxBatteryTime = 5.2f;

        public ElectricCar(string i_LicenseNumber, eColor i_Color, eNumOfDoors i_NumOfDoors,
                 float i_CurrentBatteryTimeLeft) : base(i_LicenseNumber, i_Color, i_NumOfDoors)
        {
            this.m_EnergySource = new ElectricEnergy(k_MaxBatteryTime, i_CurrentBatteryTimeLeft);
        }

    }
}
