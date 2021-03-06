// Glen De Cauwsemaecker licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace L20nCore
{
	namespace IO
	{
		namespace Parsers
		{	
			/// <summary>
			/// The combinator parser used to parse a Literal,
			/// a primitive and consant integer value.
			/// </summary>
			public static class Literal
			{
				public static AST.INode Parse(CharStream stream)
				{
					string raw;
					if (!stream.ReadReg(@"[\-\+]?[0-9]+", out raw))
					{
						throw stream.CreateException("a number literal whas expected");
					}

					return new AST.Literal(raw);
				}

				public static bool Peek(CharStream stream)
				{
					return stream.PeekReg(@"[\-\+0-9]");
				}

				public static bool PeekAndParse(
					CharStream stream, out AST.INode literal)
				{
					if (!Literal.Peek(stream))
					{
						literal = null;
						return false;
					}

					literal = Literal.Parse(stream);
					return true;
				}
			}
		}
	}
}
