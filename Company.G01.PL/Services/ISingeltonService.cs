namespace Company.G01.PL.Services
{
    public interface ISingeltonService
    {
        public Guid Guid { get; set; }

        string GetGuid();
    }
}
