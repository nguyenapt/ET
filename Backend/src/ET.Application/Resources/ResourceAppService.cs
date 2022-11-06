using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using ET.Resources.Dto;
using ET.Entities;
using Microsoft.EntityFrameworkCore;
using ET.Authorization.Users;
using Task = System.Threading.Tasks.Task;

namespace ET.Resources
{
    public class ResourceAppService : AsyncCrudAppService<Resource, ResourceDto, Guid, ResourceResultRequestDto, CreateResourceDto, ResourceDto>, IResourceAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<User, long> _userRepository;
        public ResourceAppService(IRepository<Resource, Guid> repository, UserManager userManager, IRepository<User, long> userRepository) : base(repository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public async Task<Resource> GetResourceByUserIdAsync(long userId)
        {
            return await Repository.GetAll().FirstOrDefaultAsync(x => x.UserId == userId);
        }
        public async Task<ResourceDto> GetResourceForEditAsync(long userId)
        {
            var resource = await Repository.GetAll()
                               .Include(x=> x.ResourceSkills).ThenInclude(x=>x.Skill)
                               .Include(x=> x.ResourceSkills).ThenInclude(x=>x.SkillLevel)
                               .FirstOrDefaultAsync(x => x.UserId == userId) ??
                           await CreateResourceFromUser(userId);
            return ObjectMapper.Map<ResourceDto>(resource);
        }
        public async Task<object> CreateResourcesFromUsers()
        {
            var users = await _userRepository.GetAllListAsync();
            if(users !=null && users.Any())
            {
                foreach(var user in users)
                {
                    var exist = Repository.GetAllList().FirstOrDefault(x => x.UserId == user.Id);
                    if (exist == null) {
                        var entity = new Resource()
                        {
                            UserId = user.Id,
                            FirstName = user.Name,
                            LastName = user.Surname,
                            StartDate = DateTime.Now
                        };
                        await Repository.InsertAsync(entity);
                    }
                }
                return Task.CompletedTask;
            }
            return Task.FromException(new Exception("Can not find any user"));
        }
        private async Task<Resource> CreateResourceFromUser(long userId)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                var entity = new Resource()
                {
                    UserId = user.Id,
                    FirstName = user.Name,
                    LastName = user.Surname
                };
                return await Repository.InsertAsync(entity);
            }
            return null;
        }
    }
}

