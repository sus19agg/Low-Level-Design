﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Interfaces
{
    internal interface IFIleOperations
    {
        string GetContent();
        void WriteContent(string content);
    }
}
