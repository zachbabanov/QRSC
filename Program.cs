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
		/// Объект типа ManagementObjectSearcher для получения данных о видеоадаптере из Win32 API
		/// </summary>
        private static ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");
        /// <summary>
		/// Объект типа ManagementObjectSearcher для получения данных о процессоре из Win32 API
		/// </summary>
        private static ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
        /// <summary>
		/// Объект типа ManagementObjectSearcher для получения данных о ОС из Win32 API
		/// </summary>
        private static ManagementObjectSearcher myOperativeSystemObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
        // Набор объектов и функций нужный для получения данных об операвтиной пямяти от реестра ОС из Win32API
        private static ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
        private static ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
        private static ManagementObjectCollection results = searcher.Get();
        /// <summary>
        /// Функция получения имени видеоадаптера
        /// </summary>
        private string fGPUName()
        {
            foreach (ManagementObject obj in myVideoObject.Get())
            {
                return Convert.ToString(obj["Name"]);
            }
            return "none";
        }
        /// <summary>
		/// Функция получения статуса видеоадаптера
		/// </summary>
        private string fGPUStatus()
        {
            foreach (ManagementObject obj in myVideoObject.Get())
            {
                return Convert.ToString(obj["Status"]);
            }
            return "none";
        }
        /// <summary>
		/// Функция получения количества ОЗУ видеоадаптера
		/// </summary>
        private double fGPURAM()
        {
            foreach (ManagementObject obj in myVideoObject.Get())
            {
                return Convert.ToDouble(obj["AdapterRAM"]);
            }
            return 0;
        }
        /// <summary>
		/// Функция получения версии драйвера видеоадаптера
		/// </summary>
        private string fGPUDriverVersion()
        {
            foreach (ManagementObject obj in myVideoObject.Get())
            {
                return Convert.ToString(obj["DriverVersion"]);
            }
            return "none";
        }
        /// <summary>
		/// Функция получения имени видеопроцессора видеоадаптера
		/// </summary>
        private string fGPUVideoProcessor()
        {
            foreach (ManagementObject obj in myVideoObject.Get())
            {
                return Convert.ToString(obj["VideoProcessor"]);
            }
            return "none";
        }
        /// <summary>
		/// Функция получения имени процессора
		/// </summary>
        private string fCPUName()
        {
           foreach (ManagementObject obj in myProcessorObject.Get())
            {
                return Convert.ToString(obj["Name"]);
            }
            return "none";
        }
        /// <summary>
		/// Функция получения имени производителя процессора
		/// </summary>
        private string fCPUManufacturer()
        {
           foreach (ManagementObject obj in myProcessorObject.Get())
            {
                return Convert.ToString(obj["Manufacturer"]);
            }
            return "none";
        }
        /// <summary>
		/// Функция получения текущей скорости процессора
		/// </summary>
        private long fCPUCurrentClockSpeed()
        {
           foreach (ManagementObject obj in myProcessorObject.Get())
            {
                return Convert.ToInt64(obj["CurrentClockSpeed"]);
            }
            return 0;
        }
        /// <summary>
		/// Функция получения количества ядер процессора
		/// </summary>
        private int fCPUNumberOfCores()
        {
           foreach (ManagementObject obj in myProcessorObject.Get())
            {
                return Convert.ToInt16(obj["NumberOfCores"]);
            }
            return 0;
        }
        /// <summary>
		/// Функция получения количества потоков процессора
		/// </summary>
        private int fCPUNumberOfThreads()
        {
           foreach (ManagementObject obj in myProcessorObject.Get())
            {
                return Convert.ToInt16(obj["NumberOfLogicalProcessors"]);
            }
            return 0;
        }
        /// <summary>
		/// Функция получения разрядности процессора
		/// </summary>
        private int fCPUWidth()
        {
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                return Convert.ToInt16(obj["AddressWidth"]);
            }
            return 0;
        }
        /// <summary>
        /// Функция получения названия системы
        /// </summary>
        private string fSystemName()
        {
            foreach (ManagementObject obj in myOperativeSystemObject.Get())
            {
                return Convert.ToString(obj["Caption"]);
            }
            return "none";
        }
        /// <summary>
        /// Функция получения ключа системы
        /// </summary>
        private string fSystemSerialNumber()
        {
            foreach (ManagementObject obj in myOperativeSystemObject.Get())
            {
                return Convert.ToString(obj["SerialNumber"]);
            }
            return "none";
        }
        /// <summary>
        /// Функция получения директории системы
        /// </summary>
        private string fSystemDirectory()
        {
            foreach (ManagementObject obj in myOperativeSystemObject.Get())
            {
                return Convert.ToString(obj["SystemDirectory"]);
            }
            return "none";
        }
        /// <summary>
        /// Функция получения версии системы
        /// </summary>
        private string fSystemVersion()
        {
            foreach (ManagementObject obj in myOperativeSystemObject.Get())
            {
                return Convert.ToString(obj["Version"]);
            }
            return "none";
        }
        /// <summary>
        /// Функция получения общего количества ОЗУ
        /// </summary>
        private long fRAMSize()
        {
           foreach (ManagementObject result in results)
            {
                return Convert.ToInt64(result["TotalVisibleMemorySize"]);
            }
            return 0;
        }
        /// <summary>
        /// Функция получения количества свободного ОЗУ
        /// </summary>
        private long fRAMFree()
        {
           foreach (ManagementObject result in results)
            {
                return Convert.ToInt64(result["FreePhysicalMemory"]);
            }
            return 0;
        }
        /// <summary>
        /// Структура типа данных о диске
        /// </summary>
        private struct sDisk
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
        private string fDiskParams()
        {
            // Набор переменных нужный для получения всех томов и жестких дисков из Win32API
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
                dAllDrives += $"Disk name: {sCurrentDisk.name}" +
                    $"Type: {sCurrentDisk.type}" +
                    $"Label: {sCurrentDisk.label}" +
                    $" File system: {sCurrentDisk.file_system}" +
                    $"Available space: {sCurrentDisk.avl_space}" +
                    $"Total space: {sCurrentDisk.total_space}" +
                    $"Root directory: {sCurrentDisk.root_dir}";
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

        /// <summary>
        /// Переменная для получения имени видеоадаптера
        /// </summary>
        public string dGPUName;
        /// <summary>
        /// Переменная для получения статуса видеоадаптера
        /// </summary>
        public string dGPUStatus;
        /// <summary>
        /// Переменная для получения версии драйвера видеоадаптера
        /// </summary>
        public string dGPUDriverVersion;
        /// <summary>
        /// Переменная для получения имени процессора видеоадаптера
        /// </summary>
        public string dGPUVideoProcessor;
        /// <summary>
        /// Переменная для получения имени процессора
        /// </summary>
        public string dCPUName;
        /// <summary>
        /// Переменная для получения производителя(бренда) процессора
        /// </summary>
        public string dCPUManufacturer;
        /// <summary>
        /// Переменная для получения информации об итерации Windows
        /// </summary>
        public string dSystemName;
        /// <summary>
        /// Переменная для получения ключа Windows
        /// </summary>
        public string dSystemSerialNumber;
        /// <summary>
        /// Переменная для получения пути к корневой папке системы
        /// </summary>
        public string dSystemDirectory;
        /// <summary>
        /// Переменная для получения версии Windows
        /// </summary>
        public string dSystemVersion;
        /// <summary>
        /// Переменная для получения количества ОЗУ видеоадаптерп
        /// </summary>
        public double dGPURAM;
        /// <summary>
        /// Переменная для получения количества логических ядер процессора
        /// </summary>
        public int dCPUNumberOfCores;
        /// <summary>
        /// Переменная для получения потоков процессора
        /// </summary>
        public int dCPUNumberOfThreads;
        /// <summary>
        /// Переменная для получения разрядности процессора
        /// </summary>
        public int dCPUWidth;
        /// <summary>
        /// Переменная для получения общего объема ОЗУ
        /// </summary>
        public long dRAMSize;
        /// <summary>
        /// Переменная для получения объма свободного ОЗУ
        /// </summary>
        public long dRAMFree;
        /// <summary>
        /// Переменная для получения текущей скорости процессора
        /// </summary>
        public long dCPUCurrentClockSpeed;
        /// <summary>
        /// Строковая переменная для заполнения текстовой информации о дисках и томах
        /// </summary>
        public string dDiskParams;
        /// <summary>
        /// Функция получения данных
        /// </summary>
        public void fProcessing()
        {
            //Получение данных о ПК через присваивание переменных значений возвращаемых соответсвующими функциями
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

        public void fProcessingZero()
        {
            dGPUName = "";
            dGPUStatus = "";
            dGPUDriverVersion = "";
            dGPUVideoProcessor = "";
            dCPUName = "";
            dCPUManufacturer = "";
            dSystemName = "";
            dSystemSerialNumber = "";
            dSystemDirectory = "";
            dSystemVersion = "";
            dGPURAM = 0;
            dCPUNumberOfCores = 0;
            dCPUNumberOfThreads = 0;
            dCPUWidth = 0;
            dRAMSize = 0;
            dRAMFree = 0;
            dCPUCurrentClockSpeed = 0;
            dDiskParams = "";
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
