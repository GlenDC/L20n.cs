// Glen De Cauwsemaecker licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System;

using L20nCore.Common.IO;
using L20nCore.Common.Exceptions;
using System.Text.RegularExpressions;

namespace L20nCore
{
	namespace L20n
	{
		namespace FTL
		{
			namespace Parsers
			{	
				/// <summary>
				/// The combinator parser used to parse an identifier.
				/// </summary>
				public static class Identifier
				{
					public static readonly Regex Regex = new Regex(@"[a-zA-Z_.?\-][a-zA-Z0-9_.?\-]*");

					public static L20n.FTL.AST.StringPrimitive Parse(CharStream stream)
					{
						string value = stream.ForceReadReg(Regex);
						return new L20n.FTL.AST.StringPrimitive(value, L20n.FTL.AST.StringPrimitive.Kind.Identifier);
					}

					public static bool PeekAndParse(CharStream stream, out L20n.FTL.AST.INode result)
					{
						if (!stream.PeekReg(s_RegPeek))
						{
							result = null;
							return false;
						}

						result = Parse(stream);
						return true;
					}

					private static readonly Regex s_RegPeek = new Regex(@"[a-zA-Z.?\-]");
				}
			}
		}
	}
}