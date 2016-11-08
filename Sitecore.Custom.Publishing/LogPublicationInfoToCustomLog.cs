using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Publishing.Diagnostics;
using Sitecore.Publishing.Pipelines.PublishItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Custom.Publishing
{
    public class LogPublicationInfoToCustomLog : PublishItemProcessor
    {
        public override void Process(PublishItemContext context)
        {
            LogToCustomLog(context);
        }

        private void LogToCustomLog(PublishItemContext context)
        {
            try
            {
                Assert.ArgumentNotNull(context, "context");
                Assert.ArgumentNotNull(context.PublishOptions, "context.PublishOptions");
                Assert.ArgumentNotNull(context.PublishOptions.SourceDatabase, "context.PublishOptions.SourceDatabase");
                Assert.ArgumentNotNull(context.PublishOptions.TargetDatabase, "context.PublishOptions.TargetDatabase");
                Assert.ArgumentCondition(!ID.IsNullOrEmpty(context.ItemId), "context.ItemId", "context.ItemId must be set!");
                Assert.ArgumentNotNull(context.User, "context.User");

                Database sourceDatabase = context.PublishOptions.SourceDatabase;
                Database targetDatabase = context.PublishOptions.TargetDatabase;
                ID itemId = context.ItemId;
                string userName = context.User.Name;
                Item item = context.PublishHelper.GetItemToPublish(context.ItemId);
                
                DateTime updatedDate = item.Statistics.Updated;
                var updated = updatedDate != null ? updatedDate.ToString("MMMM dd yyyy H:mm:ss tt") : String.Empty;
                PublishingLog.Info("Publishing Path :" + item.Paths.FullPath.ToString() + ", ID: " + item.ID.ToString() + ", UpdatedDate: " + updated + " , Item Language Verion: " + item.Language.Name + ", By User : " + userName);
            }
            catch (Exception ex)
            {
                PublishingLog.Error(string.Format("### Error : {0}", ex.Message), ex);
            }

        }

    }
}
