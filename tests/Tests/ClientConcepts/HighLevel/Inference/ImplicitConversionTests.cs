// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Reflection;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Domain;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	public class ImplicitConversionTests
	{
		private static T Implicit<T>(T i) => i;

		[U] public void Ids()
		{
			Implicit<Ids>(null).Should().BeNull();
			Implicit<Ids>("").Should().BeNull();
			Implicit<Ids>("   ").Should().BeNull();
			Implicit<Ids>(",, ,,").Should().BeNull();
			Implicit<Ids>(new string[] { }).Should().BeNull();
			Implicit<Ids>(new string[] { null, null }).Should().BeNull();
		}

		[U] public void LongId() => Implicit<LongId>(null).Should().BeNull();

		[U] public void DocumentPath()
		{
			Implicit<DocumentPath<Project>>((Project)null).Should().BeNull();
			Implicit<DocumentPath<Project>>((Id)null).Should().BeNull();
			Implicit<DocumentPath<Project>>((string)null).Should().BeNull();
			Implicit<DocumentPath<Project>>("").Should().BeNull();
			Implicit<DocumentPath<Project>>("   ").Should().BeNull();
		}

		[U] public void Field()
		{
			Implicit<Field>((string)null).Should().BeNull();
			Implicit<Field>((Expression)null).Should().BeNull();
			Implicit<Field>((PropertyInfo)null).Should().BeNull();
			Implicit<Field>("").Should().BeNull();
			Implicit<Field>("   ").Should().BeNull();
		}

		[U] public void Fields()
		{
			Implicit<Fields>((Expression)null).Should().BeNull();
			Implicit<Fields>((Field)null).Should().BeNull();
			Implicit<Fields>((string)null).Should().BeNull();
			Implicit<Fields>((PropertyInfo)null).Should().BeNull();
			Implicit<Fields>((string[])null).Should().BeNull();
			Implicit<Fields>((Expression[])null).Should().BeNull();
			Implicit<Fields>((PropertyInfo[])null).Should().BeNull();
			Implicit<Fields>((Field[])null).Should().BeNull();
			Implicit<Fields>("").Should().BeNull();
			Implicit<Fields>("   ").Should().BeNull();
			Implicit<Fields>(new string[] { }).Should().BeNull();
			Implicit<Fields>(new Expression[] { }).Should().BeNull();
			Implicit<Fields>(new PropertyInfo[] { }).Should().BeNull();
			Implicit<Fields>(new Field[] { }).Should().BeNull();
			Implicit<Fields>(new Expression[] { null, null }).Should().BeNull();
			Implicit<Fields>(new PropertyInfo[] { null, null }).Should().BeNull();
			Implicit<Fields>(new Field[] { null, null }).Should().BeNull();
		}

		[U] public void Id()
		{
			Implicit<Id>(null).Should().BeNull();
			Implicit<Id>("").Should().BeNull();
			Implicit<Id>("   ").Should().BeNull();
		}

		[U] public void IndexName()
		{
			Implicit<IndexName>((string)null).Should().BeNull();
			Implicit<IndexName>((Type)null).Should().BeNull();
			Implicit<IndexName>("").Should().BeNull();
			Implicit<IndexName>("   ").Should().BeNull();
		}

		[U] public void Indices()
		{
			Implicit<Nest.Indices>((string)null).Should().BeNull();
			Implicit<Nest.Indices>((Nest.Indices.ManyIndices)null).Should().BeNull();
			Implicit<Nest.Indices>((string[])null).Should().BeNull();
			Implicit<Nest.Indices>((IndexName)null).Should().BeNull();
			Implicit<Nest.Indices>((IndexName[])null).Should().BeNull();
			Implicit<Nest.Indices>((IndexName)null).Should().BeNull();
			Implicit<Nest.Indices>("").Should().BeNull();
			Implicit<Nest.Indices>("    ").Should().BeNull();
			Implicit<Nest.Indices>(",, ,,    ").Should().BeNull();
			Implicit<Nest.Indices>(new string[] { }).Should().BeNull();
			Implicit<Nest.Indices>(new IndexName[] { }).Should().BeNull();
			Implicit<Nest.Indices>(new string[] { null, null }).Should().BeNull();
			Implicit<Nest.Indices>(new IndexName[] { null, null }).Should().BeNull();
		}

		[U] public void Names()
		{
			Implicit<Names>((string)null).Should().BeNull();
			Implicit<Names>((string[])null).Should().BeNull();
			Implicit<Names>("").Should().BeNull();
			Implicit<Names>(",,").Should().BeNull();
			Implicit<Names>(",   ,").Should().BeNull();
			Implicit<Names>("   ").Should().BeNull();
			Implicit<Names>(new string[] { }).Should().BeNull();
			Implicit<Names>(new string[] { null, null }).Should().BeNull();
		}

		[U] public void Routing()
		{
			Implicit<Routing>((string)null).Should().BeNull();
			Implicit<Routing>((string[])null).Should().BeNull();
			Implicit<Routing>("").Should().BeNull();
			Implicit<Routing>(",,").Should().BeNull();
			Implicit<Routing>(",   ,").Should().BeNull();
			Implicit<Routing>("   ").Should().BeNull();
			Implicit<Routing>(new string[] { }).Should().BeNull();
			Implicit<Routing>(new string[] { null, null }).Should().BeNull();
		}

		[U] public void Metrics() => Implicit<Metrics>(null).Should().BeNull();

		[U] public void IndexMetrics() => Implicit<IndexMetrics>(null).Should().BeNull();

		[U] public void Name()
		{
			Implicit<Name>(null).Should().BeNull();
			Implicit<Name>("").Should().BeNull();
			Implicit<Name>("   ").Should().BeNull();
		}

		[U] public void NodeId()
		{
			Implicit<NodeIds>((string)null).Should().BeNull();
			Implicit<NodeIds>((string[])null).Should().BeNull();
			Implicit<NodeIds>("").Should().BeNull();
			Implicit<NodeIds>("  ").Should().BeNull();
			Implicit<NodeIds>("  ,, , ,,").Should().BeNull();
			Implicit<NodeIds>(new string[] { }).Should().BeNull();
			Implicit<NodeIds>(new string[] { null, null }).Should().BeNull();
		}

		[U] public void PropertyName()
		{
			Implicit<PropertyName>((Expression)null).Should().BeNull();
			Implicit<PropertyName>((PropertyInfo)null).Should().BeNull();
			Implicit<PropertyName>((string)null).Should().BeNull();
			Implicit<PropertyName>("").Should().BeNull();
			Implicit<PropertyName>("  ").Should().BeNull();
		}

		[U] public void RelationName()
		{
			Implicit<RelationName>((string)null).Should().BeNull();
			Implicit<RelationName>((Type)null).Should().BeNull();
			Implicit<RelationName>("   ").Should().BeNull();
		}

		[U] public void TaskId()
		{
			Implicit<TaskId>((string)null).Should().BeNull();
			Implicit<TaskId>("    ").Should().BeNull();
			Implicit<TaskId>("").Should().BeNull();
		}
	}
}
