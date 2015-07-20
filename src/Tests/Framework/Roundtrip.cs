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
	public class RoundTripper : SerializationBase
	{
		protected override object ExpectJson { get; }

		internal RoundTripper(object expected) 
		{
			this.ExpectJson = expected;

			this._expectedJsonString = this.Serialize(expected);
			this._expectedJsonJObject = JToken.Parse(this._expectedJsonString);
		}

		public RoundTripper WhenSerializing<T>(T actual)
		{
			this.AssertSerializesAndRoundTrips(actual);
			return this;
		}

		public static RoundTripper Expect(object expected) =>  new RoundTripper(expected);
	}
}
