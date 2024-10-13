using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using Marks.Module.Wiki.Repository;

namespace Marks.Module.Wiki.Services
{
    public class ServerWikiService : IWikiService
    {
        private readonly IWikiRepository _WikiRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerWikiService(IWikiRepository WikiRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _WikiRepository = WikiRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.Wiki>> GetWikisAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_WikiRepository.GetWikis(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Wiki Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.Wiki> GetWikiAsync(int WikiId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_WikiRepository.GetWiki(WikiId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Wiki Get Attempt {WikiId} {ModuleId}", WikiId, ModuleId);
                return null;
            }
        }

        public Task<Models.Wiki> AddWikiAsync(Models.Wiki Wiki)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, Wiki.ModuleId, PermissionNames.Edit))
            {
                Wiki = _WikiRepository.AddWiki(Wiki);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Wiki Added {Wiki}", Wiki);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Wiki Add Attempt {Wiki}", Wiki);
                Wiki = null;
            }
            return Task.FromResult(Wiki);
        }

        public Task<Models.Wiki> UpdateWikiAsync(Models.Wiki Wiki)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, Wiki.ModuleId, PermissionNames.Edit))
            {
                Wiki = _WikiRepository.UpdateWiki(Wiki);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Wiki Updated {Wiki}", Wiki);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Wiki Update Attempt {Wiki}", Wiki);
                Wiki = null;
            }
            return Task.FromResult(Wiki);
        }

        public Task DeleteWikiAsync(int WikiId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _WikiRepository.DeleteWiki(WikiId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Wiki Deleted {WikiId}", WikiId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Wiki Delete Attempt {WikiId} {ModuleId}", WikiId, ModuleId);
            }
            return Task.CompletedTask;
        }
    }
}
