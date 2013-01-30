using System;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace Nettrix {
  public class GameField {
		public const int Width = 16;
		public const int Height = 30;
		public const int SquareSize = 10;

		private static Square[,] arrGameField = new Square[Width, Height];
		private static int[] arrBitGameField = new int[Height];
		public static System.IntPtr WinHandle;
		public static Color BackColor;

		private const int bitEmpty = 0x0;       //00000000 0000000
		private const int bitFull = 0xFFFF;     //11111111 1111111

		public static bool IsEmpty(int x, int y) {
			if ((y<0||y>=Height)||(x<0||x>=Width)) {
				return false;
			}
			else if((arrBitGameField[y] & (1<<x)) != 0) {
				return false;
			}
			return true;
		}

		public static int CheckLines() {
			int CheckLines_result = 0;
			int y = Height - 1;

			while ( y >= 0) {
				if (arrBitGameField[y]==bitEmpty) y = 0;

				if (arrBitGameField[y]==bitFull) {
					CheckLines_result++;

					for(int index = y; index >= 0; index--) {
						if (index>0) {
							arrBitGameField[index] = arrBitGameField[index-1];
							for(int x=0; x<Width; x++) {
								arrGameField[x, index] = arrGameField[x, index-1];
								if (arrGameField[x, index] != null)
									arrGameField[x, index].Location = 
										new Point(arrGameField[x, index].Location.X, arrGameField[x, index].Location.Y+SquareSize);
							}
						}
						else {
							arrBitGameField[index] = bitEmpty;
							for(int x=0; x<Width; x++) {
								arrGameField[x, index] = null;
							}
						}
					}
				}
				else {
					y--;
				}
			}
			return CheckLines_result;
		}

		public static void StopSquare(Square square, int x, int y) {
			arrBitGameField[y] = arrBitGameField[y] | (1<<x);
			arrGameField[x, y] = square;
		}

		public static void Redraw() {
			for(int y=Height-1; y>=0; y--) 
				if (arrBitGameField[y]!=bitEmpty) 
					for(int x=Width-1; x>=0; x--) 
						if (arrGameField[x, y] != null) arrGameField[x, y].Show(WinHandle);
		}

		public static void Reset() {
			for(int i=Height; i<=0; i--) {
				arrBitGameField[i] = bitEmpty;
				for(int x=0; x<Width; x++) {
					arrGameField[x, i] = null;
				}
			}
		}
	}
}
