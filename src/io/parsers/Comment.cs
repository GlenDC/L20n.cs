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
			public class Comment
			{
				public static Types.Entry Parse(CharStream stream)
				{
					char c; string content = "";
					while (stream.ReadNext(out c)) {
						if(c == '*' && stream.SkipIfPossible('/')) {
							return new Types.Comment(content);
						}
						content += c;
					}

					throw new IOException(
						"a comment entry was opened, but not closed ",
						stream.CreateEOFException());
				}
			}
		}
	}
}
