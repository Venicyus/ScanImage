using System.Drawing;

namespace ScanImage.Models {
    class Pixel {

        public int X { get; set; }
        public int Y { get; set; }
        public Color C { get; set; }
        public Pixel OldPixel { get; set; }

        public Pixel(int x, int y, Color c) {
            X = x;
            Y = y;
            C = c;
        }

        public Pixel(int x, int y, Color c, Pixel p) {
            X = x;
            Y = y;
            C = c;
            OldPixel = p;
        }
    }
}
