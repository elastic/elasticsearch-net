using System;
using System.IO;
using System.Linq;
using System.Text;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
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

		protected string _expectedJsonString;
		protected JToken _expectedJsonJObject;

		protected SerializationBase()
		{
			var o = this.ExpectJson;
			if (o == null) return;

			this._expectedJsonString = this.Serialize(o);
			this._expectedJsonJObject = JToken.Parse(this._expectedJsonString);

			if (string.IsNullOrEmpty(this._expectedJsonString))
				throw new ArgumentNullException(nameof(this._expectedJsonString));
		}

		protected DateTime FixedDate => new DateTime(2015, 06, 06, 12, 01, 02, 123);

		protected void ShouldBeEquivalentTo(string serialized) =>
			serialized.Should().BeEquivalentTo(_expectedJsonString);

		protected bool SerializesAndMatches(object o, out string serialized)
		{
			serialized = null;
			serialized = this.Serialize(o);
			var actualJson = JToken.Parse(serialized);

			var matches = JToken.DeepEquals(this._expectedJsonJObject, actualJson);
			if (matches) return true;

			var d = new Differ();
			var inlineBuilder = new InlineDiffBuilder(d);
			var result = inlineBuilder.BuildDiffModel(_expectedJsonString, serialized);
			var diff = result.Lines.Aggregate(new StringBuilder(), (sb, line) =>
			{
				if (line.Type == ChangeType.Inserted)
					sb.Append("+ ");
				else if (line.Type == ChangeType.Deleted)
					sb.Append("- ");
				else
					sb.Append("  ");
				sb.AppendLine(line.Text);
				return sb;
			}, sb => sb.ToString());

			throw new Exception(diff);
		}

		protected static TReturn Create<TReturn>()
		{
			return Fix.Create<TReturn>();
		}

		private TObject Deserialize<TObject>(string json) =>
			TestClient.GetClient().Serializer.Deserialize<TObject>(new MemoryStream(Encoding.UTF8.GetBytes(json)));

		protected string Serialize<TObject>(TObject o)
		{
			var bytes = TestClient.GetClient().Serializer.Serialize(o);
			return Encoding.UTF8.GetString(bytes);
		}

		protected void AssertSerializesAndRoundTrips<T>(T o) 
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
