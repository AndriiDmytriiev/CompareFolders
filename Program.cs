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
        public static string[] str = { @"C:\in", @"C:\out" };
        public static string[] strArr1 = new string[5000000];
        public static string[] strArr2 = new string[5000000];
        public static string[] strArr3 = new string[5000000];
        public static string[] strArr4 = new string[5000000];
        public static int i = 0;
        public static int j = 0;
        public static int count1 = 0;
        public static int count2 = 0;

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
        public static void Main(string[] args)
        {
           
            

            //foreach (string path in str)
            {
                if (File.Exists(@str[0]))
                {
                    // This path is a file
                    ProcessFile(@str[0]);
                }
                else if (Directory.Exists(@str[0]))
                {
                    // This path is a directory
                    ProcessDirectory(@str[0]);
                }
                else
                {
                    Console.WriteLine("{0} is not a valid file or directory.", @str[0]);
                }
               
            }

            {
                if (File.Exists(@str[1]))
                {
                    // This path is a file
                    ProcessFile2(@str[1]);
                }
                else if (Directory.Exists(@str[1]))
                {
                    // This path is a directory
                    ProcessDirectory2(@str[1]);
                }
                else
                {
                    Console.WriteLine("{0} is not a valid file or directory.", @str[1]);
                }
               
            }






            var list1 = new List<Files>();
            var list2 = new List<Files>();

            for (int l=0; l< i; l++ )
            list1.Add(new Files() { ID = l, FileName = strArr1[l] });

            for (int m = 0; m < j; m++)
                list2.Add(new Files() { ID = m, FileName = strArr2[m] });






            list1.Where(x => list2.Contains(x))
                .ToList()
                .ForEach(x => Console.WriteLine(x));

           
            //< Delete file =
             var result = list2.Where(p => list1.All(p2 => p2.FileName != p.FileName));
            var lstCount1 = Count<Files>(result);
           
            //< Create file =
            var result2 = list1.Where(p => list2.All(p2 => p2.FileName != p.FileName));
            var lstCount2 = Count<Files>(result2);
var dt = System.DateTime.UtcNow;
            var strDt = dt.ToShortTimeString();
            strDt = strDt.Replace(":","");
            
            
            using (StreamWriter sw = new StreamWriter(@"C:\OUT\outputfile" + strDt + ".xml"))
            {
                sw.WriteLine("<? xml version = \"1.0\" encoding = \"utf-8\" ?>");
                sw.WriteLine("<Diff>");
                foreach (var item in result)
                {
                    sw.WriteLine("<Delete file =" + item.FileName + " />");
                }
                foreach (var item in result2)
                {
                    sw.WriteLine("<Create file =" + item.FileName + " />");
                }
                sw.WriteLine("</Diff>");
            }

            Console.ReadKey();
}
public static void ProcessDirectory(string targetDirectory)
{
// Process the list of files found in the directory.
string[] fileEntries = Directory.GetFiles(targetDirectory);
foreach (string fileName in fileEntries)
    ProcessFile(fileName);

// Recurse into subdirectories of this directory.
string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
foreach (string subdirectory in subdirectoryEntries)
    ProcessDirectory(subdirectory);
}


public static void ProcessFile(string @path)
{
int numberCharacters = @str[0].Length;
path = path.Substring(numberCharacters, path.Length-numberCharacters);

Console.WriteLine(
new XElement("Node",
new XAttribute("Bar", "file"),
new XElement("Nested", @path)));

strArr1[i] = @path;
i++;

}


public static void ProcessDirectory2(string targetDirectory)
{
// Process the list of files found in the directory.
string[] fileEntries = Directory.GetFiles(targetDirectory);
foreach (string fileName in fileEntries)
    ProcessFile2(fileName);

// Recurse into subdirectories of this directory.
string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
foreach (string subdirectory in subdirectoryEntries)
    ProcessDirectory2(subdirectory);
}

// Insert logic for processing found files here.
public static void ProcessFile2(string @path)
{
int numberCharacters = @str[1].Length;
path = path.Substring(numberCharacters, path.Length - numberCharacters);
Console.WriteLine(
 new XElement("Node",
 new XAttribute("Bar", "file"),
 new XElement("Nested", @path)));
strArr2[j] = @path;
j++;

}



}
}



