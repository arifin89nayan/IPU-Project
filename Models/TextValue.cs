using Microsoft.AspNetCore.Mvc.Rendering;

namespace Texttospeech_App.Models
{
    public class TextValue
    {
        //public string Name { get; set; }
        public string GuidText { get; set; }
        //public List<SelectListItem> languageItems { get; set; }
        public string language { get; set; }
        public string voice { get; set; }
    }
}
