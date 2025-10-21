using Microsoft.Extensions.Hosting;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using System.Threading.Tasks;
using System.IO; 


namespace ShopTARge24.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly ShopTARge24Context _context;

        public FileServices
            (
                IHostEnvironment webHost,
                ShopTARge24Context context
            )
        {
            _webHost = webHost;
            _context = context;
        }

        public async Task FilesToApi(SpaceshipDto dto, Spaceships domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (!Directory.Exists(_webHost.ContentRootPath + "\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\multipleFileUpload\\");
                }

                foreach (var file in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                        FileToApi path = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            ExistingFilePath = uniqueFileName,
                            SpaceshipId = domain.Id
                        };

                        _context.FileToApis.AddAsync(path);
                    }
                }
            }
        }

        public async Task FilesToApi(KindergartenDto dto, Kindergartens domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (!Directory.Exists(_webHost.ContentRootPath + "\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\multipleFileUpload\\");
                }

                foreach (var file in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                        FileToApi path = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            ExistingFilePath = uniqueFileName,
                            KindergartenId = domain.Id
                        };

                        _context.FileToApis.AddAsync(path);
                    }
                }
            }
        }

        public async Task UploadFilesToDatabase(RealEstateDto dto, RealEstate domain)
        {
            if (dto.Files == null || dto.Files.Count == 0) return;

            foreach (var file in dto.Files)
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);

                _context.FileToDatabases.Add(new FileToDatabase
                {
                    Id = Guid.NewGuid(),
                    ImageTitle = file.FileName,
                    ImageData = ms.ToArray(),
                    RealEstateId = domain.Id
                });
            }

            _context.SaveChanges();
        }

        public async Task UploadFilesToDatabase(KindergartenDto dto, Kindergartens domain)
        {
            if (dto.Files == null || dto.Files.Count == 0) return;

            foreach (var file in dto.Files)
            {
                await using var ms = new MemoryStream();
                await file.CopyToAsync(ms);

                await _context.FileToDatabases.AddAsync(new FileToDatabase
                {
                    Id = Guid.NewGuid(),
                    ImageTitle = file.FileName,
                    ImageData = ms.ToArray(),
                    KindergartenId = domain.Id          // ⬅️ link to kindergarten
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
