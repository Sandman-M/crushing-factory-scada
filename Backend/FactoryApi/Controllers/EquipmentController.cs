using FactoryApi.Models;
using FactoryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FactoryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly EquipmentStateStore _stateStore;

        public EquipmentController(EquipmentStateStore stateStore)
        {
            _stateStore = stateStore;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Equipment>> GetEquipment()
        {
            var data = _stateStore.GetAll();
            return Ok(data);
        }
    }
}
