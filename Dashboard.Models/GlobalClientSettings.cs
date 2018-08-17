using Dashboard.Common.Configuration;

namespace Dashboard.Models
{
    public class GlobalClientSettings
    {
        private const string DEBT_TICKET_VIEWER_URL_KEY = "DebtTicketViewerUrl";
        private const string CANVAS_HEIGHT_KEY = "CanvasHeight";
        private const string CANVAS_WIDTH_KEY = "CanvasWidth";
        private const string APPLICATION_NAME_KEY = "Application Name";
        private const string ENVIRONMENT = "Environment";

        public string ApplicationName { get; set; }
        public string CanvasWidth { get; set; }
        public string CanvasHeight { get; set; }
        public string DebtTicketViewerUrl{ get; set; }       

        public GlobalClientSettings()
        {
            DebtTicketViewerUrl = GlobalConfigurationSettings.GetSettings(DEBT_TICKET_VIEWER_URL_KEY);
            CanvasHeight = GlobalConfigurationSettings.GetSettings(CANVAS_HEIGHT_KEY);
            CanvasWidth = GlobalConfigurationSettings.GetSettings(CANVAS_WIDTH_KEY);
            ApplicationName = $"{GlobalConfigurationSettings.GetSettings(APPLICATION_NAME_KEY)}-{GlobalConfigurationSettings.GetSettings(ENVIRONMENT)}";
        }
    }
}