using NUnit.Framework;

namespace C3
{
    public class StackImpl<T>
    {
        private T[] data;

        public StackImpl()
        {
            Pointer = 0;
            data = new T[10];
        }

        public int Pointer { get; private set; }

        public int StackSize => data.Length;

        public void Push(T newData)
        {
            if (Pointer >= data.Length)
            {
                var temp = new T[data.Length * 2];
                data.CopyTo(temp, 0);
                data = temp;
            }

            data[Pointer] = newData;
            Pointer++;
        }

        public T Pop()
        {
            Pointer--;
            return data[Pointer];
        }

        public bool IsEmpty()
        {
            return Pointer == 0;
        }
    }

    [TestFixture]
    public class StackImplTest
    {
        [Test]
        public void StackImpl_PushMoreThanOriginalSize_Test()
        {
            var stack = new StackImpl<int>();
            for (var i = 0; i < 100; i++) stack.Push(i);
            Assert.That(stack.StackSize, Is.EqualTo(160));
            Assert.That(stack.Pointer, Is.EqualTo(100));
        }

        [Test]
        public void StackImpl_PopAll_Test()
        {
            var stack = new StackImpl<int>();
            for (var i = 0; i < 100; i++) stack.Push(i);
            Assert.That(stack.StackSize, Is.EqualTo(160));

            for (var i = 99; i >= 0; i--) Assert.That(i, Is.EqualTo(stack.Pop()));

            Assert.That(stack.Pointer, Is.EqualTo(0));
        }
    }
}