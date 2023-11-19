using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using GrapeCity.Documents.Pdf;
using GrapeCity.Documents.Text;
using GrapeCity.Documents.Drawing;
using GrapeCity.Documents.Pdf.Spec;

namespace BaseProject.Controllers;

[ApiController]
[Route("[controller]")]
public class GrapeCityPdfController : ControllerBase
{
    [HttpGet(Name = nameof(GrapeCityPdfTest))]
    public string GrapeCityPdfTest()
    {
        string pdfName = "Example.pdf";
        try
        {
            GcPdfDocument doc = new GcPdfDocument();
            GcPdfGraphics g = doc.NewPage().Graphics;

            TextLayout tl = new TextLayout(72)
            {
                MaxWidth = doc.PageSize.Width,
                MaxHeight = doc.PageSize.Height,
                MarginLeft = 72,
                MarginRight = 72,
                MarginTop = 72,
                MarginBottom = 72,
            };
            //tl.DefaultFormat.Font = Font.FromFile(Path.Combine("Resources", "Fonts", "segoeui.ttf"));
            tl.DefaultFormat.FontSize = 11;

            // Color for the title:
            var colorBlue = Color.FromArgb(0x3B, 0x5C, 0xAA);
            // Color for the highlights:
            var colorRed = Color.Red;
            // The text layout used to render text:
            tl.TextAlignment = TextAlignment.Center;
            tl.Append("Introduction\n", new TextFormat() { FontSize = 16, ForeColor = colorBlue });
            tl.Append("The Importance of Wetlands", new TextFormat() { FontSize = 13, ForeColor = colorBlue });
            tl.PerformLayout(true);
            g.DrawTextLayout(tl, PointF.Empty);

            // Move below the caption for the first para:
            tl.MarginTop = tl.ContentHeight + 72 * 2;
            tl.Clear();
            tl.TextAlignment = TextAlignment.Leading;
            tl.ParagraphSpacing = 12;

            string[] _paras = new string[]
            {"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent eget elit risus. Pellentesque in purus dapibus, ullamcorper eros in, condimentum felis. In pharetra et dui a euismod. Donec massa sem, sollicitudin tempor ligula at, pellentesque molestie mauris. Suspendisse et massa a nunc porttitor placerat. Fusce ultrices augue nec tellus maximus, vitae consequat ligula rhoncus. Nunc id tortor ac erat luctus finibus. Aliquam ac interdum nulla. Etiam a mauris non diam porttitor vulputate. Pellentesque efficitur volutpat augue, eu semper augue luctus vitae. Sed ac tristique tellus. Sed vulputate scelerisque eros, quis maximus sapien cursus sit amet. Maecenas vestibulum ac magna nec vulputate.\r\n\r\nNam vestibulum, nisi in aliquam porta, orci leo pretium urna, et ullamcorper sem felis vitae ipsum. Suspendisse potenti. Mauris sapien dolor, porta consectetur dignissim in, rutrum sed dui. Praesent quis lorem eget enim dictum bibendum. Quisque dignissim risus quis neque rutrum ullamcorper. Ut purus purus, scelerisque et dolor vel, consequat commodo tortor. Aliquam in scelerisque nulla, id iaculis augue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Duis euismod quis ligula ac elementum. Aliquam quis lorem tellus. Proin id luctus ante, in tempor erat. Morbi nunc libero, cursus tempor dapibus sit amet, efficitur quis."};

            tl.Append(_paras[0].Substring(0, 1), new TextFormat(tl.DefaultFormat) { FontSize = 22 });
            addPara(_paras[0].Substring(1));
            tl.PerformLayout(true);
            g.DrawTextLayout(tl, PointF.Empty);

            tl.MarginTop = tl.ContentRectangle.Bottom;
            tl.Clear();
            tl.FirstLineIndent = 36;

            List<InlineObject> images = new List<InlineObject>();

            foreach (var para in _paras.Skip(1))
            {
                // Paragraphs starting with '::' indicate images to be rendered across the page width:
                if (para.StartsWith("::"))
                {
                    var img = Image.FromFile(Path.Combine("Resources", "ImagesBis", para.Substring(2)));
                    var w = tl.MaxWidth.Value - tl.MarginLeft - tl.MarginRight;
                    var h = (float)img.Height / (float)img.Width * w;
                    tl.AppendInlineObject(img, w, h);
                    tl.AppendLine();
                }
                else
                {
                    addPara(para);
                }
            }
            // Layout the paragraphs:
            tl.PerformLayout(true);

            var tso = new TextSplitOptions(tl)
            {
                RestMarginTop = 72,
                MinLinesInFirstParagraph = 2,
                MinLinesInLastParagraph = 2,
            };

            // Image alignment used to render the pictures:
            var ia = new ImageAlign(ImageAlignHorz.Left, ImageAlignVert.Top, true, true, true, false, false) { BestFit = true };
            // In a loop, split and render the text:
            while (true)
            {
                var splitResult = tl.Split(tso, out TextLayout rest);
                g = doc.Pages.Last.Graphics;
                doc.Pages.Last.Graphics.DrawTextLayout(tl, PointF.Empty);
                // Render all images that occurred on this page:
                foreach (var io in tl.InlineObjects)
                    doc.Pages.Last.Graphics.DrawImage((Image)io.Object, io.ObjectRect.ToRectangleF(), null, ia);
                // Break unless there is more to render:
                if (splitResult != SplitResult.Split)
                    break;
                // Assign the remaining text to the 'main' TextLayout, add a new page and continue:
                tl = rest;
                doc.Pages.Add();
            }
            doc.Save(pdfName);

            void addPara(string para)
            {
                // We implement a primitive markup to highlight some fragments in red:
                var txt = para.Split(new string[] { "<red>", "</red>" }, StringSplitOptions.None);
                for (int i = 0; i < txt.Length; ++i)
                {
                    if (i % 2 == 0)
                        tl.Append(txt[i]);
                    else
                        tl.Append(txt[i], new TextFormat(tl.DefaultFormat) { ForeColor = colorRed });
                }
                tl.AppendLine();
            }
            return $"{pdfName} başarıyla oluşturulmuştur.";
        }
        catch (Exception ex)
        {
            return $"{pdfName} oluşturulurken {ex.Message} hatası ile karşılaşılmıştır.";
        }
    }


}
