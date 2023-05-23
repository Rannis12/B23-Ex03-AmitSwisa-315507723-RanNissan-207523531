using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic.entities.EnergySourceTypes;
using EX03.GarageLogic.entities.VehicleModels;

namespace EX03.GarageLogic.entities.Vehicles
{
    class FuelCar : Car
    {
        private const float k_MaxFuelTank = 46f;
        private const FuelEnergy.eFuelType k_FuelType = FuelEnergy.eFuelType.Octan95;
        
        public FuelCar(string i_LicenseNumber, eColor i_Color, 
                eNumOfDoors i_NumOfDoors, float i_CurrentFuelAmount)
                        : base(i_LicenseNumber, i_Color, i_NumOfDoors)
        {
            this.m_EnergySource = new FuelEnergy(k_MaxFuelTank, i_CurrentFuelAmount, k_FuelType);
        }

    }
}
