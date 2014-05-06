using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurifyNet
{
    internal interface IPurifier
    {
        void Purify(Uri uri);
    }
}
