using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Mischief.Plots
{
    class WallCager : IPlot
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, String pvParam, UInt32 fWinIni);

        public void Plot()
        {
            // retrieve a list of nicholas cage images and choose a random one
            List<Uri> links = this.RetrieveCageImageLinks();
            Uri randomImage = links.ElementAt(new Random().Next(links.Count()));

            // download and save the image in the temp directory
            string fileName = Path.Combine(Path.GetTempPath(), System.Guid.NewGuid().ToString() + ".jpg");
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(randomImage, fileName);
            }

            // Stretch the wallpaper and set it
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue(@"WallpaperStyle", 2.ToString());
            key.SetValue(@"TileWallpaper", 0.ToString());
            SystemParametersInfo(0x14, 0, fileName, 0x01);
        }


        /// <summary>
        /// Search google images for nicholas cage and grab a list of urls for these images
        /// </summary>
        /// <returns>list of nicholas cage image urls</returns>
        private List<Uri> RetrieveCageImageLinks()
        {
            List<Uri> imageLocations = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.google.ca/search?hl=en&q=Nicholas+Cage&um=1&ie=UTF-8&tbm=isch&source=og&sa=N&tab=wi&ei=l0wdUb-BDfOq0AGowoDIAQ&biw=1680&bih=925&sei=m0wdUafBC6y50AGSm4DYCg");
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    imageLocations = ExtractImageLinksFromHtml(reader.ReadToEnd());
                }
            }
            return imageLocations;
        }


        /// <summary>
        /// Extract image urls from an html page using regular expressions and return the urls
        /// </summary>
        /// <param name="html">html page source</param>
        /// <returns>list of image urls found in the html page source</returns>
        private List<Uri> ExtractImageLinksFromHtml(string html)
        {
            List<Uri> links = new List<Uri>();
            string regex = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
            MatchCollection matchesImgSrc = Regex.Matches(html, regex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (Match m in matchesImgSrc)
            {
                string href = m.Groups[1].Value;
                links.Add(new Uri(href));
            }
            return links;
        }
    }
}
