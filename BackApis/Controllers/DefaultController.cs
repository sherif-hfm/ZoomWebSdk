﻿using Newtonsoft.Json.Linq;
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
using BackApis.Helpers;
using System.Net.Http.Headers;
using Newtonsoft.Json;

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

        [Route("Test2")]
        [HttpGet]
        public Task<HttpResponseMessage> Test2()
        {
            Thread.Sleep(5000);
            return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.OK, "Test OK"));
        }

        [Route("GetMeeting")]
        [HttpGet]
        public Task<HttpResponseMessage> GetMeeting()
        {
            long result;
            if (MeetingsControl.Meetings.TryDequeue(out result))
            {
                return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.OK, result.ToString()));
            }
            else
            {
                return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.BadRequest,"error"));
            }
        }

        [Route("GetNewMeeting")]
        [HttpGet]
        public Task<HttpResponseMessage> GetNewMeeting()
        {
            try
            {
                string userId = "WVYgh4MITA6Ng-Q5ObWO1A";
                string srvUrl = Helpers.Settings.ZoomApiUrl + $"/users/{userId}/meetings";
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.JwtToken.GenerateToken());
                    MeetingRequest req = new MeetingRequest()
                    {
                        agenda = "agenda",
                        duration = 40,
                        password = "P@ssw0rd",
                        type = 1,
                        topic = "topic",
                        settings = new MeetingSettings()
                        {
                            host_video = true,
                            participant_video = true,
                            cn_meeting = false,
                            in_meeting = false,
                            join_before_host = false,
                            mute_upon_entry = false,
                            watermark = false,
                            approval_type = 0,
                            registration_type = 1,
                            audio = "voip",
                            auto_recording = "none",
                            enforce_login = false,
                            enforce_login_domains = false,
                            contact_name = "contact_name"
                        }
                    };
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                    var response = client.PostAsync(srvUrl, content).Result;
                    var responseStr = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var meetingResponse = JsonConvert.DeserializeObject<MeetingResponse>(responseStr);
                       return Task.FromResult<HttpResponseMessage>(Request.CreateResponse<MeetingResponse>(HttpStatusCode.OK, meetingResponse));
                    }
                    else
                        return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.BadRequest, ""));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
           
        }


        [Route("GetSignature")]
        [HttpPost]
        public Task<HttpResponseMessage> GetSignature(JObject _lLicencesDataInfoObj)
        {
            //Thread.Sleep(2000);
            string apiKey = Helpers.Settings.ApiKey;
            string apiSecret = Helpers.Settings.ApiSecret;
            string meetingNumber = _lLicencesDataInfoObj.GetValue("meetingNumber").ToString();
            String ts = _lLicencesDataInfoObj.GetValue("ts").ToString();
            //String ts = ToTimestamp(DateTime.Now).ToString();
            string role = _lLicencesDataInfoObj.GetValue("role").ToString(); ;
            string token = GenerateSignature(apiKey, apiSecret, meetingNumber, ts, role);

            return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.OK, token));
        }

        [Route("GenerateToken")]
        [HttpGet]
        public Task<HttpResponseMessage> GenerateToken()
        {
            try
            {
                var token = JwtToken.GenerateToken();
                return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.OK, token));
            }
            catch (Exception ex)
            {
                return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [Route("EndMeeting/{mid}")]
        [HttpGet]
        public Task<HttpResponseMessage> EndMeeting(string mid) 
        {
            try
            {
                string srvUrl = Helpers.Settings.ZoomApiUrl + $"/meetings/{mid}/status";
                using (var client = new HttpClient())
                {
                    HttpContent content = new StringContent("{\"action\": \"end\"}", Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.JwtToken.GenerateToken());
                    var response = client.PutAsync(srvUrl, content).Result;
                    var responseStr = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.OK));
                    }
                    else
                        return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.InternalServerError));
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static long ToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000;
            return epoch;
        }

        public string GenerateSignature(string apiKey, string apiSecret, string meetingNumber, string ts, string role)
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
