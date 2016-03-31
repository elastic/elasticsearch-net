using System;

namespace Nest
{
	public interface IMappingVisitor
	{
		int Depth { get; set; }
		void Visit(TypeMapping mapping);
		void Visit(StringProperty property);
		void Visit(TextProperty property);
		void Visit(KeywordProperty property);
		void Visit(DateProperty property);
		void Visit(BooleanProperty property);
		void Visit(BinaryProperty property);
		void Visit(ObjectProperty property);
		void Visit(NestedProperty property);
		void Visit(IpProperty property);
		void Visit(GeoPointProperty property);
		void Visit(GeoShapeProperty property);
		void Visit(AttachmentProperty property);
		void Visit(NumberProperty property);
		void Visit(CompletionProperty property);
		void Visit(Murmur3HashProperty property);
		void Visit(TokenCountProperty property);
	}

	public class NoopMappingVisitor : IMappingVisitor
	{
		public virtual int Depth { get; set; }

		public virtual void Visit(TypeMapping mapping) { }

		public virtual void Visit(StringProperty property ) { }

		public virtual void Visit(TextProperty property) { }

		public virtual void Visit(KeywordProperty property) { }

		public virtual void Visit(DateProperty property) { }

		public virtual void Visit(BooleanProperty property) { }

		public virtual void Visit(BinaryProperty property) { }

		public virtual void Visit(NumberProperty property) { }

		public virtual void Visit(ObjectProperty property) { }

		public virtual void Visit(NestedProperty property) { }

		public virtual void Visit(IpProperty property) { }

		public virtual void Visit(GeoPointProperty property) { }

		public virtual void Visit(GeoShapeProperty property) { }

		public virtual void Visit(AttachmentProperty property) { }

		public virtual void Visit(CompletionProperty property) { }

		public virtual void Visit(Murmur3HashProperty property) { }

		public virtual void Visit(TokenCountProperty property) { }
	}
}
