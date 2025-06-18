using System;
using System.Text.RegularExpressions;

public class AccountNickNameSpedcification : ISpecification<string>
{
    // �г���: �ѱ� �Ǵ� ����� ����, 2~7��
    private static readonly Regex NicknameRegex = new Regex(@"^[0-9��-�Ra-zA-Z]{2,12}$", RegexOptions.Compiled);

    // ������ �г��� (��Ӿ� ��)
    private static readonly string[] ForbiddenNicknames = { "�ٺ�", "��û��", "���", "�ڼ�ȫ" };

    public bool IsSpecificationBy(string value)
    {
        // �г��� ����
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "�г����� ������� �� �����ϴ�.";
            return false;
        }

        if (!NicknameRegex.IsMatch(value))
        {
            ErrorMessage = "�г����� 2�� �̻� 12�� ������ �ѱ� �Ǵ� �����̾�� �մϴ�.";
            return false;
        }

        foreach (var forbidden in ForbiddenNicknames)
        {
            if (value.Contains(forbidden))
            {
                ErrorMessage = ($"�г��ӿ� �������� �ܾ ���ԵǾ� �ֽ��ϴ�: {forbidden}");
                return false;
            }
        }
        return true;
    }

    public string ErrorMessage { get; private set; }
}
