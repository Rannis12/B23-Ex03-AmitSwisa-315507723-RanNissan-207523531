using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX03.GarageLogic.entities
{
    public class GarageForm
    {
        public enum eServiceStatus
        {
            InRepair,
            Fixed,
            Payed
        }

        private eServiceStatus m_ServiceStatus = eServiceStatus.InRepair;
        private readonly GarageClient r_Client;
        private readonly Vehicle r_Vehicle;
        
        public GarageForm(Vehicle i_Vehicle, GarageClient i_Client)
        {
            r_Vehicle = i_Vehicle;
            r_Client = i_Client;
        }

        public Vehicle Vehicle
        {
            get
            {
                return r_Vehicle;
            }
        }

        public GarageClient Client
        {
            get
            {
                return r_Client;
            }
        }

        public eServiceStatus ServiceStatus
        {
            get
            {
                return m_ServiceStatus;
            }
            set
            {
                m_ServiceStatus = value;
            }
        }
    }
}
