using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge24.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {

        private readonly ShopTARge24Context _context;
        private readonly IFileServices _fileServices;

        public KindergartenServices
            (
                ShopTARge24Context context,
                IFileServices fileServices
            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<Kindergartens> Create(KindergartenDto dto)
        {
            Kindergartens kindergartens = new Kindergartens();

            kindergartens.Id = Guid.NewGuid();
            kindergartens.GroupName = dto.GroupName;
            kindergartens.ChildrenCount = dto.ChildrenCount;
            kindergartens.KindergartenName = dto.KindergartenName;
            kindergartens.TeacherName = dto.TeacherName;
            kindergartens.CreatedAt = DateTime.Now;
            kindergartens.UpdatedAt = DateTime.Now;
            await _fileServices.FilesToApi(dto, kindergartens);
            await _context.Kindergartens.AddAsync(kindergartens);
            await _context.SaveChangesAsync();

            if (dto.Files is { Count: > 0 })
            await _fileServices.UploadFilesToDatabase(dto, kindergartens);

            return kindergartens;
        }

        public async Task<Kindergartens> Update(KindergartenDto dto)
        {
            //vaja leida doamini objekt, mida saaks mappida dto-ga
            Kindergartens kindergartens = new Kindergartens();

            kindergartens.Id = dto.Id;
            kindergartens.GroupName = dto.GroupName;
            kindergartens.ChildrenCount = dto.ChildrenCount;
            kindergartens.KindergartenName = dto.KindergartenName;
            kindergartens.TeacherName = dto.TeacherName;
            kindergartens.CreatedAt = dto.CreatedAt;
            kindergartens.UpdatedAt = DateTime.Now;

            //tuleb db-s teha andmete uuendamine jauue oleku salvestamine
            _context.Kindergartens.Update(kindergartens);
            await _context.SaveChangesAsync();

            if (dto.Files is { Count: > 0 })
            await _fileServices.UploadFilesToDatabase(dto, kindergartens);

            return kindergartens;
        }

        public async Task<Kindergartens> DetailAsync(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Kindergartens> Delete(Guid id)
        {
            //leida ülesse konkreetne soovitud rida, mida soovite kustutada
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);


            //kui rida on leitud, siis eemaldage andmebaasist
            _context.Kindergartens.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
