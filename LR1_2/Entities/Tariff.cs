namespace LR1_2.Entities
{
	internal class Tariff(TariffType _type = TariffType.Standart)
	{
		public TariffType Type { get => _type; }

		public decimal GetMultiplier() => _type switch
		{

			TariffType.Preferential => 0.9M,
			TariffType.Standart => 1.1M,
			TariffType.Special => 1.5M,
			_ => 1M
		};

		public override string? ToString()
		{
			return $"\t{_type}";
		}
	}
}
