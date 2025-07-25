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
        public void 프린터상태확인()
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
        public void 프린터이미지출력()
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
        public void 프린터설정테스트()
        {
            Printer.Device.openport();
            Printer.Device.sendcommand("SET ENCODER ON");
            Printer.Device.closeport();
        }

        [Fact]
        public void 프린터폼피드_일반()
        {
            Printer.FormFeed(new TSCPrinterSetupOptions
            {
                LabelWidthMillimeter = 80,
                LabelHeightMillimeter = 60,
                Offset = -1.5M,
            });
        }

        [Fact]
        public void 프린터폼피드_띠지()
        {
            Printer.FormFeed(new TSCPrinterSetupOptions
            {
                LabelWidthMillimeter = 25,
                LabelHeightMillimeter = 250,
                SensorType = SensorType.BlackLine,
                GapBlackLineHeight = 3,
                UseCutter = true,
                UseRibbon = false,
            });
        }

        [Fact]
        public void 프린터_피드_테스트()
        {
            Printer.Device.openport();
            Printer.Device.sendcommand("SET ENCODER ON");
            Printer.Device.closeport();
        }
    }
}