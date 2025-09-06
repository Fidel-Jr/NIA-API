using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace Nia.Infrastructure.Services
{
    public class QrCodeService
    {
        public string GenerateQrCodeWithLogoBase64(string content, string logoPath)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrCodeData);
            using var qrCodeImage = qrCode.GetGraphic(20);

            // Load and resize logo
            using var logo = new Bitmap(logoPath);
            int logoSize = qrCodeImage.Width / 5;
            using var resizedLogo = new Bitmap(logo, new Size(logoSize, logoSize));

            // Draw logo on center
            using var graphics = Graphics.FromImage(qrCodeImage);
            int x = (qrCodeImage.Width - logoSize) / 2;
            int y = (qrCodeImage.Height - logoSize) / 2;
            graphics.DrawImage(resizedLogo, x, y, logoSize, logoSize);

            // Convert to byte array and then Base64
            using var ms = new MemoryStream();
            qrCodeImage.Save(ms, ImageFormat.Png);
            byte[] imageBytes = ms.ToArray();

            // Return Base64 string (optionally add MIME prefix)
            string base64 = Convert.ToBase64String(imageBytes);
            return $"data:image/png;base64,{base64}";
        }
    }
}
