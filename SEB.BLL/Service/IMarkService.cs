using SEB.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEB.BLL.Service
{
    public interface IMarkService: ICommanService
    {
        Task<int> Add(int min, int max);
        Task<List<Marks>> Get(string filter);
    }
}
