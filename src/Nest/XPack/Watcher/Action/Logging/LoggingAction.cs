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

using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface ILoggingAction : IAction
	{
		[DataMember(Name = "category")]
		string Category { get; set; }

		[DataMember(Name = "level")]
		LogLevel? Level { get; set; }

		[DataMember(Name = "text")]
		string Text { get; set; }
	}

	public class LoggingAction : ActionBase, ILoggingAction
	{
		public LoggingAction(string name) : base(name) { }

		public override ActionType ActionType => ActionType.Logging;
		public string Category { get; set; }
		public LogLevel? Level { get; set; }
		public string Text { get; set; }
	}

	public class LoggingActionDescriptor : ActionsDescriptorBase<LoggingActionDescriptor, ILoggingAction>, ILoggingAction
	{
		public LoggingActionDescriptor(string name) : base(name) { }

		protected override ActionType ActionType => ActionType.Logging;
		string ILoggingAction.Category { get; set; }

		LogLevel? ILoggingAction.Level { get; set; }
		string ILoggingAction.Text { get; set; }

		public LoggingActionDescriptor Level(LogLevel? level) => Assign(level, (a, v) => a.Level = v);

		public LoggingActionDescriptor Text(string text) => Assign(text, (a, v) => a.Text = v);

		public LoggingActionDescriptor Category(string category) => Assign(category, (a, v) => a.Category = v);
	}
}
