using Microsoft.AspNetCore.Mvc;
using VigenereCryptorCourseMVC.Models;
using System.IO;
using System.Web;
using System.Net.Http.Headers;
using System.Text;
using Xceed.Words.NET;

namespace VigenereCryptorCourseMVC.Controllers
{
	public class VigenereCryptorController : Controller
	{
        public IActionResult Index(VigenereCryptor model)
        {
            return View("Index", model);
        }

        [HttpPost]
		public IActionResult UseCryptor(VigenereCryptor model, string dlink)
		{
            model.DownloadLink = dlink; // HiddenFor не работал, пришлось изголяться
            try
            {
				model.EditedText = Crypt(model.Text, model.Key, model.Mode);
                if (model.DownloadLink != null) // если было обращение к файлу
                {
                    string[] temp = model.DownloadLink.Split('.');
                    string extension = temp[temp.Length - 1];
                    bool ifDocx = extension.ToLower() == "docx" ? true : false;

                    if (ifDocx)
                    {
                        DocX docx = DocX.Create(model.DownloadLink);
                        docx.InsertParagraph().InsertText(model.EditedText);
                        docx.Save();
                    }
                    else
                    {
                        System.IO.File.WriteAllText(model.DownloadLink, model.EditedText);
                    }
                    // сразу пишем в файл, который клиент возможно захочет скачать
                }
            }
			catch (FormatException ex)
            {
                model.EditedText = ex.Message;
            }
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult Upload(VigenereCryptor model)
        {
            if (model.File == null)
            {
                return View("Index", model);
            }

            var parsedText = new StringBuilder(); // получаем текст
            using (var reader = new StreamReader(model.File.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    parsedText.AppendLine(reader.ReadLine());
                }   
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var stream = new FileStream(Path.Combine(path, model.File.FileName), FileMode.Create)) // получаем файл
            {
                model.File.CopyTo(stream);
            }

            string[] temp = model.File.FileName.Split('.');
            string extension = temp[temp.Length - 1];
            bool ifDocx = (extension.ToLower() == "docx" ? true : false);

            string result = "";
            if (ifDocx) 
            {
                var memst = new MemoryStream();
                model.File.CopyTo(memst);
                DocX docx = DocX.Load(Path.Combine(path, model.File.FileName));
                foreach (var paragraph in docx.Paragraphs)
                {
                    result += paragraph.Text;
                }
            }
            else
            {
                result = parsedText.ToString();
            }

            model.DownloadLink = Path.Combine(path, model.File.FileName);
            model.Text = result.ToString();
            model.EditedText = Crypt(model.Text, model.Key, model.Mode);
            return View("Index", model);
        }
        public static string Crypt(string text, string key, bool mode)
        {
            string result = "";

            int keySymbolIndex = 0; // номер символа в ключе, который используется для шифрования текущего символа текста
            for (int i = 0; i < text.Length; i++)
            {
                int keyIndex = VigenereCryptor.alphabet.IndexOf(key[keySymbolIndex]); // сдвиг по символу ключа
                if (keyIndex == -1)
                {
                    throw new FormatException("В ключе обнаружены символы, не входящие в алфавит, на котором производится шифрование.");
                }
                int textIndex = VigenereCryptor.alphabet.IndexOf(text[i]); // сдвиг по символу текста
                if (textIndex == -1) // если символа в алфавите нет, игнорируем его
                {
                    result += text[i];
                    continue;
                }

                int encryptedIndex; // вычисляем индекс символа алфавита, который пойдёт в результат
                if (mode == true)
                {
                    encryptedIndex = (textIndex + keyIndex) % VigenereCryptor.alphabet.Length;
                }
                else
                {
                    encryptedIndex = (textIndex - keyIndex + VigenereCryptor.alphabet.Length) % VigenereCryptor.alphabet.Length; // плюсану ещё длину алфавита, чтобы в минус не улетало
                }

                result += VigenereCryptor.alphabet[encryptedIndex];

                keySymbolIndex++; // обновляем сдвиг по ключу
                if (keySymbolIndex == key.Length)
                {
                    keySymbolIndex -= key.Length;
                }
            }
            return result;
        }
    }
}
