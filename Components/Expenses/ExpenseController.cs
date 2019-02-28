using Microsoft.AspNetCore.Mvc;

namespace Expenses
{
    [Route("expenses"), Produces("application/json")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseDataGateway _gateway;
        private readonly IProjectClient _client;

        public ExpenseController(IExpenseDataGateway gateway, IProjectClient client)
        {
            _gateway = gateway;
            _client = client;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int expenseId)
        {
            var record = _gateway.FindByExpenseId(expenseId);
            var value = new ExpenseInfo(record.Id, record.UserId, record.ProjectId, record.ExpenseType, record.TotalAmountSpent, record.Date);
            return Ok(value);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ExpenseInfo info)
        {
            if (!ProjectIsActive(info.ProjectId)) return new StatusCodeResult(304);

            var record = _gateway.Create(info.UserId, info.ProjectId, info.ExpenseType, info.TotalAmountSpent, info.Date);
            var value = new ExpenseInfo(record.Id, record.UserId, record.ProjectId, record.ExpenseType, record.TotalAmountSpent, record.Date);
            return Created($"expenses/{value.Id}", value);
        }

        private bool ProjectIsActive(long projectId)
        {
            var info = _client.Get(projectId);
            return info.Result?.Active ?? false;
        }
    }
}
