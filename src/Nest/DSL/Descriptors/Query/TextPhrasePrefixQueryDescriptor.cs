using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
	public class TextPhrasePrefixQueryDescriptor<T> : TextQueryDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "type")]
		internal override string _Type { get { return "text_phrase_prefix"; } }
	}
}
