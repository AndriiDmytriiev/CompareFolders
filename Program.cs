using System;
using System.IO;
using System.Collections;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Bibliography;


namespace CompareFolders
{
    public class Files
    {
        public int ID { get; set; }
        public string FileName { get; set; }
    }

    public static class Program
    {
        public static string[] str = { @"C:\in", @"C:\out" }; //comparing folders
        public static string[] strArr1 = new string[5000000];
        public static string[] strArr2 = new string[5000000];
        public static string[] strArr3 = new string[5000000];
        public static string[] strArr4 = new string[5000000];
        public static int i = 0;
        public static int j = 0;
        public static int count1 = 0;
        public static int count2 = 0;
        public static List<String> files1 = new List<String>();
        public static List<String> files2 = new List<String>();
        public static int Count<TSource>(this IEnumerable<TSource> source)
        {
            
            ICollection<TSource> is2 = source as ICollection<TSource>;
            if (is2 != null)
            {
                return is2.Count;
            }
            int num = 0;
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    num++;
                }
            }
            return num;
        }
        
        public static void PrintDirectoryTree1(string directory, int lvl, string[] excludedFolders = null, string lvlSeperator = "")
        {
            excludedFolders = excludedFolders ?? new string[0];
            
            
            foreach (string f in Directory.GetFiles(directory))
            {
                int numberCharacters = @str[0].Length;
                var path = directory + "\\" + Path.GetFileName(f);


                    path = path.Substring(numberCharacters, path.Length - numberCharacters);


                files1.Add(path);
            }

            foreach (string d in Directory.GetDirectories(directory))
            {
                //Console.WriteLine(directory + "\\" + Path.GetFileName(d));
                var path = directory + "\\" + Path.GetFileName(d);

                int numberCharacters = @str[0].Length;
                path = path.Substring(numberCharacters, path.Length - numberCharacters);


                files1.Add(path);
                
                if (lvl > 0 && Array.IndexOf(excludedFolders, Path.GetFileName(d)) < 0)
                {
                    PrintDirectoryTree1(d, lvl - 1, excludedFolders,  lvlSeperator);
                }
            }
        }
        public static void PrintDirectoryTree2(string directory, int lvl, string[] excludedFolders = null, string lvlSeperator = "")
        {
            excludedFolders = excludedFolders ?? new string[0];
            

            foreach (string f in Directory.GetFiles(directory))
            {
                //Console.WriteLine(directory + "\\" + Path.GetFileName(f));
                var path = directory + "\\" + Path.GetFileName(f);

                int numberCharacters = @str[1].Length;
                path = path.Substring(numberCharacters, path.Length - numberCharacters);


                files2.Add(path);
            }

            foreach (string d in Directory.GetDirectories(directory))
            {
                //Console.WriteLine(directory + "\\" + Path.GetFileName(d));
                var path = directory + "\\" + Path.GetFileName(d);

                int numberCharacters = @str[1].Length;
                path = path.Substring(numberCharacters, path.Length - numberCharacters);


                files2.Add(path);

                if (lvl > 0 && Array.IndexOf(excludedFolders, Path.GetFileName(d)) < 0)
                {
                    PrintDirectoryTree2(d, lvl - 1, excludedFolders, lvlSeperator);
                }
            }
        }
        

        public static XmlDocument Compare(string oldPath, string newPath)
        {
            XmlDocument xml = new XmlDocument();
            // TODO: fill "xml" here
            //
            {
                

            }

            {
               

            }






            






            


            PrintDirectoryTree1(str[0], 2, new string[] { "folder3" });


            foreach (var item in files1)
            {
                Console.WriteLine(item);
            }
            PrintDirectoryTree2(str[1], 2, new string[] { "folder5" });


            foreach (var item in files2)
            {
                Console.WriteLine(item);
            }
           
            var dt = System.DateTime.UtcNow;
            var strDt = dt.ToShortTimeString();
            strDt = strDt.Replace(":", "");
            var path = @"c:\temp";
            if (!Directory.Exists(path))
            {
               DirectoryInfo di = Directory.CreateDirectory(path);
            }
            
            var filename = @path + @"\outputfile" + strDt + ".xml";
            using (StreamWriter sw = new StreamWriter(@filename))
            {
                var xmlLoad = "";
                sw.WriteLine("<?xml version = \"1.0\" encoding = \"utf-8\" ?>");
                xmlLoad = "<?xml version = \"1.0\" encoding = \"utf-8\" ?>";
                sw.WriteLine("<Diff>");
                xmlLoad += "<Diff>";
                foreach (var item in files1)
                {
                    var strFolder=str[0] + @item;
                    FileAttributes attr = File.GetAttributes(strFolder);
                    

                    //detect whether its a directory or file
                    if (((attr & FileAttributes.Directory) == FileAttributes.Directory))
                    {
                        if (IsDirectoryEmpty(strFolder)) 
                        {
                            sw.WriteLine("<Delete folder ='" + item + "' />");
                            xmlLoad += "<Delete folder ='" + item + "' />"; 
                        }
                        
                    }
                    else
                    {
                        sw.WriteLine("<Delete file ='" + item + "' />");
                        xmlLoad += "<Delete file ='" + item + "' />";
                    }
                }
                foreach (var item in files2)
                {
                    var strFolder = str[1] + @item;
                    FileAttributes attr = File.GetAttributes(strFolder);

                    //detect whether its a directory or file
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                      
                        {
                            sw.WriteLine("<Create folder ='" + item + "' />");
                            xmlLoad += "<Create folder ='" + item + "' />";
                        }
                        else 
                        {
                               sw.WriteLine("<Create file ='" + item + "' />");
                               xmlLoad += "<Create file ='" + item + "' />"; 
                        }
                       
                    
                }
                sw.WriteLine("</Diff>");
                xmlLoad += "</Diff>";
                xml.LoadXml(xmlLoad);
            }





            
            
            return xml;
        }
        public static void Main(string[] args)
        {

            Compare(str[0], str[1]);

        }

        public static bool IsDirectoryEmpty(string path)
        {
            return IsDirectoryEmptyFind(new DirectoryInfo(path));
        }

        public static bool IsDirectoryEmptyFind(DirectoryInfo directory)
        {
            FileInfo[] files = directory.GetFiles();
            DirectoryInfo[] subdirs = directory.GetDirectories();

            return (files.Length == 0 && subdirs.Length == 0);
        }







}
}



