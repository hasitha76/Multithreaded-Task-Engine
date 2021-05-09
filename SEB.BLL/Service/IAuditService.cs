using SEB.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEB.BLL.Service
{
    public interface IAuditService: ICommanService
    {
        Task<int> AddAudit(MarkDTO task);
    }
}
