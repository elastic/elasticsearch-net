using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Serialization
{
	public class ElasticsearchDefaultSerializer : IElasticsearchSerializer
	{
		public T Deserialize<T>(byte[] bytes) where T : class
		{
			return SimpleJson.DeserializeObject<T>(bytes.Utf8String());
		}
		public T Deserialize<T>(Stream stream)
		{
			if (stream == null)
				return default(T);

			using (var ms = new MemoryStream())
			{
				stream.CopyTo(ms);
				byte[] buffer = ms.ToArray();
				if (buffer.Length <= 1)
					return default(T);
				return SimpleJson.DeserializeObject<T>(buffer.Utf8String());
			}
		}
		public Task<T> DeserializeAsync<T>(Stream stream)
		{
			var tcs = new TaskCompletionSource<T>();
			if (stream == null)
			{
				tcs.SetResult(default(T));
				return tcs.Task;
			}

			// return a task that reads the stream asynchronously 
			// and finally deserializes the result to T.
			this.Iterate<T>(ReadStreamAsync(stream, tcs), tcs);
			return tcs.Task;
		}

		public IEnumerable<Task> ReadStreamAsync<T>(Stream stream, TaskCompletionSource<T> tcs)
		{
			var ms = stream as MemoryStream;
			string json = null;
			if (ms != null && ms.Position > 0)
			{
				json = ms.ToArray().Utf8String();
				ms.Close();
			}
			else
			{
				using (ms = new MemoryStream())
				using (stream)
				{
					var buffer = new byte[BUFFER_SIZE];
					while (stream != null)
					{
						var read = Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead, buffer, 0, BUFFER_SIZE, null);
						yield return read;
						if (read.Result == 0) break;
						ms.Write(buffer, 0, read.Result);
					}
					json = ms.ToArray().Utf8String();
				}
			}
			var r = SimpleJson.DeserializeObject<T>(json);
			tcs.SetResult(r);
		}

		const int BUFFER_SIZE = 1024;

		public void Iterate<T>(IEnumerable<Task> asyncIterator, TaskCompletionSource<T> tcs)
		{
			var enumerator = asyncIterator.GetEnumerator();
			Action<Task> recursiveBody = null;
			recursiveBody = completedTask =>
			{
				if (completedTask != null && completedTask.IsFaulted)
				{
					//none of the individual steps in _AsyncSteps run in parallel for 1 request
					//as this would be impossible we can assume Aggregate Exception.InnerException
					var exception = completedTask.Exception.InnerException;
					tcs.TrySetException(exception);
					enumerator.Dispose();
				}
				else if (enumerator.MoveNext())
				{
					//enumerator.Current.ContinueWith(recursiveBody, TaskContinuationOptions.ExecuteSynchronously);
					enumerator.Current.ContinueWith(recursiveBody);
				}
				else
				{
					enumerator.Dispose();
				}
			};
			recursiveBody(null);
		}

		public byte[] Serialize(object data, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			var serialized = SimpleJson.SerializeObject(data);
			if (formatting == SerializationFormatting.None)
				serialized = RemoveNewLinesAndTabs(serialized);
			return serialized.Utf8Bytes();
		}

		public string Stringify(object valueType)
		{
			return ElasticsearchDefaultSerializer.DefaultStringify(valueType);
		}

		public static string DefaultStringify(object valueType)
		{
			var s = valueType as string;
			if (s != null)
				return s;
			var ss = valueType as string[];
			if (ss != null)
				return string.Join(",", ss);

			var pns = valueType as IEnumerable<object>;
			if (pns != null)
				return string.Join(",", pns);

			var e = valueType as Enum;
			if (e != null) return KnownEnums.Resolve(e);
			if (valueType is bool)
				return ((bool) valueType) ? "true" : "false";
			return valueType.ToString();
		}

		public static string RemoveNewLinesAndTabs(string input)
		{
			return new string(input
				.Where(c => c != '\r' && c != '\n')
				.ToArray());
		}
	}
}