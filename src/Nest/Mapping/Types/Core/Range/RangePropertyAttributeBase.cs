using System;
using Elasticsearch.Net;

namespace Nest
{
	public abstract class RangePropertyAttributeBase : ElasticsearchDocValuesPropertyAttributeBase, IRangeProperty
	{
		private IRangeProperty Self => this;

		protected RangePropertyAttributeBase(RangeType type) : base(type.ToFieldType()) { }

		public bool Index { get { return Self.Index.GetValueOrDefault(); } set { Self.Index = value; } }
		public double Boost { get { return Self.Boost.GetValueOrDefault(); } set { Self.Boost = value; } }
		/// <remarks>Removed in 6.x</remarks>
		public bool IncludeInAll { get { return Self.IncludeInAll.GetValueOrDefault(); } set { Self.IncludeInAll = value; } }
		public bool Coerce { get { return Self.Coerce.GetValueOrDefault(); } set { Self.Coerce = value; } }

		bool? IRangeProperty.Coerce { get; set; }
		double? IRangeProperty.Boost { get; set; }
		/// <remarks>Removed in 6.x</remarks>
		bool? IRangeProperty.IncludeInAll { get; set; }
		bool? IRangeProperty.Index { get; set; }
	}
}
