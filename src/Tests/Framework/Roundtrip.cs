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

		public virtual RoundTripper<T> WhenSerializing<T>(T actual)
		{
			var sut = this.AssertSerializesAndRoundTrips(actual);
			return new RoundTripper<T>(this.ExpectJson, sut);
		}

		public static RoundTripper Expect(object expected) =>  new RoundTripper(expected);
	}

	public class RoundTripper<T> : RoundTripper
	{
		protected T Sut { get; set;  }

		internal RoundTripper(object expected, T sut) : base(expected)
		{
			this.Sut = sut;
		}

		public RoundTripper<T> WhenSerializing(T actual)
		{
			Sut = this.AssertSerializesAndRoundTrips(actual);
			return this;
		}


		public RoundTripper<T> Result(Action<T> assert)
		{
			assert(this.Sut);
			return this;
		}

		public RoundTripper<T> Result<TOther>(Action<TOther> assert)
			where TOther : T
		{
			assert((TOther)this.Sut);
			return this;
		}


	}
}
