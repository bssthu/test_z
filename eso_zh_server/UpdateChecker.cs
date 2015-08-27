using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Helpers;
using System.Windows.Forms;

namespace eso_zh_server
{
    class UpdateChecker
    {
        private const String updateUrl = "https://api.github.com/repos/esozh/eso_zh_server/releases/latest";
        private const String releasePageUrl = "https://github.com/esozh/eso_zh_server/releases/latest";
        private String versionString;

        public UpdateChecker()
        {
            versionString = Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        public void checkUpdate()
        {
            WebClient http = new WebClient();
            http.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 5.5; Windows NT)");
            http.DownloadStringCompleted += http_DownloadStringCompleted;
            http.DownloadStringAsync(new Uri(updateUrl));
        }

        private void http_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                String response = e.Result;
                String latestVersion = getVersion(response);
                if (versionNewer(latestVersion, versionString))
                {
                    if (MessageBox.Show(String.Format("发现新版本v{0}，是否前往下载？", latestVersion), "发现新版本", MessageBoxButtons.YesNo)
                            == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(releasePageUrl);
                    }
                }
                else
                {
                    MessageBox.Show(String.Format("版本v{0}已是最新。", latestVersion));
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message + "是否手动检查？", "检查更新失败", MessageBoxButtons.YesNo)
                        == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(releasePageUrl);
                }
            }
        }

        private bool versionNewer(String latestVersion, String currentVersion)
        {
            String[] latestArray = latestVersion.Split('.');
            String[] currentArray = currentVersion.Split('.');

            int length = Math.Max(latestArray.Length, currentArray.Length);
            int[] latest = new int[length];
            int[] current = new int[length];

            for (int i = 0; i < latestArray.Length; i++)
            {
                int.TryParse(latestArray[i], out latest[i]);
            }
            for (int i = 0; i < currentArray.Length; i++)
            {
                int.TryParse(currentArray[i], out current[i]);
            }

            for (int i = 0; i < length; i++)
            {
                if (latest[i] > current[i])
                {
                    return true;
                }
                if (latest[i] < current[i])
                {
                    return false;
                }
            }

            return false;
        }

        private String getVersion(String jsonString)
        {
            dynamic json = Json.Decode(jsonString);
            String version = json.tag_name;
            if (version.StartsWith("v"))
            {
                version = version.Substring(1);
            }
            return version;
        }
    }
}
