﻿using System;
using System.IO;
using System.Management;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace QRSC
{
    [Serializable]
    public class cDataHolder
    {
        /// <summary>
		/// Наименования суффиксов байтовых значений
		/// </summary>
        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        /// <summary>
		/// Функция поразрядного перевода байтовых значений
		/// </summary>
        static string SizeSuffix(Int64 value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }
        /// <summary>
		/// Функция получения имени видеоадаптера
		/// </summary>
        public string fGPUName()
        {
            ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject obj in myVideoObject.Get())
            {
                return Convert.ToString(obj["Name"]);
            }
            return "none";
        }
        /// <summary>
		/// Функция получения статуса видеоадаптера
		/// </summary>
        public string fGPUStatus()
        {
            ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject obj in myVideoObject.Get())
            {
                return Convert.ToString(obj["Status"]);
            }
            return "none";
        }
        /// <summary>
		/// Функция получения количества ОЗУ видеоадаптера
		/// </summary>
        public double fGPURAM()
        {
            ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject obj in myVideoObject.Get())
            {
                return Convert.ToDouble(obj["AdapterRAM"]);
            }
            return 0;
        }
        /// <summary>
		/// Функция получения версии драйвера видеоадаптера
		/// </summary>
        public string fGPUDriverVersion()
        {
            ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject obj in myVideoObject.Get())
            {
                return Convert.ToString(obj["DriverVersion"]);
            }
            return "none";
        }
        /// <summary>
		/// Функция получения имени видеопроцессора видеоадаптера
		/// </summary>
        public string fGPUVideoProcessor()
        {
            ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject obj in myVideoObject.Get())
            {
                return Convert.ToString(obj["VideoProcessor"]);
            }
            return "none";
        }
        /// <summary>
		/// Функция получения имени процессора
		/// </summary>
        public string fCPUName()
        {
            ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                return Convert.ToString(obj["Name"]);
            }
            return "none";
        }
        /// <summary>
		/// Функция получения имени производителя процессора
		/// </summary>
        public string fCPUManufacturer()
        {
            ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                return Convert.ToString(obj["Manufacturer"]);
            }
            return "none";
        }
        /// <summary>
		/// Функция получения текущей скорости процессора
		/// </summary>
        public long fCPUCurrentClockSpeed()
        {
            ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                return Convert.ToInt64(obj["CurrentClockSpeed"]);
            }
            return 0;
        }
        /// <summary>
		/// Функция получения количества ядер процессора
		/// </summary>
        public int fCPUNumberOfCores()
        {
            ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                return Convert.ToInt16(obj["NumberOfCores"]);
            }
            return 0;
        }
        /// <summary>
		/// Функция получения количества потоков процессора
		/// </summary>
        public int fCPUNumberOfThreads()
        {
            ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                return Convert.ToInt16(obj["NumberOfLogicalProcessors"]);
            }
            return 0;
        }
        /// <summary>
		/// Функция получения разрядности процессора
		/// </summary>
        public int fCPUWidth()
        {
            ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                return Convert.ToInt16(obj["AddressWidth"]);
            }
            return 0;
        }
        /// <summary>
        /// Функция получения названия системы
        /// </summary>
        public string fSystemName()
        {
            ManagementObjectSearcher myOperativeSystemObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject obj in myOperativeSystemObject.Get())
            {
                return Convert.ToString(obj["Caption"]);
            }
            return "none";
        }
        /// <summary>
        /// Функция получения ключа системы
        /// </summary>
        public string fSystemSerialNumber()
        {
            ManagementObjectSearcher myOperativeSystemObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject obj in myOperativeSystemObject.Get())
            {
                return Convert.ToString(obj["SerialNumber"]);
            }
            return "none";
        }
        /// <summary>
        /// Функция получения директории системы
        /// </summary>
        public string fSystemDirectory()
        {
            ManagementObjectSearcher myOperativeSystemObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject obj in myOperativeSystemObject.Get())
            {
                return Convert.ToString(obj["SystemDirectory"]);
            }
            return "none";
        }
        /// <summary>
        /// Функция получения версии системы
        /// </summary>
        public string fSystemVersion()
        {
            ManagementObjectSearcher myOperativeSystemObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject obj in myOperativeSystemObject.Get())
            {
                return Convert.ToString(obj["Version"]);
            }
            return "none";
        }
        /// <summary>
        /// Функция получения общего количества ОЗУ
        /// </summary>
        public long fRAMSize()
        {
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            {
                return Convert.ToInt64(result["TotalVisibleMemorySize"]);
            }
            return 0;
        }
        /// <summary>
        /// Функция получения количества свободного ОЗУ
        /// </summary>
        public long fRAMFree()
        {
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            {
                return Convert.ToInt64(result["FreePhysicalMemory"]);
            }
            return 0;
        }
        /// <summary>
        /// Структура типа данных о диске
        /// </summary>
        public struct sDisk
        {
            public string name;
            public string type;
            public string label;
            public string file_system;
            public double avl_space;
            public double total_space;
            public string root_dir;
        }
        /// <summary>
        /// Заполнение структуры даных о диске
        /// </summary>
        public string fDiskParams()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            string dAllDrives = "";
            foreach (DriveInfo d in allDrives)
            {
                sDisk sCurrentDisk = new sDisk();
                if (d.IsReady)
                {
                    sCurrentDisk.name = d.Name;
                    sCurrentDisk.type = d.DriveType.ToString();
                    sCurrentDisk.label = d.VolumeLabel;
                    sCurrentDisk.file_system = d.DriveFormat.ToString();
                    sCurrentDisk.avl_space = d.AvailableFreeSpace;
                    sCurrentDisk.total_space = d.TotalSize;
                    sCurrentDisk.root_dir = d.RootDirectory.ToString();
                }
                dAllDrives += $"Disk name: {sCurrentDisk.name}\n Type: {sCurrentDisk.type}\n " +
                    $"Label: {sCurrentDisk.label}\n File system: {sCurrentDisk.file_system}\n " +
                    $"Available space: {sCurrentDisk.avl_space}\n Total space: {sCurrentDisk.total_space}\n " +
                    $"Root directory: {sCurrentDisk.root_dir}\n";
            }
            return dAllDrives;
        }

        //Тип функция получения данных о сети, но все входящие данные
        //являются уникальными параметрами, которые можно представить в виде массива строк, но оно нам нужно?
        /*public void FNetworkParams()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            if (nics == null || nics.Length < 1)
            {
                Console.WriteLine("  No network interfaces found.");
            }
            else
            {
                foreach (NetworkInterface adapter in nics)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    Console.WriteLine();
                    Console.WriteLine(adapter.Description);
                    Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
                    Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
                    Console.WriteLine("  Physical Address ........................ : {0}", adapter.GetPhysicalAddress().ToString());
                    Console.WriteLine("  Operational status ...................... : {0}", adapter.OperationalStatus);
                }
            }
        }*/

        public string dGPUName;
        public string dGPUStatus;
        public string dGPUDriverVersion;
        public string dGPUVideoProcessor;
        public string dCPUName;
        public string dCPUManufacturer;
        public string dSystemName;
        public string dSystemSerialNumber;
        public string dSystemDirectory;
        public string dSystemVersion;
        public double dGPURAM;
        public int dCPUNumberOfCores;
        public int dCPUNumberOfThreads;
        public int dCPUWidth;
        public long dRAMSize;
        public long dRAMFree;
        public long dCPUCurrentClockSpeed;
        public string dDiskParams;
        /// <summary>
        /// Функция получения данных
        /// </summary>
        public void fProcessing()
        {
            dGPUName = fGPUName();
            dGPUStatus = fGPUStatus();
            dGPUDriverVersion = fGPUDriverVersion();
            dGPUVideoProcessor = fGPUVideoProcessor();
            dCPUName = fCPUName();
            dCPUManufacturer = fCPUManufacturer();
            dSystemName = fSystemName();
            dSystemSerialNumber = fSystemSerialNumber();
            dSystemDirectory = fSystemDirectory();
            dSystemVersion = fSystemVersion();
            dGPURAM = fGPURAM();
            dCPUNumberOfCores = fCPUNumberOfCores();
            dCPUNumberOfThreads = fCPUNumberOfThreads();
            dCPUWidth = fCPUWidth();
            dRAMSize = fRAMSize();
            dRAMFree = fRAMFree();
            dCPUCurrentClockSpeed = fCPUCurrentClockSpeed();
            dDiskParams = fDiskParams();
        }
}

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
