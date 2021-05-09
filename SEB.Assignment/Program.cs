using Microsoft.Extensions.DependencyInjection;
using SEB.BLL.DTO;
using SEB.BLL.Service;
using SEB.DAL.Interface;
using SEB.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace SEB.Assignment
{
    class Program
    {
        static async Task Main(string[] args)
        {

            string TestXml =
        "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +

          "<Tasks>" +
              "<Task name='Add Random Data'>" +
                    "<Count no='1'>" +
                    "</Count>" +
                    "<Count no='2'>" +
                    "</Count>" +
              "</Task>" +
              "<Task name='Calculate Data'>" +
                  "<Scenario>" +
                      "<Min>1</Min>" +
                      "<Max>10</Max>" +
                  "</Scenario>" +
                  "<Scenario>" +
                      "<Min>61</Min>" +
                      "<Max>70</Max>" +
                  "</Scenario>" +
              "</Task>" +
              "<Task name='Add Audit Data'>" +
              "</Task>" +
              "<Task name='Clear Data'>" +
              "</Task>" +
          "</Tasks>";


            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddTransient<IMarkRepository, MarkRepository>()
                .AddTransient<IAuditRepository, AuditRepository>()
                .AddTransient<IMarkService, MarkService>()
                .AddTransient<IAuditService, AuditService>()
                .BuildServiceProvider();

            var markService = serviceProvider.GetService<IMarkService>();
            var auditService = serviceProvider.GetService<IAuditService>();
            List<MarkDTO> auditTasks = new List<MarkDTO>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(TestXml);

            string xpath = "Tasks/Task";
            var nodes = xmlDoc.SelectNodes(xpath);

            foreach (XmlNode childrenNode in nodes)
            {
                var task = childrenNode.Attributes[0].Value;

                // 1.add random scores to db
                if (task == "Add Random Data")
                {
                    List<Task> tasks = new List<Task>();
                    foreach (var item in childrenNode.ChildNodes)
                    {
                        tasks.Add(markService.Add(1, 100));
                    }
                    await Task.WhenAll(tasks);
                    Console.WriteLine("Added Random Numbers.");
                }
                // 2.get scores based on filter
                else if (task == "Calculate Data")
                {
                    foreach (var item in childrenNode.ChildNodes)
                    {
                        var childNode = ((System.Xml.XmlNode)item).ChildNodes;
                        var min = childNode.Item(0).InnerText;
                        var max = childNode.Item(1).InnerText;
                        auditTasks.Add(new MarkDTO { Task = markService.Get(min+","+max), Filter = min + "," + max });
                    }
                    
                    // Run the tasks in parallel, and
                    // wait until all have been run
                    await Task.WhenAll(auditTasks.Select(x=>x.Task));
                    Console.WriteLine("Get Marks based on Filter.");
                }
                // 3.calculate and save with more details to the databse
                else if (task == "Add Audit Data")
                {
                    foreach (var t in auditTasks)
                    {
                        await auditService.AddAudit(t);
                    }
                    Console.WriteLine("Added Audit Data.");
                }
                // 4.clear tables
                else if (task == "Clear Data")
                {
                    //await markService.RemoveData();
                    //await auditService.RemoveData();
                    Console.WriteLine("Clear table Data.");
                }

            }

        }
    }
}
