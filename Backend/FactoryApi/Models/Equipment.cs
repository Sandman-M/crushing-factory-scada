namespace FactoryApi.Models
{
    public class Equipment
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = "OFF";
        public int? Temperature { get; set; }
        public int? Load { get; set; }
        public int? Level { get; set; }
    }
}
