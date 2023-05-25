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

        public override void FillEnergy(float i_EnergyToAdd)
        {
            throw new ArgumentException("Missing fuel type argument.");
        }

        public override void FillEnergy(float i_EnergyToAdd, eFuelType i_FuelType)
        {
            if(FuelType != i_FuelType)
            {
                throw new ArgumentException("Fuel type is incorrect" +
                    ", " + FuelType + " is different then" + i_FuelType);
            }
            else
            {
                base.FillEnergy(i_EnergyToAdd);
            }
        }

        public static string[] getFuelTypes()
        {
            return Enum.GetNames(typeof(eFuelType));
        }

    }
}
