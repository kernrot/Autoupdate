#region Copyright

// -------------------------------------------------------------------------------
// Project: Kernrot.GitLab.Autoupdate
// File: Project.cs
// Author: Conrad Kernrot (gitlab_autoupdate@kernrot.de)
// -------------------------------------------------------------------------------
// Licensed under the MIT License,
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.opensource.org/licenses/mit-license.php
// -------------------------------------------------------------------------------

#endregion

namespace Kernrot.GitLab.Autoupdate.Json
{
    using System;

    public class Project
    {
        public int id { get; set; }
        public string description { get; set; }
        public string default_branch { get; set; }
        public string[] tag_list { get; set; }
        public bool _public { get; set; }
        public bool archived { get; set; }
        public int visibility_level { get; set; }
        public string ssh_url_to_repo { get; set; }
        public string http_url_to_repo { get; set; }
        public string web_url { get; set; }
        public Owner owner { get; set; }
        public string name { get; set; }
        public string name_with_namespace { get; set; }
        public string path { get; set; }
        public string path_with_namespace { get; set; }
        public bool issues_enabled { get; set; }
        public bool merge_requests_enabled { get; set; }
        public bool wiki_enabled { get; set; }
        public bool builds_enabled { get; set; }
        public bool snippets_enabled { get; set; }
        public bool? container_registry_enabled { get; set; }
        public DateTime created_at { get; set; }
        public DateTime last_activity_at { get; set; }
        public bool shared_runners_enabled { get; set; }
        public int creator_id { get; set; }
        public Namespace _namespace { get; set; }
        public string avatar_url { get; set; }
        public int star_count { get; set; }
        public int forks_count { get; set; }
        public int open_issues_count { get; set; }
        public bool public_builds { get; set; }
        public Permissions permissions { get; set; }
    }
}