using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge24.Core.Domain
{
    public class RealEstate
    {
        public Guid? Id { get; set; }
        public string? Location { get; set; } = default!;
        public decimal? Area { get; set; }         // or int if you prefer
        public int? RoomNumber { get; set; }
        public string? BuildingType { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        // If you store files for real estate:
        public ICollection<FileToDatabase>? Files { get; set; }
    }
}
