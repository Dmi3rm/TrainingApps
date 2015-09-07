using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FS.Web.HelpClasses
{
    public class CaptchaImage
    {
        private string text; // текст капчи
        private int width; // ширина картинки
        private int height; // высота картинки
        public Bitmap Image { get; set; } // само изображение капчи

        public CaptchaImage(string s, int width, int height)
        {
            text = s;
            this.width = width;
            this.height = height;
            GenerateImage();
        }
        // создаем изображение
        private void GenerateImage()
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            // отрисовка строки
            g.DrawString(text, new Font("Arial", (float)(height / 1.8), FontStyle.Bold),
                                Brushes.Black, new RectangleF(0, 0, width , height));

            g.Dispose();

            Image = bitmap;
        }

        ~CaptchaImage()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Image.Dispose();
        }
    }
}