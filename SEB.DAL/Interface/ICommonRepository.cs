using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEB.DAL.Interface
{
    public interface ICommonRepository
    {
        Task<int> GetRecordCount();
        Task<int> Remove();
    }
}
