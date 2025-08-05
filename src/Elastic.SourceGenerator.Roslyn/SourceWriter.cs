using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

using Microsoft.CodeAnalysis.Text;

namespace Elastic.SourceGenerator.Roslyn;

/// <summary>
/// A utility class for generating indented source code.
/// </summary>
public sealed class SourceWriter
{
	private readonly StringBuilder _sb = new();
	private int _indentation;

	/// <summary>
	/// Creates a new instance of <see cref="SourceWriter"/>.
	/// </summary>
	public SourceWriter()
	{
		IndentationChar = ' ';
		CharsPerIndentation = 4;
	}

	/// <summary>
	/// Creates a new instance of <see cref="SourceWriter"/> with the specified indentation settings.
	/// </summary>
	/// <param name="indentationChar">The indentation character to be used.</param>
	/// <param name="charsPerIndentation">The number of characters per indentation to be applied.</param>
	public SourceWriter(char indentationChar, int charsPerIndentation)
	{
		if (!char.IsWhiteSpace(indentationChar))
		{
			throw new ArgumentOutOfRangeException(nameof(indentationChar));
		}

		if (charsPerIndentation < 1)
		{
			throw new ArgumentOutOfRangeException(nameof(charsPerIndentation));
		}

		IndentationChar = indentationChar;
		CharsPerIndentation = charsPerIndentation;
	}

	/// <summary>
	/// Gets the character used for indentation.
	/// </summary>
	public char IndentationChar { get; }

	/// <summary>
	/// Gets the number of characters per indentation.
	/// </summary>
	public int CharsPerIndentation { get; }

	/// <summary>
	/// Gets the length of the generated source text.
	/// </summary>
	public int Length => _sb.Length;

	/// <summary>
	/// Gets or sets the current indentation level.
	/// </summary>
	public int Indentation
	{
		get => _indentation;
		set
		{
			if (value < 0)
			{
				Throw();
				static void Throw() => throw new ArgumentOutOfRangeException(nameof(value));
			}

			_indentation = value;
		}
	}

	/// <summary>
	/// Appends a new line with the current character.
	/// </summary>
	public void WriteLine(char value)
	{
		AddIndentation();
		_sb.Append(value);
		_sb.AppendLine();
	}

	/// <summary>
	/// Appends a new line with the specified text.
	/// </summary>
	/// <param name="text">The text to append.</param>
	/// <param name="disableIndentation">Append text without preserving the current indentation.</param>
	public void WriteLine([StringSyntax("c#-test")] string text, bool disableIndentation = false)
	{
		if (_indentation == 0 || disableIndentation)
		{
			_sb.AppendLine(text);
			return;
		}

		bool isFinalLine;
		var remainingText = text.AsSpan();
		do
		{
			var nextLine = GetNextLine(ref remainingText, out isFinalLine);

			AddIndentation();
			AppendSpan(nextLine);
			_sb.AppendLine();
		}
		while (!isFinalLine);
	}

	/// <summary>
	/// Appends a new line to the source text.
	/// </summary>
	public void WriteLine() => _sb.AppendLine();

	/// <summary>
	/// Encodes the currently written source to a <see cref="SourceText"/> instance.
	/// </summary>
	public SourceText ToSourceText()
	{
		Debug.Assert(_indentation == 0 && _sb.Length > 0);
		return SourceText.From(_sb.ToString(), Encoding.UTF8);
	}

	/// <summary>
	/// Renders the written text as a string.
	/// </summary>
	public override string ToString()
	{
		return _sb.ToString();
	}

	private void AddIndentation()
	{
		_sb.Append(IndentationChar, CharsPerIndentation * _indentation);
	}

	private static ReadOnlySpan<char> GetNextLine(ref ReadOnlySpan<char> remainingText, out bool isFinalLine)
	{
		if (remainingText.IsEmpty)
		{
			isFinalLine = true;
			return default;
		}

		ReadOnlySpan<char> rest;

		var lineLength = remainingText.IndexOf('\n');
		if (lineLength == -1)
		{
			lineLength = remainingText.Length;
			isFinalLine = true;
			rest = default;
		}
		else
		{
			rest = remainingText[(lineLength + 1)..];
			isFinalLine = false;
		}

		if ((uint)lineLength > 0 && remainingText[lineLength - 1] == '\r')
		{
			lineLength--;
		}

		var next = remainingText[..lineLength];
		remainingText = rest;
		return next;
	}

	private unsafe void AppendSpan(ReadOnlySpan<char> span)
	{
		fixed (char* ptr = span)
		{
			_sb.Append(ptr, span.Length);
		}
	}
}
