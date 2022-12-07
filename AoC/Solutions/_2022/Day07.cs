using AoC.Utils;
using System;
using System.Data;
using System.Linq;
using System.Xml.XPath;

namespace AoC.Solutions._2022
{
    public class Day07 : AoCDay
    {
        string[] data;
        Directory root = new Directory("/");

        public Day07() => Seed();

        void Seed()
        {
            data = GetInput();
            // create fole stucture
            Directory curr = root;
            foreach (var item in data)
            {
                if (item.StartsWith("$ cd "))
                {
                    string action = item.Replace("$ cd ", "");
                    if (action == "/") curr = root;
                    else if (action == "..") curr = curr.parent == null ? root : curr.parent;
                    else
                    {
                        var cnt = curr.SubDirs.Where(d => d.Name == action).Count();
                        if (cnt != 0) continue;
                        Directory newDir = new Directory(action);
                        newDir.parent = curr;
                        curr.SubDirs.Add(newDir);
                        curr = newDir;
                    }
                }
                else if (!item.StartsWith("$"))
                {
                    string[] lineProps = item.Split(" ");
                    string filesize = lineProps[0];
                    string filename = lineProps[1];
                    int size;
                    if (int.TryParse(filesize, out size))
                    {
                        ElfFile newFile = new ElfFile();
                        newFile.Name = filename;
                        newFile.Size = size;
                        var cnt = curr.Files.Where(d => d.Name == filename).Count();
                        if (cnt != 0) continue;
                        curr.Files.Add(newFile);
                        curr.Size += size;
                    }
                }
            }
            //Add subDirs to size
            AddDirecorySizes(root);

        }
        void AddDirecorySizes(Directory dir)
        {
            if (dir == null || dir.SubDirs.Count == 0) return;
            dir.SubDirs.ForEach(d =>
            {
                AddDirecorySizes(d);
                dir.Size += d.Size;
            });
        }



        List<int> GetAllSizes(Directory dir)
        {
            List<int> res = new List<int>();
            dir.SubDirs.ForEach(d =>
            {
                res.AddRange(GetAllSizes(d));
            });
            res.Add(dir.Size);
            return res;
        }

        public override void RunPart1()
        {
            Console.WriteLine("Answer Part 1: " + GetAllSizes(root).Where(size => size < 100000).Sum());
        }

        public override void RunPart2()
        {
            var freeSpace = 70000000 - root.Size;
            Console.WriteLine("Answer Part 2: " + GetAllSizes(root).Where(s => s + freeSpace >= 30000000).Min());
        }

    }

    public class Directory
    {
        public Directory? parent { get; set; }
        public string Name { get; set; }
        public int Size { get; set; } = 0;
        public List<Directory> SubDirs { get; set; } = new List<Directory>();
        public List<ElfFile> Files { get; set; } = new List<ElfFile>();

        public Directory(string name)
        {
            Name = name;
        }

    }

    public class ElfFile
    {
        public string Name { get; set; }
        public int Size { get; set; }
    }
}
