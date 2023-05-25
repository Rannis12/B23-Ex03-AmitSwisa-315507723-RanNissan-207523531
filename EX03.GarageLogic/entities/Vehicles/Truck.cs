using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic.entities.EnergySourceTypes;

namespace EX03.GarageLogic.entities.Vehicles
{
    class Truck : Vehicle
    {
        private const int k_NumOfWheels = 14;
        private const int k_MaxAirPressure = 26;
        private const float r_MaxFuelTank = 135f;
        private bool m_IsLoadHazardousMaterials;
        private float m_LoadAmount;
        
        public Truck(string i_LicenseNumber, float i_CurrentFuelAmount,
                bool i_IsLoadHazardousMaterials, float i_LoadAmount)
                    : base(i_LicenseNumber, k_NumOfWheels, k_MaxAirPressure)
        {
            this.m_IsLoadHazardousMaterials = i_IsLoadHazardousMaterials;
            this.m_LoadAmount = i_LoadAmount;
            this.m_EnergySource = new FuelEnergy(r_MaxFuelTank, i_CurrentFuelAmount, FuelEnergy.eFuelType.Soler);
        }

        public override List<string> GetSpecificInfo()
        {
            List<string> truckInfoList = new List<string>();
            
            if (m_IsLoadHazardousMaterials)
            {
                truckInfoList.Add("Is refrigerated truck: Yes");
            }
            else
            {
                truckInfoList.Add("Is refrigerated truck: No");
            }

            truckInfoList.Add("Truck load amount: " + m_LoadAmount);

            return truckInfoList;
        }
    }
}