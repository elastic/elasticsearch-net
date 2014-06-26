using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System.Diagnostics;
using FluentAssertions;
using Elasticsearch.Net;

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
			Client.DeleteByQuery<MyTestType>(q => q.MatchAll());
			Client.Refresh(i=>i.Index<MyTestType>());
			var someNewObjects = new List<MyTestType>()
			{
				new MyTestType(){Id="1",Data="1 Data"},
				new MyTestType(){Id="2",Data="2 Data"},
			};

			var indexResult = this.Client.IndexMany(someNewObjects);
			indexResult.IsValid.Should().BeTrue();

			var multiGetResult = this.Client.SourceMany<MyTestType>(new[] {"1", "2"});
			multiGetResult.Should().HaveCount(2);
			multiGetResult.Should().OnlyContain(h => !h.Id.IsNullOrEmpty());

		}

	}
}
