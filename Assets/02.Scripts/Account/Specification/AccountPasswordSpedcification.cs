using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class AccountPasswordSpedcification : ISpecification<string>
{

    public bool IsSpecificationBy(string value)
    {

        // 비밀번호 검증
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "비밀번호는 비어있을 수 없습니다.";
            return false;
        }

        if (value.Length <= 6 || value.Length >= 12)
        {
            ErrorMessage = "비밀번호는 6자 이상 12자 이하이어야 합니다.";
            return false;
        }

        return true;
    }

    public string ErrorMessage { get; private set; }
}
