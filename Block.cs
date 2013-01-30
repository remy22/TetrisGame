using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Nettrix {
  public class Block {

		public enum BlockTypes {
			Undefined = 0,
			Square = 1,
			Line = 2,
			J = 3,
			L = 4,
			T = 5,
			Z = 6,
			S = 7
		};
		private BlockTypes blockType;

		public BlockTypes BlockType {
			get { return blockType; }
			set { blockType = value; }
		}

		public enum RotationDirections {
			North = 1,
			East = 2,
			South = 3,
			West = 4
		};

		private RotationDirections statusRotation = RotationDirections.North;

		public RotationDirections StatusRotation {
			get { return statusRotation; }
			set { statusRotation = value; }
		}

		private Color[] backColors = {Color.Empty, Color.Red, Color.Blue, Color.Red, Color.Yellow, Color.Green, Color.White, Color.Black};
		private Color[] foreColors = {Color.Empty, Color.Purple, Color.LightBlue, Color.Yellow, Color.Red, Color.LightGreen, Color.Black, Color.White};

		private Square square1;
		private Square square2;
		private Square square3;
		private Square square4;

		public Square Square1 {
			get { return square1; }
			set { square1 = value; }
		}

		public Square Square2 {
			get { return square2; }
			set { square2 = value; }
		}

		public Square Square3 {
			get { return square3; }
			set { square3 = value; }
		}

		public Square Square4 {
			get { return square4; }
			set { square4 = value; }
		}

		private const int squareSize = GameField.SquareSize;
		private static Random random = new Random();

		public int Top() {
			return Math.Min(square1.Location.Y, Math.Min(square2.Location.Y, 
				Math.Min(square3.Location.Y, square4.Location.Y)));
		}

		public Block(Point location,  BlockTypes newBlockType) {
			if (newBlockType==BlockTypes.Undefined) {
				BlockType = (BlockTypes)(random.Next(7)) + 1;
			}
			else {
				BlockType = newBlockType;
			}
			square1 = new Square(new Size(squareSize, squareSize), backColors[(int)BlockType], foreColors[(int)BlockType]);
			square2 = new Square(new Size(squareSize, squareSize), backColors[(int)BlockType], foreColors[(int)BlockType]);
			square3 = new Square(new Size(squareSize, squareSize), backColors[(int)BlockType], foreColors[(int)BlockType]);
			square4 = new Square(new Size(squareSize, squareSize), backColors[(int)BlockType], foreColors[(int)BlockType]);

			switch(BlockType) {
				case BlockTypes.Square:
					square1.Location = new Point(location.X, location.Y);
					square2.Location = new Point(location.X+squareSize, location.Y);
					square3.Location = new Point(location.X, location.Y+squareSize);
					square4.Location = new Point(location.X+squareSize, location.Y+squareSize);
					break;
				case BlockTypes.Line:
					square1.Location = new Point(location.X, location.Y);
					square2.Location = new Point(location.X, location.Y+squareSize);
					square3.Location = new Point(location.X, location.Y+2*squareSize);
					square4.Location = new Point(location.X, location.Y+3*squareSize);
					break;
				case BlockTypes.J:
					square1.Location = new Point(location.X+squareSize, location.Y);
					square2.Location = new Point(location.X+squareSize, location.Y+squareSize);
					square3.Location = new Point(location.X+squareSize, location.Y+2*squareSize);
					square4.Location = new Point(location.X, location.Y+2*squareSize);
					break;
				case BlockTypes.L:
					square1.Location = new Point(location.X, location.Y);
					square2.Location = new Point(location.X, location.Y+squareSize);
					square3.Location = new Point(location.X, location.Y+2*squareSize);
					square4.Location = new Point(location.X+squareSize, location.Y+2*squareSize);
					break;
				case BlockTypes.T:
					square1.Location = new Point(location.X, location.Y);
					square2.Location = new Point(location.X+squareSize, location.Y);
					square3.Location = new Point(location.X+2*squareSize, location.Y);
					square4.Location = new Point(location.X+squareSize, location.Y+squareSize);
					break;
				case BlockTypes.Z:
					square1.Location = new Point(location.X, location.Y);
					square2.Location = new Point(location.X+squareSize, location.Y);
					square3.Location = new Point(location.X+squareSize, location.Y+squareSize);
					square4.Location = new Point(location.X+2*squareSize, location.Y+squareSize);
					break;
				case BlockTypes.S:
					square1.Location = new Point(location.X, location.Y+squareSize);
					square2.Location = new Point(location.X+squareSize, location.Y+squareSize);
					square3.Location = new Point(location.X+squareSize, location.Y);
					square4.Location = new Point(location.X+2*squareSize, location.Y);
					break;
			}
		}

		public void Rotate() {
			Point OldPosition1 = square1.Location;
			Point OldPosition2 = square2.Location;
			Point OldPosition3 = square3.Location;
			Point OldPosition4 = square4.Location;
			RotationDirections OldStatusRotation = StatusRotation;
			Hide(GameField.WinHandle);

			// Rotate the blocks
			switch(BlockType) {
				case BlockTypes.Square:
					// Square Doesn't rotate
					break;
				case BlockTypes.Line:
					// Rotate all squares around square 2
				switch(StatusRotation) {
					case RotationDirections.North:
						StatusRotation = RotationDirections.East;
						square1.Location = new Point(square2.Location.X-squareSize, square2.Location.Y);
						square3.Location = new Point(square2.Location.X+squareSize, square2.Location.Y);
						square4.Location = new Point(square2.Location.X+2*squareSize, square2.Location.Y);
						break;
					case RotationDirections.East:
						StatusRotation = RotationDirections.North;
						square1.Location = new Point(square2.Location.X, square2.Location.Y-squareSize);
						square3.Location = new Point(square2.Location.X, square2.Location.Y+squareSize);
						square4.Location = new Point(square2.Location.X, square2.Location.Y+2*squareSize);
						break;
				}
					break;
				case BlockTypes.J:
					// Rotate all squares around square 3  
				switch(StatusRotation) {
					case RotationDirections.North:
						StatusRotation = RotationDirections.East;
						square1.Location = new Point(square3.Location.X, square3.Location.Y-squareSize);
						square2.Location = new Point(square3.Location.X+squareSize, square3.Location.Y);
						square4.Location = new Point(square3.Location.X+2*squareSize, square3.Location.Y);
						break;
					case RotationDirections.East:
						StatusRotation = RotationDirections.South;
						square1.Location = new Point(square3.Location.X+squareSize, square3.Location.Y);
						square2.Location = new Point(square3.Location.X, square3.Location.Y+squareSize);
						square4.Location = new Point(square3.Location.X, square3.Location.Y+2*squareSize);
						break;
					case RotationDirections.South:
						StatusRotation = RotationDirections.West;
						square1.Location = new Point(square3.Location.X, square3.Location.Y+squareSize);
						square2.Location = new Point(square3.Location.X-squareSize, square3.Location.Y);
						square4.Location = new Point(square3.Location.X-2*squareSize, square3.Location.Y);
						break;
					case RotationDirections.West:
						StatusRotation = RotationDirections.North;
						square1.Location = new Point(square3.Location.X-squareSize, square3.Location.Y);
						square2.Location = new Point(square3.Location.X, square3.Location.Y-squareSize);
						square4.Location = new Point(square3.Location.X, square3.Location.Y-2*squareSize);
						break;
				}
					break;
				case BlockTypes.L:
					// Rotate all squares around square 3                
				switch(StatusRotation) {
					case RotationDirections.North:
						StatusRotation = RotationDirections.East;
						square1.Location = new Point(square3.Location.X+squareSize, square3.Location.Y);
						square2.Location = new Point(square3.Location.X+2*squareSize, square3.Location.Y);
						square4.Location = new Point(square3.Location.X, square3.Location.Y+squareSize);
						break;
					case RotationDirections.East:
						StatusRotation = RotationDirections.South;
						square1.Location = new Point(square3.Location.X-squareSize, square3.Location.Y);
						square2.Location = new Point(square3.Location.X, square3.Location.Y+squareSize);
						square4.Location = new Point(square3.Location.X, square3.Location.Y+2*squareSize);
						break;
					case RotationDirections.South:
						StatusRotation = RotationDirections.West;
						square1.Location = new Point(square3.Location.X-2*squareSize, square3.Location.Y);
						square2.Location = new Point(square3.Location.X-squareSize, square3.Location.Y);
						square4.Location = new Point(square3.Location.X, square3.Location.Y-squareSize);
						break;
					case RotationDirections.West:
						StatusRotation = RotationDirections.North;
						square1.Location = new Point(square3.Location.X, square3.Location.Y-2*squareSize);
						square2.Location = new Point(square3.Location.X, square3.Location.Y-squareSize);
						square4.Location = new Point(square3.Location.X+squareSize, square3.Location.Y);
						break;
				}
					break;
				case BlockTypes.T:
				switch(StatusRotation) {
					case RotationDirections.North:
						StatusRotation = RotationDirections.East;
						square1.Location = new Point(square2.Location.X, square2.Location.Y-squareSize);
						square3.Location = new Point(square2.Location.X, square2.Location.Y+squareSize);
						square4.Location = new Point(square2.Location.X-squareSize, square2.Location.Y);
						break;
					case RotationDirections.East:
						StatusRotation = RotationDirections.South;
						square1.Location = new Point(square2.Location.X+squareSize, square2.Location.Y);
						square3.Location = new Point(square2.Location.X-squareSize, square2.Location.Y);
						square4.Location = new Point(square2.Location.X, square2.Location.Y-squareSize);
						break;
					case RotationDirections.South:
						StatusRotation = RotationDirections.West;
						square1.Location = new Point(square2.Location.X, square2.Location.Y+squareSize);
						square3.Location = new Point(square2.Location.X, square2.Location.Y-squareSize);
						square4.Location = new Point(square2.Location.X+squareSize, square2.Location.Y);
						break;
					case RotationDirections.West:
						StatusRotation = RotationDirections.North;
						square1.Location = new Point(square2.Location.X-squareSize, square2.Location.Y);
						square3.Location = new Point(square2.Location.X+squareSize, square2.Location.Y);
						square4.Location = new Point(square2.Location.X, square2.Location.Y+squareSize);
						break;
				}
					break;
				case BlockTypes.Z:
					// Rotate all squares around square 2                
				switch(StatusRotation) {
					case RotationDirections.North:
						StatusRotation = RotationDirections.East;
						square1.Location = new Point(square2.Location.X, square2.Location.Y-squareSize);
						square3.Location = new Point(square2.Location.X-squareSize, square2.Location.Y);
						square4.Location = new Point(square2.Location.X-squareSize, square2.Location.Y+squareSize);
						break;
					case RotationDirections.East:
						StatusRotation = RotationDirections.North;
						square1.Location = new Point(square2.Location.X-squareSize, square2.Location.Y);
						square3.Location = new Point(square2.Location.X, square2.Location.Y+squareSize);
						square4.Location = new Point(square2.Location.X+squareSize, square2.Location.Y+squareSize);
						break;
				}
					break;
				case BlockTypes.S:
					// Rotate all squares around square 2                
				switch(StatusRotation) {
					case RotationDirections.North:
						StatusRotation = RotationDirections.East;
						square1.Location = new Point(square2.Location.X, square2.Location.Y-squareSize);
						square3.Location = new Point(square2.Location.X+squareSize, square2.Location.Y);
						square4.Location = new Point(square2.Location.X+squareSize, square2.Location.Y+squareSize);
						break;
					case RotationDirections.East:
						StatusRotation = RotationDirections.North;
						square1.Location = new Point(square2.Location.X-squareSize, square2.Location.Y);
						square3.Location = new Point(square2.Location.X, square2.Location.Y-squareSize);
						square4.Location = new Point(square2.Location.X+squareSize, square2.Location.Y-squareSize);
						break;
				}
					break;
			}
			if (!(GameField.IsEmpty(square1.Location.X/squareSize, square1.Location.Y/squareSize)&&GameField.IsEmpty(square2.Location.X/squareSize, square2.Location.Y/squareSize)&&GameField.IsEmpty(square3.Location.X/squareSize, square3.Location.Y/squareSize)&&GameField.IsEmpty(square4.Location.X/squareSize, square4.Location.Y/squareSize))) {
				StatusRotation = OldStatusRotation;
				square1.Location = OldPosition1;
				square2.Location = OldPosition2;
				square3.Location = OldPosition3;
				square4.Location = OldPosition4;
			}
			Show(GameField.WinHandle);
		}

		public bool Down() {   
			if (GameField.IsEmpty(square1.Location.X/squareSize, square1.Location.Y/squareSize+1)&&GameField.IsEmpty(square2.Location.X/squareSize, square2.Location.Y/squareSize+1)&&GameField.IsEmpty(square3.Location.X/squareSize, square3.Location.Y/squareSize+1)&&GameField.IsEmpty(square4.Location.X/squareSize, square4.Location.Y/squareSize+1)) {
				Hide(GameField.WinHandle);
				square1.Location = new Point(square1.Location.X, square1.Location.Y+squareSize);
				square2.Location = new Point(square2.Location.X, square2.Location.Y+squareSize);
				square3.Location = new Point(square3.Location.X, square3.Location.Y+squareSize);
				square4.Location = new Point(square4.Location.X, square4.Location.Y+squareSize);
				Show(GameField.WinHandle);
				return true;
			}
			else {
				GameField.StopSquare(square1, square1.Location.X/squareSize, square1.Location.Y/squareSize);
				GameField.StopSquare(square2, square2.Location.X/squareSize, square2.Location.Y/squareSize);
				GameField.StopSquare(square3, square3.Location.X/squareSize, square3.Location.Y/squareSize);
				GameField.StopSquare(square4, square4.Location.X/squareSize, square4.Location.Y/squareSize);
				return false;
			}
		}

		public bool Right() {    
			if (GameField.IsEmpty(square1.Location.X/squareSize+1, square1.Location.Y/squareSize)&&GameField.IsEmpty(square2.Location.X/squareSize+1, square2.Location.Y/squareSize)&&GameField.IsEmpty(square3.Location.X/squareSize+1, square3.Location.Y/squareSize)&&GameField.IsEmpty(square4.Location.X/squareSize+1, square4.Location.Y/squareSize)) {
				Hide(GameField.WinHandle);
				square1.Location = new Point(square1.Location.X+squareSize, square1.Location.Y);
				square2.Location = new Point(square2.Location.X+squareSize, square2.Location.Y);
				square3.Location = new Point(square3.Location.X+squareSize, square3.Location.Y);
				square4.Location = new Point(square4.Location.X+squareSize, square4.Location.Y);
				Show(GameField.WinHandle);
				return true;
			}
			else {
				return false;
			}
		}

		public bool Left() {
			if (GameField.IsEmpty(square1.Location.X/squareSize-1, square1.Location.Y/squareSize)&&GameField.IsEmpty(square2.Location.X/squareSize-1, square2.Location.Y/squareSize)&&GameField.IsEmpty(square3.Location.X/squareSize-1, square3.Location.Y/squareSize)&&GameField.IsEmpty(square4.Location.X/squareSize-1, square4.Location.Y/squareSize)) {
				Hide(GameField.WinHandle);
				square1.Location = new Point(square1.Location.X-squareSize, square1.Location.Y);
				square2.Location = new Point(square2.Location.X-squareSize, square2.Location.Y);
				square3.Location = new Point(square3.Location.X-squareSize, square3.Location.Y);
				square4.Location = new Point(square4.Location.X-squareSize, square4.Location.Y);
				Show(GameField.WinHandle);
				return true;
			}
			else {
				return false;
			}
		}

		public void Show(System.IntPtr winHandle) {
			square1.Show(winHandle);
			square2.Show(winHandle);
			square3.Show(winHandle);
			square4.Show(winHandle);
		}
		public void Hide(System.IntPtr winHandle) {
			square1.Hide(winHandle);
			square2.Hide(winHandle);
			square3.Hide(winHandle);
			square4.Hide(winHandle);
		}
	}
}
