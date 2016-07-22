#region Copyright

// -------------------------------------------------------------------------------
// Project: Kernrot.GitLab.Autoupdate
// File: User.cs
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
    public class User
    {
        public string name { get; set; }
        public string username { get; set; }
        public int id { get; set; }
        public string state { get; set; }
        public string avatar_url { get; set; }
        public string web_url { get; set; }
        public string created_at { get; set; }
        public bool is_admin { get; set; }
        public string bio { get; set; }
        public string location { get; set; }
        public string skype { get; set; }
        public string linkedin { get; set; }
        public string twitter { get; set; }
        public string website_url { get; set; }
    }
}