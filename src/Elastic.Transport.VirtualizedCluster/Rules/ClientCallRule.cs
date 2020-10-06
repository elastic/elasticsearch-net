// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
#if DOTNETCORE
using TheException = System.Net.Http.HttpRequestException;
#else
using TheException = System.Net.WebException;
#endif

namespace Elastic.Transport.VirtualizedCluster.Rules
{
	public interface IClientCallRule : IRule { }

	public class ClientCallRule : RuleBase<ClientCallRule>, IClientCallRule
	{
		private IClientCallRule Self => this;

		public ClientCallRule Fails(RuleOption<TimesHelper.AllTimes, int> times, RuleOption<Exception, int> errorState = null)
		{
			Self.Times = times;
			Self.Succeeds = false;
			Self.Return = errorState ?? new TheException();
			return this;
		}

		public ClientCallRule Succeeds(RuleOption<TimesHelper.AllTimes, int> times, int? validResponseCode = 200)
		{
			Self.Times = times;
			Self.Succeeds = true;
			Self.Return = validResponseCode;
			return this;
		}

		public ClientCallRule AfterSucceeds(RuleOption<Exception, int> errorState = null)
		{
			Self.AfterSucceeds = errorState;
			return this;
		}

		public ClientCallRule ThrowsAfterSucceeds()
		{
			Self.AfterSucceeds = new TheException();
			return this;
		}

		public ClientCallRule SucceedAlways(int? validResponseCode = 200) => Succeeds(TimesHelper.Always, validResponseCode);

		public ClientCallRule FailAlways(RuleOption<Exception, int> errorState = null) => Fails(TimesHelper.Always, errorState);
	}
}
