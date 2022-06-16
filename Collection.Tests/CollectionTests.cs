using Collections;
using NUnit.Framework;
using System;
using System.Linq;

namespace Collection.Tests
{
    public class CollectionTests
    {
        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            // Arrange
            var numbers = new Collection<int>();

            // Act

            // Assert
            Assert.AreEqual(0, numbers.Count);
            Assert.AreEqual(numbers.ToString(), ("[]"));
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            // Arrange
            var numbers = new Collection<int>(12);

            // Act

            // Assert
            Assert.AreEqual(numbers.ToString(), ("[12]"));
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            // Arrange
            var numbers = new Collection<int>(12, 33, 16, 21);

            // Act

            // Assert
            Assert.AreEqual(numbers.ToString(), ("[12, 33, 16, 21]"));
        }

        [Test]
        public void Test_Collections_AddMethod()
        {
            // Arrange
            var numbers = new Collection<int>();

            // Act
            numbers.Clear();
            numbers.Add(15);

            // Assert
            Assert.AreEqual(1, numbers.Count);
            Assert.That(numbers.ToString(), Is.EqualTo("[15]"));
        }

        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            var nums = new Collection<int>();

            int oldCapacity = nums.Capacity;

            var newNums = Enumerable.Range(1000, 2000).ToArray();

            nums.AddRange(newNums);

