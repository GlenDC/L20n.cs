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
			public class Entry
			{
				public static Types.Entry Parse(CharStream stream)
				{
					int startingPos = stream.Position;
					try {
						char c = stream.ForceReadNext("expected to read the first char of the opening tag for an <entry>");
						switch (c) {
						case '/':
							return ParseComment(stream);

						default:
							throw stream.CreateException("expected to read '/' to start a <comment>", -1);
						}
					}
					catch(Exception exception) {
						throw new IOException(
							String.Format(
								"something went wrong during parsing of an <entry> starting at {0}",
								stream.ComputeDetailedPosition(startingPos)),
							exception);
					}
				}

				private static Types.Entry ParseComment(CharStream stream)
				{
					char c = stream.ForceReadNext("expected to read '*' to start a <comment>");
					if(c != '*')
						throw stream.CreateException("expected to read '*' to start a <comment>", -1);

					return Comment.Parse(stream);
				}
			}
		}
	}
}
