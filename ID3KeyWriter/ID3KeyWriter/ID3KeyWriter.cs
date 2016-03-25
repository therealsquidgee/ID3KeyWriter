using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID3KeyWriter
{
    public class ID3KeyWriter
    {
        public string path;

        public readonly Dictionary<string, string> keyMap = new Dictionary<string, string>()
        {
            {"Abm", "1A" },
            {"B", "1B" },
            {"Ebm", "2A" },
            {"F#", "2B" },
            {"Bbm", "3A" },
            {"Db", "3B" },
            {"Fm", "4A" },
            {"Ab", "4B" },
            {"Cm", "5A" },
            {"Eb", "5B" },
            {"Gm", "6A" },
            {"Bb", "6B" },
            {"Dm", "7A" },
            {"F", "7B" },
            {"Am", "8A" },
            {"C", "8B" },
            {"Em", "9A" },
            {"G", "9B" },
            {"Bm", "10A" },
            {"D", "10B" },
            {"F#m", "11A" },
            {"A", "11B" },
            {"Dbm", "12A" },
            {"E", "12B" }
        };

        public ID3KeyWriter(string path)
        {
            this.path = path;
        }

        public void writeTags()
        {
            // Iterate through files.
            foreach (var path in Directory.GetFiles(path).OrderBy(x => x))
            {
                //Console.WriteLine(path); // full path
                //Console.WriteLine(Path.GetFileName(path)); // file name

                if(!(Path.GetExtension(path) == ".mp3" ||
                    Path.GetExtension(path) == ".flac"||
                    Path.GetExtension(path) == ".aiff" ||
                    Path.GetExtension(path) == ".wav" ||
                    Path.GetExtension(path) == ".flac" ||
                    Path.GetExtension(path) == ".m4a" ||
                    Path.GetExtension(path) == ".mp4")) { continue; }

                var root = Path.GetPathRoot(path);
                Directory.SetCurrentDirectory(root);

                writeTag(path);
            }
        }

        private void writeTag(string track)
        {
            try
            {
                var simpleFile = new SimpleFile(track, new FileStream(track, FileMode.Open, FileAccess.ReadWrite));

                var fileAbstraction = new SimpleFileAbstraction(simpleFile);

                var tagFile = TagLib.File.Create(fileAbstraction);

                var title = tagFile.Tag.Title;
                var titleParts = title.Split('-');

                tagFile.Tag.Title = keyMap[titleParts[0].Trim()] + "-" + titleParts[1];
                tagFile.Save();
            }

            catch(Exception e)
            {
                log(e, track);
            }
        }

        private void log(Exception e, string track)
        {
            var message = "An error: " + e.Message + " occurred trying to write to the tag for the track "
                    + Path.GetFileName(track);

            // Create a file to write to.
            using (StreamWriter sw = File.AppendText(Directory.GetCurrentDirectory() + "\\log.txt"))
            {
                sw.WriteLine(message);
            }

            Console.WriteLine(message);
        }
    }
}
