using EX03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EX03.GarageLogic.entities.EnergySourceTypes.FuelEnergy;

namespace EX03.GarageLogic.entities.EnergySourceTypes
{
    public class ElectricEnergy : EnergySource
    {
        public ElectricEnergy(float i_MaxBatteryTime, float i_CurrentBatteryTimeLeft)
            : base(i_MaxBatteryTime, i_CurrentBatteryTimeLeft)
        {
            
        }

        public override void FillEnergy(float i_EnergyToAdd, eFuelType i_FuelType)
        {
            throw new ArgumentException("Electric vehicle can't receive fuel.");
        }
    }
}