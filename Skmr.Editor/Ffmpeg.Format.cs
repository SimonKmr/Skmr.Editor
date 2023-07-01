using System;
using System.Collections.Generic;

namespace Skmr.Editor
{
    public partial class Ffmpeg
    {
        public class Format
        {
            public static Dictionary<string, Format> Extensions { get; private set; } = new Dictionary<string, Format>();


            public Format(string value) 
            { 
                Value = value;
                Extensions.Add(value, this);
            }
            public string Value { get; private set; }
            public override string ToString() => Value;

            

            public class Video : Format
            {
                public Video(string value) : base(value) { }

                public static Video Ts { get { return new Video(".ts"); } }
                public static Video Avi { get { return new Video(".avi"); } }
                public static Video Webm { get { return new Video(".webm"); } }
                public static Video Mp4 { get { return new Video(".mp4"); } }
                public static Video Flv { get { return new Video(".flv"); } }
                public static Video Mov { get { return new Video(".mov"); } }
                public static Video Mkv { get { return new Video(".mkv"); } }
                public static Video M3u8 { get { return new Video(".m3u8"); } }
            }
            public class Audio : Format
            {
                public Audio(string value) : base(value) { }

                public static Audio Aac { get { return new Audio(".aac"); } }
                public static Audio Mp3 { get { return new Audio(".mp3"); } }
                public static Audio Wav { get { return new Audio(".wav"); } }
            }
            public class Image : Format
            {
                public Image(string value) : base(value) { }

                public static Image Png { get { return new Image(".png"); } }
                public static Image Jpg { get { return new Image(".jpg"); } }
            }
        }
    }
}
