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

		private readonly string _expectedJsonString;
		private readonly JObject _expectedJsonJObject;

		public SerializationTests()
		{
			var o = this.ExpectedJson;
			if (o != null)
			{
				this._expectedJsonString = this.Serialize(o);
				this._expectedJsonJObject = JObject.Parse(this._expectedJsonString);
			}
		} 

		protected void AssertSerializes(object o)
		{
			if (string.IsNullOrEmpty(this._expectedJsonString))
				throw new ArgumentNullException(nameof(this._expectedJsonString));

			var actualJsonString = this.Serialize(o);
			var actualJson = JObject.Parse(actualJsonString);
			
			var matches = JToken.DeepEquals(this._expectedJsonJObject, actualJson);
			if (matches) return; //return early no need to do string comp

			actualJsonString.Should().BeEquivalentTo(_expectedJsonString);
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

	}
}
