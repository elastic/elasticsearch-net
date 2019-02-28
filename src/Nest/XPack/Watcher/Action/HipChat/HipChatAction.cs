using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IHipChatAction : IAction
	{
		[DataMember(Name = "account")]
		string Account { get; set; }

		[DataMember(Name = "message")]
		IHipChatMessage Message { get; set; }
	}

	public class HipChatAction : ActionBase, IHipChatAction
	{
		public HipChatAction(string name) : base(name) { }

		public string Account { get; set; }
		public override ActionType ActionType => ActionType.HipChat;

		public IHipChatMessage Message { get; set; }
	}

	public class HipChatActionDescriptor : ActionsDescriptorBase<HipChatActionDescriptor, IHipChatAction>, IHipChatAction
	{
		public HipChatActionDescriptor(string name) : base(name) { }

		protected override ActionType ActionType => ActionType.HipChat;
		string IHipChatAction.Account { get; set; }
		IHipChatMessage IHipChatAction.Message { get; set; }

		public HipChatActionDescriptor Account(string account) => Assign(a => a.Account = account);

		public HipChatActionDescriptor Message(Func<HipChatMessageDescriptor, IHipChatMessage> selector) =>
			Assign(a => a.Message = selector.InvokeOrDefault(new HipChatMessageDescriptor()));
	}
}
