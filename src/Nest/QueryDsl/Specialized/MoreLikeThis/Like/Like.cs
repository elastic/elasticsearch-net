using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	[JsonFormatter(typeof(LikeFormatter))]
	public class Like : Union<string, ILikeDocument>
	{
		public Like(string item) : base(item) { }

		public Like(ILikeDocument item) : base(item) { }

		public static implicit operator Like(string likeText) => new Like(likeText);

		public static implicit operator Like(LikeDocumentBase like) => new Like(like);

		internal static bool IsConditionless(Like like) =>
			like.Item1.IsNullOrEmpty() && (like.Item2 == null || like.Item2.Id == null && like.Item2.Document == null);
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

	internal class LikeFormatter : IJsonFormatter<Like>
	{
		private static readonly UnionFormatter<string, ILikeDocument> UnionFormatter = new UnionFormatter<string, ILikeDocument>();

		public Like Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var union = UnionFormatter.Deserialize(ref reader, formatterResolver);

			if (union == null)
				return null;

			switch (union._tag)
			{
				case 0:
					return new Like(union.Item1);
				case 1:
					return new Like(union.Item2);
				default:
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, Like value, IJsonFormatterResolver formatterResolver) =>
			UnionFormatter.Serialize(ref writer, value, formatterResolver);
	}
}
