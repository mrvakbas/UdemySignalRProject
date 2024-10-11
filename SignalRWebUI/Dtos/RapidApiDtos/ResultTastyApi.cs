namespace SignalRWebUI.Dtos.RapidApiDtos
{
    public class RootTastApi
    {
        public List<ResultTastyApi> Results { get; set; }
    }
    public class ResultTastyApi
    {
        public string name { get; set; }
        public string original_video_url { get; set; }
        public int total_time_minutes { get; set; }
        public string thumbnail_url { get; set; }
    }
}
