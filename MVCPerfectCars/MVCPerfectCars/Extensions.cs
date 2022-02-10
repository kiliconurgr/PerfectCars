

using X.PagedList.Web.Common;

namespace MVCPerfectCars
{
    public static class Extensions
    { 

        public static PagedListRenderOptions PagedListRenderOptions { get
            {
                return new PagedListRenderOptions { 
                
                    ActiveLiElementClass = "active",
                    UlElementClasses = new [] {"pagination","pagination-sm"},
                    LiElementClasses = new[] { "page-item" },
                    PageClasses= new[] { "page-link" },
                    ContainerDivClasses = new[] { "d-flex","justify-content-center","p-2" },
                };
            } }
    }
}
