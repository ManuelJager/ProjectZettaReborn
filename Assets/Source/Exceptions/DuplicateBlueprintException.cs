using System;

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