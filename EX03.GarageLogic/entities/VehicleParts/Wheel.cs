using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX03.GarageLogic.entities
{
    public class Wheel
    {
        private string m_Manufacturer;
        private float m_CurrentTirePressure;
        private readonly float r_MaximumTirePressure;

        public Wheel(float i_MaximumTirePressure)
        {
            this.r_MaximumTirePressure = i_MaximumTirePressure;
        }

        public Wheel(string i_Manufacturer, float i_CurrentTirePressure, float i_MaximumTirePressure)
        {
            this.m_Manufacturer = i_Manufacturer;
            this.m_CurrentTirePressure = i_CurrentTirePressure;
            this.r_MaximumTirePressure = i_MaximumTirePressure;
        }

        private void inflateTire(int i_AmountOfPressureToInflate)
        {

        }
    }
}