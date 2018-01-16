using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nest
{
	[JsonObject]
	public interface IEmailAction : IAction
	{
		[JsonProperty("account")]
		string Account { get; set; }

		[JsonProperty("from")]
		string From { get; set; }

		[JsonProperty("to")]
		IEnumerable<string> To { get; set; }

		[JsonProperty("cc")]
		IEnumerable<string> Cc { get; set; }

		[JsonProperty("bcc")]
		IEnumerable<string> Bcc { get; set; }

		[JsonProperty("reply_to")]
		IEnumerable<string> ReplyTo { get; set; }

		[JsonProperty("subject")]
		string Subject { get; set; }

		[JsonProperty("body")]
		IEmailBody Body { get; set; }

		[JsonProperty("priority")]
		EmailPriority? Priority { get; set; }

		[JsonProperty("attachments")]
		IEmailAttachments Attachments { get; set; }
	}

	public class EmailAction : ActionBase, IEmailAction
	{
		public override ActionType ActionType => ActionType.Email;

		public EmailAction(string name) : base(name) {}

		public string Account { get; set; }

		public string From { get; set; }

		public IEnumerable<string> To { get; set; }

		public IEnumerable<string> Cc { get; set; }

		public IEnumerable<string> Bcc { get; set; }

		public IEnumerable<string> ReplyTo { get; set; }

		public string Subject { get; set; }

		public IEmailBody Body { get; set; }

		public EmailPriority? Priority { get; set; }

		public IEmailAttachments Attachments { get; set; }
	}

	public class EmailActionDescriptor : ActionsDescriptorBase<EmailActionDescriptor, IEmailAction>, IEmailAction
	{
		public EmailActionDescriptor(string name) : base(name) {}

		protected override ActionType ActionType => ActionType.Email;

		string IEmailAction.Account { get; set; }
		string IEmailAction.From { get; set; }
		IEnumerable<string> IEmailAction.To { get; set; }
		IEnumerable<string> IEmailAction.Cc { get; set; }
		IEnumerable<string> IEmailAction.Bcc { get; set; }
		IEnumerable<string> IEmailAction.ReplyTo { get; set; }
		string IEmailAction.Subject { get; set; }
		IEmailBody IEmailAction.Body { get; set; }
		EmailPriority? IEmailAction.Priority { get; set; }
		IEmailAttachments IEmailAction.Attachments { get; set; }

		public EmailActionDescriptor Account(string account) => Assign(a => a.Account = account);

		public EmailActionDescriptor From(string from) => Assign(a => a.From = from);

		public EmailActionDescriptor To(IEnumerable<string> to) => Assign(a => a.To = to);

		public EmailActionDescriptor To(params string[] to) => Assign(a => a.To = to);

		public EmailActionDescriptor Cc(IEnumerable<string> cc) => Assign(a => a.Cc = cc);

		public EmailActionDescriptor Cc(params string[] cc) => Assign(a => a.Cc = cc);

		public EmailActionDescriptor Bcc(IEnumerable<string> bcc) => Assign(a => a.Bcc = bcc);

		public EmailActionDescriptor Bcc(params string[] bcc) => Assign(a => a.Bcc = bcc);

		public EmailActionDescriptor ReplyTo(IEnumerable<string> replyTo) => Assign(a => a.ReplyTo = replyTo);

		public EmailActionDescriptor ReplyTo(params string[] replyTo) => Assign(a => a.ReplyTo = replyTo);

		public EmailActionDescriptor Subject(string subject) => Assign(a => a.Subject = subject);

		public EmailActionDescriptor Body(Func<EmailBodyDescriptor, IEmailBody> selector) =>
			Assign(a => a.Body = selector.InvokeOrDefault(new EmailBodyDescriptor()));

		public EmailActionDescriptor Priority(EmailPriority? priority) => Assign(a => a.Priority = priority);

		public EmailActionDescriptor Attachments(Func<EmailAttachmentsDescriptor, IPromise<IEmailAttachments>> selector) =>
			Assign(a => a.Attachments = selector?.Invoke(new EmailAttachmentsDescriptor())?.Value);
	}
}
