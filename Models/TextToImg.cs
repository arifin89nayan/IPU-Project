using Microsoft.AspNetCore.Mvc.Rendering;
namespace Texttospeech_App.Models
{
    public class TextToImg
    {
       /* public TextToImg()
        {
            
        }*/
        public string InputText { get; set; }
        public string TextFont { get; set; }
        public string TextColor { get; set; }
        public string TextStyle { get; set; }
        public int TextSize { get; set; }
        

    }
}
