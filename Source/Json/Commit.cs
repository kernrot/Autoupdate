#region Copyright

// -------------------------------------------------------------------------------
// Project: Kernrot.GitLab.Autoupdate
// File: Commit.cs
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
    public class Commit
    {
        public string id { get; set; }
        public string short_id { get; set; }
        public string title { get; set; }
        public string author_name { get; set; }
        public string author_email { get; set; }
        public string created_at { get; set; }
        public string message { get; set; }
    }
}