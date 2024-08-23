using Application.Data;
using Domain.Permissions;
using Domain.PermissionTypes;
using Domain.Primitives;
using Infraestructure.Persistence.Documents;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IElasticRepository<PermissionDocument> elasticRepository;
        private readonly IUnitOfWork unitOfWork;

        public PermissionRepository(ApplicationDbContext context, IElasticRepository<PermissionDocument> elasticRepository, IUnitOfWork unitOfWork )
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.elasticRepository = elasticRepository ?? throw new ArgumentNullException(nameof(context));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task Add(Permission permission) 
        {
            await this.context.Permissions.AddAsync(permission);

            await unitOfWork.SaveChangesAsync();

            PermissionDocument permissionDocument = new PermissionDocument()
            {
                Id = permission.Id,
                NameEmployee = permission.NameEmployee,
                LastNameEmployee = permission.LastNameEmployee,
                PermissionTypeId = permission.PermissionTypeId,
                Date = permission.Date
            };

            await elasticRepository.Index(new List<PermissionDocument> { permissionDocument });
        }

        public async Task<IEnumerable<Permission>> GetAllAsync() 
        { 
            return await this.context.Permissions.Include(p => p.PermissionType).ToListAsync();
        }

        public async Task<Permission?> GetByIdAsync(long id) 
        {
            if (await elasticRepository.GetById(id.ToString()) is not PermissionDocument permissionType)
                return await this.context.Permissions.Include(p => p.PermissionType).FirstOrDefaultAsync(p => p.Id == id);

            PermissionType pt = await context.PermissionTypes.FindAsync(permissionType.PermissionTypeId);

            Permission permission = new Permission(
                permissionType.Id, permissionType.NameEmployee, 
                permissionType.LastNameEmployee, permissionType.PermissionTypeId, 
                permissionType.Date, pt);


            return permission;
        } 

        public async Task UpdateAsync(long id, string? nameEmployee, string? lastNameEmployee, long? permissionTypeId, DateTime? date)
        {
            Permission permission = await this.context.Permissions.Include(p => p.PermissionType).FirstOrDefaultAsync(p => p.Id == id);
            permission.UpdatePermission(nameEmployee, lastNameEmployee, permissionTypeId, date);

            this.context.Permissions.Update(permission);

            await unitOfWork.SaveChangesAsync();

            PermissionDocument pd = new PermissionDocument()
            {
                Id = permission.Id,
                NameEmployee = permission.NameEmployee,
                LastNameEmployee = permission.LastNameEmployee,
                PermissionTypeId = permission.PermissionTypeId,
                Date = permission.Date
            };

            await elasticRepository.Update(pd, pd.Id.ToString());
        }    
    }
}
