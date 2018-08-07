using System;
using System.IO;
using Elasticsearch.Net;
using Nest;

namespace Tests.Framework
{
	public interface IRule
	{
		Union<TimesHelper.AllTimes, int> Times { get; set; }
		int? OnPort { get; set; }
		bool Succeeds { get; set; }
		TimeSpan? Takes { get; set; }

		/// <summary>
		/// Either a hard exception or soft HTTP error code
		/// </summary>
		Union<Exception, int> Return { get; set; }

		Union<Exception, int> AfterSucceeds { get; set; }

		byte[] ReturnResponse { get; set; }
		string ReturnContentType { get; set; }
	}

	public abstract class RuleBase<TRule> : IRule
		where TRule : RuleBase<TRule>, IRule
	{
		private IRule Self => this;
		int? IRule.OnPort { get; set; }
		bool IRule.Succeeds { get; set; }
		TimeSpan? IRule.Takes { get; set; }
		Union<TimesHelper.AllTimes, int> IRule.Times { get; set; }
		Union<Exception, int> IRule.Return { get; set; }
		Union<Exception, int> IRule.AfterSucceeds { get; set; }
		byte[] IRule.ReturnResponse { get; set; }
		string IRule.ReturnContentType { get; set; }

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
			using (var ms = new MemoryStream())
			{
				new LowLevelRequestResponseSerializer().Serialize(response, ms);
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
