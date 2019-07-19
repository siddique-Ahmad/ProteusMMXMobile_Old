using ProteusMMX.DependencyInterface;
using ProteusMMX.UWP.DependencyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Xamarin.Forms;

[assembly: Dependency(typeof(SpeechtoText))]
namespace ProteusMMX.UWP.DependencyService
{
    public class SpeechtoText : ISpeechToText
    {
        StringBuilder builder = new StringBuilder();
        public async Task<string> SpeechToText()
        {
            string recognizedText = string.Empty;
            //try
            //{


            //    SpeechRecognizer recognizer;

            //    recognizer = new SpeechRecognizer();
            //    await recognizer.CompileConstraintsAsync();

            //    recognizer.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(10.0);
            //    recognizer.Timeouts.BabbleTimeout = TimeSpan.FromSeconds(10.0);
            //    recognizer.Timeouts.EndSilenceTimeout = TimeSpan.FromSeconds(5.0);

            //    recognizer.UIOptions.AudiblePrompt = "";
            //    recognizer.UIOptions.ExampleText = "";
            //    recognizer.UIOptions.ShowConfirmation = true;
            //    recognizer.UIOptions.IsReadBackEnabled = true;



            //    var result = await recognizer.RecognizeWithUIAsync();

            //    if (result != null)
            //    {


            //        builder.AppendLine(
            //          $"I have {result.Confidence} confidence that you said [{result.Text}] " +
            //          $"and it took {result.PhraseDuration.TotalSeconds} seconds to say it " +
            //          $"starting at {result.PhraseStartTime:g}");

            //        var alternates = result.GetAlternates(10);

            //        builder.AppendLine(
            //          $"There were {alternates?.Count} alternates - listed below (if any)");

            //        if (alternates != null)
            //        {
            //            foreach (var alternate in alternates)
            //            {
            //                builder.AppendLine(
            //                  $"{alternate.Text}");
            //            }
            //        }


            //    }

            //}
            //catch (Exception ex)
            //{

            //}
            //return builder.ToString();
            try
            {
               

                using (SpeechRecognizer recognizer =
                  new Windows.Media.SpeechRecognition.SpeechRecognizer())
                {
                    await recognizer.CompileConstraintsAsync();

                    SpeechRecognitionResult result = await recognizer.RecognizeWithUIAsync();

                    if (result.Status == SpeechRecognitionResultStatus.Success)
                    {
                        recognizedText = result.Text;
                    }
                }
               
            }
           catch(Exception ex)
            {

            }
            return (recognizedText);
        }
        public SpeechtoText()
        {

        }

    }
}
