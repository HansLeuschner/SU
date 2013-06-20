using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cumis
{
    [Serializable]
    public class JavascriptExecption : Exception
    {
        public JavascriptExecption(string message) : base(message) { }
    }
}