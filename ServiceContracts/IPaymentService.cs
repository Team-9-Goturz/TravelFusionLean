using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ServiceContracts
{
    public interface IPaymentService: ICrudService<Payment>
    {
    }
}
