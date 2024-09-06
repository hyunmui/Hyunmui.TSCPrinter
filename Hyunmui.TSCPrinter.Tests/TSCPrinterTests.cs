using Hyunmui.TSCPrinter.Enums;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using TSCSDK;
using Xunit;
using Xunit.Abstractions;

namespace Hyunmui.TSCPrinter.Tests
{
    public class TSCPrinterTests
    {
        ITestOutputHelper console;

        TSCPrinter Printer;

        public TSCPrinterTests(ITestOutputHelper output)
        {
            console = output;
            Printer = new TSCPrinter(new usb());
        }

        [Fact]
        public void �����ͻ���Ȯ��()
        {
            var options = new TSCPrinterSetupOptions
            {
                LabelWidthMillimeter = 80,
                LabelHeightMillimeter = 60,
                Offset = -1.5M,
            };
            Printer.Device.openport();
            Printer.Device.setup(
                options.LabelWidthMillimeter.ToString(),
                options.LabelHeightMillimeter.ToString(),
                options.Speed.ToString(),
                options.Density.ToString(),
                ((int)options.SensorType).ToString(),
                options.GapBlackLineHeight.ToString(),
                options.GapBlackLineOffset.ToString());
            var dpi = Printer.Device.printersetting("SYSTEM", "INFORMATION", "DPI");
            console.WriteLine("DPI=" + dpi);
            Printer.Device.closeport();
        }

        [Fact]
        public void �������̹������()
        {
            var bitmap = new Bitmap("D:/test.bmp");
            Printer.Print(bitmap, new TSCPrinterSetupOptions
            {
                LabelWidthMillimeter = 80,
                LabelHeightMillimeter = 60,
                Offset = -1.5M,
            });
        }

        [Fact]
        public void �����ͼ����׽�Ʈ()
        {
            Printer.Device.openport();
            Printer.Device.sendcommand("SET ENCODER ON");
            Printer.Device.closeport();
        }

        [Fact]
        public void ���������ǵ�_�Ϲ�()
        {
            Printer.FormFeed(new TSCPrinterSetupOptions
            {
                LabelWidthMillimeter = 80,
                LabelHeightMillimeter = 60,
                Offset = -1.5M,
            });
        }

        [Fact]
        public void ���������ǵ�_����()
        {
            Printer.FormFeed(new TSCPrinterSetupOptions
            {
                LabelWidthMillimeter = 25,
                LabelHeightMillimeter = 250,
                SensorType = SensorType.BlackLine,
                GapBlackLineHeight = 3,
                UseCutter = true,
            });
        }

        [Fact]
        public void ������_�ǵ�_�׽�Ʈ()
        {
            Printer.Device.openport();
            Printer.Device.sendcommand("SET ENCODER ON");
            Printer.Device.closeport();
        }
    }
}