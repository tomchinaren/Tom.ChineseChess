using Tom.Api.Request;

namespace Tom.ChineseChess.Sdk
{
    class Test
    {
        public static void Main()
        {
            var serverUrl = "http://localhost:28310/";
            IClient client = new DefaultClient(serverUrl, null, null);
        }
    }
}
