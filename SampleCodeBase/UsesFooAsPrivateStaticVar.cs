using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodeBase
{
    public class UsesFooAsPrivateStaticVar
    {
#pragma warning disable 169
        private static Foo foo;
#pragma warning restore 169
    }
}
