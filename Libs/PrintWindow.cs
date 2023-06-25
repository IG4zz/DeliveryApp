using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DeliveryApp.Libs
{
    /// <summary>
    /// Класс PrintWindow
    /// Отвечает за сохранение окна в PDF файл
    /// </summary>
    class PrintWindow
    {
        /// <summary>
        /// Метод GetImage
        /// Получает изображение окна приложения
        /// </summary>
        /// <param name="view"> Выбранное окно </param>
        /// <returns></returns>
        public static RenderTargetBitmap GetImage(UIElement view)
        {
            Size size = new Size(view.RenderSize.Width, view.RenderSize.Height);

            if (size.IsEmpty)
            {
                return null;
            }
            RenderTargetBitmap result = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext context = drawingVisual.RenderOpen())
            {

                context.DrawRectangle(new VisualBrush(view), null, new Rect(0, 0, (int)size.Width, (int)size.Height));

                context.Close();

            }
            result.Render(drawingVisual);
            return result;
        }

        /// <summary>
        /// Метод createPdfFromImage
        /// Создает файл PDF ил выбранного файла PNG
        /// </summary>
        /// <param name="imageFile"> Файл PNG </param>
        /// <param name="pdfFile"> Файл PDf </param>
        public static void createPdfFromImage(string imageFile, string pdfFile)
        {
            using (var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER.Rotate(), 0, 0, 0, 0))
            {
                using (var fileStream = new FileStream(pdfFile, FileMode.Create))
                {
                    using (var ms = new MemoryStream())
                    {
                        PdfWriter.GetInstance(document, fileStream).SetFullCompression();
                        document.Open();

                        using (var fs = new FileStream(imageFile, FileMode.Open))
                        {
                            var image = iTextSharp.text.Image.GetInstance(fs);
                            image.Alignment = iTextSharp.text.Image.ALIGN_TOP;

                            document.Add(image);
                        }

                        Process.Start("explorer.exe", pdfFile);
                        document.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Метод SaveAsPng
        /// Сохраняет изображение в файл PNG
        /// </summary>
        /// <param name="src"> Источник изображения </param>
        /// <param name="targetFile"> Итоговый файл </param>
        public static void SaveAsPng(RenderTargetBitmap src, string targetFile)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(src));
            using (var stream = System.IO.File.Create(targetFile))
            {
                encoder.Save(stream);
            }
        }
    }
}
