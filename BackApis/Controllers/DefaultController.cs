using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading;

namespace BackApis.Controllers
{
    [RoutePrefix("api")]
    public class DefaultController : ApiController
    {
        char[] padding = { '=' };

        [Route("Test")]
        [HttpGet]
        public Task<HttpResponseMessage> Test()
        {
            return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.OK, "Test OK"));
        }

        [Route("GetMeeting")]
        [HttpGet]
        public Task<HttpResponseMessage> GetMeeting()
        {
            int result;
            if (MeetingsControl.Meetings.TryDequeue(out result))
            {
                return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.OK, $"result:{result},Count:{MeetingsControl.Meetings.Count}"));
            }
            else
            {
                return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.BadRequest,"error"));
            }
        }

        [Route("GetSignature")]
        [HttpPost]
        public Task<HttpResponseMessage> GetSignature(JObject _lLicencesDataInfoObj)
        {
            Thread.Sleep(2000);
            string apiKey = "MbhIt-Z8T5iospJ_FaMNRw";
            string apiSecret = "ii9VKoAX280w7ZwZrX0TKT1dkqNU4NMZBMiY";
            string meetingNumber = _lLicencesDataInfoObj.GetValue("meetingNumber").ToString();
            String ts = _lLicencesDataInfoObj.GetValue("ts").ToString();
            string role = _lLicencesDataInfoObj.GetValue("role").ToString(); ;
            string token = GenerateToken(apiKey, apiSecret, meetingNumber, ts, role);

            return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.OK, token));
        }

        public string GenerateToken(string apiKey, string apiSecret, string meetingNumber, string ts, string role)
        {
            string message = String.Format("{0}{1}{2}{3}", apiKey, meetingNumber, ts, role);
            apiSecret = apiSecret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(apiSecret);
            byte[] messageBytesTest = encoding.GetBytes(message);
            string msgHashPreHmac = System.Convert.ToBase64String(messageBytesTest);
            byte[] messageBytes = encoding.GetBytes(msgHashPreHmac);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                string msgHash = System.Convert.ToBase64String(hashmessage);
                string token = String.Format("{0}.{1}.{2}.{3}.{4}", apiKey, meetingNumber, ts, role, msgHash);
                var tokenBytes = System.Text.Encoding.UTF8.GetBytes(token);
                return System.Convert.ToBase64String(tokenBytes).TrimEnd(padding);
            }
        }
    }
}
