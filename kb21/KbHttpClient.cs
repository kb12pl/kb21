using System.Net.Http;
using System.Threading.Tasks;


namespace kb21
{
    internal class KbHttpClient
    {
        private static HttpClient client;
        
        public KbHttpClient()
        {        
            client ??= new HttpClient();
        }

        public async Task<int>Send(MyArg arg, KbWindow win)
        {
            Ret ret = new();
            var response = await client.GetAsync("https://tagmet.com.pl");            
            ret.Set("id",arg.Get("id"));
            ret.Set("a",await response.Content.ReadAsStringAsync());
            win.Integration("on_http","",ret);
            return 0;
        }
    }
}
