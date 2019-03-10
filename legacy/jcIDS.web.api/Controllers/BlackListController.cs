using System.Collections.Generic;

using jcIDS.library.core.DAL.Objects;
using jcIDS.library.core.Managers;

using Microsoft.AspNetCore.Mvc;

namespace jcIDS.web.api.Controllers
{
    public class BlackListController : BaseController
    {
        private readonly BlackListManager _blackListManager;

        public BlackListController(BlackListManager blackListManager)
        {
            _blackListManager = blackListManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BlackListObject>> Get()
        {
            return _blackListManager.GetAll();
        }
    }
}