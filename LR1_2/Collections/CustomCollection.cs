using System.Collections;
using LR1_2.Interfaces;

namespace LR1_2.Collections
{
	class CustomCollection<T> : ICustomCollection<T>
	{
		private Node<T>? _head;
		private Node<T>? _cursor;
		private Node<T>? _tail;
		private int _count;

		public CustomCollection()
		{
			_head = null;
			_tail = _head;
			_count = 0;
		}

		public T this[int index] 
		{				
			get 
			{
				if (index < 0 || index >= _count)
					throw new IndexOutOfRangeException($"Index: {index}, Count: {_count}");
				Node<T>? node = _head;
				for (int i = 0; i < index && node != null; i++)
					node = node._next;
				return node!._item!;
			}
			set
			{
				if (index < 0 || index >= _count)
					throw new IndexOutOfRangeException($"Index: {index}, Count: {_count}");
				Node<T>? node = _head;
				for (int i = 0; i < index && node != null; i++)
					node = node._next;
				node!._item = value;
			}
		}

		public int Count { get => _count; }

		public void Add(T item)
		{
			var node = new Node<T>(item);
			if (_head == null)
			{
				_head = node;
				_cursor = _head;
				_tail = _head;
			}
			else if (_tail != null)
			{
				_tail._next = node;
				_tail = node;
			}
			_count++;
		}

		public T Current()
		{
			return _cursor!._item!;
		}

		public void Next()
		{
			if (_cursor != null)
				_cursor = _cursor._next;
		}

		public void Remove(T item)
		{
			Node<T>? _prev = null;
			Node<T>? _node = _head;
			while (_node != null && !_node._item!.Equals(item))
			{
				_prev = _node;
				_node = _node._next;
			}
			if (_node == null)
				throw new ItemNotFoundException($"Item: {item}");
			if (_prev == null)
				_head = _node._next;
			else
				_prev._next = _node._next;
		}

		public T RemoveCurrent()
		{
			Node<T>? _prev = null;
			Node<T> _node = _head!;
			while (_node != null && _node != _cursor)
			{
				_prev = _node;
				_node = _node._next!;
			}
			if (_prev == null)
				_head = _node!._next;
			else
				_prev._next = _node!._next;
			return _node._item!;
		}

		public void Reset()
		{
			_cursor = _head;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new CustomEnum<T>(_head);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
