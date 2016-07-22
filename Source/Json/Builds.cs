namespace GitLabUpdater.Json
{
    public class Build
    {
        public int id { get; set; }
        public string status { get; set; }
        public string stage { get; set; }
        public string name { get; set; }
        public string @ref { get; set; }
        public bool tag { get; set; }
        public object coverage { get; set; }
        public string created_at { get; set; }
        public string started_at { get; set; }
        public string finished_at { get; set; }
        public User user { get; set; }
        public ArtifactFile artifacts_file { get; set; }
        public Commit commit { get; set; }
        public Runner runner { get; set; }
    }
}
