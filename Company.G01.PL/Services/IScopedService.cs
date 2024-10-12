namespace Company.G01.PL.Services
{
    public interface IScopedService
    {
        public Guid Guid { get; set; }

        string GetGuid();
    }
}
