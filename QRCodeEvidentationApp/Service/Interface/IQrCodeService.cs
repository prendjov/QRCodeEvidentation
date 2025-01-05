namespace QRCodeEvidentationApp.Service.Interface;

public interface IQrCodeService
{
    public byte[] GenerateQRCodeWithImage(string url, string imagePath);
}