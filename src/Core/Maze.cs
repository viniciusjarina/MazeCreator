﻿/*
 * This file is part of MazeCreator.
 * 
 * Copyright (c) 2017 Vinicius Jarina (viniciusjarina@gmail.com)
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

namespace MazeCreator.Core
{
	public class Maze
	{
		public int Columns {
			get;
		}

		public int Lines {
			get;
		}

		public int TotalCells {
			get {
				return Columns * Lines;
			}
		}

		readonly Cell [] cells;

		public Maze (int lines, int columns)
		{
			Columns = columns;
			Lines = lines;

			cells = new Cell [lines * columns];
			for (int i = 0; i < TotalCells; i++)
				cells [i] = new Cell (CellInfo.AllWalls);
			                                             
			for (int i = 0; i < Lines; i++) {
				int index = IndexFromPosition (new Position (i, 0));
				cells [index] = new Cell (CellInfo.AllWalls | CellInfo.LeftBorder);

				index = IndexFromPosition (new Position (i, Columns - 1));
				cells [index] = new Cell (CellInfo.AllWalls | CellInfo.RightBorder);
			}

			for (int i = 0; i < Columns; i++) {
				int index = IndexFromPosition (new Position (0, i));
				cells [index] = new Cell (CellInfo.AllWalls | CellInfo.TopBorder);

				index = IndexFromPosition (new Position (Lines - 1, i));
				cells [index] = new Cell (CellInfo.AllWalls | CellInfo.BottomBorder);
			}
		}

		public static Position GetNextPosition (Position position, Direction direction)
		{
			switch (direction) {
				case Direction.Up:
					return new Position (position.Line - 1, position.Column);
				case Direction.Left:
					return new Position (position.Line, position.Column - 1);
				case Direction.Down:
					return new Position (position.Line + 1, position.Column);
				case Direction.Right:
					return new Position (position.Line, position.Column + 1);
				}
			return position;
		}

		public static Position GetPreviousPosition (Position position, Direction direction)
		{
			switch (direction) {
			case Direction.Up:
				return new Position (position.Line + 1, position.Column);
			case Direction.Left:
				return new Position (position.Line, position.Column + 1);
			case Direction.Down:
				return new Position (position.Line - 1, position.Column);
			case Direction.Right:
				return new Position (position.Line, position.Column - 1);
			}
			return position;
		}

		public int IndexFromPosition (Position position)
		{
			return Columns * position.Line + position.Column;
		}

		bool IsValidPosition (Position position)
		{
			return 0 <= position.Line && position.Line < Lines &&
				   0 <= position.Column && position.Column < Columns;
		}

		public Cell CellAt (Position position)
		{
			return this [position];
		}

		public Cell CellAt (int line, int column)
		{
			return this [new Position (line, column)];
		}

		public Cell this [int line, int column] {
			get {
				return this [new Position (line, column)];
			}
		}

		public Cell this[Position position] {
			get {
				if (!IsValidPosition (position))
					return Cell.EmptyCell;
				
				int index = IndexFromPosition (position);
				return cells [index];
			}
			set {
				if (!IsValidPosition (position))
					return;
				
				int index = IndexFromPosition (position);
				cells [index] = value;
			}
		}

		public void RemoveWalls (Position position, Position nextPosition, Direction direction)
		{
			Cell start	= this [position];
			Cell end 	= this [nextPosition];

			start.RemoveStartWall (direction);
			end.RemoveEndWall (direction);

			this [position] 	= start;
			this [nextPosition] = end;
		}
	}
}