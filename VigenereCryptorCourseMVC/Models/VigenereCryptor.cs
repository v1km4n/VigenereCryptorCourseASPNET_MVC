namespace VigenereCryptorCourseMVC.Models
{
    public class VigenereCryptor
    {
        public string Text { get; set; } = "пример текста";
        public string Key { get; set; } = "примерключа";
        public string EditedText { get; set; }
        public bool Mode { get; set; } = true;
        public IFormFile? File { get; set; } = null;
        public string? DownloadLink = null;
        public static string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
    }
}
