using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX03.GarageLogic.entities
{
    public class GarageClient
    {
        private readonly string m_ClientName;
        private readonly string m_PhoneNumber;

        public GarageClient(string i_ClientName, string i_PhoneNumber)
        {
            m_ClientName = i_ClientName;
            m_PhoneNumber = i_PhoneNumber;
        }

        public string ClientName
        {
            get
            {
                return m_ClientName;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return m_PhoneNumber;
            }
        }
    }
}
