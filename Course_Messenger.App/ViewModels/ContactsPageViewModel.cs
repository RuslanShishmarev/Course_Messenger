using CommunityToolkit.Mvvm.Input;

using Course_Messenger.App.Models;
using Course_Messenger.App.Services;
using Course_Messenger.App.Views;

using System.Collections.ObjectModel;

namespace Course_Messenger.App.ViewModels;

internal class ContactsPageViewModel : BindableObject
{
    private string? _namePattern = null;
    public string? NamePattern
    {
        get => _namePattern;
        set
        {
            _namePattern = value;
            OnPropertyChanged(nameof(NamePattern));
            FillDataUsers();
        }
    }

    public ObservableCollection<UserShort> Users { get; set; } = new ObservableCollection<UserShort>();

    public RelayCommand<UserShort> OpenChatCommand { get; }

    private UserRequestService _userRequestService;

    public ContactsPageViewModel()
    {
        _userRequestService = new UserRequestService();

        OpenChatCommand = new RelayCommand<UserShort>(OpenChat);
        FillDataUsers();
    }

    private async void FillDataUsers()
    {
        var result = await _userRequestService.GetAll(App.Token, NamePattern);
        Users.Clear();

        if (result is null) return;
        foreach (var user in result)
        {
            Users.Add(user);
        }
    }

    private async void OpenChat(UserShort? selectedContact)
    {
        await App.Current.MainPage.Navigation.PushModalAsync(new ChatUserPage
        {
            BindingContext = new ChatUserPageViewModel(selectedContact)
        });
    }
}
