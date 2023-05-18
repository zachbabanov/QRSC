using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.Common;
using ZXing;
using ZXing.QrCode;
using Newtonsoft.Json;

namespace QRSC
{
	public partial class Form1 : Form
	{
		// Создаем итерацию нашего класса в виде объекта в форме
		cDataHolder cCurrentDataHolder = new cDataHolder();
		// Создаем переменную для хранения параметров отрисовки изображения qr-кода
		private QrCodeEncodingOptions options;
		public Form1()
		{
			///<summary>
			///Запуск окна приложения
			///</summary>
			InitializeComponent();
			//Задание параметров отрисовки изображения qr-кода
			options = new QrCodeEncodingOptions
			{
				// Отключаем проблемную функцию в qr для большей поддержки
				DisableECI = true,
				// Выставляем кодировку
				CharacterSet = "UTF-8",
				// Ширина и высота отрисовки
				Width = 640,
				Height = 640,
			};

			// Получкеме данных в объекте класса
			cCurrentDataHolder.fProcessing();

			// Отрисовка qr по полученным данным конвертированным в json
			var writer = new BarcodeWriter();
			writer.Format = BarcodeFormat.QR_CODE;
			writer.Options = options;
			var qr = new ZXing.BarcodeWriter();
			qr.Options = options;
			qr.Format = ZXing.BarcodeFormat.QR_CODE;
			// Конвертируем наш объект класса в json-формат
			var result = new Bitmap(qr.Write(JsonConvert.SerializeObject(cCurrentDataHolder)));
			// Передаем отрисованый qr в объект picturebox1
			pictureBox1.Image = result;
		}

        private void timer1_Tick(object sender, EventArgs e)
        {
			// Обновление qr-кода после получения новых данных каждые 10 секунд

			// Обнуляем все переменные в объекте класса
			cCurrentDataHolder.fProcessingZero();
			// Вновь получаем все данные 
			cCurrentDataHolder.fProcessing();

			// Отрисовываем новый qr с новыми данными конвертированными в json
			var qr = new ZXing.BarcodeWriter();
			qr.Options = options;
			qr.Format = ZXing.BarcodeFormat.QR_CODE;
			// Конвертируем наш объект класса в json-формат
			var result = new Bitmap(qr.Write(JsonConvert.SerializeObject(cCurrentDataHolder)));
			// Передаем отрисованый qr в объект picturebox1 и обновляем его
			pictureBox1.Image = result;
			pictureBox1.Refresh();
		}
		/// <summary>
		/// Функция разворачивания окна из иконки трея
		/// </summary>
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
			// Если кликнули по иконе в трее => показываем окно и прячем иконку в трее
			this.Show();
			this.WindowState = FormWindowState.Normal;
			notifyIcon1.Visible = false;
		}
		/// <summary>
		/// Функция сворачивания окна в иконку трея
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
			// Если окно свернуто => прячем окно и показываем иконку в трее
			if (WindowState == FormWindowState.Minimized)
            {
				this.Hide(); 
				notifyIcon1.Visible = true;
			}
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
