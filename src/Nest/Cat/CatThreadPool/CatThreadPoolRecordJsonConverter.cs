using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class CatThreadPoolRecordJsonConverter : JsonConverter
	{
		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		private static readonly string[] _threadPoolShortHands = new[]
		{
			"b", "f", "g", "ge", "i", "ma", "m", "o", "p", "r", "s", "sn", "su", "w"
		};

		private static readonly string[] _fieldShortHands = new[]
		{
			"t", "a", "s", "q", "qs", "r", "l", "c", "mi", "ma", "k"
		};
		private static IEnumerable<Tuple<string, string>> _combinations =
			_threadPoolShortHands.SelectMany<string, string, Tuple<string, string>>(t => _fieldShortHands, Tuple.Create);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			var o = new CatThreadPoolRecord();
			while (reader.Read())
			{
				var prop = reader.Value as string;
				if (prop == null) return o;
				switch (prop)
				{
					case "id":
					case "nodeId":
						o.Id = reader.ReadAsString();
						continue;
					case "pid":
					case "p":
						o.Pid = reader.ReadAsString();
						continue;
					case "host":
					case "h":
						o.Host = reader.ReadAsString();
						continue;
					case "ip":
					case "i":
						o.Ip = reader.ReadAsString();
						continue;
					case "port":
					case "po":
						o.Port = reader.ReadAsString();
						continue;
					default:
						var threadPoolField = GetThreadPoolAndField(prop);
						if (threadPoolField == null) continue;
						string threadPool = threadPoolField.Item1, field = threadPoolField.Item2;

						var value = reader.ReadAsString();
						SetThreadPool(threadPool, o, field, value);
						continue;
				}
			}
			return o;
		}

		private void SetThreadPool(string threadPool, CatThreadPoolRecord o, string field, string value)
		{
			switch (threadPool)
			{
				case "bulk":
				case "b":
					if (o.Bulk == null) o.Bulk = new CatThreadPool();
					this.SetFieldValue(o.Bulk, field, value);
					return;
				case "flush":
				case "f":
					if (o.Flush == null) o.Flush = new CatThreadPool();
					this.SetFieldValue(o.Flush, field, value);
					return;
				case "generic":
				case "ge":
					if (o.Generic == null) o.Generic = new CatThreadPool();
					this.SetFieldValue(o.Generic, field, value);
					return;
				case "get":
				case "g":
					if (o.Get == null) o.Get = new CatThreadPool();
					this.SetFieldValue(o.Get, field, value);
					return;
				case "index":
				case "i":
					if (o.Index == null) o.Index = new CatThreadPool();
					this.SetFieldValue(o.Index, field, value);
					return;
				case "management":
				case "ma":
					if (o.Management == null) o.Management = new CatThreadPool();
					this.SetFieldValue(o.Management, field, value);
					return;
				case "merge":
				case "m":
					if (o.Merge == null) o.Merge = new CatThreadPool();
					this.SetFieldValue(o.Merge, field, value);
					return;
				case "optimize":
				case "o":
					if (o.Optimize == null) o.Optimize = new CatThreadPool();
					this.SetFieldValue(o.Optimize, field, value);
					return;
				case "percolate":
				case "p":
					if (o.Percolate == null) o.Percolate = new CatThreadPool();
					this.SetFieldValue(o.Percolate, field, value);
					return;
				case "refresh":
				case "r":
					if (o.Refresh == null) o.Refresh = new CatThreadPool();
					this.SetFieldValue(o.Refresh, field, value);
					return;
				case "search":
				case "s":
					if (o.Search == null) o.Search = new CatThreadPool();
					this.SetFieldValue(o.Search, field, value);
					return;
				case "snapshot":
				case "sn":
					if (o.Snapshot == null) o.Snapshot = new CatThreadPool();
					this.SetFieldValue(o.Snapshot, field, value);
					return;
				case "suggest":
				case "su":
					if (o.Suggest == null) o.Suggest = new CatThreadPool();
					this.SetFieldValue(o.Suggest, field, value);
					return;
				case "warmer":
				case "w":
					if (o.Warmer == null) o.Warmer = new CatThreadPool();
					this.SetFieldValue(o.Warmer, field, value);
					return;
			}
		}

		private static Tuple<string, string> GetThreadPoolAndField(string prop)
		{
			var tokens = prop.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			if (tokens.Length == 0 || tokens.Length > 2) return null;
			if (tokens.Length == 2) return Tuple.Create(tokens[0], tokens[1]);
			var match = _combinations.FirstOrDefault(c => c.Item1 + c.Item2 == tokens[0]);
			return match == null ? null : Tuple.Create(match.Item1, match.Item2);
		}

		private void SetFieldValue(CatThreadPool pool, string field, string value)
		{
			switch(field)
			{
				case "type": 
				case "t":
					pool.Type = value;
					return;
				case "active": 
				case "a":
					pool.Active = value;
					return;
				case "size": 
				case "s":
					pool.Size = value;
					return;
				case "queue": 
				case "q":
					pool.Queue = value;
					return;
				case "queueSize": 
				case "qs":
					pool.QueueSize = value;
					return;
				case "rejected": 
				case "r":
					pool.Rejected = value;
					return;
				case "largest": 
				case "l":
					pool.Largest = value;
					return;
				case "completed": 
				case "c":
					pool.Completed = value;
					return;
				case "min": 
				case "mi":
					pool.Min = value;
					return;
				case "max": 
				case "ma":
					pool.Max = value;
					return;
				case "keepAlive": 
				case "k":
					pool.KeepAlive = value;
					return;
			}
		}


		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(CatThreadPoolRecord);
		}
	}
}