﻿/*
 * This file is part of MazeCreator.
 * 
 * Copyright (c)  2021 Vinicius Jarina (viniciusjarina@gmail.com)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using MazeCreator.Core;

namespace MazeCreator
{
	public struct PositionDirection
	{
		public PositionDirection (Position position, Direction direction)
		{
			Position  = position;
			Direction = direction;
		}

		public Position Position {
			get;
			set;
		}

		public Direction Direction {
			get;
			set;
		}

		public override bool Equals (object obj)
		{
			var positionDirection = obj as PositionDirection?;
			if (positionDirection == null)
				return false;
			return this == positionDirection;
		}

		public static bool operator == (PositionDirection lhs, PositionDirection rhs)
		{
			return lhs.Position == rhs.Position && lhs.Direction == rhs.Direction;
		}

		public static bool operator != (PositionDirection lhs, PositionDirection rhs)
		{
			return !(lhs == rhs);
		}

		public override int GetHashCode ()
		{
			return (Position.GetHashCode () << 8) ^ Direction.GetHashCode ();
		}

		public override string ToString ()
		{
			return string.Format ("[Position: {0}, Direction={1}]", Position.ToString (), Direction.ToString ());
		}
	}
}
