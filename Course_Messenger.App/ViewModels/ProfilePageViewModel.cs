using CommunityToolkit.Mvvm.Input;
using Course_Messenger.App.Services;

namespace Course_Messenger.App.ViewModels;

internal class ProfilePageViewModel : BindableObject
{
    public string? ProfileName => App.CurrentUser?.Name;

	private string _name = string.Empty;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			OnPropertyChanged(nameof(Name));
		}
	}

    private string _login = string.Empty;
    public string Login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged(nameof(Login));
        }
    }

    private byte[]? _photo = null;
    public byte[]? Photo
    {
        get => _photo;
        set
        {
            _photo = value;
            OnPropertyChanged(nameof(Photo));
        }
    }

    public RelayCommand SetNewDataCommand { get; }

    public RelayCommand SetNewPhotoCommand { get; }

    private UserRequestService _userRequestService;

    public ProfilePageViewModel()
    {
        _userRequestService = new UserRequestService();
        Task.Run(() =>
        {
            while (App.CurrentUser is null)
            {
                Task.Delay(100);
            }
            Name = App.CurrentUser.Name;
            Login = App.CurrentUser.Email;
            Photo = App.CurrentUser.Photo;
        });

        SetNewDataCommand = new RelayCommand(SetNewData);
        SetNewPhotoCommand = new RelayCommand(SetNewPhoto);
    }

    private async void SetNewData()
    {
        var newUserData = new Models.User
        {
            Id = App.CurrentUser.Id,
            Name = string.IsNullOrEmpty(this.Name) ? App.CurrentUser.Name : this.Name,
            Email = string.IsNullOrEmpty(this.Login) ? App.CurrentUser.Email : this.Login,
            Photo = this.Photo,
        };

        var newData = await _userRequestService.Update(newUserData, App.Token);

        if (newData is null) return;

        App.CurrentUser = newData;
        Name = App.CurrentUser.Name;
        Login = App.CurrentUser.Email;
        Photo = App.CurrentUser.Photo;

        OnPropertyChanged(nameof(ProfileName));
    }

    private async void SetNewPhoto()
    {
        FileResult? result = await FilePicker.PickAsync(
            new PickOptions
            {
                PickerTitle = "Select Profile Image",
                FileTypes = FilePickerFileType.Images,
            });

        if (result is null)
        {
            return;
        }

        using Stream stream = await result.OpenReadAsync();

        var photoBytes = new byte[16 * 1024];

        using MemoryStream photoStream = new MemoryStream();

        int read;
        while ((read = stream.Read(photoBytes, 0, photoBytes.Length)) > 0)
        {
            photoStream.Write(photoBytes, 0, read);
        }
        var selected = photoStream.ToArray();
        Photo = selected;
    }
}
