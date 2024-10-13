using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Marks.Module.Wiki.Repository;
using Oqtane.Controllers;
using System.Net;

namespace Marks.Module.Wiki.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class WikiController : ModuleControllerBase
    {
        private readonly IWikiRepository _WikiRepository;

        public WikiController(IWikiRepository WikiRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _WikiRepository = WikiRepository;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.Wiki> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return _WikiRepository.GetWikis(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Wiki Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.Wiki Get(int id)
        {
            Models.Wiki Wiki = _WikiRepository.GetWiki(id);
            if (Wiki != null && IsAuthorizedEntityId(EntityNames.Module, Wiki.ModuleId))
            {
                return Wiki;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Wiki Get Attempt {WikiId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Wiki Post([FromBody] Models.Wiki Wiki)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, Wiki.ModuleId))
            {
                Wiki = _WikiRepository.AddWiki(Wiki);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Wiki Added {Wiki}", Wiki);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Wiki Post Attempt {Wiki}", Wiki);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Wiki = null;
            }
            return Wiki;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Wiki Put(int id, [FromBody] Models.Wiki Wiki)
        {
            if (ModelState.IsValid && Wiki.WikiId == id && IsAuthorizedEntityId(EntityNames.Module, Wiki.ModuleId) && _WikiRepository.GetWiki(Wiki.WikiId, false) != null)
            {
                Wiki = _WikiRepository.UpdateWiki(Wiki);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Wiki Updated {Wiki}", Wiki);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Wiki Put Attempt {Wiki}", Wiki);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Wiki = null;
            }
            return Wiki;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.Wiki Wiki = _WikiRepository.GetWiki(id);
            if (Wiki != null && IsAuthorizedEntityId(EntityNames.Module, Wiki.ModuleId))
            {
                _WikiRepository.DeleteWiki(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Wiki Deleted {WikiId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Wiki Delete Attempt {WikiId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
