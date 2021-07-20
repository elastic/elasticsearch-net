namespace Playground
{
	public class Person
	{
		public Person(string name) => Name = name;
		public string Name { get; }
		public int? Age { get; init; }
		public string? Email { get; init; }
	}
}
