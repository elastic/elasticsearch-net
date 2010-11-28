using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using HackerNews.Indexer.Domain;
using ElasticSearch.Client;
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

		private static SemaphoreSlim ResourceLock;

		static void Main(string[] args)
		{
			var filePath = args.First();
			Program.ResourceLock = new SemaphoreSlim(15);

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
							client.IndexAsync<Post>(postQueue, (c) =>
							{
								if (!c.Success)
									dropped++;
							});

							postQueue = new List<Post>();
							processed++;
						}
		
						

						Console.Write("\rProcessed:{0}, Dropped:{2} in {1}", processed, sw.Elapsed, dropped);

						post = new Post();
						meta = new PostMetaData();
					}


				}
				sw.Stop();
				Console.WriteLine("\nDone! {0}", sw.Elapsed);
			}
			catch (Exception e)
			{

			}

		
		}
	}
}
