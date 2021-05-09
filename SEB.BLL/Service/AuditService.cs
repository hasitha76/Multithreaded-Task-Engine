using SEB.BLL.DTO;
using SEB.DAL.Entity;
using SEB.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEB.BLL.Service
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;
        public AuditService(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public async Task<int> AddAudit(MarkDTO task)
        {
            try
            {

                var audit = new Audits
                {
                    Id = Guid.NewGuid(),
                    Who = task.Task.Id.ToString(),
                    Created = DateTime.Now
                };
                var list = ((Task<List<Marks>>)task.Task).Result;
                audit.Total = list.Sum(x => x.Score);
                audit.Filters = task.Filter;
                return await _auditRepository.Add(audit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }

        public async Task<int> GetRecordCount()
        {
            try
            {
                return await _auditRepository.GetRecordCount();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }

        public async Task<int> RemoveData()
        {            
            try
            {
                return await _auditRepository.Remove();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }

    }
}
