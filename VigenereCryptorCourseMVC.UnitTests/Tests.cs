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
            string text = "����� ��� ���� ������ ����������� �����, �� ����� ���";
            string key = "���������������������������������������������������";
            bool mode = true;
            string expected = "����� ��� ���� ������ ����������� �����, �� ����� ���";

            // Act
            string result = Controllers.VigenereCryptorController.Crypt(text, key, mode);

            // Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Crypt_TextIsPredefined_ReturnsCorrectDecryptedString()
        {
            // arrange
            string text = "����� ��� ���� ������ ����������� �����, �� ����� ���";
            string key = "���������������������������������������������������";
            bool mode = false;
            string expectedResult = "����� ��� ���� ������ ����������� �����, �� ����� ���";

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
            string text = "����� ��� ���� ������ ����������� �����, �� ����� ���";
            string key = "��������� ������ ����� ������� �������� ���� ���� ���� ����"; // �������� � �������� ��� - ������ ����������

            // act
            string result = Controllers.VigenereCryptorController.Crypt(text, key, true);

            // assert
            // ��� ����������!!!
        }
        // ��� ��������� �������� ������ ����������� - ��� ������� ������, ������ ��� ��� ���������� �� ���������� ��������, ������ ���
    }
}