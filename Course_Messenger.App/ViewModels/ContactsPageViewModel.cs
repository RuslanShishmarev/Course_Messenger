using Course_Messenger.App.Models;
using Course_Messenger.App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    private UserRequestService _userRequestService;

    public ContactsPageViewModel()
    {
        _userRequestService = new UserRequestService();
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
}
