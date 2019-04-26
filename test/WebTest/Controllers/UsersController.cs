using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.AspNetCore.Identity;
using WebTest.Entities;
using WebTest.Models;

namespace WebTest.Controllers {

    [Route("api/[controller]")]
    public class UsersController : Controller {

        private readonly IMapper _mapper;
        private UserManager<ApplicationUser> manager;

        public UsersController(UserManager<ApplicationUser> manager,
            IMapper mapper) {
            this.manager = manager;
            _mapper = mapper;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                manager = null;
            }
        }

        [HttpGet("")]
        public ActionResult<IList<ApplicationUser>> GetAll() {
            var users = manager.Users.ToList();
            return (users);
        }

        [HttpPost]
        public IActionResult Create([FromBody]UserModel userModel) {
            var applicationUser = _mapper.Map<ApplicationUser>(userModel);
            var identityResult = Task.Run(async () => await manager.CreateAsync(applicationUser,
                    userModel.password))
                .Result;
            return identityResult.Succeeded
                ? Ok(Task.Run(async () => await manager.FindByNameAsync(userModel.username))
                    .Result)
                : GetErrorResult(identityResult);
        }

        private IActionResult GetErrorResult(IdentityResult result) {
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            if (result.Succeeded)
                return null;

            if (result.Errors == null)
                return BadRequest();
            var errors = result.Errors.Select(error => error.Description)
                .ToList();
            return BadRequest(errors);
        }
    }
}
