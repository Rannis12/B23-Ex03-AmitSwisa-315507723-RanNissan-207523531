using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic.Exceptions;
using static EX03.GarageLogic.entities.EnergySourceTypes.FuelEnergy;

namespace EX03.GarageLogic.entities.EnergySourceTypes
{
    public abstract class EnergySource
    {
        protected float m_CurrentEnergyAmount;
        protected readonly float m_MaxEnergyAmount;

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

        public virtual void FillEnergy(float i_EnergyToAdd)
        {
            float totalEnergy = i_EnergyToAdd + CurrentEnergyAmount;

            if (totalEnergy > m_MaxEnergyAmount)
            {
                throw new ValueOutOfRangeException(totalEnergy, MaxEnergyAmount);
            }
            else
            {
                this.m_CurrentEnergyAmount += i_EnergyToAdd;
            }
        }

        public abstract void FillEnergy(float i_EnergyToAdd, eFuelType i_FuelType);

        public float EnergyLeftPrecentage()
        {
            return (CurrentEnergyAmount / MaxEnergyAmount) * 100;
        }
    }
}
