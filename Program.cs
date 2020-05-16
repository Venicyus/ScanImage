using ScanImage.Service;
using System;

namespace ScanImage {
    class Program {
        static void Main(string[] args) {
            try {
                ImageV.Processing(@"C:\img1.jpeg", 48);

            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
