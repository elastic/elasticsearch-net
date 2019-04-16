using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IEmailAction : IAction
	{
		[DataMember(Name = "account")]
		string Account { get; set; }

		[DataMember(Name = "attachments")]
		IEmailAttachments Attachments { get; set; }

		[DataMember(Name = "bcc")]
		IEnumerable<string> Bcc { get; set; }

		[DataMember(Name = "body")]
		IEmailBody Body { get; set; }

		[DataMember(Name = "cc")]
		IEnumerable<string> Cc { get; set; }

		[DataMember(Name = "from")]
		string From { get; set; }

		[DataMember(Name = "priority")]
		EmailPriority? Priority { get; set; }

		[DataMember(Name = "reply_to")]
		IEnumerable<string> ReplyTo { get; set; }

		[DataMember(Name = "subject")]
		string Subject { get; set; }

		[DataMember(Name = "to")]
		IEnumerable<string> To { get; set; }
	}

	public class EmailAction : ActionBase, IEmailAction
	{
		public EmailAction(string name) : base(name) { }

		public string Account { get; set; }
		public override ActionType ActionType => ActionType.Email;

		public IEmailAttachments Attachments { get; set; }

		public IEnumerable<string> Bcc { get; set; }

		public IEmailBody Body { get; set; }

		public IEnumerable<string> Cc { get; set; }

		public string From { get; set; }

		public EmailPriority? Priority { get; set; }

		public IEnumerable<string> ReplyTo { get; set; }

		public string Subject { get; set; }

		public IEnumerable<string> To { get; set; }
	}

	public class EmailActionDescriptor : ActionsDescriptorBase<EmailActionDescriptor, IEmailAction>, IEmailAction
	{
		public EmailActionDescriptor(string name) : base(name) { }

		protected override ActionType ActionType => ActionType.Email;

		string IEmailAction.Account { get; set; }
		IEmailAttachments IEmailAction.Attachments { get; set; }
		IEnumerable<string> IEmailAction.Bcc { get; set; }
		IEmailBody IEmailAction.Body { get; set; }
		IEnumerable<string> IEmailAction.Cc { get; set; }
		string IEmailAction.From { get; set; }
		EmailPriority? IEmailAction.Priority { get; set; }
		IEnumerable<string> IEmailAction.ReplyTo { get; set; }
		string IEmailAction.Subject { get; set; }
		IEnumerable<string> IEmailAction.To { get; set; }

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
