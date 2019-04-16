using System.Runtime.Serialization;
using Elasticsearch.Net;

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

		public LoggingActionDescriptor Level(LogLevel? level) => Assign(a => a.Level = level);

		public LoggingActionDescriptor Text(string text) => Assign(a => a.Text = text);

		public LoggingActionDescriptor Category(string category) => Assign(a => a.Category = category);
	}
}
