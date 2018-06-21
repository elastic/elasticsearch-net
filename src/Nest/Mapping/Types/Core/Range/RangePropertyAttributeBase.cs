using System;
using Elasticsearch.Net;

namespace Nest
{
	public abstract class RangePropertyAttributeBase : ElasticsearchDocValuesPropertyAttributeBase, IRangeProperty
	{
		private IRangeProperty Self => this;

		protected RangePropertyAttributeBase(RangeType type) : base(type.ToFieldType()) { }

		public bool Index { get => Self.Index.GetValueOrDefault(); set => Self.Index = value; }
		public double Boost { get => Self.Boost.GetValueOrDefault(); set => Self.Boost = value; }
		public bool Coerce { get => Self.Coerce.GetValueOrDefault(); set => Self.Coerce = value; }

		bool? IRangeProperty.Coerce { get; set; }
		double? IRangeProperty.Boost { get; set; }
		bool? IRangeProperty.Index { get; set; }
	}
}
