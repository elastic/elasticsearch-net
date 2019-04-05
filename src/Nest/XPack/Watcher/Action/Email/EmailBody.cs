using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<EmailBody>))]
	public interface IEmailBody
	{
		[JsonProperty("html")]
		string Html { get; set; }

		[JsonProperty("text")]
		string Text { get; set; }
	}

	public class EmailBody : IEmailBody
	{
		public string Html { get; set; }
		public string Text { get; set; }
	}

	public class EmailBodyDescriptor : DescriptorBase<EmailBodyDescriptor, IEmailBody>, IEmailBody
	{
		string IEmailBody.Html { get; set; }
		string IEmailBody.Text { get; set; }

		public EmailBodyDescriptor Text(string text) => Assign(text, (a, v) => a.Text = v);

		public EmailBodyDescriptor Html(string html) => Assign(html, (a, v) => a.Html = v);
	}
}
