﻿using ShopTARge24.Core.Dto;

namespace ShopTARge24.Models.Kindergartens
{
    public class KindergartenCreateUpdateViewModel
    {
        public Guid? Id { get; set; }
        public string? GroupName { get; set; }
        public int? ChildrenCount { get; set; }
        public string? KindergartenName { get; set; }
        public string? TeacherName { get; set; }

        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToApiDto> Image { get; set; }
            = new List<FileToApiDto>();

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
