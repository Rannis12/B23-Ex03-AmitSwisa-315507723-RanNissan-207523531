﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX03.GarageLogic.entities
{
    class Client
    {
        private readonly string m_ClientName;
        private readonly string m_PhoneNumber;

        public Client(string i_ClientName, string i_PhoneNumber)
        {
            m_ClientName = i_ClientName;
            m_PhoneNumber = i_PhoneNumber;
        }
    }
}
