using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface ILoggingAction : IAction
	{
		[JsonProperty("text")]
		string Text { get; set; }

		[JsonProperty("category")]
		string Category { get; set; }

		[JsonProperty("level")]
		LogLevel? Level { get; set; }
	}

	public class LoggingAction : ActionBase, ILoggingAction
	{
		public override ActionType ActionType => ActionType.Logging;
		public string Text { get; set; }
		public string Category { get; set; }
		public LogLevel? Level { get; set; }

		public LoggingAction(string name) : base(name) {}
	}

	public class LoggingActionDescriptor : ActionsDescriptorBase<LoggingActionDescriptor, ILoggingAction>, ILoggingAction
	{
		protected override ActionType ActionType => ActionType.Logging;

		LogLevel? ILoggingAction.Level { get; set; }
		string ILoggingAction.Text { get; set; }
		string ILoggingAction.Category { get; set; }

		public LoggingActionDescriptor(string name) : base(name) {}

		public LoggingActionDescriptor Level(LogLevel level) => Assign(a => a.Level = level);

		public LoggingActionDescriptor Text(string text) => Assign(a => a.Text = text);

		public LoggingActionDescriptor Category(string category) => Assign(a => a.Category = category);
	}
}
