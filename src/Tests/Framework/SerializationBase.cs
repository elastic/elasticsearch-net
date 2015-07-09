using System;
using System.IO;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Ploeh.AutoFixture;

namespace Tests.Framework
{

	public abstract class SerializationBase
	{
		protected readonly Fixture _fixture = new Fixture();
		protected static readonly Fixture Fix = new Fixture();

		protected abstract object ExpectJson { get; }

		private readonly string _expectedJsonString;
		private readonly JObject _expectedJsonJObject;

		protected SerializationBase()
		{
			var o = this.ExpectJson;
			if (o == null)
				throw new ArgumentNullException(nameof(this.ExpectJson));

			this._expectedJsonString = this.Serialize(o);
			this._expectedJsonJObject = JObject.Parse(this._expectedJsonString);

			if (string.IsNullOrEmpty(this._expectedJsonString))
				throw new ArgumentNullException(nameof(this._expectedJsonString));

		}

		protected void ShouldBeEquivalentTo(string serialized) =>
			serialized.Should().BeEquivalentTo(_expectedJsonString);

		protected bool SerializesAndMatches(object o, out string serialized)
		{
			serialized = null;
			serialized = this.Serialize(o);
			var actualJson = JObject.Parse(serialized);

			var matches = JToken.DeepEquals(this._expectedJsonJObject, actualJson);
			if (matches) return true;

			this.ShouldBeEquivalentTo(serialized);
			return false;
		}

		protected static TReturn Create<TReturn>()
		{
			return Fix.Create<TReturn>();
		}

		private TObject Deserialize<TObject>(string json) =>
			TestClient.GetClient().Serializer.Deserialize<TObject>(new MemoryStream(Encoding.UTF8.GetBytes(json))); 

		private string Serialize<TObject>(TObject o)
		{
			var bytes = TestClient.GetClient().Serializer.Serialize(o);
			return Encoding.UTF8.GetString(bytes);
		}

		protected void AssertSerializesAndRoundTrips<T>(T o) where T : class
		{
			//first serialize to string and assert it looks like this.ExpectedJson
			string serialized;
			if (!this.SerializesAndMatches(o, out serialized)) return;

			//deserialize serialized json back again 
			var oAgain = this.Deserialize<T>(serialized);
			//now use deserialized `o` and serialize again making sure
			//it still looks like this.ExpectedJson
			this.SerializesAndMatches(oAgain, out serialized);
		}
	}
}
