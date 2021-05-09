using SEB.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEB.DAL.Interface
{
    public interface IAuditRepository: ICommonRepository
    {
        Task<int> Add(Audits entity);
    }
}
