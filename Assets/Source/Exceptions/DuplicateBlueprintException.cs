﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.Exceptions
{
    public class DuplicateBlueprintException : Exception
    {
        public DuplicateBlueprintException()
            : base("Duplicate blueprint added")
        {
        }
    }
}
