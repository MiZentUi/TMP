namespace LR4
{
	class CustomComparer<T> : IComparer<T> where T : INamed
	{
		public int Compare(T? x, T? y) =>
			x != null && y != null ? x.Name.CompareTo(y.Name) : 0;
	}
}
