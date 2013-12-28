using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(TermConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class Term : IQuery
	{
		internal string Field { get; set; }
		internal object Value { get; set; }
		internal double? Boost { get; set; }

		bool IQuery.IsConditionless { get { return this.Value == null || this.Value.ToString().IsNullOrEmpty() || this.Field.IsNullOrEmpty(); } }	

		public Term()
		{
		}
	}
}
