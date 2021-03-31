﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_2.Database.Model
{
    [Keyless]
    public class Reading
    {
        
        public Int16 BuildingId { get; set; }

        public byte ObjectId { get; set; }

        public byte DataFieldId { get; set; }

        [Column(TypeName="decimal(18,2)")]
        public decimal Value { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime TimeStamp { get; set; }

        
    }
}
