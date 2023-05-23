using EX03.GarageLogic.entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX03.GarageLogic.entities.Factory
{
    public static class VehicleFactory
    {
        public enum eVehicleTypes
        {
            ElectricCar = 0,
            ElectricMotorcycle = 1,
            FuelCar = 2,
            FuelMotorcycle = 3,
            Truck = 4
        }

        public static string[] GetVehiclesTypesAsArray()
        {
            return Enum.GetNames(typeof(eVehicleTypes));
        }

        

    }
}
