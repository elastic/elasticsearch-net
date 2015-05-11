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
using Xunit.Sdk;

namespace Nest.Tests.Literate
{
	public abstract class SerializationTests
	{
		protected readonly Fixture _fixture = new Fixture();
		protected static readonly Fixture Fix = new Fixture();
		
		protected abstract object ExpectedJson { get; }
		private readonly string _json;

		public SerializationTests()
		{
			var o = this.ExpectedJson;
			if (o != null)
			{
				this._json = this.Serialize(o);
			}
		} 

		protected void AssertSerializes(object o)
		{
			if (string.IsNullOrEmpty(this._json)) return;

			var json = this.Serialize(o);
			this.AssertJsonEquals(this._json, json);
		}

		protected static TReturn Create<TReturn>()
		{
			return Fix.Create<TReturn>();
		}

		private string Serialize<TObject>(TObject o)
		{
			var bytes = TestClient.GetClient().Serializer.Serialize(o);
			return Encoding.UTF8.GetString(bytes);
		}

		private bool JsonEquals(string expected, string actual)
		{
			var expectedJson = JObject.Parse(expected);
			var actualJson = JObject.Parse(actual);
			var matches = JToken.DeepEquals(expectedJson, actualJson);
			return matches;
		}

		private void AssertJsonEquals(string expected, string actual)
		{
			var matches = this.JsonEquals(expected, actual);
			if (matches) return;
			//will throw a descriptive exception
			expected.Should().BeEquivalentTo(actual);
		}

	}
}
