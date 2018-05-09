using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nest
{
	[JsonConverter(typeof(EmailAttachmentsJsonConverter))]
	public interface IEmailAttachments : IIsADictionary<string, IEmailAttachment> {}

	public class EmailAttachments : IsADictionaryBase<string, IEmailAttachment>, IEmailAttachments
	{
		public EmailAttachments() {}

		public EmailAttachments(IDictionary<string, IEmailAttachment> attachments) : base(attachments) {}

		public void Add(string name, IEmailAttachment attachment) => this.BackingDictionary.Add(name, attachment);
	}

	public class EmailAttachmentsDescriptor : DescriptorPromiseBase<EmailAttachmentsDescriptor, IEmailAttachments>
	{
		public EmailAttachmentsDescriptor() : base(new EmailAttachments()) {}

		public EmailAttachmentsDescriptor HttpAttachment(string name, Func<HttpAttachmentDescriptor, IHttpAttachment> selector) =>
			Assign(a => a.Add(name, selector?.Invoke(new HttpAttachmentDescriptor())));

		public EmailAttachmentsDescriptor DataAttachment(string name, Func<DataAttachmentDescriptor, IDataAttachment> selector) =>
			Assign(a => a.Add(name, selector?.Invoke(new DataAttachmentDescriptor())));
	}

	public interface IEmailAttachment {}

	internal class EmailAttachmentsJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteStartObject();
			var attachments = (IDictionary<string, IEmailAttachment>)value;
			if (attachments != null)
			{
				foreach (var attachment in attachments)
				{
					writer.WritePropertyName(attachment.Key);
					writer.WriteStartObject();

					var emailAttachment = attachment.Value;

					if (emailAttachment is IHttpAttachment)
						writer.WritePropertyName("http");
					else if (emailAttachment is IDataAttachment)
						writer.WritePropertyName("data");
					else throw new ArgumentException($"{emailAttachment.GetType().FullName} is not a supported email attachment type");

					serializer.Serialize(writer, emailAttachment);
					writer.WriteEndObject();
				}
			}
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject) return null;
			var attachments = new Dictionary<string, IEmailAttachment>();
			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.PropertyName)
				{
					var name = (string)reader.Value;
					IEmailAttachment attachment;

					reader.Read();
					reader.Read();
					var type = (string)reader.Value;
					reader.Read();

					switch (type)
					{
						case "http":
							attachment = serializer.Deserialize<HttpAttachment>(reader);
							break;
						case "data":
							attachment = serializer.Deserialize<DataAttachment>(reader);
							break;
						default:
							throw new ArgumentException($"Unknown email attachment type '{type}'");
					}

					reader.Read();
					attachments.Add(name, attachment);
				}
				else if (reader.TokenType == JsonToken.EndObject) break;
			}

			return new EmailAttachments(attachments);
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
