using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Helpers;

namespace Server.Controllers
{
    public class OauthController : Controller
    {
        [HttpGet]
        public IActionResult Authorize(string response_type, // authorization flow type 
            string client_id, // client id
            string redirect_uri,
            string scope, // what info I want = email,grandma,tel
            string state)
        {
            var query = new QueryBuilder();
            query.Add("redirectUri", redirect_uri);
            query.Add("state", state);

            return View(model: query.ToString());
        }
        [HttpPost]
        public IActionResult Authorize(
            string username,
            string redirectUri,
            string state)
        {
            const string code = "BABAABABABA";

            var query = new QueryBuilder();
            query.Add("code", code);
            query.Add("state", state);


            return Redirect($"{redirectUri}{query.ToString()}");
        }
        public async Task<IActionResult> Token(string grant_type, // flow of access_token request
            string code, // confirmation of the authentication process
            string redirect_uri,
            string client_id,
            string refresh_token)
        {
            var token = JwtHelpers.GetToken();
            var responseObject = new
            {
                access_token = token,
                token_type = "Bearer",
                raw_claim = "test",
                refresh_token = "RefreshTokenSecretKey"

            };
            var responseJson = JsonConvert.SerializeObject(responseObject);
            var responseByte = Encoding.UTF8.GetBytes(responseJson);
            await Response.Body.WriteAsync(responseByte, 0, responseByte.Length);
            
            return Redirect(redirect_uri);
        }

    }
}
