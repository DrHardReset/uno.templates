//-:cnd:noEmit
namespace MyExtensionsApp._1.Presentation;

public partial class LoginViewModel : ObservableObject
{
	private IAuthenticationService _authentication;

	private INavigator _navigator;

 //+:cnd:noEmit
#if useCustomAuthentication
	[ObservableProperty]
	private string? _username;

	[ObservableProperty]
	private string? _password;
#endif

	public LoginViewModel(
		INavigator navigator,
		IAuthenticationService authentication)
	{
		_navigator = navigator;
		_authentication = authentication;
		Login = new AsyncRelayCommand(DoLogin);
	}

	private async Task DoLogin()
	{
#if useCustomAuthentication
        var success = await _authentication.LoginAsync(new Dictionary<string, string> { { nameof(Username), Username ?? string.Empty }, { nameof(Password), Password ?? string.Empty } });
#else
        var success = await _authentication.LoginAsync();
#endif
//-:cnd:noEmit
        if (success)
        {
            await _navigator.NavigateViewModelAsync<MainViewModel>(this, qualifier: Qualifiers.ClearBackStack);
        }
	}

	public string Title { get; } = "Login";

	public ICommand Login { get; }
}
