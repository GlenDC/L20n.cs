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
using NUnit.Framework;

using L20n.Utils;
using L20n.Exceptions;

namespace L20nTests
{
	[TestFixture()]
	public class UtilTests
	{	
		[Test()]
		public void OptionalTests()
		{
			var a = new Option<string>();
			Assert.IsFalse(a.IsSet);
			Assert.Throws<UnexpectedObjectException>(() => a.Unwrap());
			Assert.AreEqual("Oi", a.UnwrapOr("Oi"));

			var b = new Option<string>("Hello, World!");
			Assert.IsTrue(b.IsSet);
			Assert.AreEqual("Hello, World!", b.Unwrap());
			Assert.AreEqual("Hello, World!", b.UnwrapOr("Goodbye!"));

			var c = new Option<int>(5);
			Assert.AreEqual(42, c.Map((x) => new Option<int>(37 + x)).Unwrap());
			
			var d = new Option<int>();
			Assert.IsFalse (d.Map((x) => new Option<int>(x)).IsSet);
			Assert.AreEqual(42, d.MapOr(42, (x) => x));
			Assert.AreEqual(42, d.MapOrElse((x) => x, () => c.MapOr(0, (x) => 37 + x)));

			Assert.IsFalse(a.And(b).IsSet);
			Assert.IsTrue(b.And(c).IsSet);
			Assert.AreEqual(5, b.And(c).Unwrap());
			
			Assert.IsTrue(a.Or(b).IsSet);
			Assert.AreEqual(b.Or(a), a.Or(b));
			Assert.AreEqual(5, d.Or(c).Unwrap());
			
			Assert.AreEqual(5, c.OrElse(() => new Option<int>(42)).Unwrap());
			Assert.AreEqual(42, d.OrElse(() => new Option<int>(42)).Unwrap());
		}

		[Test()]
		public void ShadowStackTests()
		{
			var stack = new ShadowStack<int>();
			
			Assert.Throws<InvalidOperationException>(() => stack.Pop("oops"));

			stack.Push("apples", 1);
			stack.Push("bananas", 2);
			stack.Push("apples", 5);
			
			Assert.AreEqual(5, stack.Peek("apples"));
			Assert.AreEqual(2, stack.Peek("bananas"));

			Assert.AreEqual(5, stack.Pop("apples"));
			Assert.AreEqual(2, stack.Pop("bananas"));
			
			stack.Push("bananas", 8);
			stack.Push("oranges", 99);
			stack.Push("apples", 34);
			
			Assert.AreEqual(8, stack.Pop("bananas"));
			Assert.AreEqual(34, stack.Pop("apples"));
			Assert.AreEqual(1, stack.Pop("apples"));
			Assert.AreEqual(99, stack.Pop("oranges"));

			Assert.Throws<InvalidOperationException>(() => stack.Pop ("apples"));
		}
	}
}