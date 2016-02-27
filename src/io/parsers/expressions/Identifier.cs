/**
 * This source file is part of the Commercial L20n Unity Plugin.
 * 
 * Copyright (c) 2016 - 2017 Glen De Cauwsemaecker (contact@glendc.com)
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.IO;

namespace L20n
{
	namespace IO
	{
		namespace Parsers
		{
			namespace Expressions
			{
				public class Identifier
				{
					public static Types.Internal.Expressions.Identifier Parse(CharStream stream)
					{
						var startingPos = stream.Position;

						try {
							string id;
							if (RawIdentifier.PeekAndParse (stream, out id))
								return new Types.Internal.Expressions.Identifier(id);
							
							Types.Internal.Expressions.Identifier identifier;
							if (Variable.PeekAndParse (stream, out identifier))
								return identifier;
							
							if (Global.PeekAndParse (stream, out identifier))
								return identifier;
							
							if (This.PeekAndParse (stream, out identifier))
								return identifier;
						}
						catch(Exception e) {
							string msg = String.Format(
								"something went wrong parsing an <identifier> starting at {0}",
								stream.ComputeDetailedPosition(startingPos));
							throw new IOException(msg, e);
						}

						throw stream.CreateException (
							"no valid start for an <identnfier_expression> could be found");
					}

					public static bool Peek(CharStream stream)
					{
						return RawIdentifier.Peek(stream)
							|| Variable.Peek(stream)
							|| Global.Peek(stream)
							|| This.Peek(stream);
					}
				}
			}
		}
	}
}
