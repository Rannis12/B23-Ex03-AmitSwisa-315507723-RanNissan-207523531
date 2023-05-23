using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX03.GarageLogic.Exceptions
{
    class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException(float i_CurrentValue, float i_MaximumValue) :
            base($"The current value '{i_CurrentValue}' exceeds the maximum value '{i_MaximumValue}'.")
        {

        }
    }
}
