using System;
using Microsoft.Win32;
using System.Windows.Forms;


/// <summary>
///  Original from http://www.codeproject.com/Articles/3389/Read-write-and-delete-from-registry-with-C
///  Modified by Robert Brandon to be more malleable.
/// </summary>
public class RegEdit
{
    private bool _ShowError;// = true;
    private string _SubKey;// = @"Software\Kurnai\NetbookHelper";
    private RegistryKey _BaseRegistryKey;// = Registry.CurrentUser;

    public enum Base
    {
        ClassesRoot,
        CurrentConfig,
        CurrentUser,
        LocalMachine,
        Users
    }

    public RegEdit(Base baseRegistryKey, string subKey) : this(baseRegistryKey, subKey, false) { }

    public RegEdit (Base baseRegistryKey, string subKey, bool showError)
    {
        _ShowError = showError;
        _SubKey = subKey;

        switch (baseRegistryKey)
        {
            case Base.ClassesRoot:
                _BaseRegistryKey = Registry.ClassesRoot;
                break;
            case Base.CurrentConfig:
                _BaseRegistryKey = Registry.CurrentConfig;
                break;
            case Base.CurrentUser:
                _BaseRegistryKey = Registry.CurrentUser;
                break;
            case Base.LocalMachine:
                _BaseRegistryKey = Registry.LocalMachine;
                break;
            default:
                _BaseRegistryKey = Registry.Users;
                break;
        }
    }

    public string Read(string KeyName)
    {
        // Opening the registry key
        RegistryKey rk = _BaseRegistryKey;
        // Open a subKey as read-only
        RegistryKey sk1 = rk.OpenSubKey(_SubKey);
        // If the RegistrySubKey doesn't exist -> (null)
        if (sk1 == null)
        {
            return null;
        }
        else
        {
            try
            {
                // If the RegistryKey exists I get its value
                // or null is returned.
                return (string)sk1.GetValue(KeyName);
            }
            catch// (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                //ShowErrorMessage(e, "Reading registry " + KeyName);
                return null;
            }
        }
    }

    public bool Write(string KeyName, object Value)
    {
        try
        {
            // Setting
            RegistryKey rk = _BaseRegistryKey;
            // I have to use CreateSubKey 
            // (create or open it if already exits), 
            // 'cause OpenSubKey open a subKey as read-only
            RegistryKey sk1 = rk.CreateSubKey(_SubKey);
            // Save the value
            sk1.SetValue(KeyName, Value);

            return true;
        }
        catch// (Exception e)
        {
            // AAAAAAAAAAARGH, an error!
            //ShowErrorMessage(e, "Writing registry " + KeyName);
            return false;
        }
    }

    public bool DeleteKey(string KeyName)
    {
        try
        {
            // Setting
            RegistryKey rk = _BaseRegistryKey;
            RegistryKey sk1 = rk.CreateSubKey(_SubKey);
            // If the RegistrySubKey doesn't exists -> (true)
            if (sk1 == null)
                return true;
            else
                sk1.DeleteValue(KeyName);

            return true;
        }
        catch (Exception e)
        {
            // AAAAAAAAAAARGH, an error!
            ShowErrorMessage(e, "Deleting SubKey " + _SubKey);
            return false;
        }
    }

    public bool DeleteSubKeyTree()
    {
        try
        {
            // Setting
            RegistryKey rk = _BaseRegistryKey;
            RegistryKey sk1 = rk.OpenSubKey(_SubKey);
            // If the RegistryKey exists, I delete it
            if (sk1 != null)
                rk.DeleteSubKeyTree(_SubKey);

            return true;
        }
        catch (Exception e)
        {
            // AAAAAAAAAAARGH, an error!
            ShowErrorMessage(e, "Deleting SubKey " + _SubKey);
            return false;
        }
    }

    public int SubKeyCount()
    {
        try
        {
            // Setting
            RegistryKey rk = _BaseRegistryKey;
            RegistryKey sk1 = rk.OpenSubKey(_SubKey);
            // If the RegistryKey exists...
            if (sk1 != null)
                return sk1.SubKeyCount;
            else
                return 0;
        }
        catch (Exception e)
        {
            // AAAAAAAAAAARGH, an error!
            ShowErrorMessage(e, "Retriving subkeys of " + _SubKey);
            return 0;
        }
    }

    public int ValueCount()
    {
        try
        {
            // Setting
            RegistryKey rk = _BaseRegistryKey;
            RegistryKey sk1 = rk.OpenSubKey(_SubKey);
            // If the RegistryKey exists...
            if (sk1 != null)
                return sk1.ValueCount;
            else
                return 0;
        }
        catch (Exception e)
        {
            // AAAAAAAAAAARGH, an error!
            ShowErrorMessage(e, "Retriving keys of " + _SubKey);
            return 0;
        }
    }



    private void ShowErrorMessage(Exception e, string Title)
    {
        if (_ShowError == true)
            MessageBox.Show(e.Message,
                    Title
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
    }
}
    
