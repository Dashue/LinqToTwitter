using System;
using LinqToTwitter.Common;
using LitJson;
using Newtonsoft.Json;

namespace LinqToTwitter
{
    public class Event
    {
        public Event() { }
        public Event(JsonData evt)
        {
            Target = new User(evt.GetValue<JsonData>("target"));
            Source = new User(evt.GetValue<JsonData>("source"));
            EventName = evt.GetValue<string>("event");
            var targetObj = evt.GetValue<JsonData>("target_object", null);
            TargetObject = targetObj == null ? null : targetObj.ToString();
            if (EventName == "quoted_tweet")
            {
                TargetObject = JsonConvert.SerializeObject(new Status(targetObj), new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
            CreatedAt = evt.GetValue<string>("created_at").GetDate(DateTime.MaxValue);
        }

        public User Target { get; set; }

        public User Source { get; set; }

        public string EventName { get; set; }

        public string TargetObject { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
