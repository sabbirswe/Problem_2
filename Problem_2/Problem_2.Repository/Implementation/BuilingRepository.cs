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

namespace Problem_2.Repository.Implementation
{
    public class BuildingRepository : IBuildingRepository
    {
       private readonly AppDbContext context;

       



        public BuildingRepository(AppDbContext context)
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

    }
}
