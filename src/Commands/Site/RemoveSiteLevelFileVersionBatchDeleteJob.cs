using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Management.Automation;

using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Sites
{
    [Cmdlet(VerbsCommon.Remove, "SiteLevelFileVersionBatchDeleteJob")]
    public class RemoveSiteLevelFileVersionBatchDeleteJob : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue("It will stop processing further version deletion batches. Are you sure you want to continue?", Resources.Confirm))
            {
                var site = ClientContext.Site;
                site.CancelDeleteFileVersions();
                ClientContext.ExecuteQueryRetry();

                WriteVerbose("Future deletion is successfully stopped.");
            }
            else
            {
                WriteVerbose("Did not receive confirmation to stop deletion. Continuing to delete specified versions.");
            }
        }
    }
}
