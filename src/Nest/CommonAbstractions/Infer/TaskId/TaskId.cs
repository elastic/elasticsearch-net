// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Diagnostics;
using System.Globalization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	[JsonFormatter(typeof(TaskIdFormatter))]
	public class TaskId : IUrlParameter, IEquatable<TaskId>
	{
		/// <summary>
		/// A task id exists in the form [node_id]:[task_id]
		/// </summary>
		/// <param name="taskId">the task identifier</param>
		public TaskId(string taskId)
		{
			if (string.IsNullOrWhiteSpace(taskId))
				throw new ArgumentException("TaskId can not be an empty string", nameof(taskId));

			var tokens = taskId.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
			if (tokens.Length != 2)
				throw new ArgumentException($"TaskId:{taskId} not in expected format of <node_id>:<task_id>", nameof(taskId));

			NodeId = tokens[0];
			FullyQualifiedId = taskId;

			if (!long.TryParse(tokens[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var t) || t < -1 || t == 0)
				throw new ArgumentException($"TaskId task component:{tokens[1]} could not be parsed to long or is out of range", nameof(taskId));

			TaskNumber = t;
		}

		public string FullyQualifiedId { get; }
		public string NodeId { get; }
		public long TaskNumber { get; }

		private string DebugDisplay => FullyQualifiedId;

		public bool Equals(TaskId other) => EqualsString(other?.FullyQualifiedId);

		public string GetString(IConnectionConfigurationValues settings) => FullyQualifiedId;

		public override string ToString() => FullyQualifiedId;

		public static implicit operator TaskId(string taskId) => taskId.IsNullOrEmpty() ? null : new TaskId(taskId);

		public static bool operator ==(TaskId left, TaskId right) => Equals(left, right);

		public static bool operator !=(TaskId left, TaskId right) => !Equals(left, right);

		public override bool Equals(object obj) =>
			obj != null && obj is string s ? EqualsString(s) : obj is TaskId i && EqualsString(i.FullyQualifiedId);

		private bool EqualsString(string other) => !other.IsNullOrEmpty() && other == FullyQualifiedId;

		public override int GetHashCode()
		{
			unchecked
			{
				return (NodeId.GetHashCode() * 397) ^ TaskNumber.GetHashCode();
			}
		}
	}

	internal class TaskIdFormatter : IJsonFormatter<TaskId>, IObjectPropertyNameFormatter<TaskId>
	{
		public TaskId Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.String)
				return new TaskId(reader.ReadString());

			reader.ReadNextBlock();
			return null;
		}

		public void Serialize(ref JsonWriter writer, TaskId value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteString(value.ToString());
		}

		public TaskId DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			Deserialize(ref reader, formatterResolver);

		public void SerializeToPropertyName(ref JsonWriter writer, TaskId value, IJsonFormatterResolver formatterResolver) =>
			Serialize(ref writer, value, formatterResolver);
	}
}
