using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest.DSL
{
  public enum TermsStatsOrder
  {
    term = 0,
    reverse_term,
    count,
    reverse_count,
    total,
    reverse_total,
    min,
    reverse_min,
    max,
    reverse_max
  }
}
