﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Problem_2.Utility;
using Problem_2.Utility.Model;

namespace Problem_2.Repository.Interface
{
    public interface IDataFieldRepository
    {
        List<SelectModel> DataFieldSelectModels();
    }
}
