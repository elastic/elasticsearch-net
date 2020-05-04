// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
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
