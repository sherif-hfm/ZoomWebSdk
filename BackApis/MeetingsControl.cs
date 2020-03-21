using BackApis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BackApis
{
    public static class MeetingsControl
    {
        public static ConcurrentQueue<long> Meetings = new ConcurrentQueue<long>();

        public static CancellationTokenSource token=new CancellationTokenSource();

        public static async Task AddMeetings()
        {
            while (true)
            {
                try
                {
                    if(Meetings.Count == 0)
                    {
                        FillMeetings();
                    }
                }
                catch (Exception e)
                {
                    // Handle exception
                }
                await Task.Delay(TimeSpan.FromSeconds(10), token.Token);
            }
        }

        private static void FillMeetings()
        {
            //for (int index = 0; index < 1000; index++)
            //    Meetings.Enqueue(index);
            var users = GetUsers();
            if(users != null)
            {
                foreach (var user in users.users.Where(u => u.status == "active"))
                {
                    Thread.Sleep(200);
                    var meeting = GetMeetingtatus(user.pmi);
                    if (meeting != null)
                    {
                        if (meeting.status == "waiting")
                        {
                            Meetings.Enqueue(meeting.id);
                        }
                    }
                }
            }
        }

        private static ZoomUsers GetUsers()
        {
            try
            {
                string srvUrl = Helpers.Settings.ZoomApiUrl + @"/users/?page_size=50";
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.JwtToken.GenerateToken());
                    var response = client.GetAsync(srvUrl).Result;
                    var responseStr = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<ZoomUsers>(responseStr);
                    }
                    else
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static ZoomMeeting GetMeetingtatus(long meetingId)
        {
            try
            {
                string srvUrl = Helpers.Settings.ZoomApiUrl + @"/meetings/" + meetingId.ToString();
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.JwtToken.GenerateToken());
                    var response = client.GetAsync(srvUrl).Result;
                    var responseStr = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<ZoomMeeting>(responseStr);
                    }
                    else
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}