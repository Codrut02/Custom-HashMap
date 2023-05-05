using CustomHashTable;
namespace CustomHashTableTests
{
    [TestClass]
    public class MyHashTableTests
    {
        [TestMethod]
        public void TestGet()
        {
            // Arrange
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>(5);
            myHashTable.Put("key1", 1);
            myHashTable.Put("key2", 2);
            myHashTable.Put("key3", 3);
            MyHashTable<string, int> emptyHashTable = new MyHashTable<string, int>(5);

            // Act
            int value1 = myHashTable.Get("key1");
            int value2 = myHashTable.Get("key2");
            int value3 = myHashTable.Get("key3");

            // Assert
            Assert.AreEqual(1, value1);
            Assert.AreEqual(2, value2);
            Assert.AreEqual(3, value3);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                myHashTable.Get(null);
            });
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                myHashTable.Get("nonExistentKey");
            });
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                emptyHashTable.Get("key1");
            });
        }

        [TestMethod]
        public void TestPut()
        {
            // Arrange
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>(5);

            // Act
            myHashTable.Put("key1", 1);
            myHashTable.Put("key2", 2);
            myHashTable.Put("key3", 3);
            myHashTable.Put("key3", 10);

            // Assert
            Assert.AreEqual(3, myHashTable.Count());
            Assert.AreEqual(1, myHashTable.Get("key1"));
            Assert.AreEqual(2, myHashTable.Get("key2"));
            Assert.AreEqual(10, myHashTable.Get("key3"));
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                myHashTable.Put(null, 1);
            });
        }

        [TestMethod]
        public void TestRemove()
        {
            // Arrange
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>(5);
            myHashTable.Put("key1", 1);
            myHashTable.Put("key2", 2);
            myHashTable.Put("key3", 3);
            MyHashTable<string, int> emptyHashTable = new MyHashTable<string, int>(5);

            // Act
            myHashTable.Remove("key2");
            myHashTable.Remove("key2");
            myHashTable.Remove("key7");
            myHashTable.Remove(null);
            myHashTable.Remove("");
            emptyHashTable.Remove("key");

            // Assert
            Assert.AreEqual(2, myHashTable.Count());
            Assert.IsFalse(myHashTable.ContainsKey("key2"));
            Assert.AreEqual(0, emptyHashTable.Count());
        }

        [TestMethod]
        public void TestContainsKey()
        {
            // Arrange
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>(5);
            myHashTable.Put("key1", 1);
            myHashTable.Put("key2", 2);
            myHashTable.Put("key3", 3);
            MyHashTable<string, int> emptyHashTable = new MyHashTable<string, int>(5);

            // Act
            bool containsKey1 = myHashTable.ContainsKey("key1");
            bool containsKey4 = myHashTable.ContainsKey("key4");
            bool containsNull = myHashTable.ContainsKey(null);
            bool containsKey2 = emptyHashTable.ContainsKey("key2");

            // Assert
            Assert.IsTrue(containsKey1);
            Assert.IsFalse(containsKey4);
            Assert.IsFalse(containsNull);
            Assert.IsFalse(containsKey2);
        }

        [TestMethod]
        public void TestCount()
        {
            // Arrange
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>(5);
            myHashTable.Put("key1", 1);
            myHashTable.Put("key2", 2);
            myHashTable.Put("key3", 3);
            MyHashTable<string, int> emptyHashTable = new MyHashTable<string, int>(5);

            // Act
            int count = myHashTable.Count();
            int countEmpty = emptyHashTable.Count();

            // Assert
            Assert.AreEqual(3, count);
            Assert.AreEqual(0, countEmpty);
        }
    }

}