using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using HackerNews.Indexer.Domain;
using Nest;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace HackerNews.Indexer
{
	class Program
	{
		/// <summary>
		/// Depends on hn_full_11-07-2010.xml which you can download from: 
		/// http://api.ihackernews.com/torrents/hn_full_11-07-2010.zip.torrent
		/// 
		/// When run from debug make sure to change the default debug arguments.
		/// <param name="args">Full filepath to hn_full_11-07-2010.xml</param>
		static void Main(string[] args)
		{
			var filePath = args.First();
			var elasticSettings = new ConnectionSettings("127.0.0.1.", 9200)
										.SetDefaultIndex("mpdreamz")
										.SetMaximumAsyncConnections(50);
			var client = new ElasticClient(elasticSettings);
			ConnectionStatus connectionStatus;
			if (!client.TryConnect(out connectionStatus))
			{
				Console.Error.WriteLine("Could not connect to {0}:\r\n{1}",
					elasticSettings.Host, connectionStatus.Error.OriginalException.Message);
				Console.Read();
				return;
			}


			var reader = new XmlTextReader(filePath);
			Post post = new Post();
			PostMetaData meta = new PostMetaData();

			int processed = 0, dropped = 0;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var postQueue = new List<Post>();
			try
			{
				while (reader.Read())
				{
					var name = reader.Name;

					if (reader.NodeType == XmlNodeType.Element)
					{
						if (name == "HackerNews")
							continue;

						if (name == "ID")
							post.Id = reader.ReadElementContentAsInt();

						else if (name == "ParentID")
							post.ParentId = reader.ReadElementContentAsInt();
						else if (name == "Url")
							post.Url = reader.ReadElementContentAsString();
						else if (name == "Title")
							post.Title = reader.ReadElementContentAsString();
						else if (name == "Text")
							post.Text = reader.ReadElementContentAsString();
						else if (name == "Username")
							meta.Username = reader.ReadElementContentAsString();
						else if (name == "Points")
							meta.Points = reader.ReadElementContentAsInt();
						else if (name == "Type")
							meta.Type = reader.ReadElementContentAsInt();
						else if (name == "Timestamp")
							meta.Created = reader.ReadElementContentAsDateTime();
						else if (name == "CommentCount")
							meta.CommentsCount = reader.ReadElementContentAsInt();
					}

					if (reader.NodeType == XmlNodeType.EndElement
						&& name == "row")
					{
						post.Meta = meta;
						postQueue.Add(post);
						if (postQueue.Count() == 1000)
						{
							var t = client.IndexManyAsync(postQueue);
							t.ContinueWith(c =>
							{
								var result = c.Result;
								if (!result.IsValid)
									dropped++;
							});
							processed += postQueue.Count();
							postQueue = new List<Post>();
							
						}
						Console.Write("\rProcessed:{0}, Dropped:{2} in {1}", processed, sw.Elapsed, dropped);

						post = new Post();
						meta = new PostMetaData();
					}
				}
				if (postQueue.Count() > 0)
				{
					var task = client.IndexManyAsync(postQueue).ContinueWith(t =>
					{
						var c = t.Result;
						if (!c.IsValid)
							Interlocked.Increment(ref dropped);
						return t;
					});
					Interlocked.Add(ref processed, postQueue.Count());
					postQueue = new List<Post>();
					
				}
				sw.Stop();
				Console.WriteLine("\nDone!", sw.Elapsed);
				Console.WriteLine("{0} docs in {1} => {2} docs/s", processed, sw.Elapsed, processed / sw.Elapsed.TotalSeconds);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

		
		}
	}
}
