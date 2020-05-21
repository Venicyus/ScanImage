using ScanImage.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace ScanImage.Services {
    public class ImageV {

        private static int GetLevel(int level) => 255 * (level > 0 && level <= 100 ? level : 100) / 100;

        public static void Processing(string path, int level) {
            //-> Buscando a imagem original
            Bitmap img = new Bitmap(path, true);

            //-> Trantando a imagem para encontrar o documento
            var pixels = new List<Pixel>();

            for (var x = 0; x < img.Width; x++) {
                var teste = -1;
                for (var y = 0; y < img.Height; y++) {
                    Color colorO = img.GetPixel(x, y);
                    Color colorN = Color.FromArgb(255 - colorO.R, 255 - colorO.G, 255 - colorO.B);

                    if (colorN.R <= GetLevel(level) && colorN.G <= GetLevel(level) && colorN.B <= GetLevel(level)) {
                        if (teste == -1) {
                            teste = y;
                        }

                        pixels.Add(new Pixel(x, y - teste, colorO));
                    }
                }
            }

            //-> Buscando o tamanho do documento
            int maxX = pixels.OrderBy(p => p.X).ToList()[pixels.Count - 1].X;
            int minX = pixels.OrderBy(p => p.X).ToList()[0].X;
            int maxY = pixels.OrderBy(p => p.Y).ToList()[pixels.Count - 1].Y;
            int minY = pixels.OrderBy(p => p.Y).ToList()[0].Y;

            //-> Trantando a imagem para encontrar o documento
            Bitmap imgNew = new Bitmap(maxX - minX + 1, maxY - minY + 1);

            foreach (var pixel in pixels) {
                imgNew.SetPixel(pixel.X - minX, pixel.Y - minY, pixel.C);
            }

            //-> Salvando a imagem modificada
            imgNew.Save(@"C:\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".jpeg", ImageFormat.Jpeg);

            //-> Liberando o uso da imagens
            imgNew.Dispose();
            img.Dispose();
        }
    }
}
