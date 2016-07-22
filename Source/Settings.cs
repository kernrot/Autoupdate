#region Copyright

// -------------------------------------------------------------------------------
// Project: Kernrot.GitLab.Autoupdate
// File: Settings.cs
// Author: Conrad Kernrot (gitlab_autoupdate@kernrot.de)
// -------------------------------------------------------------------------------
// Licensed under the MIT License,
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.opensource.org/licenses/mit-license.php
// -------------------------------------------------------------------------------

#endregion

namespace Kernrot.GitLab.Autoupdate
{
    internal static class Settings
    {
        /// <summary>
        ///     Hostname of the GitLab instance you want to update from.
        /// </summary>
        public static string Host => "gilab.com";

        /// <summary>
        ///     Project "path_with_namespace" of the GitLab project you want to update from.
        /// </summary>
        public static string ProjectName => "kernrot/MyProject";

        /// <summary>
        ///     Branch to use.
        /// </summary>
        public static string Branch => "master";

        /// <summary>
        ///     Stage defined in your ".gitlab-ci.yml" to use the artifacts from.
        /// </summary>
        public static string Stage => "deploy";

        /// <summary>
        ///     Personal Access Token to use for accessing GitLab.
        /// </summary>
        public static string Token => "gitlab-token-plzhere";

        /// <summary>
        ///     Filename of the Application to run from the <see cref="ApplicationFolder" />.
        /// </summary>
        public static string ApplicationExecutable => "MyApp.exe";

        /// <summary>
        ///     Location of the GitLab API (default).
        /// </summary>
        public static string Api => $"http://{Host}/api/v3";

        /// <summary>
        ///     Temporary folder to use for the downloaded artifact content.
        /// </summary>
        public static string DownloadFolder => "Update";

        /// <summary>
        ///     Folder to extract the artifact content to.
        /// </summary>
        public static string ApplicationFolder => "Application";

        /// <summary>
        ///     Name of the file to store information of the currently used artifact.
        /// </summary>
        public static string CurrentVersionFile => "version.json";
    }
}