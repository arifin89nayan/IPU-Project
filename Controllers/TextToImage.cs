using Microsoft.AspNetCore.Mvc;
using Texttospeech_App.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices.JavaScript;

using System.Windows.Forms;
using System;
using System.Reflection;
using Texttospeech_App.Service;



namespace Texttospeech_App.Controllers
{
    public class TextToImage : Controller
    {
        private readonly ImageSlicerService _imageSlicerService;

        public TextToImage(ImageSlicerService imageSlicerService)
        {
            _imageSlicerService = imageSlicerService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()

        { 
            return View();
        }
        [HttpPost]
       public IActionResult Create(TextToImg model)
        {
           

            string text = model.InputText;
            int textSize1 = model.TextSize;
            string tfont = model.TextFont switch
            {
                "1" => "Arial",
                "2" => "Times New Roman",
                "3" => "Segoe UI",
                _ => throw new ArgumentOutOfRangeException(nameof(model.TextFont)) // Handle unexpected values
            };
            string tColor = model.TextColor switch
            {
                "1" => "Black",
                "2" => "Blue",
                "3" => "Red",
                _ => throw new ArgumentOutOfRangeException(nameof(model.TextColor))
            };
            string tStyle = model.TextStyle switch
            {
                "1" => "Regular",
                "2" => "Bold",
                "3" => "Italic",
                _ => throw new ArgumentOutOfRangeException(nameof(model.TextStyle)) 
            };

            // Font font = new Font("Arial", textSize);
            //FontStyle style=new FontStyle(stylea);
            //Font font = new Font(textFont, textSize1);
            FontStyle style = FontStyle.Regular;
            if (tStyle== "Bold")
            {
                style = FontStyle.Bold;
            }
           else if (tStyle == "Italic")
            {
                style = FontStyle.Italic;
            }

            Font font = new Font(tfont, textSize1, style);
            int maxWidth = 340;

            Brush textColor = Brushes.Black;
            if (tColor == "Blue")
            {
                textColor = Brushes.Blue;
            }
            if (tColor == "Red")
            {
                textColor = Brushes.Red;
            }
            //Brush textColor = new SolidBrush(Color.FromName(textColor1));
            //string textColor = "Black";

            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
         
            SizeF textSize = drawing.MeasureString(text, font , maxWidth);
            StringFormat sf = new StringFormat();
            sf.Trimming = StringTrimming.Word;
            sf.Alignment= StringAlignment.Center;
            sf.LineAlignment= StringAlignment.Center;
            img.Dispose();
            drawing.Dispose();
            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);
            

            drawing = Graphics.FromImage(img);
            //Adjust for high quality
            drawing.CompositingQuality = CompositingQuality.HighQuality;
            drawing.InterpolationMode = InterpolationMode.HighQualityBilinear;
            drawing.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawing.SmoothingMode = SmoothingMode.HighQuality;
            drawing.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            //paint the background
            drawing.Clear(Color.White);
            //create a brush for the text
           // Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textColor, new RectangleF(0, 0, textSize.Width, textSize.Height), sf);

            drawing.Save();

            textColor.Dispose();
            drawing.Dispose();
            
            img.Save(@"D:\\Iwate University japan\\test\\Texttospeech App\\wwwroot\\image\\output1.jpg", ImageFormat.Jpeg);
            img.Dispose();
            // Console.WriteLine("Image created successfully.");

            //return View();
            ViewBag.imageSrc = @"/image/output1.jpg";
            
            string imagePath = @"D:\\Iwate University japan\\test\\Texttospeech App\\wwwroot\\image\\output1.jpg";
            string outputDirectory = @"D:\\Iwate University japan\\test\\Texttospeech App\\wwwroot\\image";
            
            _imageSlicerService.SliceImage(imagePath, outputDirectory);


            //return RedirectToAction("Create", viewModel);
            return View();
        }
       
    }
}
