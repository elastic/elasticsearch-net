using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
  public enum RegexFlags
  {
    CANNON_EQ,
    CASE_INSENSITIVE,
    COMMENTS,
    DOTALL,
    LITERAL,
    MULTILINE,
    UNICODE_CASE,
    UNIX_LINES
  }
}
