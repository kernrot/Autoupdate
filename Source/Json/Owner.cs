#region Copyright

// -------------------------------------------------------------------------------
// Project: Kernrot.GitLab.Autoupdate
// File: Owner.cs
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
    public class Owner
    {
        public string name { get; set; }
        public string username { get; set; }
        public int id { get; set; }
        public string state { get; set; }
        public string avatar_url { get; set; }
        public string web_url { get; set; }
    }
}