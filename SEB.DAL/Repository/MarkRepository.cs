using Dapper;
using Microsoft.Extensions.Configuration;
using SEB.DAL.Entity;
using SEB.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEB.DAL.Repository
{
    public class MarkRepository : DBBaseConnection, IMarkRepository
    {
        public async Task<int> Add(Marks entity)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = new
                {
                    Id = entity.Id,
                    Score = entity.Score
                };

                cn.Open();
                var result = cn.ExecuteAsync(
                    "INSERT INTO Marks (Id, Score) VALUES(@Id,@Score)",
                    parameters);

                return await result;
            }
        }

        public async Task<IEnumerable<Marks>> Get(int min,int max)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = new
                {
                    Min = min,
                    Max = max
                };
                var result = cn.QueryAsync<Marks>("SELECT * FROM Marks where Score>=@Min and Score<=@Max", parameters);

                return await result;
            }
        }

        public async Task<int> GetRecordCount()
        {
            using (IDbConnection cn = Connection)
            {
                var result = cn.QuerySingleAsync<int>("SELECT count(*) FROM Marks");

                return await result;
            }
        }

        public async Task<int> Remove()
        {
            using (IDbConnection cn = Connection)
            {

                cn.Open();
                var result = cn.ExecuteAsync("DELETE from Marks");

                return await result;
            }
        }


    }
}
