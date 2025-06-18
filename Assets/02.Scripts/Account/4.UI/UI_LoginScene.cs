using System;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

[Serializable]
public class UI_IputFields
{
    public TextMeshProUGUI ResultText;
    public TMP_InputField IDInputField;
    public TMP_InputField PasswordInputFielid;
    public TMP_InputField PasswordComfirmInputField;
    public TMP_InputField NickNameInputField;
    // 닉네임 생성
    public Button ConfirmButton;

}

public class UI_LoginScene : MonoBehaviour
{
    [Header("패널")]
    public GameObject LoginPanel;
    public GameObject ResisterPanel;

    [Header("로그인")]
    public UI_IputFields LoginInputFields;

    [Header("회원가입")]
    public UI_IputFields RegisterInputFields;

  
    private const string PREFIX = "ID_";

    private void Start()
    {
        LoginPanel.SetActive(true);
        ResisterPanel.SetActive(false);

        LoginInputFields.ResultText.text = string.Empty;
        RegisterInputFields.ResultText.text = string.Empty;

        //LoginCheck();
    }

    public void OnClickGoToResiterButton()
    {
        LoginPanel.SetActive(false);
        ResisterPanel.SetActive(true);
    }

    public void OnClickGoToLoginButton()
    {
        LoginPanel.SetActive(true);
        ResisterPanel.SetActive(false);
    }

    // 회원가입
    public void Resister()
    {
  
        string email = RegisterInputFields.IDInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSpecificationBy(email))
        {
            RegisterInputFields.ResultText.text = "아이디를 입력해주세요.";
            return;
        }

        string password = RegisterInputFields.PasswordInputFielid.text;
        var passwordSpecification = new AccountPasswordSpedcification();
        if (!passwordSpecification.IsSpecificationBy(password))
        {
            RegisterInputFields.ResultText.text = passwordSpecification.ErrorMessage;
        }

        string PasswordComfirm = RegisterInputFields.PasswordComfirmInputField.text;
        var PasswordComfirmSpecification = new AccountPasswordSpedcification();
        if (!PasswordComfirmSpecification.IsSpecificationBy(PasswordComfirm))
        {
            RegisterInputFields.ResultText.text = PasswordComfirmSpecification.ErrorMessage;
        }

        if (password != PasswordComfirm)
        {
            RegisterInputFields.ResultText.text = "비밀번호와 아이디를 확인해주세요.";
            return;
        }

        string nickname = RegisterInputFields.NickNameInputField.text;
        var nickNameSpecification = new AccountNickNameSpedcification();
        if(!nickNameSpecification.IsSpecificationBy(nickname))
        {
            RegisterInputFields.ResultText.text = nickNameSpecification.ErrorMessage;
        }

        Result result = AccountManager.Instance.TryRegister(email, nickname, password);
        if (result.IsSucces)
        {
            // 5. 로그인 창으로 돌아간다. (이때 아이디는 자동 입력되어 있다.)
            // LoginInputFields.IDInputField.text = id;
            OnClickGoToLoginButton();
        }
        else
        {
            RegisterInputFields.ResultText.text = result.Message;
        }
       
    }


    public void Login()
    {
        string email = LoginInputFields.IDInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSpecificationBy(email))
        {
            LoginInputFields.ResultText.text = emailSpecification.ErrorMessage;
            return;
        }

        string password = LoginInputFields.PasswordInputFielid.text;
        var passwordSpecification = new AccountPasswordSpedcification();
        if (!passwordSpecification.IsSpecificationBy(password))
        {
            LoginInputFields.ResultText.text = passwordSpecification.ErrorMessage;
        }
        if (AccountManager.Instance.TryLogin(email, password))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            LoginInputFields.ResultText.text = "이메일과 비밀번호를 확인해주세요.";
        }
    }


    // 아이디와 비밀번호 InputField 값이 바뀌었을 경우에만 
    public void LoginCheck()
    {
        string id = LoginInputFields.IDInputField.text;
        string password = LoginInputFields.PasswordInputFielid.text;

        LoginInputFields.ConfirmButton.enabled = !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(password);
    }
}
