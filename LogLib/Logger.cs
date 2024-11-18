using System;
using System.IO;
using System.Text;

namespace LogLib
{
    public class Logger
    {
        private static readonly object _lock = new object();
        private readonly string _logDirectory;
        private readonly LogLevel _minimumLevel;
        private readonly long _maxFileSize; // 以字节为单位
        private string _currentLogFile;

        public enum LogLevel
        {
            Debug = 0,
            Info = 1,
            Warning = 2,
            Error = 3,
            Fatal = 4
        }

        /// <summary>
        /// 初始化日志记录器
        /// </summary>
        /// <param name="logDirectory">日志目录</param>
        /// <param name="maxFileSizeInMB">单个日志文件最大大小(MB)</param>
        /// <param name="minimumLevel">最小日志级别</param>
        public Logger(string logDirectory, int maxFileSizeInMB = 10, LogLevel minimumLevel = LogLevel.Info)
        {
            _logDirectory = logDirectory;
            _minimumLevel = minimumLevel;
            _maxFileSize = maxFileSizeInMB * 1024 * 1024; // 转换为字节

            // 确保日志目录存在
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }

            // 初始化当前日志文件路径
            UpdateLogFile();
        }

        private void UpdateLogFile()
        {
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd");
            string baseFileName = Path.Combine(_logDirectory, $"log_{dateStr}.txt");

            // 如果基础文件不存在，直接使用
            if (!File.Exists(baseFileName))
            {
                _currentLogFile = baseFileName;
                return;
            }

            // 检查文件大小，如果超过限制则创建新文件
            FileInfo fileInfo = new FileInfo(baseFileName);
            if (fileInfo.Length < _maxFileSize)
            {
                _currentLogFile = baseFileName;
                return;
            }

            // 查找当天最新的日志文件序号
            int sequence = 1;
            string newFileName;
            do
            {
                newFileName = Path.Combine(_logDirectory, $"log_{dateStr}_{sequence}.txt");
                if (!File.Exists(newFileName))
                {
                    _currentLogFile = newFileName;
                    return;
                }

                fileInfo = new FileInfo(newFileName);
                if (fileInfo.Length < _maxFileSize)
                {
                    _currentLogFile = newFileName;
                    return;
                }

                sequence++;
            } while (true);
        }

        public void Log(LogLevel level, string message, Exception exception = null)
        {
            if (level < _minimumLevel) return;

            string logMessage = FormatLogMessage(level, message, exception);

            lock (_lock)
            {
                try
                {
                    // 检查是否需要更新日志文件
                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string fileDate = Path.GetFileName(_currentLogFile).Split('_')[1].Split('.')[0];

                    if (currentDate != fileDate)
                    {
                        UpdateLogFile();
                    }
                    else
                    {
                        FileInfo fileInfo = new FileInfo(_currentLogFile);
                        if (fileInfo.Exists && fileInfo.Length >= _maxFileSize)
                        {
                            UpdateLogFile();
                        }
                    }

                    File.AppendAllText(_currentLogFile, logMessage, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"写入日志失败: {ex.Message}");
                }
            }
        }

        private string FormatLogMessage(LogLevel level, string message, Exception exception)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{level}] {message}");

            if (exception != null)
            {
                sb.AppendLine($"异常信息: {exception.Message}");
                sb.AppendLine($"堆栈跟踪: {exception.StackTrace}");
            }

            return sb.ToString();
        }

        // 便捷方法
        public void Debug(string message, Exception exception = null)
            => Log(LogLevel.Debug, message, exception);

        public void Info(string message, Exception exception = null)
            => Log(LogLevel.Info, message, exception);

        public void Warning(string message, Exception exception = null)
            => Log(LogLevel.Warning, message, exception);

        public void Error(string message, Exception exception = null)
            => Log(LogLevel.Error, message, exception);

        public void Fatal(string message, Exception exception = null)
            => Log(LogLevel.Fatal, message, exception);
    }
}
