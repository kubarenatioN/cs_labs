using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Globalization;
//using System.IO.Compression.FileSystem;

namespace lab13
{
    class KNVDiskInfo
    {
        static public void FreeDiskSpace(DriveInfo[] drives)
        {
            Console.WriteLine("\n   Информация о свободном месте на диске");
            foreach (var d in drives)
            {
                if (d.IsReady)
                {
                   Console.WriteLine($"Диск {d.Name}: {d.AvailableFreeSpace / 1000000000} Гб");
                }
            }
        }
        static public void FileSystem()
        {
            Console.WriteLine("\n   Информация о файловой системе");

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (var d in drives)
            {
                Console.WriteLine($"Диск {d.Name}");
                if (d.IsReady)
                {
                    string[] repos = Directory.GetDirectories(d.Name);//массив строк папок диска
                    foreach (var repo in repos)
                    {
                        try
                        {
                            string[] files = Directory.GetFiles(repo);//массив строк файлов папки
                            Console.WriteLine($"\tПапка {repo}");
                            foreach (var file in files)
                            {
                                FileInfo fileInfo = new FileInfo(file);
                                Console.WriteLine($"\t\tФайл {fileInfo.Name}");
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("\t\t" + e.Message);
                        }
                    }
                }
            }
        }
        static public void DiskInfo(DriveInfo[] drives)
        {
            Console.WriteLine("\n   Информация о диске");
            foreach (var d in drives)
            {
                Console.Write($"Диск {d.Name}");
                if (d.IsReady)
                {
                    Console.WriteLine($"({d.TotalSize/1000000000} Гб) " +
                            $"доступно {d.AvailableFreeSpace/1000000000} Гб " +
                            $"метка {d.VolumeLabel}");
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }
    }
    class KNVFileInfo
    {
        static public void FullPath(string file)//Выводит полный путь к файлу
        {
            Console.WriteLine("\n   Полный путь к файлу");
            FileInfo fileInfo = new FileInfo(file);
            if (fileInfo.Exists)
            {
                Console.WriteLine(fileInfo.DirectoryName);
            }
            else
            {
                Console.WriteLine("Такого файла не существует.");
            }
        }
        static public void FileProps(string file)//Свойства файла
        {
            Console.WriteLine("\n   Свойства файла");
            FileInfo fileInfo = new FileInfo(file);
            if (fileInfo.Exists)
            {
                Console.WriteLine($"Размер: {fileInfo.Length} байт\nРасширение: {fileInfo.Extension}\nИмя: {fileInfo.Name}");
            }
            else
            {
                Console.WriteLine("Такого файла не существует.");
            }
        }
        static public void CreationTime(string file)//Дата создания
        {
            Console.WriteLine("\n   Время создания файла");
            FileInfo fileInfo = new FileInfo(file);
            if (fileInfo.Exists)
            {
                Console.WriteLine(fileInfo.CreationTime);
            }
            else
            {
                Console.WriteLine("Такого файла не существует.");
            }
        }
    }
    class KNVDirInfo
    {
        static public void FilesCount(string dirpath)//Количество файлов в папке
        {
            Console.WriteLine("\n   Количество файлов в директории " + dirpath);
            DirectoryInfo dirInfo = new DirectoryInfo(dirpath);
            if (dirInfo.Exists)
            {
                string[] files = Directory.GetFiles(dirpath);
                //foreach (var f in files)
                //{
                //    Console.WriteLine(f);
                //}
                Console.WriteLine($"Количество файлов: {files.Length}");
            }
            else
            {
                Console.WriteLine("Такого каталога не существует.");
            }
        }
        static public void CreationTime(string dirpath)
        {
            Console.WriteLine("\n   Дата создания директория " + dirpath);
            DirectoryInfo dirInfo = new DirectoryInfo(dirpath);
            if (dirInfo.Exists)
            {
                Console.WriteLine("Дата создания: " + dirInfo.CreationTime);
            }
            else
            {
                Console.WriteLine("Такого каталога не существует.");
            }
        }
        static public void SubDirsCount(string dirpath)
        {
            Console.WriteLine("\n   Количество поддиректорий в " + dirpath);
            if((new DirectoryInfo(dirpath)).Exists)
            {
                string[] subdirs = Directory.GetDirectories(dirpath);
                Console.WriteLine("Количество поддиректорий: " + subdirs.Length);
            }
            else
            {
                Console.WriteLine("Такого каталога не существует.");
            }
        }
        static public void ParentDirs(string dirpath)//Родительские директории
        {
            Console.WriteLine("\n   Список родительских директорий директория " + dirpath);
            DirectoryInfo dirInfo = new DirectoryInfo(dirpath);
            if (dirInfo.Exists)
            {
                bool flag = true;
                Stack<string> parentdirs = new Stack<string>();
                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (drive.Name == dirInfo.Name)
                    {
                        flag = false;
                        Console.WriteLine("Директория является диском.");
                        break;
                    }
                }
                while(flag)
                {
                    parentdirs.Push(dirInfo.Parent.Name);
                    dirInfo = dirInfo.Parent;
                    foreach (var drive in DriveInfo.GetDrives())
                    {
                        if (drive.Name == dirInfo.Name)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                while(parentdirs.Count != 0)
                {
                    Console.WriteLine(parentdirs.Pop());
                }
            }
            else
            {
                Console.WriteLine("Такого каталога не существует.");
            }
        }
    }
    class KNVFileManager
    {
        static public void SaveDiskChildrenInfo(string drive)
        {
            Console.WriteLine("\n   Считываем и записываем информацию о содержимом диска " + drive);
            DirectoryInfo dirInfo = new DirectoryInfo(drive);
            if (dirInfo.Exists)
            {
                DirectoryInfo inspectdir = new DirectoryInfo(@"D:\Visual_Studio\2 course\1-sem\lab13\KNVInspect");
                inspectdir.Create();
                string filepath = $@"{inspectdir.FullName}\knvdirinfo.txt";
                FileInfo fileInfo = new FileInfo(filepath);
                try
                {
                    using (StreamWriter Writer = new StreamWriter(filepath))
                    {
                        Writer.WriteLine("\tПапки:");
                        foreach (var item in dirInfo.GetDirectories())
                        {
                            Writer.WriteLine(item);
                        }
                        Writer.WriteLine("\tФайлы:");
                        foreach (var item in dirInfo.GetFiles())
                        {
                            Writer.WriteLine(item);
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                fileInfo.CopyTo($@"{inspectdir.FullName}\knvdirinfo-copy.txt", true);
                File.Delete(filepath);
            }
            else
            {
                Console.WriteLine("Диск не готов.");
            }
        }
        static public void CopyFilesWithExtension(string dirpath, string extension)
        {
            Console.WriteLine("\n   Копирование файлов одного расширения ");
            DirectoryInfo dirFrom = new DirectoryInfo(dirpath);
            //Создаем объект DirectoryInfo для работы с папкой KNVFiles
            DirectoryInfo dirExt = new DirectoryInfo(@"D:\Visual_Studio\2 course\1-sem\lab13\KNVFiles");
            dirExt.Create();
            if (dirFrom.Exists)
            {
                foreach (var file in dirFrom.GetFiles())
                {
                    if(file.Extension == extension)
                    {
                        File.Copy(file.FullName, $@"{dirExt.FullName}\{file.Name}", true);
                    }
                }
                DirectoryInfo dirTo = new DirectoryInfo(@"D:\Visual_Studio\2 course\1-sem\lab13\KNVInspect\KNVFiles");
                if (dirTo.Exists)
                {
                    dirTo.Delete(true);
                    if (dirExt.Exists)
                    {
                        dirExt.MoveTo(dirTo.FullName);
                    }
                }
                else
                {
                    if (dirExt.Exists)
                    {
                        dirExt.MoveTo(dirTo.FullName);
                    }
                }
            }
            else
            {
                Console.WriteLine("Такой директории не существует.");
            }
        }
        static public void CreateArchive(string dirpath)
        {
            Console.WriteLine("\n   Архивирование файлов ");
            DirectoryInfo dirToArchive = new DirectoryInfo(dirpath);
            string archivedDir = @"D:\Visual_Studio\2 course\1-sem\lab13\KNVInspect\Archived";
            Directory.CreateDirectory(archivedDir);
            if (dirToArchive.Exists)
            {
                foreach (var file in dirToArchive.GetFiles())
                {
                    FileStream sourceStream = new FileStream(file.FullName, FileMode.OpenOrCreate);
                    FileStream targetStream = File.Create($@"{archivedDir}\{file.Name}.gz");
                    GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress);
                    //ZipFile.CreateFromDirectory
                    sourceStream.CopyTo(compressionStream);
                    sourceStream.Close();
                    compressionStream.Close();
                    targetStream.Close();
                    Console.WriteLine($"Сжатие файла {file.Name}");
                }
            }
            else
            {
                Console.WriteLine("Такой папки не существует.");
            }
        } 
        static public void Unzip(string dirpath)
        {
            Console.WriteLine("\n   Разархивирование файлов ");
            DirectoryInfo dirToUnzip = new DirectoryInfo(dirpath);
            string unzipDir = @"D:\Visual_Studio\2 course\1-sem\lab13\KNVInspect\Unzipped";
            Directory.CreateDirectory(unzipDir);
            if (dirToUnzip.Exists)
            {

                foreach (var file in dirToUnzip.GetFiles())
                {
                    FileStream sourceStream = new FileStream(file.FullName, FileMode.OpenOrCreate);
                    FileStream targetStream = File.Create($@"{unzipDir}\new-{file.Name}.svg");
                    GZipStream unzipStream = new GZipStream(sourceStream, CompressionMode.Decompress);
                    //ZipFile.CreateFromDirectory
                    unzipStream.CopyTo(targetStream);
                    unzipStream.Close();
                    targetStream.Close();
                    sourceStream.Close();
                    Console.WriteLine($"Разархивация файла {file.Name}");
                }
            }
            else
            {
                Console.WriteLine("Такой папки не существует.");
            }
        }
    }
    class KNVLog
    {
        static public string path = @"D:\Visual_Studio\2 course\1-sem\lab13\KNVInspect\knvlogfile.txt";
        static public void ReadLog(string strToSearch)
        {
            using (StreamReader Reader = new StreamReader(path))
            {
                string line = Reader.ReadLine();
                string nextline = Reader.ReadLine();
                while (nextline != null)
                {
                    if(line.Contains(strToSearch))
                    {
                        Console.WriteLine(line);
                        Console.WriteLine(nextline);
                        Console.WriteLine();
                    }
                    line = nextline;
                    nextline = Reader.ReadLine();
                }
            }
        }
        static public void SearchOperationType()
        {
            Console.WriteLine("Выберите тип операции для поиска...");
            Console.WriteLine("1 - Создание");
            Console.WriteLine("2 - Изменение");
            Console.WriteLine("3 - Переименование");
            Console.WriteLine("4 - Удаление");
            string type = Console.ReadLine();
            switch (type)
            {
                case "1":
                    ReadLog("Created");
                    break;
                case "2":
                    ReadLog("Changed");
                    break;
                case "3":
                    ReadLog("переименован");
                    break;
                case "4":
                    ReadLog("Deleted");
                    break;
                default:
                    Console.WriteLine("Error, invalid operation choice");
                    break;
            }
        }
        static public void SearchOperationTime()
        {
            Console.WriteLine("Введите дату операции в формате ХХ.ХХ.ХХХХ");
            string date = Console.ReadLine();
            using (StreamReader Reader = new StreamReader(path))
            {
                bool isfind = false;
                string line = Reader.ReadLine();
                string nextline = Reader.ReadLine();
                while (nextline != null)
                {
                    if (nextline.Contains(date))
                    {
                        isfind = true;
                        Console.WriteLine(line);
                        Console.WriteLine(nextline);
                        Console.WriteLine();
                    }
                    line = nextline;
                    nextline = Reader.ReadLine();
                }
                if (!isfind)
                {
                    Console.WriteLine("Ничего не найдено.");
                }
            }
        }
        static public void NumberOfFields()
        {
            int number = 0;
            using (StreamReader Reader = new StreamReader(path))
            {
                string line;
                while ((line = Reader.ReadLine()) != null)
                {
                    if (line.Contains("#"))
                    {
                        number++;
                    }
                }
            }
            Console.WriteLine($"Количество записей: {number}");
        }
        static public void Log()
        {
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = $@"D:\Visual_Studio\2 course\1-sem\lab13";

                watcher.NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.CreationTime;

                // Only watch text files.
                //watcher.Filter = "*.txt";

                // Add event handlers.
                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                watcher.Deleted += OnChanged;
                watcher.Renamed += OnRenamed;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Нажмите q чтобы выйти из наблюдения.");
                while (Console.ReadLine() != "q");
            }
        }
        //Обработчики
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            using (StreamWriter Writer = new StreamWriter(path, true))
            {
                Writer.WriteLine("#");
                Writer.WriteLine($"Файл ({e.Name}): {e.ChangeType} {e.FullPath}");
                Writer.WriteLine($"Дата и время: { DateTime.Now.ToString()}");
            }
        }
        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            using (StreamWriter Writer = new StreamWriter(path, true))
            {
                Writer.WriteLine("#");
                Writer.WriteLine($"Файл ({e.OldName}): переименован в  {e.Name}");
                Writer.WriteLine($"Дата и время: { DateTime.Now.ToString()}");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();//Массив дисков компьютера
            KNVDiskInfo.FreeDiskSpace(drives);
            KNVDiskInfo.FileSystem();
            KNVDiskInfo.DiskInfo(drives);

            KNVFileInfo.FullPath(@"C:\Users\Lenovo\Desktop\ООП Лекции\11CS_Файловая_система_потоквые классы.ppt");
            KNVFileInfo.FileProps(@"C:\Users\Lenovo\Desktop\ООП Лекции\11CS_Файловая_система_потоквые классы.ppt");
            KNVFileInfo.CreationTime (@"C:\Users\Lenovo\Desktop\ООП Лекции\11CS_Файловая_система_потоквые классы.ppt");

            KNVDirInfo.FilesCount(@"C:\Users\Lenovo\Desktop\ООП Лекции");
            KNVDirInfo.CreationTime(@"C:\Users\Lenovo\Desktop\react");
            KNVDirInfo.SubDirsCount(@"C:\Users\Lenovo\Desktop\projects");
            KNVDirInfo.ParentDirs(@"C:\Users\Lenovo\Desktop\projects");

            KNVFileManager.SaveDiskChildrenInfo(@"C:\");
            KNVFileManager.CopyFilesWithExtension(@"C:\Users\Lenovo\Desktop\projects\eastpack rainbow\app\images", ".svg");
            KNVFileManager.CreateArchive(@"D:\Visual_Studio\2 course\1-sem\lab13\KNVInspect\KNVFiles");
            KNVFileManager.Unzip(@"D:\Visual_Studio\2 course\1-sem\lab13\KNVInspect\Archived");

            KNVLog.Log();

            KNVLog.SearchOperationType();
            KNVLog.SearchOperationTime();
            KNVLog.NumberOfFields();
            
            Console.Read();
        }
    }
}
