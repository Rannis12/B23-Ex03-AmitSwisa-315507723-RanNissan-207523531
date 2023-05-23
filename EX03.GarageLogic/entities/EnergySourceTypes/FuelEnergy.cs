using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX03.GarageLogic.entities.EnergySourceTypes
{
    public class FuelEnergy : EnergySource
    {
        public enum eFuelType
        {
            Soler, 
            Octan95, 
            Octan96, 
            Octan98
        }

        private eFuelType m_FuelType;
        
        public eFuelType FuelType
        {
            get
            {
                return this.m_FuelType;
            }
        }

        public FuelEnergy(float m_MaxEnergyAmount, float i_CurrentFuelAmount, eFuelType i_FuelType)
            : base(m_MaxEnergyAmount, i_CurrentFuelAmount)
        {
            this.m_FuelType = i_FuelType;
        }

    }
}
