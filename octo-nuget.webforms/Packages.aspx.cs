using System;
using OctoNuget.Core.Services;

namespace OctoNuget.WebForms
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