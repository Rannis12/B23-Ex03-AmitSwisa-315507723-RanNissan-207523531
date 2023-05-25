using EX03.GarageLogic.Exceptions;
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

        private void inflateTire(float i_AmountOfPressureToInflate)
        {
            float totalPressure = i_AmountOfPressureToInflate + m_CurrentTirePressure;

            if (totalPressure > r_MaximumTirePressure)
            {
                throw new ValueOutOfRangeException(this.m_CurrentTirePressure, this.r_MaximumTirePressure);
            }
            else
            {
                this.m_CurrentTirePressure = totalPressure;
            }
        }

        public void InflateTireToMaximum()
        {
            inflateTire(r_MaximumTirePressure - m_CurrentTirePressure);
        }
    }
}