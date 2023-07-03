using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kb21_tools
{
    public interface IKbWindow
    {
        void ok(object mess);
        string GetConfig(string key);
    }
}
