namespace Nest
{
	public interface IMappingVisitor
	{
		int Depth { get; set; }
		void Visit(TypeMapping mapping);
		void Visit(StringProperty mapping);
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
	}
}