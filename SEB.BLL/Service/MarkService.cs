using SEB.DAL.Entity;
using SEB.DAL.Interface;
using SEB.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEB.BLL.Service
{
    public class MarkService : IMarkService
    {
        private readonly IMarkRepository _markRepository;
        public MarkService(IMarkRepository markRepository)
        {
            _markRepository = markRepository;
        }

        public async Task<int> Add(int min, int max)
        {
            try
            {
                Random rnd = new Random();
                var mark = new Marks
                {
                    Id = Guid.NewGuid(),
                    Score = rnd.Next(min, max)
                };

                return await _markRepository.Add(mark);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return  0;
            }
        }

        public async Task<List<Marks>> Get(string filter)
        {
            try
            {

                List<int> scoresFilter = filter.Split(',').Select(int.Parse).ToList();
                var scores = await _markRepository.Get(scoresFilter[0], scoresFilter[1]);
                return scores.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Marks>();
            }
        }

        public async Task<int> GetRecordCount()
        {
            try
            {
                return await _markRepository.GetRecordCount();
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
                return await _markRepository.Remove();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

    }
}
