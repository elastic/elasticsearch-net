// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 using System;
 using System.Security.Cryptography;
 using System.Threading;

namespace Elasticsearch.Net.VirtualizedCluster.Rules
{
	public interface IRule
	{
		RuleOption<Exception, int> AfterSucceeds { get; set; }
		int? OnPort { get; set; }

		/// <summary>
		/// Either a hard exception or soft HTTP error code
		/// </summary>
		RuleOption<Exception, int> Return { get; set; }

		string ReturnContentType { get; set; }

		byte[] ReturnResponse { get; set; }
		bool Succeeds { get; set; }
		TimeSpan? Takes { get; set; }
		RuleOption<TimesHelper.AllTimes, int> Times { get; set; }

		int Executed { get; }

		void RecordExecuted();
	}

	public abstract class RuleBase<TRule> : IRule
		where TRule : RuleBase<TRule>, IRule
	{
		private int _executed;
		RuleOption<Exception, int> IRule.AfterSucceeds { get; set; }
		int? IRule.OnPort { get; set; }
		RuleOption<Exception, int> IRule.Return { get; set; }
		string IRule.ReturnContentType { get; set; }
		byte[] IRule.ReturnResponse { get; set; }
		private IRule Self => this;
		bool IRule.Succeeds { get; set; }
		TimeSpan? IRule.Takes { get; set; }
		RuleOption<TimesHelper.AllTimes, int> IRule.Times { get; set; }

		int IRule.Executed => _executed;

		void IRule.RecordExecuted() => Interlocked.Increment(ref _executed);

		public TRule OnPort(int port)
		{
			Self.OnPort = port;
			return (TRule)this;
		}

		public TRule Takes(TimeSpan span)
		{
			Self.Takes = span;
			return (TRule)this;
		}

		public TRule ReturnResponse<T>(T response)
			where T : class
		{
			byte[] r;
			using (var ms = MemoryStreamFactory.Default.Create())
			{
				LowLevelRequestResponseSerializer.Instance.Serialize(response, ms);
				r = ms.ToArray();
			}
			Self.ReturnResponse = r;
			Self.ReturnContentType = RequestData.MimeType;
			return (TRule)this;
		}

		public TRule ReturnByteResponse(byte[] response, string responseContentType = RequestData.MimeType)
		{
			Self.ReturnResponse = response;
			Self.ReturnContentType = responseContentType;
			return (TRule)this;
		}
	}
}
