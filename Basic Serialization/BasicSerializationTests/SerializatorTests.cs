using BasicSerialization.Models;

namespace BasicSerializationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BasicSerialization;
    using NUnit.Framework;
    using System.IO;

    [TestFixture]
    public class SerializatorTests
    {
        private string _path;

        [SetUp]
        public void Init()
        {
            var dir = Path.GetDirectoryName(typeof(Serializator).Assembly.Location);
            Environment.CurrentDirectory = dir ?? throw new InvalidOperationException("Directory can't be null");
            _path = $"{Directory.GetCurrentDirectory()}\\";
        }

        [Test]
        [Category("Integrations")]
        public void ReadTest()
        {
            var serializator = new Serializator();
            var result = serializator.ReadXml($"{_path}books.xml", "http://library.by/catalog");

            Assert.IsTrue(result.Books.Count == 12);
            Assert.IsTrue(result.Books[11].Id == "bk112");
        }

        [Test]
        [Category("Integrations")]
        public void WriteTest()
        {
            var serializator = new Serializator();
            var catalog = new Catalog()
            {
                Date = DateTime.Now,
                Books = new List<Book>
                {
                    new Book
                    {
                        Author = "Mikhail",
                        Description = "test",
                        Genre = "test",
                        Id = "test",
                        Isbn = "test",
                        PublishDate = DateTime.Now,
                        Publisher = "test",
                        RegistrationDate = DateTime.Now,
                        Title = "test"
                    }
                }
            };
            serializator.WriteXml(catalog, $"{_path}books2.xml", "http://library.by/catalog");

            
            Assert.IsTrue(File.Exists($"{_path}books2.xml"));
            Assert.IsTrue(File.ReadAllLines($"{_path}books2.xml").Length > 0);
        }
    }
}
