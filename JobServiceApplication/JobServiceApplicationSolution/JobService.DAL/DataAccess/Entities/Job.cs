using DataAccess.Entities.Common;

namespace DataAccess.Entities
{
    public class Job:BaseEntity
    {
        /// <summary>
        /// Pozisyon
        /// </summary>
        public required string Role { get; set; }

        /// <summary>
        /// İlan Açıklaması
        /// </summary>
        public required string JobDescription { get; set; }
        /// <summary>
        /// İlanın Oluşturulma Tarihi
        /// </summary>
        public required DateTime CreatedDate { get; set; }

        /// <summary>
        /// İlan Kalitesi Mask 5 puan Olabilir.
        /// </summary>
        public int JobQuality { get; set; }

        /// <summary>
        /// Yan Haklar
        /// </summary>
        public string? Benefits { get; set; }

        /// <summary>
        /// Çalışma Türü / Tam Zamanlı-Part Time
        /// </summary>
        public string? WorkType { get; set; }

        /// <summary>
        /// Ücret,Maaş
        /// </summary>
        public int Salary { get; set; }
        
        /// <summary>
        /// Bu İlanı Oluşturan Firmanın Id'si
        /// </summary>
        public Guid CompanyId { get; set; }
    }
}
