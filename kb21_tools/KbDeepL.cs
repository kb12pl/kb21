using DeepL;
using static kb21_tools.KbLog;
using static kb21_tools.KbConf;


namespace kb21_tools
{
    public class KbDeepL
    {
        private static string url = "https://api-free.deepl.com/v2/translate";
        private static string apiKey = Secret("deepl.key");        
        private static HttpClient client;

        public KbDeepL()
        {


        }
        
        public async Task<int> Test()
        {
            ok(123);
            return 2;
        }
        
        public async Task<int> Trans()
        {
            //Ret ret = new();            

            var translator = new Translator(apiKey);

            var translatedText = await translator.TranslateTextAsync(     "Hello, world!",     LanguageCode.English,     LanguageCode.French);

            ok(translatedText.ToString());
            return 0;
        }
 
    }
}