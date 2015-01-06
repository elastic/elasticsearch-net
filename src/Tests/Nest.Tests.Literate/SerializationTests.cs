using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Ploeh.AutoFixture;
using Xunit;

namespace Nest.Tests.Literate
{
	public abstract class SerializationTests<TInterface, TDescriptor, TInitializer>
		where TDescriptor : TInterface, new() where TInitializer : TInterface
	{
		protected readonly Fixture _fixture = new Fixture();
		protected static readonly Fixture Fix = new Fixture();

		protected static TReturn Create<TReturn>()
		{
			return Fix.Create<TReturn>();
		}

		private readonly object _expectedJson;
		private readonly TInitializer _initializer;
		private readonly TDescriptor _fluent;
		protected readonly string initializerJson;
		protected readonly string fluentJson;
		protected readonly string expectedJson;

		public SerializationTests(
			object ExpectedJson,
			TInitializer Initializer,
			Func<TDescriptor, TDescriptor> Fluent
			)
		{
			this._expectedJson = ExpectedJson;
			this._initializer = Initializer;
			this._fluent = Fluent(new TDescriptor());

			this.initializerJson = this.Serialize(Initializer);
			this.fluentJson = this.Serialize(this._fluent);
			this.expectedJson = this.Serialize(ExpectedJson);
		}

		protected string Serialize<TObject>(TObject o)
		{
			var bytes = TestClient.Client.Serializer.Serialize(o);
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

		[Fact]
		public void fluent_syntax_serializes_to_expected_json()
		{
			this.AssertJsonEquals(this.expectedJson, this.fluentJson);
		}

		[Fact]
		public void initializer_syntax_roundtrips_withoutloss()
		{
			var stream = new MemoryStream(Encoding.UTF8.GetBytes(this.initializerJson));
			var deserialized = TestClient.Client.Serializer.Deserialize<TInitializer>(stream);
			this._initializer.ShouldBeEquivalentTo(deserialized);
		}

		[Fact]
		public void fluent_syntax_roundtrips_withoutloss()
		{
			var stream = new MemoryStream(Encoding.UTF8.GetBytes(this.fluentJson));
			var deserialized = TestClient.Client.Serializer.Deserialize<TDescriptor>(stream);
			this._fluent.ShouldBeEquivalentTo(deserialized);
		}

	}
}
