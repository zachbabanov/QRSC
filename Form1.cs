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
		cDataHolder cCurrentDataHolder = new cDataHolder();
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
				DisableECI = true,
				CharacterSet = "UTF-8",
				Width = 640,
				Height = 640,
			};
			cCurrentDataHolder.fProcessing();
			//Отрисовка qr
			var writer = new BarcodeWriter();
			writer.Format = BarcodeFormat.QR_CODE;
			writer.Options = options;

			var qr = new ZXing.BarcodeWriter();
			qr.Options = options;
			qr.Format = ZXing.BarcodeFormat.QR_CODE;
			var result = new Bitmap(qr.Write(JsonConvert.SerializeObject(cCurrentDataHolder)));
			pictureBox1.Image = result;
		}

        private void timer1_Tick(object sender, EventArgs e)
        {
			//Обновление qr-кода после получения новых данных каждые 5 секунд
			cCurrentDataHolder.fProcessingZero();
			cCurrentDataHolder.fProcessing();
			var qr = new ZXing.BarcodeWriter();
			qr.Options = options;
			qr.Format = ZXing.BarcodeFormat.QR_CODE;
			var result = new Bitmap(qr.Write(JsonConvert.SerializeObject(cCurrentDataHolder)));
			pictureBox1.Image = result;
			pictureBox1.Refresh();
		}
    }
}
