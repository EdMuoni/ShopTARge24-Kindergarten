using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IFileServices
    {
        Task FilesToApi(SpaceshipDto dto, Spaceships domain);
        Task FilesToApi(KindergartenDto dto, Kindergartens domain);
        Task UploadFilesToDatabase(RealEstateDto dto, RealEstate domain);
        Task UploadFilesToDatabase(KindergartenDto dto, Kindergartens domain);
    }
}
