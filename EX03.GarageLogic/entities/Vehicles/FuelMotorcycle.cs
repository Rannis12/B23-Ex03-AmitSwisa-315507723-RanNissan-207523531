using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic.entities.EnergySourceTypes;
using EX03.GarageLogic.entities.VehicleModels;

namespace EX03.GarageLogic.entities.Vehicles
{
    public class FuelMotorcycle : Motorcycle
    {
        private const float k_MaxFuelTank = 6.4f;
        private const FuelEnergy.eFuelType k_FuelType = FuelEnergy.eFuelType.Octan95;
        
        public FuelEnergy.eFuelType FuelType
        {
            get
            {
                return k_FuelType;
            }
        }

        public FuelMotorcycle(string i_LicenseNumber, int i_EngineCapacity, 
            eLicenseType i_LicenseType, float i_CurrentFuelAmount)
                : base(i_LicenseNumber, i_EngineCapacity, i_LicenseType)
        {
            this.m_EnergySource = new FuelEnergy(k_MaxFuelTank, i_CurrentFuelAmount, k_FuelType);
        }

    }
}
