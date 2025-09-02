using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionGrade.Core.Entities
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Failed
    }
}
