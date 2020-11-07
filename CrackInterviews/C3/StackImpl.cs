using NUnit.Framework;

namespace C3
{
    public class StackImpl<T>
    {
        private int pointer;

        private T[] data;

        public int Pointer => this.pointer;

        public int StackSize => this.data.Length;

        public StackImpl()
        {
            this.pointer = 0;
            this.data = new T[10];
        }

        public void Push(T newData)
        {
            if (pointer >= this.data.Length)
            {
                var temp = new T[this.data.Length * 2];
                this.data.CopyTo(temp, 0);
                this.data = temp;
            }

            this.data[pointer] = newData;
            this.pointer++;
        }

        public T Pop()
        {
            this.pointer--;
            return this.data[pointer];
        }

        public bool IsEmpty()
        {
            return this.pointer == 0;
        }
    }

    [TestFixture]
    public class StackImplTest
    {
        [Test]
        public void StackImpl_PushMoreThanOriginalSize_Test()
        {
            var stack = new StackImpl<int>();
            for (int i = 0; i < 100; i++)
            {
                stack.Push(i);
            }
            Assert.That(stack.StackSize, Is.EqualTo(160));
            Assert.That(stack.Pointer, Is.EqualTo(100));
        }

        [Test]
        public void StackImpl_PopAll_Test()
        {
            var stack = new StackImpl<int>();
            for (int i = 0; i < 100; i++)
            {
                stack.Push(i);
            }
            Assert.That(stack.StackSize, Is.EqualTo(160));

            for (int i = 99; i >= 0; i--)
            {
                Assert.That(i, Is.EqualTo(stack.Pop()));
            }

            Assert.That(stack.Pointer, Is.EqualTo(0));
        }
    }
}