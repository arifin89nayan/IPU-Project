using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Texttospeech_App.Service
{
    public class ImageSlicerService
    {
        public void SliceImage(string imagePath, string outputDirectory)
        {
            // Load the image
            using (Image image = Image.FromFile(imagePath))
            {
                // Calculate the width of each chunk
               
                int chunkHeight = image.Height/2;

                // Loop through and slice the image
                for (int i = 0; i < 2; i++)
                {
                    // Create a new bitmap for the chunk
                    using (Bitmap chunk = new Bitmap(chunkHeight, image.Width))
                    {
                        // Create a graphics object from the chunk
                        using (Graphics graphics = Graphics.FromImage(chunk))
                        {
                            // Set the quality settings
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                            // Draw the chunk from the original image
                            //graphics.DrawImage(image, new Rectangle(0, 0, chunk.Width, chunk.Height), new Rectangle(i * chunkHeight, 0, chunkHeight, image.Width), GraphicsUnit.Pixel);
                            graphics.DrawImage(image, new Rectangle(0, 0, chunk.Width, chunk.Height), new Rectangle(0,i * chunkHeight, image.Width, chunkHeight), GraphicsUnit.Pixel);
                        }

                        // Save the chunk to file
                        string chunkPath = Path.Combine(outputDirectory, $"chunk_{i}.png");
                        chunk.Save(chunkPath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
        }
    }
}
