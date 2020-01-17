using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.MVVM.Core
{
    public interface IInitializable<T>
    {
        void Initialize(T value);
    }
}
