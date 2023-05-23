using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX03.GarageLogic.entities.EnergySourceTypes
{
    public class ElectricEnergy : EnergySource
    {
        public ElectricEnergy(float i_MaxBatteryTime, float i_CurrentBatteryTimeLeft)
            : base(i_MaxBatteryTime, i_CurrentBatteryTimeLeft)
        {
            
        }
    }
}