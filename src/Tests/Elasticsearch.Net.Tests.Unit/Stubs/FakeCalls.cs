using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Providers;
using FakeItEasy;
using FakeItEasy.Configuration;

namespace Elasticsearch.Net.Tests.Unit.Stubs
{
	public static class FakeCalls 
	{
		public static IReturnValueConfiguration<ElasticsearchResponse<Dictionary<string, object>>> GetSyncCall(AutoFake fake)
		{
			return A.CallTo(() =>
				fake.Resolve<IConnection>().GetSync<Dictionary<string, object>>(A<Uri>._, null));
		}
		public static IReturnValueArgumentValidationConfiguration<Task<ElasticsearchResponse<Dictionary<string, object>>>> GetCall(AutoFake fake)
		{
			return A.CallTo(() => 
				fake.Resolve<IConnection>().Get<Dictionary<string, object>>(A<Uri>._, A<object>._));
		}

		public static IReturnValueArgumentValidationConfiguration<bool> Ping(AutoFake fake)
		{
			return A.CallTo(() => fake.Resolve<IConnection>().Ping(A<Uri>._));
		}

		public static ITransport ProvideDefaultTransport(AutoFake fake)
		{
			var param = new TypedParameter(typeof(IDateTimeProvider), null);
			return fake.Provide<ITransport, Transport>(param);
		}

	}
}