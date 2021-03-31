using AutoMapper;
using Problem_2.Database;
using Problem_2.Database.Model;
using Problem_2.Repository.Interface;
using Problem_2.Utility;
using Problem_2.Utility.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Problem_2.Repository.Implementation
{
    public class ReadingRepository : IReadingRepository
    {
        private readonly AppDbContext context;

        public ReadingRepository(AppDbContext context)
        {
            this.context = context;
        }

        public List<SelectModel> BuildingSelectModels()
        {
            var selectModel = context.Building.ToList();
            return selectModel.OrderBy(x => x.Name).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.Id
            }).ToList();
        }

        public void GenerateSampleData()
        {
            var truncateQuery = "truncate table Reading truncate table Building truncate table DataField truncate table [Object] ";
            context.Database.ExecuteSqlRaw(truncateQuery);

            for (byte i = 1; i <= 100; i++)
            {
                Building building = new Building();
                building.Id = i;
                building.Name = "Building" + i;
                context.Building.Add(building);
                Objects objects = new Objects();
                objects.Id = i;
                objects.Name = "Object" + i;
                context.Objects.Add(objects);

                DataField dataField = new DataField();
                dataField.Id = i;
                dataField.Name = "DataField" + i;
                context.DataField.Add(dataField);
                int save = context.SaveChanges();
                if (save > 0)
                {
                    for (int j = 1; j <= 1051200; j++)
                    {
                        for (int k = 1; k <= 10; k++)
                        {
                            Reading r = new Reading();
                            r.BuildingId = i;
                            r.DataFieldId = Convert.ToByte(i);
                            r.ObjectId = Convert.ToByte(i);
                            Random rnd = new Random();

                            DateTime currentDate = Convert.ToDateTime("26 Mar 2021");
                            currentDate = currentDate.Add(new TimeSpan(0, j - 1, 0));
                            var saveQuery = "insert into Reading Values(" + r.BuildingId + "," + r.ObjectId + "," + r.DataFieldId + "," + rnd.NextDouble() + ",'" + currentDate.ToString("yyyy-MM-dd HH:mm") + "')";
                            context.Database.ExecuteSqlRaw(saveQuery);
                        }
                    }
                }

            }

        }

        public object GenerateTimeSeriesReport(Int16? buildingId, byte? objectId, byte? datafildId, DateTime startDate, DateTime endDate)
        {
            int topValue = 14400;
            var differece = endDate.Date - startDate.Date;
            if (differece.Days > 0)
            {
                topValue = topValue * (differece.Days+1);
            }

            var query = "select top " + topValue + "  * from Reading where BuildingId=" + buildingId + " and ObjectId=" + objectId + " and DataFieldId=" + datafildId + " and convert(date,TimeStamp) between '" + startDate + "' and '" + endDate + "'";
            var data = context.Reading.FromSqlRaw(query);

            StringBuilder sb = new StringBuilder("{'report':[");

            foreach (var item in data)
            {
                sb.Append("[" + (long)(item.TimeStamp - new DateTime(1970, 1, 1)).TotalMilliseconds + "," + item.Value + "],");
            }
            sb.Append("]}");

            var jsonData = JObject.Parse(sb.ToString());

            return jsonData;
        }

    }
}
