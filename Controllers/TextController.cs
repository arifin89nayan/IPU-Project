using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using Texttospeech_App.Models;
using  Azure;
using Microsoft.CognitiveServices.Speech;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;


namespace Texttospeech_App.Controllers
{
    public class TextController : Controller
    {
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
        public  async Task <IActionResult> Create(TextValue Value)
        {
             string lang = "";
             string voice = "";
            if (Value.voice == "1" && Value.language == "2")
            {
                lang = "en-US";
                voice = "en-US-DavisNeural";
            }
            
            else if (Value.voice == "2" && Value.language == "2")
            {
                lang = "en-US";
                voice = "en-US-JennyMultilingualV2Neural";
               
            }
            else if (Value.voice == "1" && Value.language == "1")
            {
                lang = "ja-JP";
                voice = "ja-JP-DaichiNeural";

            }
            else if (Value.voice == "2" && Value.language == "1")
            {
                lang = "ja-JP";
                voice = "ja-JP-NanamiNeural";

            }

            // Set your Azure Text-to-Speech credentials
            string key = "8fa775b3a24a4c7e9366d351928848e2";
            //string key2 = "e29bc3d756594f4189d74c54ceece966";
            // string endpoint = "https://japaneast.api.cognitive.microsoft.com/";
            string region = "japaneast"; 
            string inputText = Value.GuidText;
            //ViewBag.TextData = inputText;
            //Session["TextData"] = inputText;
            TempData["TextData"] = inputText;

            string language = lang;
            string voice1 = voice;
            //string outputFilePath = @"D:\Iwate University japan\Texttospeech App\wwwroot\audio\output.mp3";
            string outputFilePath = @"C:\Users\WALTON\Desktop\Entrance exam\Texttospeech App\wwwroot\audio\output.mp3";
        
            try
            {
                
                await SynthesizeAudioAsync(key, region, inputText, language, voice1,outputFilePath);
                //ViewBag.AudioFilePath = outputFilePath;
                
                return RedirectToAction("Index", TempData["TextData"]);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }

        }
        //Craete Audio File using Azure AI
        static async Task SynthesizeAudioAsync(string key, string region, string text, string language,string voice1, string outputFilePath)
        {
            var speechConfig = SpeechConfig.FromSubscription(key, region);
            speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio48Khz96KBitRateMonoMp3);

            speechConfig.SpeechSynthesisLanguage = language;
            speechConfig.SpeechSynthesisVoiceName = voice1;

            using var speechSynthesizer = new SpeechSynthesizer(speechConfig, null);
            var result = await speechSynthesizer.SpeakTextAsync(text);

            using var stream = AudioDataStream.FromResult(result);
            await stream.SaveToWaveFileAsync(outputFilePath);
        }
        
    }
}
