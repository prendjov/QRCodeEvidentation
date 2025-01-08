namespace QRCodeEvidentationApp.Service.Interface;

public interface IQrCodeService
{
    /// <param name="url">The url where the QR code should lead to.</param>
    /// <param name="imagePath">Path of the image that should be over the QR code.</param>
    /// <returns>Array of bites.</returns>
    public byte[] GenerateQRCodeWithImage(string url, string imagePath);
}