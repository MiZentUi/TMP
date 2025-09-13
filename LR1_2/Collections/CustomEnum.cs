using System.Collections;

namespace LR1_2.Collections
{
    class CustomEnum<T> : IEnumerator<T>
    {
        private Node<T>? _head;
		private Node<T>? _cursor;

        internal CustomEnum(Node<T>? head)
		{
            _head = head;
			_cursor = new Node<T>(_head);
		}

		public T Current => _cursor!._item!;

		object IEnumerator.Current
		{
			get
			{
				if (_cursor == null)
					throw new InvalidOperationException();
				return _cursor._item!;
			}
		}

		public void Dispose() { }

		public bool MoveNext()
		{
			if (_cursor != null && _cursor!._next != null)
			{
				_cursor = _cursor._next;
				return true;
			}
			return false;
		}

		public void Reset()
		{
			_cursor = _head;
		}
	}
}
