using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFApplication.Listeners
{
    public interface SingleBarInterface
    {
        void settotalnumber(int totalnumber);
        void settotalcurrent(int current);
        void progressend();
    }
}
