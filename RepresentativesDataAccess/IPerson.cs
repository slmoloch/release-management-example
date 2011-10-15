namespace RepresentativesDataAccess
{
    public interface IPerson
    {
        int Id { get; set; }

        int RegionId { get; set; }

        string Name { get; set; }

        int? Actual { get; set; }

        int? Estimated { get; set; }

        string Email { get; set; }
    }
}