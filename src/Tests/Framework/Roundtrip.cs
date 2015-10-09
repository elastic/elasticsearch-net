using System;
using System.IO;
using System.Linq;
using System.Text;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;

namespace Tests.Framework
{
	public class RoundTripper : SerializationTestBase
	{
		protected override object ExpectJson { get; }

		internal RoundTripper(object expected, Func<ConnectionSettings, ConnectionSettings> settings = null) 
		{
			this.ExpectJson = expected;
			this._connectionSettingsModifier = settings;

			this._expectedJsonString = this.Serialize(expected);
			this._expectedJsonJObject = JToken.Parse(this._expectedJsonString);
		}

		public virtual RoundTripper<T> WhenSerializing<T>(T actual)
		{
			var sut = this.AssertSerializesAndRoundTrips(actual);
			return new RoundTripper<T>(this.ExpectJson, sut);
		}
		public RoundTripper WhenInferringIdOn<T>(T project) where T : class
		{
			this.GetClient().Infer.Id<T>(project).Should().Be((string)this.ExpectJson);
			return this;
		}

		public static IntermediateChangedSettings WithConnectionSettings(Func<ConnectionSettings, ConnectionSettings> settings) =>  new IntermediateChangedSettings(settings);

		public static RoundTripper Expect(object expected) =>  new RoundTripper(expected);
	}

	public class IntermediateChangedSettings
	{
		private Func<ConnectionSettings, ConnectionSettings> _connectionSettingsModifier;

		internal IntermediateChangedSettings(Func<ConnectionSettings, ConnectionSettings> settings)
		{
			this._connectionSettingsModifier = settings;
		}
		public RoundTripper Expect(object expected) =>  new RoundTripper(expected, _connectionSettingsModifier);
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
