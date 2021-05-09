using Dapper;
using SEB.DAL.Entity;
using SEB.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SEB.DAL.Repository
{
    public class AuditRepository : DBBaseConnection, IAuditRepository
    {
        public async Task<int> Add(Audits entity)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = new
                {
                    Id = entity.Id,
                    Total = entity.Total,
                    Who = entity.Who,
                    Created = entity.Created,
                    Filters = entity.Filters
                };

                cn.Open();
                var result = cn.ExecuteAsync(
                    "INSERT INTO Audits (Id, Total,Who,Created,Filters) VALUES(@Id,@Total,@Who,@Created,@Filters)",
                    parameters);

                return await result;
            }
        }

        public async Task<int> GetRecordCount()
        {
            using (IDbConnection cn = Connection)
            {
                var result = cn.QuerySingleAsync<int>("SELECT count(*) FROM Audits");

                return await result;
            }
        }

        public async Task<int> Remove()
        {
            using (IDbConnection cn = Connection)
            {

                cn.Open();
                var result = cn.ExecuteAsync("DELETE from Audits");

                return await result;
            }
        }

    }
}
