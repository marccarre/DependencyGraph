using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodeBase
{
    public class IndirectlyUsesFooAsTempVar
    {
        public void bar()
        {
            UsesFooAsTempVar foo = new UsesFooAsTempVar();
        }
    }
}