            string expectedNums = "[" + string.Join(", ", newNums) + "]";

            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));

            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));

            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            // Arrange
            var numbers = new Collection<int>();

            // Act
            numbers.AddRange(22, 100, 7, 1);

            // Assert
            Assert.That(numbers[0], Is.EqualTo(22));
            Assert.That(numbers[1], Is.EqualTo(100));
            Assert.That(numbers[2], Is.EqualTo(7));
            Assert.That(numbers[3], Is.EqualTo(1));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            // Arrange
            var names = new Collection<string>();

            // Act
            names.Add("Maria");
            names.Add("Georgi");

            // Assert
            Assert.That(() => { var name = names[-1]; },

    Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(() => { var name = names[2]; },

              Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(() => { var name = names[100]; },

              Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(names.ToString(), Is.EqualTo("[Maria, Georgi]"));
        }

        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            var names = new Collection<string>("Pesho", "Gosho");

            var nums = new Collection<int>(99, 6);

            var dates = new Collection<DateTime>();

            var nested = new Collection<object>(names, nums, dates);

            string nestedToString = nested.ToString();

            Assert.That(nestedToString,

              Is.EqualTo("[[Pesho, Gosho], [99, 6], []]"));
        }

        [Test]
        public void Test_Collection_1MillionItems()
        {
            var numbers = new Collection<int>();
            const int itemsCount = 1000000;
            var newNumbers = Enumerable.Range(12, itemsCount).ToArray();

            numbers.AddRange(newNumbers);

            Assert.That(itemsCount, Is.EqualTo(numbers.Count));

            for (int i = itemsCount - 1; i >= 0; i--)
            {
                numbers.RemoveAt(i);
            }

            Assert.That(numbers.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_InitialCapacity()
        {
            Collection<int> collection = new Collection<int>();
            int capacity = 16;
            Assert.That(capacity == collection.Capacity);
        }

        [Test]
        public void Test_Collection_WhenAddNumberInTheCollection_NumberExist()
        {
            Collection<int> collection = new Collection<int>(new int[] { 10, 20, 30, 40, 50 });
            collection.Add(60);
            int expectedNumber = 60;
            Assert.That(expectedNumber == collection[5]);

        }

        [Test]
        public void Test_AddRange_AddNewRangeToCollectionWithCountMoreThanInitialCapacity()
        {
            Collection<int> collection = new Collection<int>(new int[] { 10, 20 });

            int[] newArr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            int expectedCollctionCapacity = 32;

            collection.AddRange(newArr);

            Assert.AreEqual(expectedCollctionCapacity, collection.Capacity);

        }

        [Test]
        public void Test_Collection_InsertAtMiddle()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3, 4, 5 });

            int index = 2;
            int item = 100;

            collection.InsertAt(index, item);

            var expectedCollection = new Collection<int>(new int[] { 1, 2, 100, 3, 4, 5 });

            Assert.AreEqual(expectedCollection.ToString(), collection.ToString());
        }

        [Test]
        public void Test_Collection_ExchangeFirstLast()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3, 4, 5 });

            collection.Exchange(0, 4);
            var expectedCollection = new Collection<int>(new int[] { 5, 2, 3, 4, 1 });

            Assert.AreEqual(expectedCollection.ToString(), collection.ToString());
        }

        [Test]
        public void Test_Collection_ExchangeMiddle()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3, 4, 5, 6 });

            collection.Exchange(2, 3);
            var expectedCollection = new Collection<int>(new int[] { 1, 2, 4, 3, 5, 6 });

            Assert.AreEqual(expectedCollection.ToString(), collection.ToString());
        }

        [Test]
        public void Test_Collection_ExchangeInvalidIndexes_NegativeInputIndex()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3, 4, 5 });

            Assert.That(() => collection.Exchange(-2, 2),
                               Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_ExchangeInvalidIndexes_PositiveInputIndex()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3, 4, 5 });

            Assert.That(() => collection.Exchange(10, 2),
                               Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_ExchangeInvalidIndexes_NegativeOutputIndex()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3, 4, 5 });

            Assert.That(() => collection.Exchange(2, -2),
                               Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_ExchangeInvalidIndexes_PositiveOutputIndex()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3, 4, 5 });

            Assert.That(() => collection.Exchange(2, 20),
                               Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_EnsureCapacity_WhenCountAndCapacityAreEqual()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            collection.Add(12);

            Assert.That(collection.Capacity == 32);
        }

        [Test]
        public void Test_RemoveAt_WhenIndexIsValid()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3 });

            collection.RemoveAt(0);

            Assert.That(collection.Count == 2);
        }

        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3 });
            var expectedCollection = new Collection<int>(new int[] { 2, 3 });
            collection.RemoveAt(0);

            Assert.AreEqual(expectedCollection.ToString(), collection.ToString());
        }

        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3 });
            var expectedCollection = new Collection<int>(new int[] { 1, 2 });
            collection.RemoveAt(2);

            Assert.AreEqual(expectedCollection.ToString(), collection.ToString());
        }

        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3, 4, 5 });
            var expectedCollection = new Collection<int>(new int[] { 1, 2, 4, 5 });
            collection.RemoveAt(2);

            Assert.AreEqual(expectedCollection.ToString(), collection.ToString());
        }

        [Test]
        public void Test_Collection_RemoveAtAll()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3, 4, 5 });

            while (collection.Count > 0)
            {
                collection.RemoveAt(0);
            }

            Assert.AreEqual("[]", collection.ToString());
        }

        [Test]
        public void Test_Collection_RemoveAtInvalidIndex_NegativeIndex()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3 });

            Assert.That(() => collection.RemoveAt(-2),
                                           Throws.InstanceOf<ArgumentOutOfRangeException>());

        }

        [Test]
        public void Test_Collection_Clear_WhenCollectionIsNotEmpty()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3 });

            collection.Clear();

            Assert.That(collection.Count == 0);
        }

        [Test]
        public void Test_Collection_Clear_WhenCollectionIsNotEmpty_CheckToStingMethod()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3 });
            var expectedCollection = new Collection<int>();

            collection.Clear();

            Assert.That(expectedCollection.ToString(), Is.EqualTo(collection.ToString()));
        }

        [Test]
        public void Test_Collection_ToStringMultiple()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3 });
            var expectedMessage = "[1, 2, 3]";

            var message = collection.ToString();

            Assert.That(expectedMessage, Is.EqualTo(message));
        }
        [Test]
        public void Test_Collection_ToStringSingle()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1 });
            var expectedMessage = "[1]";

            var message = collection.ToString();

            Assert.That(expectedMessage, Is.EqualTo(message));
        }

        [Test]
        public void Test_Collection_ToStringEmpty()
        {
            Collection<int> collection = new Collection<int>();
            var expectedMessage = "[]";

            var message = collection.ToString();

            Assert.That(expectedMessage, Is.EqualTo(message));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            Collection<int> collection = new Collection<int>(1);

            Assert.AreEqual(1, collection[0]);
        }

        [Test]
        public void Test_Collection_SetByInvalidIndex_NegativeIndex()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3 });

            Assert.That(() => collection[-1],
                                           Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_SetByInvalidIndex_PositiveIndex()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3 });

            Assert.That(() => collection[100],
                                           Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_InsertAtStart()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3, 4, 5 });

            int index = 0;
            int item = 100;

            collection.InsertAt(index, item);

            var expectedCollection = new Collection<int>(new int[] { 100, 1, 2, 3, 4, 5 });

            Assert.AreEqual(expectedCollection.ToString(), collection.ToString());
        }

        [Test]
        public void Test_Collection_InsertAtEnd()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3, 4, 5 });

            int index = 5;
            int item = 100;

            collection.InsertAt(index, item);

            var expectedCollection = new Collection<int>(new int[] { 1, 2, 3, 4, 5, 100 });

            Assert.AreEqual(expectedCollection.ToString(), collection.ToString());
        }

        [Test]
        public void Test_Collection_InsertAtInvalidIndex_NegativeIndex()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3 });

            Assert.That(() => collection.InsertAt(-1, 23),
                                           Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_InsertAtInvalidIndex_PositiveIndex()
        {
            Collection<int> collection = new Collection<int>(new int[] { 1, 2, 3 });

            Assert.That(() => collection.InsertAt(100, 6),
                                           Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
    }
}