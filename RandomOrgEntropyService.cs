using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace StandaloneWalletGenerator
{
    public class RandomOrgEntropyService
    {
        private const string RemoteUrl = "https://www.random.org/integers/";

        public static byte[] GetRandomBytes(int numberOfBytes)
        {
            return new HttpClient()
                .GetStringAsync(
                    Uri(
                        RemoteUrl,
                        new Dictionary<string, string>
                        {
                            {"num", $"{numberOfBytes}"},
                            {"min", "0"},
                            {"max", "255"},
                            {"col", "1"},
                            {"base", "10"},
                            {"format", "plain"},
                            {"rnd", "new"},
                        }
                    )
                ).GetAwaiter().GetResult()
                .TrimEnd()
                .Split("\n")
                .Select(number =>
                {
                    byte.TryParse(number, out var b);
                    return b;
                })
                .ToArray();
        }

        private static string Uri(string remoteUrl, IDictionary<string, string> parameters)
        {
            var uri = parameters.Aggregate(
                $"{remoteUrl}?",
                (current, keyValuePair) => keyValuePair.Value != null && !keyValuePair.Value.Equals("")
                    ? current + $"{keyValuePair.Key}={keyValuePair.Value}&"
                    : current
            );
            return uri.Substring(0, uri.Length - 1);
        }
    }
}
