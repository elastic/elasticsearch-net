using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class Range<T> where T : struct
  {
    [JsonProperty(PropertyName = "from")]
    internal T? _From { get; set; }

    [JsonProperty(PropertyName = "to")]
    internal T? _To { get; set; }
	  
	[JsonProperty(PropertyName = "key")]
    internal string _Key { get; set; }

    public Range<T> Key(string key)
    {
      this._Key = key;
      return this;
    }
    public Range<T> From(T value)
    {
      this._From = value;
      return this;
    }
    public Range<T> To(T value)
    {
      this._To = value;
      return this;
    }
  }
}
