using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMM.Core.Data;

namespace PMM.Core
{
    public interface IWorkContext
    {
        UserDetail CurrentUser { get; set; }
    }
}
