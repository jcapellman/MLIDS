using System.Collections.Generic;

using jcIDS.library.core.DAL.Objects;
using jcIDS.library.core.Managers;

using Microsoft.AspNetCore.Mvc;

namespace jcIDS.web.api.Controllers
{
    public class DevicesController : BaseController
    {
        private readonly NetworkDeviceManager _deviceManager;

        public DevicesController(NetworkDeviceManager deviceManager)
        {
            _deviceManager = deviceManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<NetworkDeviceObject>> Get()
        {
            return _deviceManager.GetAll();
        }
    }
}