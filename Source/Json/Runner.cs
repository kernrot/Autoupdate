#region Copyright

// -------------------------------------------------------------------------------
// Project: Kernrot.GitLab.Autoupdate
// File: Runner.cs
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
    public class Runner
    {
        public int id { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public bool is_shared { get; set; }
        public string name { get; set; }
    }
}