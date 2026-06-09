namespace AutoFy.Mobile.ViewModels;

public class AboutViewModel : BaseViewModel
{
    #region Fields

    private string _appVersion = "1.0.0";

    #endregion

    #region Init

    public AboutViewModel()
    {
        Title = "За приложението";
    }

    #endregion

    #region Properties

    public string AppVersion
    {
        get => _appVersion;
        set => SetProperty(ref _appVersion, value);
    }

    #endregion
}