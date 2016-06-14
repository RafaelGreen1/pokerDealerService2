using Microsoft.WindowsAzure.Mobile.Service;

namespace pokerDealerService2.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}