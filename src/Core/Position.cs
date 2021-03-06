﻿
/*
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

using System;

namespace MazeCreator.Core
{
	public struct Position : IEquatable<Position>
	{
		public Position (int row, int column)
		{
			Row = row;
			Column = column;
		}

		public int Row {
			get; set;
		}

		public int Column {
			get; set;
		}

		public Position Up {
			get {
				return new Position (Row - 1, Column);
			}
		}

		public Position UpLeft {
			get {
				return new Position (Row - 1, Column - 1);
			}
		}

		public Position Left {
			get {
				return new Position (Row, Column - 1);
			}
		}

		public Position Down {
			get {
				return new Position (Row + 1, Column);
			}
		}

		public Position Right {
			get {
				return new Position (Row, Column + 1);
			}
		}

		public static Position RandomPosition (int maxRow, int maxColumn, IRandomGenerator random)
		{
			int row = random.Next (maxRow);
			int column = random.Next (maxColumn);
			return new Position (row, column);
		}

		public static Position GetNextPosition (Position position, Direction direction)
		{
			switch (direction) {
			case Direction.Up:
				return new Position (position.Row - 1, position.Column);
			case Direction.Left:
				return new Position (position.Row, position.Column - 1);
			case Direction.Down:
				return new Position (position.Row + 1, position.Column);
			case Direction.Right:
				return new Position (position.Row, position.Column + 1);
			}
			return position;
		}

		public static Position GetPreviousPosition (Position position, Direction direction)
		{
			switch (direction) {
			case Direction.Up:
				return new Position (position.Row + 1, position.Column);
			case Direction.Left:
				return new Position (position.Row, position.Column + 1);
			case Direction.Down:
				return new Position (position.Row - 1, position.Column);
			case Direction.Right:
				return new Position (position.Row, position.Column - 1);
			}
			return position;
		}

		public static int IndexFromPosition (Position position, int columns)
		{
			return columns * position.Row + position.Column;
		}

		public override bool Equals (object obj)
		{
			return obj is Position && Equals ((Position)obj);
		}

		public static bool operator == (Position lhs, Position rhs)
		{
			return lhs.Row == rhs.Row && lhs.Column == rhs.Column;
		}

		public static bool operator != (Position lhs, Position rhs)
		{
			return !(lhs == rhs);
		}

		public override int GetHashCode ()
		{
			return (Row.GetHashCode () << 8) ^ Column.GetHashCode ();
		}

		public override string ToString ()
		{
			return string.Format ("[Position: Row={0}, Column={1}]", Row, Column);
		}

		public bool Equals (Position other)
		{
			return this == other;
		}
	}
}
