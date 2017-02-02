﻿
/*
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

using System.Collections.Generic;
using System.Linq;

using MazeCreator.Core;

namespace MazeCreator.Creator
{
	public class DFS : ICreator
	{
		Direction [] GetAvailableDirections (Maze maze, Position position)
		{
			var directions = new List<Direction> ();

			if (CanGoUp (position, maze))
				directions.Add (Direction.Up);
			if (CanGoLeft (position, maze))
				directions.Add (Direction.Left);
			if (CanGoDown (position, maze))
				directions.Add (Direction.Down);
			if (CanGoRight (position, maze))
				directions.Add (Direction.Right);
			
			return directions.ToArray ();
		}

		bool CanGoUp (Position position, Maze maze)
		{
			Cell cell = maze [position];
			return !cell.HasTopBorder && maze [position.Up].HasAllWalls;
		}

		bool CanGoLeft (Position position, Maze maze)
		{
			Cell cell = maze [position];
			return !cell.HasLeftBorder && maze [position.Left].HasAllWalls;
		}

		bool CanGoDown (Position position, Maze maze)
		{
			Cell cell = maze [position];
			return !cell.HasBottomBorder && maze [position.Down].HasAllWalls;
		}

		bool CanGoRight (Position position, Maze maze)
		{
			Cell cell = maze [position];
			return !cell.HasRightBorder && maze [position.Right].HasAllWalls;
		}

		Direction GetRandomDirection (Direction [] directions, IRandomGenerator random)
		{
			if (directions.Length == 1)
				return directions [0];
			return directions [random.Next (directions.Length)];
		}



		public Maze Create (int lines, int columns, IRandomGenerator random)
		{
			Maze maze = new Maze (lines, columns);
			var position = Position.RandomPosition (lines, columns, random);

			int totalCells = maze.TotalCells;
			var backtrack = new Direction [totalCells];
			int backtrackPosition = 0;

			int visited = 1;

			while (visited < totalCells) {

				var directions = GetAvailableDirections (maze, position);

				if (directions.Any ()) {

					var direction = GetRandomDirection (directions, random);
					var nextPosition = Maze.GetNextPosition (position, direction);

					maze.RemoveWalls (position, nextPosition, direction);

					backtrack [backtrackPosition] = direction;
					backtrackPosition++;

					position = nextPosition;

					visited++;
				} else {
					backtrackPosition--;
					position = Maze.GetPreviousPosition (position, backtrack [backtrackPosition]);
				}
			}

			return maze;
		}

	}
}