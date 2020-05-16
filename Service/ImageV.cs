using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace ScanImage.Service {
    public class ImageV {

        private static int GetLevel(int level) => 255 * (level > 0 && level <= 100 ? level : 100) / 100;

        public static void Processing(string path, int level) {
            //-> Buscando a imagem original
            Bitmap img = new Bitmap(path, true);

            //-> Trantando a imagem para encontrar o documento
            Bitmap imgNew = new Bitmap(img.Width, img.Height);

            var pixelsR = new List<int[]>();

            for (var x = 0; x < img.Width; x++) {
                for (var y = 0; y < img.Height; y++) {
                    Color colorO = img.GetPixel(x, y);
                    Color colorN = Color.FromArgb(255 - colorO.R, 255 - colorO.G, 255 - colorO.B);

                    if (colorN.R <= GetLevel(level) && colorN.G <= GetLevel(level) && colorN.B <= GetLevel(level)) {
                        pixelsR.Add(new int[2] { x, y });
                        imgNew.SetPixel(x, y, colorO);
                    }
                }
            }

            //-> Salvando a imagem modificada
            imgNew.Save(@"C:\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png", ImageFormat.Png);

            //-> Liberando o uso da imagens
            img.Dispose();
            imgNew.Dispose();
        }
    }
}
