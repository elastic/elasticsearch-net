using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(LikeJsonConverter))]
	public class Like : Union<string, ILikeDocument>
	{
		public Like(string item) : base(item) { }
		public Like(ILikeDocument item) : base(item) { }

		public static implicit operator Like(string likeText) => new Like(likeText);
		public static implicit operator Like(LikeDocumentBase like) => new Like(like);

		internal static bool IsConditionless(Like like) =>
			like.Item1.IsNullOrEmpty() && (like.Item2 == null || (like.Item2.Id == null && like.Item2.Document == null));
	}

	public class LikeDescriptor<T> : DescriptorPromiseBase<LikeDescriptor<T>, List<Like>>
		where T : class
	{
		public LikeDescriptor() : base(new List<Like>()) { }

		public LikeDescriptor<T> Text(string likeText) => Assign(a => a.Add(likeText));

		public LikeDescriptor<T> Document(Func<LikeDocumentDescriptor<T>, ILikeDocument> selector)
		{
			var l = selector?.Invoke(new LikeDocumentDescriptor<T>());
			return l == null ? this : Assign(a => a.Add(new Like(l)));
		}

	}
	internal class LikeJsonConverter :JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;

		public static UnionJsonConverter<string, ILikeDocument> Unionconverter = new UnionJsonConverter<string, ILikeDocument>();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var union = Unionconverter.ReadJson(reader, objectType, existingValue, serializer) as Union<string, ILikeDocument>;
			if (union == null) return null;
			if (union.Item1 != null) return new Like(union.Item1);
			return new Like(union.Item2);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Unionconverter.WriteJson(writer, value, serializer);
		}

		public override bool CanConvert(Type objectType) => true;
	}

}
