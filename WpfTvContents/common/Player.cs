using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTvContents.data;

namespace WpfTvContents.common
{
    public class Player
    {
        public List<PlayerInfo> listPlayer;
        public List<string> StoreList = new List<string>();

        public Player()
        {
            listPlayer = new List<PlayerInfo>();

            listPlayer.Add(new PlayerInfo("WMP", "wmplayer.exe"));
            listPlayer.Add(new PlayerInfo("GOM", "GOM.exe"));

            StoreList.Add(@"F:\JH-STORAGE\SHARE-TVCONTENTS\SHARE1");
            StoreList.Add(@"F:\JH-STORAGE\SHARE-TVCONTENTS\UTATANE");
            StoreList.Add(@"F:\JH-STORAGE\SHARE0\HDTV");
        }
        public List<PlayerInfo> GetPlayers()
        {
            return listPlayer;
        }
        public void Execute(ContentsData myData)
        {
            string executePathname = "";
            string playerName = "GOM";

            if (myData.Path.Length <= 0)
                return;

            //if (playerName.Equals("GOM"))
            //    executePathname = PlayList.MakeAsxFile(myData, Path.GetTempPath());
            //else if (playerName.Equals("WMP"))
            //    executePathname = PlayList.MakeWplFile(myData, Path.GetTempPath());
            //executePathname = PlayList.MakeWplFile(myMovieContents.Path, null, Path.GetTempPath(), myMovieContents.Name);

            var targets = from player in listPlayer
                          where player.Name.ToUpper() == playerName.ToUpper()
                          select player;

            foreach (string store in StoreList)
            {
                 string name = Path.Combine(myData.Path, myData.Name.Replace(" − ", " － "));
                if (File.Exists(name))
                {
                    executePathname = name;
                    break;
                }
            }

            if (executePathname.Length <= 0)
                return;

            foreach (PlayerInfo info in targets)
            {
                // 起動するファイル名の前後を""でくくる  例) test.mp4 --> "test.mp4"
                Process.Start(info.ExecuteName, "\"" + @executePathname + "\"");
                break;
            }
        }
    }

    public class PlayerInfo
    {
        public PlayerInfo(string myName, string myExecuteName)
        {
            Name = myName;
            ExecuteName = myExecuteName;
        }
        public string Name;
        public string ExecuteName;
    }

    class PlayList
    {
        /*
        public static string MakeWplFile(MovieContents myMovieContents, string myListOutputDir)
        {
            string WplFilename = System.IO.Path.Combine(myListOutputDir, "_" + myMovieContents.Name + ".wpl");

            StreamWriter utf16Writer = new StreamWriter(WplFilename, false, Encoding.GetEncoding("UTF-16"));

            // 先頭部分を出力
            utf16Writer.WriteLine("<?wpl version=\"1.0\"?>");
            utf16Writer.WriteLine("<smil>");
            utf16Writer.WriteLine("\t<body>");
            utf16Writer.WriteLine("\t\t<seq>");

            try
            {
                List<FileInfo> fileinfoList = null;
                if (myMovieContents.ListFile != null)
                    fileinfoList = GetFileInfoList(myMovieContents.ListFile);
                else
                    fileinfoList = myMovieContents.MovieList;

                foreach (FileInfo fileinfo in fileinfoList)
                    utf16Writer.WriteLine("\t\t\t<media src=\"" + fileinfo.FullName + "\" />");

                utf16Writer.WriteLine("\t\t</seq>");
                utf16Writer.WriteLine("\t</body>");
                utf16Writer.WriteLine("</smil>");
            }
            finally
            {
                utf16Writer.Close();
            }

            return WplFilename;
        }

        public static List<FileInfo> GetFileInfoList(FileInfo myList)
        {
            List<FileInfo> fileinfoList = new List<FileInfo>();

            StreamReader sreader = new StreamReader(myList.FullName);
            string line = sreader.ReadLine();
            while (line != null)
            {
                string movieFilename = Path.Combine(myList.DirectoryName, line);
                FileInfo fileinfo = new FileInfo(movieFilename);
                if (fileinfo.Exists)
                    fileinfoList.Add(fileinfo);

                line = sreader.ReadLine();
            }

            sreader.Close();

            return fileinfoList;
        }

        public static string MakeAsxFile(MovieContents myMovieContents, string myListOutputDir)
        {
            string playListPathname = System.IO.Path.Combine(myListOutputDir, "_" + myMovieContents.Name + ".asx");

            StreamWriter utf16Writer = new StreamWriter(playListPathname, false, Encoding.GetEncoding("UTF-16"));

            try
            {
                List<FileInfo> fileinfoList = null;
                if (myMovieContents.ListFile != null)
                    fileinfoList = GetFileInfoList(myMovieContents.ListFile);
                else
                    fileinfoList = myMovieContents.MovieList;

                // 先頭部分を出力
                utf16Writer.WriteLine("<asx version = \"3.0\" >");

                foreach (FileInfo fileinfo in fileinfoList)
                {
                    utf16Writer.WriteLine("<entry>");
                    utf16Writer.WriteLine("<title>" + fileinfo.Name + "</title>");

                    // １動画分のリスト生成
                    utf16Writer.WriteLine("\t\t\t<ref href=\"" + fileinfo.FullName + "\" />");

                    utf16Writer.WriteLine("</entry>");
                }

                utf16Writer.WriteLine("</asx>");
            }
            catch (Exception e)
            {
                throw new Exception(playListPathname + " " + e.Message);
            }
            finally
            {
                utf16Writer.Close();
            }

            return playListPathname;
        }
         */
    }
}
