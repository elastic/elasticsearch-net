using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;

namespace Nest.Tests.Integration.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce450Tests : IntegrationTests
	{
		[ElasticType(Name = "MyTestType")]
		public class MyTestType
		{
			public string Id { get; set; }

			[ElasticProperty(Name = "Data")]
			public string Data { get; set; }
		}


		/// <summary>
		/// https://github.com/Mpdreamz/NEST/issues/450
		/// </summary>
		[Test]
		public void MultiGetDoesNotSetId()
		{
			_client.DeleteByQuery<MyTestType>(q => q.MatchAll());
			_client.Refresh(i=>i.Index<MyTestType>());
			var someNewObjects = new List<MyTestType>()
			{
				new MyTestType(){Id="1",Data="1 Data"},
				new MyTestType(){Id="2",Data="2 Data"},
			};

			var indexResult = this._client.IndexMany(someNewObjects);
			indexResult.IsValid.Should().BeTrue();

			var multiGetResult = this._client.SourceMany<MyTestType>(new[] {"1", "2"});
			multiGetResult.Should().HaveCount(2);
			multiGetResult.Should().OnlyContain(h => !h.Id.IsNullOrEmpty());

		}

	}
}
