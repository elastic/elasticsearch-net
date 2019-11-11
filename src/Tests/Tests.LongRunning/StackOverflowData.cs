using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Nest;
using Tests.LongRunning.Models;

namespace Tests.LongRunning
{
	public static class StackOverflowData
	{
		public static IEnumerable<(int, List<string>)> GetPostTagsWithAnswers(string path)
		{
			using (var stream = File.OpenRead(path))
			using (var reader = XmlReader.Create(stream))
			{
				reader.ReadToDescendant("posts");
				reader.ReadToDescendant("row");
				do
				{
					var item = (XElement)XNode.ReadFrom(reader);
					var id = int.Parse(item.Attribute("Id").Value, CultureInfo.InvariantCulture);
					var postTypeId = int.Parse(item.Attribute("PostTypeId").Value, CultureInfo.InvariantCulture);

					// not a question
					if (postTypeId != 1)
						continue;

					var answerCount = int.Parse(item.Attribute("AnswerCount").Value,
						CultureInfo.InvariantCulture);

					// no answers
					if (answerCount == 0)
						continue;

					var tags = GetTags(item.Attribute("Tags"));

					// no tags
					if (tags == null)
						continue;

					yield return (id, tags);
				} while (reader.ReadToNextSibling("row"));
			}
		}

		public static IEnumerable<Post> GetPosts(string path)
		{
			using (var stream = File.OpenRead(path))
			using (var reader = XmlReader.Create(stream))
			{
				reader.ReadToDescendant("posts");
				reader.ReadToDescendant("row");
				do
				{
					var item = (XElement)XNode.ReadFrom(reader);
					var id = int.Parse(item.Attribute("Id").Value, CultureInfo.InvariantCulture);
					var postTypeId = int.Parse(item.Attribute("PostTypeId").Value, CultureInfo.InvariantCulture);
					var score = int.Parse(item.Attribute("Score").Value, CultureInfo.InvariantCulture);
					var body = item.Attribute("Body")?.Value;
					var creationDate = DateTimeOffset.Parse(item.Attribute("CreationDate").Value,
						CultureInfo.InvariantCulture);
					var commentCount = int.Parse(item.Attribute("CommentCount").Value, CultureInfo.InvariantCulture);
					var ownerUserId = item.Attribute("OwnerUserId") != null
						? int.Parse(item.Attribute("OwnerUserId").Value, CultureInfo.InvariantCulture)
						: (int?)null;
					var ownerDisplayName = item.Attribute("OwnerDisplayName")?.Value;
					var lastEditorUserId = item.Attribute("LastEditorUserId") != null
						? int.Parse(item.Attribute("LastEditorUserId").Value, CultureInfo.InvariantCulture)
						: (int?)null;
					var lastEditDate = item.Attribute("LastEditDate") != null
						? DateTimeOffset.Parse(item.Attribute("LastEditDate").Value, CultureInfo.InvariantCulture)
						: (DateTimeOffset?)null;
					var lastActivityDate = item.Attribute("LastActivityDate") != null
						? DateTimeOffset.Parse(item.Attribute("LastActivityDate").Value, CultureInfo.InvariantCulture)
						: (DateTimeOffset?)null;

					switch (postTypeId)
					{
						case 1:
							var title = item.Attribute("Title").Value;

							var question = new Question
							{
								Id = id,
								ParentId = JoinField.Root<Question>(),
								AcceptedAnswerId = item.Attribute("AcceptedAnswerId") != null
									? int.Parse(item.Attribute("AcceptedAnswerId").Value, CultureInfo.InvariantCulture)
									: (int?)null,
								CreationDate = creationDate,
								Score = score,
								ViewCount = int.Parse(item.Attribute("ViewCount").Value, CultureInfo.InvariantCulture),
								Body = body,
								OwnerUserId = ownerUserId,
								OwnerDisplayName = ownerDisplayName,
								LastEditorUserId = lastEditorUserId,
								LastEditorDisplayName = item.Attribute("LastEditorDisplayName")?.Value,
								LastEditDate = lastEditDate,
								LastActivityDate = lastActivityDate,
								Title = title,
								TitleSuggest = new CompletionField { Input = new[] { title }, Weight = score < 0 ? 0 : score },
								Tags = GetTags(item.Attribute("Tags")),
								AnswerCount = int.Parse(item.Attribute("AnswerCount").Value,
									CultureInfo.InvariantCulture),
								CommentCount = commentCount,
								FavoriteCount = item.Attribute("FavoriteCount") != null
									? int.Parse(item.Attribute("FavoriteCount").Value, CultureInfo.InvariantCulture)
									: 0,
								CommunityOwnedDate = item.Attribute("CommunityOwnedDate") != null
									? DateTimeOffset.Parse(item.Attribute("CommunityOwnedDate").Value,
										CultureInfo.InvariantCulture)
									: (DateTimeOffset?)null
							};

							yield return question;

							break;
						case 2:
							var answer = new Answer
							{
								Id = id,
								ParentId = JoinField.Link<Answer>(int.Parse(item.Attribute("ParentId").Value)),
								CreationDate = creationDate,
								Score = score,
								Body = body,
								OwnerUserId = ownerUserId,
								OwnerDisplayName = ownerDisplayName,
								LastEditorUserId = lastEditorUserId,
								LastEditDate = lastEditDate,
								LastActivityDate = lastActivityDate,
								CommentCount = commentCount
							};

							yield return answer;

							break;
					}
				} while (reader.ReadToNextSibling("row"));
			}
		}

