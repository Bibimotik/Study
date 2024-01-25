using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

public class BMOLog
{
    private const string LogFileName = "bmologfile.txt";

    public void WriteLog(string action, string detail)
    {
        string logEntry = $"{DateTime.Now} - Действие: {action}, Детали: {detail}";

        try
        {
            using (StreamWriter writer = new StreamWriter(LogFileName, true))
            {
                writer.WriteLine(logEntry);
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка записи в log file: {ex.Message}");
        }
    }

    public void ReadLog()
    {
        try
        {
            using (StreamReader reader = new StreamReader(LogFileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка чтения из log file: {ex.Message}");
        }
    }

    public void SearchLogByKeyword(string searchTerm)
    {
        try
        {
            using (StreamReader reader = new StreamReader(LogFileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(searchTerm))
                    {
                        Console.WriteLine(line);
                    }
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка поиска в log file: {ex.Message}");
        }
    }

    public void SearchLogByDateTime(DateTime fromDate, DateTime toDate)
    {
        try
        {
            using (StreamReader reader = new StreamReader(LogFileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    DateTime logDateTime;
                    if (TryParseLogDateTime(line, out logDateTime))
                    {
                        if (logDateTime >= fromDate && logDateTime <= toDate)
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка поиска в log file: {ex.Message}");
        }
    }

    public int GetLogEntryCount()
    {
        int count = 0;
        try
        {
            using (StreamReader reader = new StreamReader(LogFileName))
            {
                while (reader.ReadLine() != null)
                {
                    count++;
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка чтения из log file: {ex.Message}");
        }

        return count;
    }

    private bool TryParseLogDateTime(string logLine, out DateTime logDateTime)
    {
        logDateTime = DateTime.MinValue;
        int index = logLine.IndexOf('-');
        if (index >= 0 && index < logLine.Length - 2)
        {
            string dateTimeString = logLine.Substring(0, index).Trim();
            return DateTime.TryParse(dateTimeString, out logDateTime);
        }
        return false;
    }
}

public class BMODiskInfo
{
    private readonly BMOLog logger;

    public BMODiskInfo()
    {
        logger = new BMOLog();
    }

    public void GetFreeSpace(string driveName)
    {
        DriveInfo drive = new DriveInfo(driveName);
        Console.WriteLine($"Свободное место на диске {driveName}: {drive.AvailableFreeSpace} байтов");

        logger.WriteLog("GetFreeSpace", $"Диск: {driveName}");
    }

    public void GetFileSystem(string driveName)
    {
        DriveInfo drive = new DriveInfo(driveName);
        Console.WriteLine($"Файловая система диска {driveName}: {drive.DriveFormat}");

        logger.WriteLog("GetFileSystem", $"Диск: {driveName}");
    }

    public void GetAllDrivesInfo()
    {
        DriveInfo[] drives = DriveInfo.GetDrives();

        foreach (DriveInfo drive in drives)
        {
            Console.WriteLine($"Имя диска: {drive.Name}");
            Console.WriteLine($"Общий размер: {drive.TotalSize} байтов");
            Console.WriteLine($"Свободное место: {drive.TotalFreeSpace} байтов");
            Console.WriteLine($"Метка тома: {drive.VolumeLabel}");
            Console.WriteLine();

            logger.WriteLog("GetAllDrivesInfo", $"Диск: {drive.Name}");
        }
    }
}

public class BMOFileInfo
{
    private readonly BMOLog logger;

    public BMOFileInfo()
    {
        logger = new BMOLog();
    }

    public void PrintFullPath(string filePath)
    {
        try
        {
            string fullPath = Path.GetFullPath(filePath);
            Console.WriteLine($"Полный путь файла: {fullPath}");

            logger.WriteLog("PrintFullPath", $"Файл: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    public void PrintFileInfo(string filePath)
    {
        try
        {
            FileInfo fileInfo = new FileInfo(filePath);
            Console.WriteLine($"Имя файла: {fileInfo.Name}");
            Console.WriteLine($"Расширение файла: {fileInfo.Extension}");
            Console.WriteLine($"Размер файла: {fileInfo.Length} байтов");

            logger.WriteLog("PrintFileInfo", $"Файл: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    public void PrintFileDates(string filePath)
    {
        try
        {
            FileInfo fileInfo = new FileInfo(filePath);
            Console.WriteLine($"Дата создания файла: {fileInfo.CreationTime}");
            Console.WriteLine($"Дата последнего изменения файла: {fileInfo.LastWriteTime}");

            logger.WriteLog("PrintFileDates", $"Файл: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}

public class BMODirInfo
{
    private readonly BMOLog logger;

    public BMODirInfo()
    {
        logger = new BMOLog();
    }

    public void PrintFilesCount(string directoryPath)
    {
        try
        {
            string[] files = Directory.GetFiles(directoryPath);
            int fileCount = files.Length;
            Console.WriteLine($"Количество файлов: {fileCount}");

            logger.WriteLog("PrintFilesCount", $"Директория: {directoryPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    public void PrintCreationTime(string directoryPath)
    {
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            DateTime creationTime = directoryInfo.CreationTime;
            Console.WriteLine($"Время создания: {creationTime}");

            logger.WriteLog("PrintCreationTime", $"Директория: {directoryPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    public void PrintSubdirectoriesCount(string directoryPath)
    {
        try
        {
            string[] subdirectories = Directory.GetDirectories(directoryPath);
            int subdirectoryCount = subdirectories.Length;
            Console.WriteLine($"Количество поддиректориев: {subdirectoryCount}");

            logger.WriteLog("PrintSubdirectoriesCount", $"Директория: {directoryPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    public void PrintParentDirectories(string directoryPath)
    {
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            DirectoryInfo parentDirectory = directoryInfo.Parent;
            while (parentDirectory != null)
            {
                Console.WriteLine($"Родительская директория: {parentDirectory.FullName}");
                parentDirectory = parentDirectory.Parent;
            }

            logger.WriteLog("PrintParentDirectories", $"Директория: {directoryPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}

public class BMOFileManager
{
    private string diskPath;
    private readonly BMOLog logger;

    public BMOFileManager()
    {
        logger = new BMOLog();
    }

    public BMOFileManager(string diskPath)
    {
        this.diskPath = diskPath;
        logger = new BMOLog();
    }

    public void ExecuteActions()
    {
        string inspectDirPath = Path.Combine(diskPath, "BMOInspect");
        Directory.CreateDirectory(inspectDirPath);
        logger.WriteLog("ExecuteActions", $"Created directory: {inspectDirPath}");

        string dirInfoFilePath = Path.Combine(inspectDirPath, "BMOdirinfo.txt");
        SaveDirectoryInfoToFile(dirInfoFilePath);
        logger.WriteLog("ExecuteActions", $"Saved directory info to file: {dirInfoFilePath}");

        string copiedFilePath = CopyAndRenameFile(dirInfoFilePath);
        logger.WriteLog("ExecuteActions", $"Copied and renamed file: {dirInfoFilePath} to {copiedFilePath}");

        DeleteFile(dirInfoFilePath);
        logger.WriteLog("ExecuteActions", $"Deleted file: {dirInfoFilePath}");

        string filesDirPath = Path.Combine(diskPath, "BMOFiles");
        Directory.CreateDirectory(filesDirPath);
        logger.WriteLog("ExecuteActions", $"Created directory: {filesDirPath}");

        string extension = GetUserInput("Enter file extension:");
        logger.WriteLog("ExecuteActions", $"User entered file extension: {extension}");
        CopyFilesByExtension(extension, filesDirPath);
        logger.WriteLog("ExecuteActions", $"Copied files with extension: {extension} to directory: {filesDirPath}");

        string archiveFilePath = Path.Combine(inspectDirPath, "BMOFiles.zip");
        CreateArchive(filesDirPath, archiveFilePath);
        logger.WriteLog("ExecuteActions", $"Created archive: {archiveFilePath}");

        string extractionDirPath = Path.Combine(diskPath, "BMODestination");
        ExtractArchive(archiveFilePath, extractionDirPath);
        logger.WriteLog("ExecuteActions", $"Extracted archive: {archiveFilePath} to directory: {extractionDirPath}");
    }

    private void SaveDirectoryInfoToFile(string filePath)
    {
        string[] directoryContents = Directory.GetFileSystemEntries(diskPath);
        File.WriteAllLines(filePath, directoryContents);
    }

    private string CopyAndRenameFile(string sourceFilePath)
    {
        string destinationFilePath = Path.Combine(diskPath, "BMOdirinfo_copy.txt");
        File.Copy(sourceFilePath, destinationFilePath);
        return destinationFilePath;
    }

    private void DeleteFile(string filePath)
    {
        File.Delete(filePath);
    }

    private void CopyFilesByExtension(string extension, string destinationDirPath)
    {
        string[] files = Directory.GetFiles(diskPath, $"*.{extension}");
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            string destinationFilePath = Path.Combine(destinationDirPath, fileName);
            File.Copy(file, destinationFilePath);
        }
    }

    private void CreateArchive(string sourceDirPath, string archiveFilePath)
    {
        ZipFile.CreateFromDirectory(sourceDirPath, archiveFilePath);
    }

    private void ExtractArchive(string archiveFilePath, string destinationDirPath)
    {
        ZipFile.ExtractToDirectory(archiveFilePath, destinationDirPath);
    }

    private static string GetUserInput(string message)
    {
        Console.WriteLine(message);
        return Console.ReadLine();
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("---------------------------------");

        BMODiskInfo diskInfo = new BMODiskInfo();

        diskInfo.GetFreeSpace("C:");

        diskInfo.GetFileSystem("C:");

        diskInfo.GetAllDrivesInfo();
        Console.WriteLine("---------------------------------");

        string filePath = "D:\\Учеба\\C#\\new\\Hello.txt";

        BMOFileInfo fileInfo = new BMOFileInfo();

        fileInfo.PrintFullPath(filePath);
        fileInfo.PrintFileInfo(filePath);
        Console.WriteLine("---------------------------------");

        string directoryPath = "D:\\Учеба";

        BMODirInfo dirInfo = new BMODirInfo();

        dirInfo.PrintFilesCount(directoryPath);
        dirInfo.PrintCreationTime(directoryPath);
        Console.WriteLine("---------------------------------");

        string diskPath = "D:\\";
        BMOFileManager fileManager = new BMOFileManager(diskPath);
        fileManager.ExecuteActions();
        Console.WriteLine("---------------------------------");

        BMOLog logger = new BMOLog();
        DateTime fromDate = new DateTime(2023, 11, 6, 0, 0, 0);
        DateTime toDate = new DateTime(2023, 11, 6, 23, 59, 59);
        logger.SearchLogByDateTime(fromDate, toDate);
        Console.WriteLine("---------------------------------");

        int entryCount = logger.GetLogEntryCount();
        Console.WriteLine($"Количество записей в лог-файле: {entryCount}");
        Console.WriteLine("---------------------------------");
    }
}