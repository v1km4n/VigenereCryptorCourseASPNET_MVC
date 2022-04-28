using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace VigenereCryptorCourseMVC.UnitTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Crypt_TextIsPredefined_ReturnsCorrectEncryptedString()
        {
            // Arrange
            string text = "съешь ещЄ этих м€гких французских булок, да выпей чаю";
            string key = "рандомныйключикчтобытекстикпомешалотудасюдатудасюда";
            bool mode = true;
            string expected = "вътьк сжб жэфу дзнвыд хлттбеъъхшд ншдоц, тт х€пцз ыар";

            // Act
            string result = Controllers.VigenereCryptorController.Crypt(text, key, mode);

            // Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Crypt_TextIsPredefined_ReturnsCorrectDecryptedString()
        {
            // arrange
            string text = "съешь ещЄ этих м€гких французских булок, да выпей чаю";
            string key = "рандомныйключикчтобытекстикпомешалотудасюдатудасюда";
            bool mode = false;
            string expectedResult = "бъчфн шлк узэч хцшуцж ухнилвхиащж фоуо€, хн очпул уал";

            // act
            string result = Controllers.VigenereCryptorController.Crypt(text, key, mode);

            // assert
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Crypt_KeyContainsCharsNotFromAlphabet_ThrowAnException()
        {
            // arrange
            string text = "съешь ещЄ этих м€гких французских булок, да выпей чаю";
            string key = "рандомный ключик чтобы текстик помешало туда сюда туда сюда"; // пробелов в алфавите нет - кидаем исключение

            // act
            string result = Controllers.VigenereCryptorController.Crypt(text, key, true);

            // assert
            // ждЄм исключени€!!!
        }
        //  ак проверить основные методы контроллера - это хороший вопрос, потому что они возвращают не конкретное значение, скажем так
    }
}