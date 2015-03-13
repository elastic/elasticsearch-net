using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<IdsQueryDescriptor>))]
	public interface IIdsQuery : IQuery
	{
		[JsonProperty(PropertyName = "type")]
		IEnumerable<string> Type { get; set; }

		[JsonProperty(PropertyName = "values")]
		IEnumerable<string> Values { get; set; }
	}
	
	public class IdsQuery : PlainQuery, IIdsQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Ids = this;
		}

		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public IEnumerable<string> Type { get; set; }
		public IEnumerable<string> Values { get; set; }
	}

	public class IdsQueryDescriptor : IIdsQuery
	{
		[JsonProperty(PropertyName = "_name")]
		public string Name { get; set; }
		[JsonProperty(PropertyName = "type")]
		public IEnumerable<string> Type { get; set; }
		[JsonProperty(PropertyName = "values")]
		public IEnumerable<string> Values { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !this.Values.HasAny() || this.Values.All(s=>s.IsNullOrEmpty());
			}
		}
	}
}
