using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IHipChatAction : IAction
	{
		[JsonProperty("account")]
		string Account { get; set; }

		[JsonProperty("message")]
		IHipChatMessage Message { get; set; }
	}

	public class HipChatAction : ActionBase, IHipChatAction
	{
		public override ActionType ActionType => ActionType.HipChat;

		public HipChatAction(string name) : base(name) {}

		public string Account { get; set; }

		public IHipChatMessage Message { get; set; }
	}

	public class HipChatActionDescriptor : ActionsDescriptorBase<HipChatActionDescriptor, IHipChatAction>, IHipChatAction
	{
		string IHipChatAction.Account { get; set; }
		IHipChatMessage IHipChatAction.Message { get; set; }

		protected override ActionType ActionType => ActionType.HipChat;

		public HipChatActionDescriptor(string name) : base(name) {}

		public HipChatActionDescriptor Account(string account) => Assign(a => a.Account = account);

		public HipChatActionDescriptor Message(Func<HipChatMessageDescriptor, IHipChatMessage> selector) =>
			Assign(a => a.Message = selector.InvokeOrDefault(new HipChatMessageDescriptor()));
	}
}
