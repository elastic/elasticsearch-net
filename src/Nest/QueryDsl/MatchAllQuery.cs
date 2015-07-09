using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<MatchAllQuery>))]
	public interface IMatchAllQuery : IQuery
	{
		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

		[JsonProperty(PropertyName = "norm_field")]
		string NormField { get; set; }
	}

	public class MatchAllQuery : QueryBase, IMatchAllQuery
	{
		public double? Boost { get;  set; }
		public string NormField { get;  set; }

		bool IQuery.Conditionless => false;

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.MatchAllQuery = this;
		}
	}

	public class MatchAllQueryDescriptor
		: QueryDescriptorBase<MatchAllQueryDescriptor, IMatchAllQuery>
			, IMatchAllQuery 
	{
		bool IQuery.Conditionless => false;

		string IQuery.Name { get; set; }

		double? IMatchAllQuery.Boost { get; set; }

		string IMatchAllQuery.NormField { get; set; }

		public MatchAllQueryDescriptor Boost(double? boost) => Assign(a => a.Boost = boost);

		public MatchAllQueryDescriptor NormField(string normField) => Assign(a => a.NormField = normField);
	}
}
