using CutItUp.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace Intranet.Views.ViewCoponents
{
    public class GPTMessageViewComponent: ViewComponent
    {
        private readonly CutItUpContext _db;
        public GPTMessageViewComponent(CutItUpContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var messages = _db.GPTMessage.ToList();
            return View("Default", messages);
        }
    }
}
