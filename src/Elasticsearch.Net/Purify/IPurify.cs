using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUrify
{
    interface IPurifier
    {
        void Purify(Uri uri);
    }
}
