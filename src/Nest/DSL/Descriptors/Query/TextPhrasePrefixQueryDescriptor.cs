using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
	/// <summary>
	/// A Query that matches documents containing a particular sequence of terms.
	/// It allows for prefix matches on the last term in the text.
	/// </summary>
	/// <typeparam name="T">Type of document</typeparam>
	public class TextPhrasePrefixQueryDescriptor<T> : TextQueryDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "type")]
		internal override string _Type { get { return "phrase_prefix"; } }
	}
}
