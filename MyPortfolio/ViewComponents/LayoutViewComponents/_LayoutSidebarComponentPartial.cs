using Microsoft.AspNetCore.Mvc;

namespace MyPortolio.ViewComponents.LayoutViewComponent
{
	public class _LayoutSidebarComponentPartial:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
