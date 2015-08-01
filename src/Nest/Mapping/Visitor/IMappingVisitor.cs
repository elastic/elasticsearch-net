namespace Nest.DSL.Visitor
{
	public interface IMappingVisitor
	{
		int Depth { get; set; }
		void Visit(RootObjectType mapping);
		void Visit(StringType mapping);
		void Visit(NumberTypeName mapping);
		void Visit(DateType mapping);
		void Visit(BooleanType mapping);
		void Visit(BinaryType mapping);
		void Visit(ObjectType mapping);
		void Visit(NestedType mapping);
		void Visit(IpType mapping);
		void Visit(GeoPointType mapping);
		void Visit(GeoShapeType mapping);
		void Visit(AttachmentType mapping);
		void Visit(NumberType mapping);
	}
}