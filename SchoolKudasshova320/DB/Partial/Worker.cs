using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolKudasshova320.DB
{
    public partial class Worker
    {
        public bool IsChief
        {
            get
            {
                if (ID_Chief == ID)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
        }
    }
}
