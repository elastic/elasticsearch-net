using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IActions : IIsADictionary<string, IAction> {}

	[JsonConverter(typeof(ActionsJsonConverter))]
	public class Actions : IsADictionaryBase<string, IAction>, IActions
	{
		private static IDictionary<string, IAction> ReduceCombinators(IDictionary<string, IAction> actions)
		{
			if (!actions.Values.OfType<ActionCombinator>().Any())
				return actions;

			var reducedActions = new Dictionary<string, IAction>(actions.Count);
			foreach (var action in actions)
			{
				var combinator = action.Value as ActionCombinator;
				if (combinator != null)
				{
					foreach (var combinatorAction in combinator.Actions)
					{
						if (combinatorAction.Name.IsNullOrEmpty())
							throw new ArgumentException($"{combinatorAction.GetType().Name}.Name is not set!");
						reducedActions.Add(combinatorAction.Name, combinatorAction);
					}
				}
				else reducedActions.Add(action.Key, action.Value);
			}

			return reducedActions;
		}

		public Actions() {}

		public Actions(IDictionary<string, IAction> actions) : base(ReduceCombinators(actions)) {}

		public static implicit operator Actions(ActionBase action)
		{
			if (action == null) return null;

			var combinator = action as ActionCombinator;
			Dictionary<string, IAction> actions;
			if (combinator != null)
			{
				actions = new Dictionary<string, IAction>(combinator.Actions.Count);
				foreach (var actionBase in combinator.Actions)
				{
					if (actionBase.Name.IsNullOrEmpty())
						throw new ArgumentException($"{actionBase.GetType().Name}.Name is not set!");
					actions.Add(actionBase.Name, actionBase);
				}
				return new Actions(actions);
			}

			if (action.Name.IsNullOrEmpty())
				throw new ArgumentException($"{action.GetType().Name}.Name is not set!");

			actions = new Dictionary<string, IAction>{{ action.Name, action }};
			return new Actions(actions);
		}
	}

	public class ActionsDescriptor : IsADictionaryDescriptorBase<ActionsDescriptor, Actions, string, IAction>
	{
		public ActionsDescriptor() : base(new Actions()) {}

		public ActionsDescriptor Email(string name, Func<EmailActionDescriptor, IEmailAction> selector) =>
			Assign(name, selector.InvokeOrDefault(new EmailActionDescriptor(name)));

		public ActionsDescriptor HipChat(string name, Func<HipChatActionDescriptor, IHipChatAction> selector) =>
			Assign(name, selector.InvokeOrDefault(new HipChatActionDescriptor(name)));

		public ActionsDescriptor Index(string name, Func<IndexActionDescriptor, IIndexAction> selector) =>
			Assign(name, selector.InvokeOrDefault(new IndexActionDescriptor(name)));

		public ActionsDescriptor Logging(string name, Func<LoggingActionDescriptor, ILoggingAction> selector) =>
			Assign(name, selector.InvokeOrDefault(new LoggingActionDescriptor(name)));

		public ActionsDescriptor PagerDuty(string name, Func<PagerDutyActionDescriptor, IPagerDutyAction> selector) =>
			Assign(name, selector.InvokeOrDefault(new PagerDutyActionDescriptor(name)));

		public ActionsDescriptor Slack(string name, Func<SlackActionDescriptor, ISlackAction> selector) =>
			Assign(name, selector.InvokeOrDefault(new SlackActionDescriptor(name)));

		public ActionsDescriptor Webhook(string name, Func<WebhookActionDescriptor, IWebhookAction> selector) =>
			Assign(name, selector.InvokeOrDefault(new WebhookActionDescriptor(name)));
	}
}
