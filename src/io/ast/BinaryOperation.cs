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
using L20n.Exceptions;

namespace L20n
{
	namespace IO
	{
		namespace AST
		{
			public sealed class BinaryOperation : INode
			{
				INode m_First;
				INode m_Second;
				Operation m_Operation;
				
				public BinaryOperation(INode first, INode second, string op)
				{
					m_First = first;
					m_Second = second;
					
					switch (op) {
					case "<":
						m_Operation = Operation.LessThan;
						break;
						
					case ">":
						m_Operation = Operation.GreaterThan;
						break;
						
					case "<=":
						m_Operation = Operation.LessThanOrEqual;
						break;
						
					case ">=":
						m_Operation = Operation.GreaterThanOrEqual;
						break;
						
					case "+":
						m_Operation = Operation.Add;
						break;
						
					case "-":
						m_Operation = Operation.Subtract;
						break;
						
					case "*":
						m_Operation = Operation.Multiply;
						break;
						
					case "/":
						m_Operation = Operation.Divide;
						break;
						
					case "%":
						m_Operation = Operation.Modulo;
						break;
						
					case "==":
						m_Operation = Operation.IsEqual;
						break;
						
					case "!=":
						m_Operation = Operation.IsNotEqual;
						break;
						
					default:
						throw new ParseException(
							String.Format("{0} is not a valid <binary> operation", op));
					}
				}
				
				public L20n.Objects.L20nObject Eval()
				{
					var first = m_First.Eval();
					var second = m_Second.Eval();
					
					switch (m_Operation) {
					case Operation.LessThan:
						return new L20n.Objects.LessThanExpression(first, second);
						
					case Operation.GreaterThan:
						return new L20n.Objects.GreaterThanExpression(first, second);
						
					case Operation.LessThanOrEqual:
						return new L20n.Objects.LessThanOrEqualExpression(first, second);
						
					case Operation.GreaterThanOrEqual:
						return new L20n.Objects.GreaterThanOrEqualExpression(first, second);
						
					case Operation.Add:
						return new L20n.Objects.AddExpression(first, second);
						
					case Operation.Subtract:
						return new L20n.Objects.SubstractExpression(first, second);
						
					case Operation.Multiply:
						return new L20n.Objects.MultiplyExpression(first, second);
						
					case Operation.Divide:
						return new L20n.Objects.DivideExpression(first, second);
						
					case Operation.Modulo:
						return new L20n.Objects.ModuloExpression(first, second);
						
					case Operation.IsEqual:
						return new L20n.Objects.IsEqualExpression(first, second);
						
					case Operation.IsNotEqual:
						return new L20n.Objects.IsNotEqualExpression(first, second);
					}
					
					throw new EvaluateException(
						String.Format("{0} is not a valid <binary> operation", m_Operation));
				}
				
				public string Display()
				{
					string op = null;
					switch (m_Operation) {
					case Operation.LessThan: op = "<"; break;
					case Operation.GreaterThan: op = ">"; break;
					case Operation.LessThanOrEqual: op = "<="; break;
					case Operation.GreaterThanOrEqual: op = ">="; break;
					case Operation.Add: op = "+"; break;
					case Operation.Subtract: op = "-"; break;
					case Operation.Multiply: op = "*"; break;
					case Operation.Divide: op = "/"; break;
					case Operation.Modulo: op = "%"; break;
					case Operation.IsEqual: op = "=="; break;
					case Operation.IsNotEqual: op = "!="; break;
					}

					return string.Format("{0}{1}{2}",
						m_First.Display(), op, m_Second.Display());
				}
				
				enum Operation
				{
					LessThan,				// <
					GreaterThan,			// >
					LessThanOrEqual,		// <=
					GreaterThanOrEqual,		// >=
					Add,					// +
					Subtract,				// -
					Multiply,				// *
					Divide,					// /
					Modulo,					// %
					IsEqual,				// ==
					IsNotEqual,				// !=
				}
			}
		}
	}
}