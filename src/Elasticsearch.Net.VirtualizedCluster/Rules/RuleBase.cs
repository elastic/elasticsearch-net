/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

 using System;
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
			using (var ms = RecyclableMemoryStreamFactory.Default.Create())
			{
				LowLevelRequestResponseSerializer.Instance.Serialize(response, ms);
				r = ms.ToArray();
			}
			Self.ReturnResponse = r;
			Self.ReturnContentType = RequestData.DefaultJsonMimeType;
			return (TRule)this;
		}

		public TRule ReturnByteResponse(byte[] response, string responseContentType = null)
		{
			Self.ReturnResponse = response;
			Self.ReturnContentType = responseContentType ?? RequestData.DefaultJsonMimeType;
			return (TRule)this;
		}
	}
}
