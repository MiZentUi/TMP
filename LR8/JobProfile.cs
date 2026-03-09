class JobProfile(int id, string name, int candidatesCount)
{
    public int Id { get => id; }

    public string Name { get => name; }

    public int CandidatesCount { get => candidatesCount; }

    public override string? ToString() =>
            $"JobProfile: {{id: {id}, name: {name}, candidates_count: {candidatesCount}}}";
}
