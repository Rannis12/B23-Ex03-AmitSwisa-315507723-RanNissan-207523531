using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic.entities.EnergySourceTypes;

namespace EX03.GarageLogic.entities
{
    public abstract class Vehicle
    {
        private string m_Model { get; set; }
        private readonly string r_LicenseNumber;
        private Wheel[] m_Wheels;
        protected EnergySource m_EnergySource;

        public string Model
        {
            get
            {
                return this.m_Model;
            }
        }
        
        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        internal Vehicle(string i_LicenseNumber, int i_NumberOfWheels, int i_MaximumWheelsAirPressure)
        {
            this.r_LicenseNumber = i_LicenseNumber;
            this.m_Wheels = new Wheel[i_NumberOfWheels];

            for(int i = 0; i < i_NumberOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(i_MaximumWheelsAirPressure);
            }
        }

        public int GetNumOfWheels()
        {
            return this.m_Wheels.Length;
        }

        public Wheel[] Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        public EnergySource EnergySource
        {
            get
            {
                return m_EnergySource;
            }
        }

        public void UpdateWheels(Wheel i_WheelModel)
        {
            for(int i = 0; i < this.m_Wheels.Length; i++)
            {
                this.m_Wheels[i] = i_WheelModel;
            }
        }

        public void UpdateModel(string i_Model)
        {
            this.m_Model = i_Model;
        }

        public abstract List<string> GetSpecificInfo();
    }
}
