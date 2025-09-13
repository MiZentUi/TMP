namespace LR1_2.Collections
{
	internal class Node<T>
	{
		public T? _item;
		public Node<T>? _next;

		public Node(T item)
		{
			_item = item;
			_next = null;
		}

		public Node(Node<T>? next)
		{
			_item = default;
			_next = next;
		}

		public Node(T? item, Node<T>? next)
		{
			_item = item;
			_next = next;
		}
	};
}
