using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface ISlackAction : IAction
	{
		[DataMember(Name = "account")]
		string Account { get; set; }

		[DataMember(Name = "message")]
		ISlackMessage Message { get; set; }
	}

	public class SlackAction : ActionBase, ISlackAction
	{
		public SlackAction(string name) : base(name) { }

		public string Account { get; set; }

		public override ActionType ActionType => ActionType.Slack;

		public ISlackMessage Message { get; set; }
	}

	public class SlackActionDescriptor : ActionsDescriptorBase<SlackActionDescriptor, ISlackAction>, ISlackAction
	{
		public SlackActionDescriptor(string name) : base(name) { }

		protected override ActionType ActionType => ActionType.Slack;
		string ISlackAction.Account { get; set; }
		ISlackMessage ISlackAction.Message { get; set; }

		public SlackActionDescriptor Account(string account) => Assign(a => a.Account = account);

		public SlackActionDescriptor Message(Func<SlackMessageDescriptor, ISlackMessage> selector) =>
			Assign(a => a.Message = selector.InvokeOrDefault(new SlackMessageDescriptor()));
	}
}
