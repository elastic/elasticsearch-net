using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Nest.Tests.Literate
{
	public abstract class LiterateTests<TInterface, TDescriptor, TInitializer>
		where TDescriptor : TInterface, new() where TInitializer : TInterface
	{
		protected ConnectionSettings Settings { get; private set; }
		protected IConnection Connection { get; private set; }
		protected IElasticClient Client { get; private set; }

		public abstract object ExpectedJson { get; }
		public abstract TInitializer InitializerExample { get; }
		public abstract TDescriptor FluentExample(TDescriptor descriptor);

		protected readonly string initializerJson;
		protected readonly string fluentJson;
		protected readonly string expectedJson;

		public LiterateTests()
		{
			this.Settings = new ConnectionSettings();
			this.Connection = new InMemoryConnection(this.Settings);
			this.Client = new ElasticClient(this.Settings, this.Connection);

			this.initializerJson = this.Serialize(this.InitializerExample);
			this.fluentJson = this.Serialize(this.FluentExample(new TDescriptor()));
			this.expectedJson = this.Serialize(this.ExpectedJson);
		}

		protected string Serialize<TObject>(TObject o)
		{
			var bytes = this.Client.Serializer.Serialize(o);
			return Encoding.UTF8.GetString(bytes);
		}

		protected bool JsonEquals(string expected, string actual)
		{
			var expectedJson = JObject.Parse(expected);
			var actualJson = JObject.Parse(actual);
			var matches = JToken.DeepEquals(expectedJson, actualJson);
			return matches;
		}

		protected void AssertJsonEquals(string expected, string actual)
		{
			var matches = this.JsonEquals(expected, actual);
			if (matches) return;
			//will throw a descriptive exception
			expected.Should().BeEquivalentTo(actual);
		}

		[Fact]
		public void initializer_syntax_serializes_to_expected_json()
		{
			this.AssertJsonEquals(this.expectedJson, this.initializerJson);
		}


	}
}
