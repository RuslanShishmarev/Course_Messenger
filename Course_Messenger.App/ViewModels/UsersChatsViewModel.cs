using CommunityToolkit.Mvvm.Input;

using Course_Messenger.App.Models;
using Course_Messenger.App.Views;

using System.Collections.ObjectModel;

namespace Course_Messenger.App.ViewModels;

public class UsersChatsViewModel<T> : BindableObject
{
    private string? _namePattern = null;
    public string? NamePattern
    {
        get => _namePattern;
        set
        {
            _namePattern = value;
            OnPropertyChanged(nameof(NamePattern));
            LoadDataAction?.Invoke();
        }
    }
    public ObservableCollection<T> Elements { get; set; } = new ObservableCollection<T>();

    public RelayCommand<T> OpenChatCommand { get; }

    protected Action LoadDataAction { get; set; }

    public UsersChatsViewModel()
    {
        OpenChatCommand = new RelayCommand<T>(OpenChat);
    }

    private async void OpenChat(T? element)
    {
        if (element is UserShort selectedContact)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new ChatUserPage
            {
                BindingContext = new ChatUserPageViewModel(selectedContact)
            });
        }
        else if (element is Chat chat)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new ChatUserPage
            {
                BindingContext = new ChatUserPageViewModel(chat)
            });
        }
    }
}
