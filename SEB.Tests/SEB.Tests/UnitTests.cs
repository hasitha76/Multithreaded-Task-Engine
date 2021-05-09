using Moq;
using NUnit.Framework;
using SEB.BLL.DTO;
using SEB.BLL.Service;
using SEB.DAL.Interface;
using SEB.DAL.Repository;
using System.Linq;

namespace SEB.Tests
{
    public class Tests
    {
        IMarkRepository _testmarkRepository;
        IAuditRepository _testAuditRepository;
        IMarkService _testMarkService;
        IAuditService _testAuditService;

        [SetUp]
        public void Setup()
        {
            _testmarkRepository = new MarkRepository();
            _testAuditRepository = new AuditRepository();
            _testMarkService = new MarkService(_testmarkRepository);
            _testAuditService = new AuditService(_testAuditRepository);

        }

        [Test]
        public void adding_randomdata()
        {
            var result = _testMarkService.Add(1,100);

            Assert.AreEqual(result.Result, 1);
        }

        [Test]
        public void get_marks()
        {
            _testMarkService.Add(101,110);
            var result = _testMarkService.Get("101,110");

            Assert.AreEqual(result.Result.Count(), 1);
        }

        [Test]
        public void adding_auditdata()
        {
            MarkDTO dto = new MarkDTO();
            dto.Task = _testMarkService.Get("1,10");
            dto.Filter = "1,10";
            var result = _testAuditService.AddAudit(dto);

            Assert.AreEqual(result.Result, 1);
        }

        [Test]
        public void clear_tabledata()
        {
            _testMarkService.RemoveData();
            _testAuditService.RemoveData();

            var recordCountMarks = _testMarkService.GetRecordCount();
            var recordCountAudits = _testAuditService.GetRecordCount();

            Assert.AreEqual(recordCountMarks.Result, 0);
            Assert.AreEqual(recordCountAudits.Result, 0);
        }
    }
}