		public static IEnumerable<User> GetUsers(string path)
		{
			using (var stream = File.OpenRead(path))
			using (var reader = XmlReader.Create(stream))
			{
				reader.ReadToDescendant("users");
				reader.ReadToDescendant("row");
				do
				{
					var item = (XElement)XNode.ReadFrom(reader);
					yield return new User
					{
						Id = int.Parse(item.Attribute("Id").Value, CultureInfo.InvariantCulture),
						Reputation = int.Parse(item.Attribute("Reputation").Value, CultureInfo.InvariantCulture),
						CreationDate = DateTimeOffset.Parse(item.Attribute("CreationDate").Value,
							CultureInfo.InvariantCulture),
						DisplayName = item.Attribute("DisplayName")?.Value,
						LastAccessDate = DateTimeOffset.Parse(item.Attribute("LastAccessDate").Value,
							CultureInfo.InvariantCulture),
						WebsiteUrl = item.Attribute("WebsiteUrl")?.Value,
						Location = item.Attribute("Location")?.Value,
						AboutMe = item.Attribute("AboutMe") != null ? item.Attribute("AboutMe").Value : null,
						Views = int.Parse(item.Attribute("Views").Value, CultureInfo.InvariantCulture),
						UpVotes = int.Parse(item.Attribute("UpVotes").Value, CultureInfo.InvariantCulture),
						DownVotes = int.Parse(item.Attribute("DownVotes").Value, CultureInfo.InvariantCulture),
						ProfileImageUrl = item.Attribute("ProfileImageUrl") != null
							? item.Attribute("ProfileImageUrl").Value
							: null,
						Age = item.Attribute("Age") != null
							? int.Parse(item.Attribute("Age").Value, CultureInfo.InvariantCulture)
							: (int?)null,
						AccountId = item.Attribute("AccountId") != null
							? int.Parse(item.Attribute("AccountId").Value, CultureInfo.InvariantCulture)
							: 0
					};
				} while (reader.ReadToNextSibling("row"));
			}
		}

		public static IEnumerable<BadgeMeta> GetBadgeMetas(string path)
		{
			using (var stream = File.OpenRead(path))
			using (var reader = XmlReader.Create(stream))
			{
				reader.ReadToDescendant("badges");
				reader.ReadToDescendant("row");

				do
				{
					var item = (XElement)XNode.ReadFrom(reader);

					// only interested in tag badges
					if (bool.Parse(item.Attribute("TagBased").Value) == false)
						continue;

					var badgeClass = (BadgeClass)Enum.Parse(typeof(BadgeClass), item.Attribute("Class").Value);
					var name = item.Attribute("Name").Value;

					yield return new BadgeMeta
					{
						UserId = int.Parse(item.Attribute("UserId").Value, CultureInfo.InvariantCulture),
						Badge = new Badge
						{
							Name = name,
							Class = badgeClass,
							Date = DateTimeOffset.Parse(item.Attribute("Date").Value, CultureInfo.InvariantCulture)
						}
					};
				} while (reader.ReadToNextSibling("row"));
			}
		}

		private static List<string> GetTags(XAttribute attribute) =>
			attribute?.Value.Replace("<", string.Empty)
				.Split(new[] { ">" }, StringSplitOptions.RemoveEmptyEntries)
				.ToList();
	}
}
