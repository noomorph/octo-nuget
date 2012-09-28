using System;
using System.Web;
using System.Web.UI;
using Code.ReleaseServices;
using Code.ReleaseServices.Core.Services;

namespace Code.Services
{
    public partial class Packages : System.Web.UI.Page
    {
        public IReleaseService ReleaseService { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (IsPostBack)
                return;

            rptPackages.DataSource = ReleaseService.List();
            rptPackages.DataBind();
        }
    }
}