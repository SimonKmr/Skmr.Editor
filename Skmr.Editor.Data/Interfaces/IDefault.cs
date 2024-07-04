using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Data.Interfaces
{
    public interface IDefault<T>
    {
        public abstract static T GetDefault();
    }
}
