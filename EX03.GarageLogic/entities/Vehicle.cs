﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX03.GarageLogic.entities.EnergySourceTypes;

namespace EX03.GarageLogic.entities
{
    public abstract class Vehicle
    {
        private string m_Model { get; }
        private readonly string r_LicenseNumber;
        private Wheel[] m_Wheels;
        protected EnergySource m_EnergySource;

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
    }
}