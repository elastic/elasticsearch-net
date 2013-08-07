using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
  public enum TermsOrder
  {
    count = 0,
    term,
    reverse_count,
    reverse_term
  }
}
