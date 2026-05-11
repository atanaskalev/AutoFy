namespace AutoFy.Mobile.ViewModels;

public class AboutViewModel : BaseViewModel
{
    private string _appVersion = "1.0.0";

    public string AppVersion
    {
        get => _appVersion;
        set => SetProperty(ref _appVersion, value);
    }

    public AboutViewModel()
    {
        Title = "За приложението";
    }
}