using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Base64GUI;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnClickedEncode(object sender, EventArgs e)
    {
        try
        {
            string stringToEncode = CheckBoxEncodeReplaceCRLFWithLF.IsChecked 
                ? EditorInput.Text?.ReplaceLineEndings("\n")
                : EditorInput.Text;

            stringToEncode ??= string.Empty;

            EditorOutput.Text = CheckBoxUrlEncoded.IsChecked
                ? Base64UrlEncoder.Encode(stringToEncode)
                : Convert.ToBase64String(Encoding.UTF8.GetBytes(stringToEncode));
        }
        catch (Exception exception)
        {
            EditorOutput.Text = $"Failed to encode input string. Thrown exception: {exception.ToString()}";
        }
    }

    private void OnClickedDecode(object sender, EventArgs e)
    {
        try
        {
            string stringToDecode = EditorInput.Text ?? string.Empty;

            string decodedString = CheckBoxUrlEncoded.IsChecked
                ? Base64UrlEncoder.Decode(stringToDecode)
                : Encoding.UTF8.GetString(Convert.FromBase64String(stringToDecode));

            EditorOutput.Text = decodedString;

            if (CheckBoxDecodeReplaceCRLFWithLF.IsChecked)
            {
                EditorOutput.Text = EditorOutput.Text.Replace("\r\n", "\n").Replace("\r", string.Empty);
            }
        }
        catch (Exception exception)
        {
            EditorOutput.Text = $"Failed to decode input string. Thrown exception: {exception.ToString()}";
        }
    }
}