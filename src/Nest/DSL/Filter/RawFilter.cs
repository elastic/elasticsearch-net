using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(CustomJsonConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[Obsolete("Never used and scheduled to be removed in 2.0")]
	public class RawFilter : IQuery, ICustomJson
	{
		internal object Json { get; set; }
		bool IQuery.IsConditionless { get { return this.Json == null || this.Json.ToString().IsNullOrEmpty(); } }
		
		string IQuery.Name { get; set;  }

		object ICustomJson.GetCustomJson()
		{
			return this.Json;
		}
	}
}
