using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFApplication.Listeners
{
    public interface ProgressBarListeners
    {
        void settotalnumber(int totalnumber);

        void setsubnumber(int totalnumber);
        void settotalcurrent(int current, string files);

        void setsubcurrent(int current);

        void progressend();
    }
}
