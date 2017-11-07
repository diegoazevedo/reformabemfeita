using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeaderReplace
{
    class Program
    {
        static void Main(string[] args)
        {
            string root = @"C:\reformabemfeita\reformabemfeita";

            var index = File.ReadAllText(Path.Combine(root, "index.html"));

            var middleMasterContent = GetContentBetween(index);

            var files = Directory.GetFiles(root, "*.html");

            foreach (var file in files)
            {
                if (file.Contains("index.html"))
                    continue;

                string contents = File.ReadAllText(file, Encoding.UTF8);

                var middlePageContent = GetContentBetween(contents);
                contents = index.Replace(middleMasterContent, middlePageContent);

                File.WriteAllText(file, contents, Encoding.UTF8);
            }
        }

        static string GetContentBetween(string content, string start = "<!-- begin -->", string end = "<!-- end -->")
        {
            int indexStart = content.IndexOf(start);
            int indexEnd = content.IndexOf(end);

            return content.Substring(indexStart, indexEnd - indexStart + end.Length);
        }
    }
}