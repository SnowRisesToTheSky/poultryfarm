using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sidonia.models_and_secondary.Secondary.AntiForgery
{
    public static class AntiForgeryTokenExtractor
    {
        //ПОЛЯ
        //1. 
        //2.
        public static string FIELD_TOKEN_NAME { get; } = "RequestVerificationToken";
        public static string COOKIE_TOKEN_NAME { get; } = "AntiForgeryTokenCookie";


        //МЕТОДЫ
        //1. Извлечь значение куки
        private static string ExtractCookieValue(HttpResponseMessage response)
        {
            //1. Получить токен из куки
            var cookieToken = new List<string>(response.Headers.GetValues("Set-Cookie"))
                .Find(cookie => cookie.Contains(COOKIE_TOKEN_NAME));
            //2. индекс начала токена
            //3. индекс конца токена
            //4. достать сам токен
            var assignIndex = cookieToken.IndexOf("=");
            var semicolonIndex = cookieToken.IndexOf(";");
            cookieToken = cookieToken.Substring(assignIndex + 1, semicolonIndex - assignIndex - 1);
            //5. Ответ
            return cookieToken;
        }

        //2. Извлечь токен из поля
        private static string ExtractAntiForgeryToken(HttpResponseMessage response)
        {
            var fieldToken = new List<string>(response.Headers.GetValues(AntiForgeryTokenExtractor.FIELD_TOKEN_NAME))[0];
            return fieldToken;
        }


        //3. Извлечь куки и токен
        public static (string field, string cookie) ExtractAntiForgeryValues(HttpResponseMessage response)
        {
            var cookie = ExtractCookieValue(response);
            var token = ExtractAntiForgeryToken(response);

            return (token, cookie);
        }
    }
}
