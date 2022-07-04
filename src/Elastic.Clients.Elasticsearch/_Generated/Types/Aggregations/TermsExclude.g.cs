// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using Elastic.Transport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Aggregations
{
	[SingleOrMany(typeof(string))]
	public partial class TermsExclude : IList<string>
	{
		private readonly IList<string> _backingList = new List<string>();
		public string this[int index] { get => _backingList[index]; set => _backingList[index] = value; }

		public int Count => _backingList.Count;
		public bool IsReadOnly => _backingList.IsReadOnly;
		public void Add(string item) => _backingList.Add(item);
		public void Clear() => _backingList.Clear();
		public bool Contains(string item) => _backingList.Contains(item);
		public void CopyTo(string[] array, int arrayIndex) => _backingList.CopyTo(array, arrayIndex);
		public IEnumerator<string> GetEnumerator() => _backingList.GetEnumerator();
		public int IndexOf(string item) => _backingList.IndexOf(item);
		public void Insert(int index, string item) => _backingList.Insert(index, item);
		public bool Remove(string item) => _backingList.Remove(item);
		public void RemoveAt(int index) => _backingList.RemoveAt(index);
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_backingList).GetEnumerator();
	}
}