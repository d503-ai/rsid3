using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSID3
{
    /// <summary>
    /// TAG's frames
    /// </summary>
    public class TAGS
    {
        public const string TITLE_FRAME_ID = "TIT2";
        public const string ARTIST_FRAME_ID = "TPE1";
        public const string ALBUM_FRAME_ID = "TALB";
        public const string GENRE_FRAME_ID = "TCON";
        public const string YEAR_FRAME_ID = "TYER";
    }
    /// <summary>
    /// Конструкторы
    /// </summary>
    public class MP3
    {
        public string FileNameAddress { get; set; } //Абсолютний шлях до файлу
        public virtual string Title { get; set; }   //Назва
        public virtual string Artist { get; set; }  //Виконавець
        public virtual string Album { get; set; }   //Альбом
        public virtual string Year { get; set; }    //Рік виконання
        public virtual string Genre { get; set; }   //Жанр

    }
}
