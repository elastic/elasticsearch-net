using System;
using System.IO;

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
	}

	public abstract class RuleBase<TRule> : IRule
		where TRule : RuleBase<TRule>, IRule
	{
		RuleOption<Exception, int> IRule.AfterSucceeds { get; set; }
		int? IRule.OnPort { get; set; }
		RuleOption<Exception, int> IRule.Return { get; set; }
		string IRule.ReturnContentType { get; set; }
		byte[] IRule.ReturnResponse { get; set; }
		private IRule Self => this;
		bool IRule.Succeeds { get; set; }
		TimeSpan? IRule.Takes { get; set; }
		RuleOption<TimesHelper.AllTimes, int> IRule.Times { get; set; }

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
			using (var ms = RecyclableMemoryStreamFactory.Default.Create())
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
