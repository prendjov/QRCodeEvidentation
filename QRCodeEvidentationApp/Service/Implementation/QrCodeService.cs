using QRCodeEvidentationApp.Service.Interface;
using QRCoder;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace QRCodeEvidentationApp.Service.Implementation;

public class QrCodeService : IQrCodeService
{
    public byte[] GenerateQRCodeWithImage(string url, string overlayImagePath)
    {
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q))
        {
            // Generate the QR code as a byte array
            using (BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData))
            {
                byte[] qrCodeBytes = qrCode.GetGraphic(20);

                // Convert the byte array to a Bitmap using ImageSharp (which supports full-color images)
                using (MemoryStream ms = new MemoryStream(qrCodeBytes))
                {
                    // Load the QR code as an ImageSharp image
                    using (Image<Rgba32> qrCodeImage = Image.Load<Rgba32>(ms))
                    {
                        // Load the overlay image in full color (make sure it's not converted to grayscale)
                        using (Image<Rgba32> overlayImage = Image.Load<Rgba32>(overlayImagePath))
                        {
                            // Resize the overlay image to fit within the QR code
                            int overlaySize = qrCodeImage.Width / 3; // Resize the overlay to 1/5th the QR code's size
                            overlayImage.Mutate(x => x.Resize(overlaySize, overlaySize));

                            // Draw the overlay image onto the QR code
                            qrCodeImage.Mutate(x => x.DrawImage(overlayImage, new Point((qrCodeImage.Width - overlaySize) / 2, (qrCodeImage.Height - overlaySize) / 2), 1f));

                            // Save the final QR code image to a byte array
                            using (MemoryStream finalStream = new MemoryStream())
                            {
                                qrCodeImage.SaveAsPng(finalStream); // Save as PNG to preserve colors and transparency
                                return finalStream.ToArray(); // Return the byte array of the final image
                            }
                        }
                    }
                }
            }
        }
    }

}