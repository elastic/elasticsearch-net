using System;

namespace Nest
{
	public interface IMappingVisitor
	{
		int Depth { get; set; }
		void Visit(ITypeMapping mapping);
#pragma warning disable 618
		void Visit(IStringProperty property);
#pragma warning restore 618
		void Visit(ITextProperty property);
		void Visit(IKeywordProperty property);
		void Visit(IDateProperty property);
		void Visit(IBooleanProperty property);
		void Visit(IBinaryProperty property);
		void Visit(IObjectProperty property);
		void Visit(INestedProperty property);
		void Visit(IIpProperty property);
		void Visit(IGeoPointProperty property);
		void Visit(IGeoShapeProperty property);
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
		void Visit(IJoinProperty property);
	}

	public class NoopMappingVisitor : IMappingVisitor
	{
		public virtual int Depth { get; set; }

		public virtual void Visit(ITypeMapping mapping) { }

#pragma warning disable 618
		public virtual void Visit(IStringProperty property ) { }
#pragma warning restore 618

		public virtual void Visit(ITextProperty property) { }

		public virtual void Visit(IKeywordProperty property) { }

		public virtual void Visit(IDateProperty property) { }

		public virtual void Visit(IBooleanProperty property) { }

		public virtual void Visit(IBinaryProperty property) { }

		public virtual void Visit(INumberProperty property) { }

		public virtual void Visit(IObjectProperty property) { }

		public virtual void Visit(INestedProperty property) { }

		public virtual void Visit(IIpProperty property) { }

		public virtual void Visit(IGeoPointProperty property) { }

		public virtual void Visit(IGeoShapeProperty property) { }

		public virtual void Visit(ICompletionProperty property) { }

		public virtual void Visit(IMurmur3HashProperty property) { }

		public virtual void Visit(ITokenCountProperty property) { }

		public virtual void Visit(IPercolatorProperty property) { }

		public virtual void Visit(IIntegerRangeProperty property) { }

		public virtual void Visit(IFloatRangeProperty property) { }

		public virtual void Visit(ILongRangeProperty property) { }

		public virtual void Visit(IDoubleRangeProperty property) { }

		public virtual void Visit(IDateRangeProperty property) { }

		public virtual void Visit(IJoinProperty property) { }
	}
}
