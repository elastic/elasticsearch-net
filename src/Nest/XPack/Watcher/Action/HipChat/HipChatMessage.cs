using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(HipChatMessage))]
	public interface IHipChatMessage
	{
		[DataMember(Name ="body")]
		string Body { get; set; }

		[DataMember(Name ="color")]
		HipChatMessageColor? Color { get; set; }

		[DataMember(Name ="format")]
		HipChatMessageFormat? Format { get; set; }

		[DataMember(Name ="from")]
		string From { get; set; }

		[DataMember(Name ="notify")]
		bool? Notify { get; set; }

		[DataMember(Name ="room")]
		[JsonConverter(typeof(ReadSingleOrEnumerableJsonConverter<string>))]
		IEnumerable<string> Room { get; set; }

		[DataMember(Name ="user")]
		[JsonConverter(typeof(ReadSingleOrEnumerableJsonConverter<string>))]
		IEnumerable<string> User { get; set; }
	}

	public class HipChatMessage : IHipChatMessage
	{
		public string Body { get; set; }

		public HipChatMessageColor? Color { get; set; }

		public HipChatMessageFormat? Format { get; set; }

		public string From { get; set; }

		public bool? Notify { get; set; }

		public IEnumerable<string> Room { get; set; }

		public IEnumerable<string> User { get; set; }
	}

	public class HipChatMessageDescriptor : DescriptorBase<HipChatMessageDescriptor, IHipChatMessage>, IHipChatMessage
	{
		string IHipChatMessage.Body { get; set; }
		HipChatMessageColor? IHipChatMessage.Color { get; set; }
		HipChatMessageFormat? IHipChatMessage.Format { get; set; }
		string IHipChatMessage.From { get; set; }
		bool? IHipChatMessage.Notify { get; set; }
		IEnumerable<string> IHipChatMessage.Room { get; set; }
		IEnumerable<string> IHipChatMessage.User { get; set; }

		public HipChatMessageDescriptor Body(string body) => Assign(a => a.Body = body);

		public HipChatMessageDescriptor Format(HipChatMessageFormat? format) => Assign(a => a.Format = format);

		public HipChatMessageDescriptor Color(HipChatMessageColor? color) => Assign(a => a.Color = color);

		public HipChatMessageDescriptor Notify(bool? notify = true) => Assign(a => a.Notify = notify);

		public HipChatMessageDescriptor From(string from) => Assign(a => a.From = from);

		public HipChatMessageDescriptor Room(IEnumerable<string> room) => Assign(a => a.Room = room);

		public HipChatMessageDescriptor Room(params string[] room) => Assign(a => a.Room = room);

		public HipChatMessageDescriptor User(IEnumerable<string> user) => Assign(a => a.User = user);

		public HipChatMessageDescriptor User(params string[] user) => Assign(a => a.User = user);
	}


	public enum HipChatMessageFormat
	{
		[EnumMember(Value = "html")]
		Html,

		[EnumMember(Value = "text")]
		Text
	}


	public enum HipChatMessageColor
	{
		[EnumMember(Value = "gray")]
		Gray,

		[EnumMember(Value = "green")]
		Green,

		[EnumMember(Value = "purple")]
		Purple,

		[EnumMember(Value = "red")]
		Red,

		[EnumMember(Value = "yellow")]
		Yellow,
	}
}
