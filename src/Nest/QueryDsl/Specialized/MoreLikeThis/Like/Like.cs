/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using Nest.Utf8Json;

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

		public LikeDescriptor<T> Text(string likeText) => Assign(likeText, (a, v) => a.Add(v));

		public LikeDescriptor<T> Document(Func<LikeDocumentDescriptor<T>, ILikeDocument> selector)
		{
			var l = selector?.Invoke(new LikeDocumentDescriptor<T>());
			return l == null ? this : Assign(l, (a, v) => a.Add(new Like(v)));
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

			switch (union.Tag)
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
