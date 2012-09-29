using System;
using System.Collections;
using System.Linq;
using Code.ReleaseServices.Core.Configuration;

namespace Code.Services
{
    public partial class Configuration : System.Web.UI.Page
    {
        public IJiraConfiguration JiraConfiguration { get; set; }
        
        public IFeedsConfiguration FeedsConfiguration { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var settings = new ArrayList {new {Key = "JIRA HOST", Value = JiraConfiguration.JiraHost}};
            settings.AddRange(
                FeedsConfiguration.Feeds.Select(
                    config => new {Key = config.Id.ToUpper() + " PRIVATE PATH", Value = config.PrivatePath}).OrderBy(item => item.Key).ToList());
            settings.AddRange(
                FeedsConfiguration.Feeds.Select(
                    config => new { Key = config.Id.ToUpper() + " PUBLIC PATH", Value = config.PublicPath }).OrderBy(item => item.Key).ToList());
            rptSettings.DataSource = settings;
            rptSettings.DataBind();
        }
    }
}