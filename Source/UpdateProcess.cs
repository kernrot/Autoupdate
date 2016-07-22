#region Copyright

// -------------------------------------------------------------------------------
// Project: Kernrot.GitLab.Autoupdate
// File: UpdateProcess.cs
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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using Json;
    using SimpleJson;

    /// <summary>
    ///     Contains all update and query logic.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class UpdateProcess : INotifyPropertyChanged
    {
        private List<Build> _buildsAll;
        private Build _buildsInstalled;
        private Build _buildsLatest;
        private bool _hasNewerVersion;

        private Project _project;


        /// <summary>
        ///     Initializes a new instance of the <see cref="UpdateProcess" /> class.
        /// </summary>
        public UpdateProcess()
        {
            if (ServerIsReachable(Settings.Host))
            {
                Project = GetProject(Settings.ProjectName);
                BuildsInstalled = GetInstalledBuild();
                BuildsLatest = GetLatestBuild();

                HasNewerVersion = BuildsInstalled.id < BuildsLatest.id;
                if (!HasNewerVersion) StartApplication();
            }
            else
            {
                StartApplication();
            }
        }

        /// <summary>
        ///     Gets or sets the project.
        /// </summary>
        public Project Project
        {
            get { return _project; }
            private set { _project = value; }
        }

        /// <summary>
        ///     All available builds.
        /// </summary>
        public List<Build> BuildsAll
        {
            get { return _buildsAll; }
            private set
            {
                _buildsAll = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The locally installed build.
        /// </summary>
        public Build BuildsInstalled
        {
            get { return _buildsInstalled; }
            private set
            {
                _buildsInstalled = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The latest available build.
        /// </summary>
        public Build BuildsLatest
        {
            get { return _buildsLatest; }
            private set
            {
                _buildsLatest = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Indicating whether this instance has newer build version to update to.
        /// </summary>
        public bool HasNewerVersion
        {
            get { return _hasNewerVersion; }
            private set
            {
                _hasNewerVersion = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        ///     Gets the update command.
        /// </summary>
        public ICommand UpdateCommand => new RelayCommand(() => { UpdateTo(BuildsLatest); });

        /// <summary>
        ///     Gets the start application command.
        /// </summary>
        public ICommand StartApplicationCommand => new RelayCommand(StartApplication);


        /// <summary>
        ///     Occurs on change of a property.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Starts the application.
        /// </summary>
        private void StartApplication()
        {
            var exe =
                Directory.EnumerateFiles(Settings.ApplicationFolder, Settings.ApplicationExecutable,
                    SearchOption.AllDirectories).Single();
            var process = new Process {StartInfo = new ProcessStartInfo(exe)};

            if (process.Start())
            {
                Application.Current.Shutdown(0);
                Environment.Exit(0);
            }
        }

        /// <summary>
        ///     Gets the currently installed build from the local version file.
        ///     (if file exists, else creates new empty file)
        /// </summary>
        /// <returns>currently installed build</returns>
        private Build GetInstalledBuild()
        {
            Build installed;
            if (File.Exists(Settings.CurrentVersionFile))
            {
                installed = SimpleJson.DeserializeObject<Build>(File.ReadAllText(Settings.CurrentVersionFile));
            }
            else
            {
                installed = new Build();
                SetInstalledBuild(installed);
            }
            return installed;
        }

        /// <summary>
        ///     Sets the installed build to the local version file.
        /// </summary>
        /// <param name="build">The build to write.</param>
        private void SetInstalledBuild(Build build)
        {
            File.WriteAllText(Settings.CurrentVersionFile, SimpleJson.SerializeObject(build));
            BuildsInstalled = build;
        }

        /// <summary>
        ///     Gets the project.
        /// </summary>
        /// <param name="projectName">Name of the project with namespace. (path_with_namespace)</param>
        /// <returns></returns>
        private Project GetProject(string projectName)
        {
            var w = new WebClient();
            string projectsRequest =
                $"{Settings.Api}/projects/?private_token={Settings.Token}";

            var projectsResponse = w.DownloadString(projectsRequest);

            var projects = SimpleJson.DeserializeObject<List<Project>>(projectsResponse);

            var selectedProject = projects.Single(p => p.path_with_namespace == projectName);
            return selectedProject;
        }

        /// <summary>
        ///     Gets the latest build.
        /// </summary>
        /// <returns></returns>
        private Build GetLatestBuild()
        {
            var w = new WebClient();
            string buildsRequest =
                $"{Settings.Api}/projects/{Project.id}/builds/?private_token={Settings.Token}";
            var buildsResponse = w.DownloadString(buildsRequest);

            var builds = BuildsAll = SimpleJson.DeserializeObject<List<Build>>(buildsResponse);

            var latestBuild = builds.Where(b => b.@ref == Settings.Branch && b.stage == Settings.Stage)
                .OrderByDescending(f => f.id).First();

            return latestBuild;
        }

        /// <summary>
        ///     Checks if a Server is reachable vi ping.
        /// </summary>
        /// <param name="host">The host to check.</param>
        /// <returns></returns>
        private bool ServerIsReachable(string host)
        {
            try
            {
                var x = new Ping();
                var reply = x.Send(host);
                return reply?.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Updates to the specified build by downloading its artifacts and extracting them.
        /// </summary>
        /// <param name="build">The build to update to.</param>
        private async void UpdateTo(Build build)
        {
            var w = new WebClient();

            string artifactRequest =
                $"{Settings.Api}/projects/{Project.id}/builds/{build.id}/artifacts/?private_token={Settings.Token}";

            var downloaded = await w.DownloadDataTaskAsync(artifactRequest);

            UnpackZip(downloaded);
            SetInstalledBuild(build);

            StartApplication();
        }

        /// <summary>
        ///     Unpacks from a zip byte-array.
        /// </summary>
        /// <param name="zipBytes">The zip bytes.</param>
        private void UnpackZip(byte[] zipBytes)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(zipBytes, 0, zipBytes.Length);
                var z = new ZipArchive(ms);
                if (Directory.Exists(Settings.DownloadFolder)) Directory.Delete(Settings.DownloadFolder, true);
                z.ExtractToDirectory(Settings.DownloadFolder);
            }

            if (Directory.Exists(Settings.ApplicationFolder))
            {
                Directory.Delete(Settings.ApplicationFolder, true);
            }
            Directory.Move(Settings.DownloadFolder, Settings.ApplicationFolder);
        }

        /// <summary>
        ///     Called when property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}