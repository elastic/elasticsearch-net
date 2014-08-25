using System;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.DSL.Visitor;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Mapping
{
	[TestFixture]
	public class MappingVisitorTests : IntegrationTests
	{
		[Test]
		public void VisitorToPrettyPrintMapping()
		{
			var mapping = this.Client.GetMapping<ElasticsearchProject>();
			var visitor = new PrettyPrintMappingVisitor();
			mapping.Accept(visitor);
			Assert.Pass(visitor.ToString());
		}

		public class PrettyPrintMappingVisitor : IMappingVisitor
		{
			private readonly StringBuilder _stringBuilder = new StringBuilder(); 

			public int Depth { get; set; }

			private void PrettyPrint(IElasticType type)
			{
				var indent = new string('-', (this.Depth + 1) * 2);
				var typeName = type.Type != null ? type.Type.Name : "root";
				var name = type.Name != null ? type.Name.Name : "{default}";

				var s = "{0} {1}: {2}".F(indent, name, typeName);
				this._stringBuilder.AppendLine(s);
			}

			public void Visit(RootObjectMapping mapping)
			{
				this.PrettyPrint(mapping);
			}

			public void Visit(StringMapping mapping)
			{
				this.PrettyPrint(mapping);
			}

			public void Visit(NumberMapping mapping)
			{
				this.PrettyPrint(mapping);
			}

			public void Visit(DateMapping mapping)
			{
				this.PrettyPrint(mapping);
			}

			public void Visit(BooleanMapping mapping)
			{
				this.PrettyPrint(mapping);
			}

			public void Visit(BinaryMapping mapping)
			{
				this.PrettyPrint(mapping);
			}

			public void Visit(ObjectMapping mapping)
			{
				this.PrettyPrint(mapping);
			}

			public void Visit(NestedObjectMapping mapping)
			{
				this.PrettyPrint(mapping);
			}

			public void Visit(MultiFieldMapping mapping)
			{
				this.PrettyPrint(mapping);
			}

			public void Visit(IPMapping mapping)
			{
				this.PrettyPrint(mapping);
			}

			public void Visit(GeoPointMapping mapping)
			{
				this.PrettyPrint(mapping);
			}

			public void Visit(GeoShapeMapping mapping)
			{
				this.PrettyPrint(mapping);
			}

			public void Visit(AttachmentMapping mapping)
			{
				this.PrettyPrint(mapping);
			}

			public override string ToString()
			{
				return this._stringBuilder.ToString();
			}
		}



	}
}
