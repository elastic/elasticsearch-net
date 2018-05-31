using System;
using System.Reflection;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.Troubleshooting
{
	public class DebuggerDisplayTests
	{
		private string DebugFor<T>(T o)
		{
			var property = o.GetType().GetProperty("DebugDisplay", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
			var getter = property.GetGetMethod(true);
			var get = (Func<T, string>) getter.CreateDelegate(typeof(Func<T, string>));
			return get(o);
		}

        [U] public void Field()
		{
			var fieldFromString = new Field("name^2");
			var fieldFromExpressionWithBoost = Infer.Field<Project>(p=>p.Name, 14);
			var propertyInfoField = Infer.Field(typeof(Project).GetProperty(nameof(Project.Name)));

			DebugFor(fieldFromString).Should().Be("name^2");
			DebugFor(fieldFromExpressionWithBoost).Should().Be("p => p.Name^14 typeof: Project");
			DebugFor(propertyInfoField).Should().Be("PropertyInfo: Name typeof: Project");
		}
        [U] public void FieldsDebug()
        {
	        Nest.Fields fromString = "field1, field2";
	        Nest.Fields multiple = Infer.Field("somefield").And<Project>(p=>p.Description, 12);

			DebugFor(fromString).Should().Be($"Count: 2 [(1: field1),(2: field2)]");
			DebugFor(multiple).Should().Be($"Count: 2 [(1: somefield),(2: p => p.Description^12 typeof: {nameof(Project)})]");
		}

        [U] public void PropertyName()
		{
			var fieldFromString = new PropertyName("name");
			var fieldFromExpressionWithBoost = Infer.Property<Project>(p=>p.Name);
			PropertyName propertyInfoField = typeof(Project).GetProperty(nameof(Project.Name));

			DebugFor(fieldFromString).Should().Be("name");
			DebugFor(fieldFromExpressionWithBoost).Should().Be("p => p.Name typeof: Project");
			DebugFor(propertyInfoField).Should().Be("PropertyInfo: Name typeof: Project");
		}

        [U] public void ActionIds()
		{
			ActionIds fromString = "a,b,  c,d    ";
			ActionIds fromStringArray = new [] {"a", "b"};

			DebugFor(fromString).Should().Be("a,b,c,d");
			DebugFor(fromStringArray).Should().Be("a,b");
		}

        [U] public void NodeIds()
		{
			NodeIds fromString = "a,b,c,d";
			DebugFor(fromString).Should().Be("a,b,c,d");
		}

        [U] public void Name()
		{
			Name fromString = "somename";
			DebugFor(fromString).Should().Be("somename");
		}

        [U] public void Names()
		{
			Name name = "somename";
			Names fromString = "somename";
			Names fromName = name;
			DebugFor(fromString).Should().Be("somename");
			DebugFor(fromName).Should().Be("somename");
		}

        [U] public void Id()
		{
			Id fromString = "myid";
			Id fromLong = 1337;
			var guid = Guid.NewGuid();
			Id fromGuid = guid;
			var fromInstance = Infer.Id(new Project { Name = "hello"});

			DebugFor(fromString).Should().Be("myid");
			DebugFor(fromLong).Should().Be("1337");
			DebugFor(fromGuid).Should().Be(guid.ToString());
			DebugFor(fromInstance).Should().Be("Id from instance typeof: Project");
		}

        [U] public void IndexName()
		{
			IndexName fromString = "index-1";
			var fromType = Infer.Index<Project>();

			DebugFor(fromString).Should().Be("index-1");
			DebugFor(fromType).Should().Be("IndexName for typeof: Project");
		}

        [U] public void IndicesDebug()
        {
	        Nest.Indices all = Nest.Indices.All;
	        Nest.Indices fromTypeName = Infer.Index<Project>();
	        Nest.Indices fromType = typeof(CommitActivity);
	        Nest.Indices multiple = Infer.Index("someindex").And<Project>();

			DebugFor(all).Should().Be("_all");
			DebugFor(fromTypeName).Should().Be($"Count: 1 [(1: IndexName for typeof: {nameof(Project)})]");
			DebugFor(fromType).Should().Be($"Count: 1 [(1: IndexName for typeof: {nameof(CommitActivity)})]");
			DebugFor(multiple).Should().Be($"Count: 2 [(1: someindex),(2: IndexName for typeof: {nameof(Project)})]");
		}

        [U] public void TypeName()
		{
			TypeName fromString = "index-1";
			var fromType = Infer.Type<Project>();

			DebugFor(fromString).Should().Be("index-1");
			DebugFor(fromType).Should().Be("TypeName for typeof: Project");
		}

        [U] public void TypesDebug()
        {
	        Types all = Types.All;
	        Types fromTypeName = Infer.Type<Project>();
	        Types fromType = typeof(CommitActivity);
	        Types multiple = Infer.Type("sometype").And<Project>();

			DebugFor(all).Should().Be("_all");
			DebugFor(fromTypeName).Should().Be($"Count: 1 [(1: TypeName for typeof: {nameof(Project)})]");
			DebugFor(fromType).Should().Be($"Count: 1 [(1: TypeName for typeof: {nameof(CommitActivity)})]");
			DebugFor(multiple).Should().Be($"Count: 2 [(1: sometype),(2: TypeName for typeof: {nameof(Project)})]");
		}

        [U] public void TaskId()
		{
			TaskId fromString = "node-1:12";
			DebugFor(fromString).Should().Be("node-1:12");
		}

		[U]
		public void MappingProperties()
		{
			var nested = new NestedProperty() {Name = "hello"};
			var ip = new IpPropertyDescriptor<Project>().Name("field").Boost(2);

			DebugFor(nested).Should().StartWith("Type: nested");
			DebugFor(ip).Should().StartWith("Type: ip");
		}
	}
}
