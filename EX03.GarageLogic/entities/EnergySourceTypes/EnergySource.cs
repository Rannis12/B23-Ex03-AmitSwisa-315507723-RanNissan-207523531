using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic.Exceptions;


namespace EX03.GarageLogic.entities.EnergySourceTypes
{
    public abstract class EnergySource
    {
        private float m_CurrentEnergyAmount;
        private readonly float m_MaxEnergyAmount;

        public EnergySource(float i_MaxEnergyAmount, float i_CurrentFuelAmount)
        {
            this.m_MaxEnergyAmount = i_MaxEnergyAmount;
            this.m_CurrentEnergyAmount = i_CurrentFuelAmount;
        }

        public float CurrentEnergyAmount
        {
            get
            {
                return m_CurrentEnergyAmount;
            }
        }

        public float MaxEnergyAmount
        {
            get
            {
                return m_MaxEnergyAmount;
            }
        }

        public void FillEnergy(float i_EnergyToAdd)
        {
            float totalEnergy = i_EnergyToAdd + CurrentEnergyAmount;

            if (totalEnergy > m_MaxEnergyAmount)
            {
                throw new ValueOutOfRangeException(totalEnergy, MaxEnergyAmount);
            }

            this.m_CurrentEnergyAmount += i_EnergyToAdd;
        }

        public float EnergyLeftPrecentage()
        {
            return (CurrentEnergyAmount / MaxEnergyAmount) * 100;
        }
    }
}
