using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFee20190822.Repository
{
    public interface ICheckInFeeRepository
    {
        decimal GetFee(Customer customer);
    }
}
