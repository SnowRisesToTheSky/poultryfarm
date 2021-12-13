using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace sidonia.models_and_secondary.Secondary.Auth
{
    public class AuthCookieExtractor
    {
        //КОНСТАНТЫ
        public static readonly string COOKIE_NAME = ".AspNetCore.Cookies";

        //МЕТОДЫ
        //1. Извлечь значение куки
        public static string ExtractCookieValue(HttpResponseMessage response)
        {
            //1. Получить содержимое куки
            var cookieToken = new List<string>(response.Headers.GetValues("Set-Cookie"))
                .Find(cookie => cookie.Contains(COOKIE_NAME));
            //2. индекс начала содержимого
            //3. индекс конца содержимого
            //4. достать само содержимоеS
            var assignIndex = cookieToken.IndexOf("=");
            var semicolonIndex = cookieToken.IndexOf(";");
            cookieToken = cookieToken.Substring(assignIndex + 1, semicolonIndex - assignIndex - 1);
            //5. Ответ
            return cookieToken;
        }
    }
}
