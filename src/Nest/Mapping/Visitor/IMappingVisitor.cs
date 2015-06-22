namespace Nest.DSL.Visitor
{
	public interface IMappingVisitor
	{
		int Depth { get; set; }
		void Visit(RootObjectMapping mapping);
		void Visit(StringMapping mapping);
		void Visit(NumberMapping mapping);
		void Visit(DateMapping mapping);
		void Visit(BooleanMapping mapping);
		void Visit(BinaryMapping mapping);
		void Visit(ObjectMapping mapping);
		void Visit(NestedObjectMapping mapping);
		void Visit(MultiFieldMapping mapping);
		void Visit(IPMapping mapping);
		void Visit(GeoPointMapping mapping);
		void Visit(GeoShapeMapping mapping);
		void Visit(AttachmentMapping mapping);
	}
}