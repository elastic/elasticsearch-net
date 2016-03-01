using System;

namespace Nest
{
	public interface IMappingVisitor
	{
		int Depth { get; set; }
		void Visit(TypeMapping mapping);
		void Visit(StringProperty mapping);
		[Obsolete("Use Visit(NumberProperty mapping) for number mappings")]
		void Visit(NumberType mapping);
		void Visit(DateProperty mapping);
		void Visit(BooleanProperty mapping);
		void Visit(BinaryProperty mapping);
		void Visit(ObjectProperty mapping);
		void Visit(NestedProperty mapping);
		void Visit(IpProperty mapping);
		void Visit(GeoPointProperty mapping);
		void Visit(GeoShapeProperty mapping);
		void Visit(AttachmentProperty mapping);
		void Visit(NumberProperty mapping);
		void Visit(CompletionProperty mapping);
		void Visit(Murmur3HashProperty mapping);
		void Visit(TokenCountProperty mapping);
	}

	public class NoopMappingVisitor : IMappingVisitor
	{
		public virtual int Depth { get; set; }

		public virtual void Visit(TypeMapping mapping)
		{
		}

		public virtual void Visit(StringProperty mapping)
		{
		}

		[Obsolete("Use Visit(NumberProperty mapping) for number mappings")]
		public virtual void Visit(NumberType mapping)
		{
		}

		public virtual void Visit(DateProperty mapping)
		{
		}

		public virtual void Visit(BooleanProperty mapping)
		{
		}

		public virtual void Visit(BinaryProperty mapping)
		{
		}

		public virtual void Visit(ObjectProperty mapping)
		{
		}

		public virtual void Visit(NestedProperty mapping)
		{
		}

		public virtual void Visit(IpProperty mapping)
		{
		}

		public virtual void Visit(GeoPointProperty mapping)
		{
		}

		public virtual void Visit(GeoShapeProperty mapping)
		{
		}

		public virtual void Visit(AttachmentProperty mapping)
		{
		}

		public virtual void Visit(NumberProperty mapping)
		{
		}

		public virtual void Visit(CompletionProperty mapping)
		{
		}

		public virtual void Visit(Murmur3HashProperty mapping)
		{
		}

		public virtual void Visit(TokenCountProperty mapping)
		{
		}
	}
}