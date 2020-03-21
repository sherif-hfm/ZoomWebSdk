using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BackApis
{
    public static class MeetingsControl
    {
        public static ConcurrentQueue<int> Meetings = new ConcurrentQueue<int>();

        public static CancellationTokenSource token=new CancellationTokenSource();

        public static async Task AddMeetings()
        {
            while (true)
            {
                try
                {
                    if(Meetings.Count == 0)
                    {
                        for (int index = 0; index < 1000; index++)
                            Meetings.Enqueue(index);
                    }
                }
                catch (Exception e)
                {
                    // Handle exception
                }
                await Task.Delay(TimeSpan.FromSeconds(10), token.Token);
            }
        }
    }
}