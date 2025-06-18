using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class AccountPasswordSpedcification : ISpecification<string>
{

    public bool IsSpecificationBy(string value)
    {

        // ��й�ȣ ����
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "��й�ȣ�� ������� �� �����ϴ�.";
            return false;
        }

        if (value.Length <= 6 || value.Length >= 12)
        {
            ErrorMessage = "��й�ȣ�� 6�� �̻� 12�� �����̾�� �մϴ�.";
            return false;
        }

        return true;
    }

    public string ErrorMessage { get; private set; }
}
