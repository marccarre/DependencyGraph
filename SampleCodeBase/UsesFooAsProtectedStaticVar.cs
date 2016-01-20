using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodeBase
{
    public class UsesFooAsProtectedStaticVar
    {
#pragma warning disable 169
        protected static Foo foo;
#pragma warning restore 169
    }
}
