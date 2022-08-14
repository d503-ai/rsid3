using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using TagLib;


namespace RSID3
{
    /// <summary>
    /// Constructor
    /// </summary>
    public class FactoryMP3
    {

        readonly byte[] fileinbytes;            //Count bytes in thread
        readonly FileStream fs;                 //Thread for reading
        /// <summary>
        /// CONSTRUCT
        /// </summary>
        /// <param name="filename"></param>
        public FactoryMP3(string filename)
        {
            fileinbytes = System.IO.File.ReadAllBytes(filename);            //ReadAllBytes in selected file
            fs = new FileStream(filename, FileMode.Open, FileAccess.Read);  //Open File and Read it
        }
        //READ BYTES FOR FRAME
        public string GetTag(string tag)
        {
            int pos = 0;
            byte[] buffer = new byte[4]; //variable for reading 4-bytes in stream
            for (int i = 0; i < fileinbytes.Count(); i++)
            {
                //Read 4 bytes from the stream and write them into buffer
                //Offset is 4
                fs.Read(buffer, 4, 4);
                //Check if Equals
                if(String.Equals(Convert.ToString(buffer), tag))
                {
                    pos = i + 8;
                }
            }
            //Check whether position is not 0
            if (pos != 0)
            {
                int nextbytes;
                string tag_res = "";
                //Sets the current position of this stream to the given value
                fs.Seek(pos, SeekOrigin.Begin);
                //Write in result the founded tag
                while ((nextbytes = fs.ReadByte()) > 0)
                {
                    tag_res += Convert.ToChar(nextbytes);
                }
                return tag_res;
            }
            else return "";
        }
        /// <summary>
        /// Method to add object into List
        /// </summary>
        /// <param name="files"></param>
        /// <param name="f"></param>
        /// <param name="title"></param>
        /// <param name="artist"></param>
        /// <param name="album"></param>
        /// <param name="year"></param>
        /// <param name="genre"></param>
        public static void AddToList(List<MP3> files, string f, string[] tags)
        {
            files.Add(new MP3
            {
                FileNameAddress = f,
                Title = tags[0],
                Artist = tags[1],
                Album = tags[2],
                Year = tags[3],
                Genre = tags[4]
            });
        }
        /// <summary>
        /// Recursive search in folders
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static IEnumerable<MP3> DirSearch(string folder, List<MP3> files, string [] parames)
        {
            var allFiles = Directory.GetFiles(folder, "*.mp3", SearchOption.AllDirectories);
            bool isempty = true;
            if (parames != null)
            {
                isempty = false;
            }
            
            foreach (string f in allFiles)
            {
                FactoryMP3 ft = new FactoryMP3(f);
                bool contains = true;
                string title_org = ft.GetTag(TAGS.TITLE_FRAME_ID);
                string album_org = ft.GetTag(TAGS.ALBUM_FRAME_ID);
                string artist_org = ft.GetTag(TAGS.ARTIST_FRAME_ID);
                string year_org = ft.GetTag(TAGS.YEAR_FRAME_ID);
                string genre_org = ft.GetTag(TAGS.GENRE_FRAME_ID);

                string[] tags = { title_org, artist_org, album_org, year_org, genre_org };

                for (int x = 0; x < tags.Length; x++)
                {
                    if (tags[x] != null)
                    {
                        continue;
                    }
                    else
                    {
                        tags[x] = "";
                    }
                }
                /* Check if params are null
                 * TRUE - Add all the .mp3 files
                 * FALSE - Add all the specific .mp3 files
                 */
                if (!isempty)
                {
                    for (int i = 0; i < tags.Length; i++)
                    {
                        if (parames[i] != "")
                        {
                            if (tags[i].Contains(parames[i]))
                            {
                                continue;
                            }
                            else
                            {
                                contains = false;
                                break;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                if (contains)
                {
                    AddToList(files, f, tags);
                }
            }
            return files;
        }
    }
}