using EventShared.Common;
using System.Text.Json.Serialization;

namespace EventShared
{
    public class CompanyCanShareJobEvent : IEvent
    {
        public bool CanShare { get; set; }

        /// <summary>
        /// Pozisyon
        /// </summary>
        public  string Role { get; set; }

        /// <summary>
        /// İlan Açıklaması
        /// </summary>
        public  string JobDescription { get; set; }


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
        /// Firma Id'si
        /// </summary>
        public Guid CompanyId { get; set; }
    }
}
