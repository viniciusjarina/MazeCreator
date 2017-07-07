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

using System;
using MazeCreator.Extensions;

namespace MazeCreator.Core
{
	public struct Cell : IEquatable<Cell>
	{
		CellInfo info;

		readonly static CellInfo [] removeWallFlags = {
			CellInfo.RemoveTopWall,
			CellInfo.RemoveLeftWall,
			CellInfo.RemoveBottomWall,
			CellInfo.RemoveRightWall,
		};

		public Cell (CellInfo info)
		{
			this.info = info;
		}

		public static Cell EmptyCell {
			get {
				return new Cell (CellInfo.EmptyCell);
			}
		}

		public CellInfo CellInfo {
			get {
				return info;
			}
		}

		public bool HasLeftBorder {
			get {
				return (info & CellInfo.LeftBorder) == CellInfo.LeftBorder;
			}
		}

		public bool HasTopBorder {
			get {
				return (info & CellInfo.TopBorder) == CellInfo.TopBorder;
			}
		}

		public bool HasBottomBorder {
			get {
				return (info & CellInfo.BottomBorder) == CellInfo.BottomBorder;
			}
		}

		public bool HasRightBorder {
			get {
				return (info & CellInfo.RightBorder) == CellInfo.RightBorder;
			}
		}

		public bool HasLeftWall {
			get {
				return (info & CellInfo.LeftWall) == CellInfo.LeftWall;
			}
		}

		public bool HasTopWall {
			get {
				return (info & CellInfo.TopWall) == CellInfo.TopWall;
			}
		}

		public bool HasBottomWall {
			get {
				return (info & CellInfo.BottomWall) == CellInfo.BottomWall;
			}
		}

		public bool HasRightWall {
			get {
				return (info & CellInfo.RightWall) == CellInfo.RightWall;
			}
		}

		public bool HasAllWalls {
			get {
				return (info & CellInfo.AllWalls) == CellInfo.AllWalls;
			}
		}

		public void RemoveStartWall (Direction direction)
		{
			info &= removeWallFlags [(int)direction];
		}

		public void RemoveEndWall (Direction direction)
		{
			info &= removeWallFlags [(int)direction.Oposite()];
		}

		public override bool Equals (object obj)
		{
			var cell = obj as Cell?;
			if (cell == null)
				return false;
			return Equals(cell);
		}

		public bool Equals (Cell other)
		{
			return this == other;
		}

		public static bool operator == (Cell lhs, Cell rhs)
		{
			return lhs.CellInfo == rhs.CellInfo;
		}

		public static bool operator != (Cell lhs, Cell rhs)
		{
			return !(lhs == rhs);
		}

		public override int GetHashCode ()
		{
			return CellInfo.GetHashCode ();
		}


	}
}
