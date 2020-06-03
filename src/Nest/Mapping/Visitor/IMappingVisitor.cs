// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public interface IMappingVisitor
	{
		int Depth { get; set; }

		void Visit(ITypeMapping mapping);

		void Visit(ITextProperty property);

		void Visit(IKeywordProperty property);

		void Visit(IDateProperty property);

		void Visit(IDateNanosProperty property);

		void Visit(IBooleanProperty property);

		void Visit(IBinaryProperty property);

		void Visit(IObjectProperty property);

		void Visit(INestedProperty property);

		void Visit(IIpProperty property);

		void Visit(IGeoPointProperty property);

		void Visit(IGeoShapeProperty property);

		void Visit(IShapeProperty property);

		void Visit(IPointProperty property);

		void Visit(INumberProperty property);

		void Visit(ICompletionProperty property);

		void Visit(IMurmur3HashProperty property);

		void Visit(ITokenCountProperty property);

		void Visit(IPercolatorProperty property);

		void Visit(IIntegerRangeProperty property);

		void Visit(IFloatRangeProperty property);

		void Visit(ILongRangeProperty property);

		void Visit(IDoubleRangeProperty property);

		void Visit(IDateRangeProperty property);

		void Visit(IIpRangeProperty property);

		void Visit(IJoinProperty property);

		void Visit(IRankFeatureProperty property);

		void Visit(IRankFeaturesProperty property);

		void Visit(ISearchAsYouTypeProperty property);

		void Visit(IFlattenedProperty property);

		void Visit(IHistogramProperty property);

		void Visit(IConstantKeywordProperty property);
	}

	public class NoopMappingVisitor : IMappingVisitor
	{
		public virtual int Depth { get; set; }

		public virtual void Visit(ITypeMapping mapping) { }

		public virtual void Visit(ITextProperty property) { }

		public virtual void Visit(IKeywordProperty property) { }

		public virtual void Visit(IDateProperty property) { }

		public virtual void Visit(IDateNanosProperty property) { }

		public virtual void Visit(IBooleanProperty property) { }

		public virtual void Visit(IBinaryProperty property) { }

		public virtual void Visit(IPointProperty property) { }

		public virtual void Visit(INumberProperty property) { }

		public virtual void Visit(IObjectProperty property) { }

		public virtual void Visit(INestedProperty property) { }

		public virtual void Visit(IIpProperty property) { }

		public virtual void Visit(IGeoPointProperty property) { }

		public virtual void Visit(IGeoShapeProperty property) { }

		public virtual void Visit(IShapeProperty property) { }

		public virtual void Visit(ICompletionProperty property) { }

		public virtual void Visit(IMurmur3HashProperty property) { }

		public virtual void Visit(ITokenCountProperty property) { }

		public virtual void Visit(IPercolatorProperty property) { }

		public virtual void Visit(IIntegerRangeProperty property) { }

		public virtual void Visit(IFloatRangeProperty property) { }

		public virtual void Visit(ILongRangeProperty property) { }

		public virtual void Visit(IDoubleRangeProperty property) { }

		public virtual void Visit(IDateRangeProperty property) { }

		public virtual void Visit(IIpRangeProperty property) { }

		public virtual void Visit(IJoinProperty property) { }

		public virtual void Visit(IRankFeatureProperty property) { }

		public virtual void Visit(IRankFeaturesProperty property) { }

		public virtual void Visit(ISearchAsYouTypeProperty property) { }

		public virtual void Visit(IFlattenedProperty property) { }

		public virtual void Visit(IHistogramProperty property) { }

		public virtual void Visit(IConstantKeywordProperty property) { }
	}
}
