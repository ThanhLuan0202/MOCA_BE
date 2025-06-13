using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.Pkcs;

namespace MOCA_Repositories.Helper
{
    public static class VnPayHelper
    {
        public static string CreateRequestUrl(Dictionary<string, string> data, string vnp_HashSecret)
        {
            var sorted = new SortedDictionary<string, string>(data);
            var query = new StringBuilder();
            var hashData = new StringBuilder();

            foreach (var kv in sorted)
            {
                if (!string.IsNullOrEmpty(kv.Value))
                {
                    var encodedKey = WebUtility.UrlEncode(kv.Key);
                    var encodedValue = WebUtility.UrlEncode(kv.Value);

                    query.Append($"{encodedKey}={encodedValue}&");
                    hashData.Append($"{encodedKey}={encodedValue}&");
                }
            }

            string rawHash = hashData.ToString().TrimEnd('&');

            string secureHash = GenerateHmacSHA512(vnp_HashSecret, rawHash);

            query.Append("vnp_SecureHash=" + secureHash);

            return query.ToString();
        }

        public static string GenerateHmacSHA512(string key, string inputData)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                var hash = hmac.ComputeHash(inputBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }


}
