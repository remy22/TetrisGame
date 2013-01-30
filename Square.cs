using System;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace Nettrix {
  public class Square {
		private Point squareLocation;

		public Point Location {
			get { return squareLocation; }
			set { squareLocation = value;	}
		}

		private Size squareSize;

		public Size Size {
			get { return squareSize; }
			set { squareSize = value;	}
		}

		private Color foregroundColor;

		public Color ForeColor {
			get { return foregroundColor;	}
			set { foregroundColor = value; }
		}

		private Color backgroundColor;

		public Color BackColor {
			get { return backgroundColor; }
			set { backgroundColor = value; }
		}

		public void Show(System.IntPtr winHandle) {
			Graphics GameGraphics;
			GraphicsPath graphPath;
			PathGradientBrush brushSquare;
			Color[] surroundColor;
			Rectangle rectSquare;

			GameGraphics = Graphics.FromHwnd(winHandle);

			graphPath = new GraphicsPath();
			rectSquare = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);
			graphPath.AddRectangle(rectSquare);

			brushSquare = new PathGradientBrush(graphPath);
			brushSquare.CenterColor = ForeColor;
			surroundColor = new Color[]{BackColor};
			brushSquare.SurroundColors = surroundColor;

			GameGraphics.FillPath(brushSquare, graphPath);
		}

		public void Hide(System.IntPtr winHandle) {
			Graphics GameGraphics;
			Rectangle rectSquare;
			GameGraphics = Graphics.FromHwnd(winHandle);

			rectSquare = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);
			GameGraphics.FillRectangle(new SolidBrush(GameField.BackColor), rectSquare);
		}

		public Square(Size initialSize, Color initialBackColor, Color initialForeColor) {
			Size = initialSize;
			BackColor = initialBackColor;
			ForeColor = initialForeColor;
		}
	}
}
