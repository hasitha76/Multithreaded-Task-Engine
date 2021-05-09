using SEB.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEB.DAL.Interface
{
    public interface IMarkRepository: ICommonRepository
    {
        Task<IEnumerable<Marks>> Get(int min,int max);
        Task<int> Add(Marks entity);

    }
}
