using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;

namespace Tests.Framework
{
	public abstract class ApiIntegrationTestBase<TResponse, TInterface, TDescriptor, TInitializer>
		: ApiTestBase<TResponse, TInterface, TDescriptor, TInitializer>
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected abstract int ExpectStatusCode { get; }
		protected abstract bool ExpectIsValid { get; }
		protected virtual void ExpectResponse(TResponse response) { }

		protected ApiIntegrationTestBase(IIntegrationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ForceInMemory => false;

		protected override TInitializer Initializer => Activator.CreateInstance<TInitializer>();

		[I] protected async Task HandlesStatusCode() =>
			await this.AssertOnAllResponses(r=>r.ApiCall.HttpStatusCode.Should().Be(this.ExpectStatusCode));

		[I] protected async Task ReturnsExpectedIsValid() =>
			await this.AssertOnAllResponses(r=>r.IsValid.Should().Be(this.ExpectIsValid));

		[I] protected async Task ReturnsExpectedResponse() => await this.AssertOnAllResponses(ExpectResponse);

		protected override Task AssertOnAllResponses(Action<TResponse> assert)
		{
			if (!this.ExpectIsValid) return base.AssertOnAllResponses(assert);

			return base.AssertOnAllResponses((r) =>
			{
				if (TestClient.Configuration.RunIntegrationTests && !r.IsValid && r.CallDetails.OriginalException != null
					&& IsNotRequestExceptionType(r.CallDetails.OriginalException.GetType()))
				{
					ExceptionDispatchInfo.Capture(r.CallDetails.OriginalException).Throw();
					return;
				}

				assert(r);
			});
		}

		private static bool IsNotRequestExceptionType(Type exceptionType)
		{
#if DOTNETCORE
			return exceptionType != typeof(System.Net.Http.HttpRequestException);
#else
			return exceptionType != typeof (WebException);
#endif
		}
	}
}
