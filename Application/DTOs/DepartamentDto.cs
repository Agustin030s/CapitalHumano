namespace Application.DTOs
{
    public class DepartamentDto
    {
        public int Id { get; set; }
        public string DepartamentCode { get; set; }
        public string Description { get; set; }
        public ICollection<PositionDto> Positions { get; set; }
    }
}
