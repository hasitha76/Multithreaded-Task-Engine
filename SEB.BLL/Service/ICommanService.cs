using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEB.BLL.Service
{
    public interface ICommanService
    {
        Task<int> GetRecordCount();
        Task<int> RemoveData();
    }
}
