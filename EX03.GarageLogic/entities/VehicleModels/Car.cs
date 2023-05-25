using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX03.GarageLogic.entities.VehicleModels
{
    public class Car : Vehicle
    {
        public enum eColor
        {
            Red,
            White,
            Black,
            Yellow
        }

        public enum eNumOfDoors
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        private const int k_NumOfWheels = 5;
        private const int k_MaxWheelAirPressure = 33;
        private readonly eColor r_Color;
        private readonly eNumOfDoors r_NumOfDoors;

        public Car(string i_LicenseNumber, eColor i_Color, eNumOfDoors i_NumOfDoors)
            : base(i_LicenseNumber, k_NumOfWheels, k_MaxWheelAirPressure)
        {
            this.r_Color = i_Color;
            this.r_NumOfDoors = i_NumOfDoors;
        }

        public override List<string> GetSpecificInfo()
        {
            List<string> carInfoList = new List<string>();

            carInfoList.Add("Color: " + Enum.GetName(typeof(eColor), r_Color));
            carInfoList.Add("Number of doors: " + r_NumOfDoors.ToString());

            return carInfoList;
        }
    }
}
