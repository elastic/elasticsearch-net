namespace ApiGenerator.Domain.Code.HighLevel.Methods 
{
	public class HighLevelModel
	{
		public CsharpNames CsharpNames { get; set; }
		public FluentMethod Fluent { get; set; }
		public BoundFluentMethod FluentBound { get; set; }
		public InitializerMethod Initializer { get; set; }

	}
}