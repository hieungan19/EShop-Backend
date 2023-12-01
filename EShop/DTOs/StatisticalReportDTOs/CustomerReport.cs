namespace EShop.DTOs.StatisticalReportDTOs
{
    public class CustomerReport
    {
        public int Level0 { get; set;  }
        public int Level1k { get; set; }
        public int Level5k { get; set; }
        public int Level10k { get; set; }
        public int LevelOver10k { get; set; }
        public int Total { get; set; }
    }
}
