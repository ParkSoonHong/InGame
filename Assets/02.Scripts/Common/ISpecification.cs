using System.Text.RegularExpressions;
using UnityEngine;

public interface ISpecification<T>
{
    public bool IsSpecificationBy(T value);
    public string ErrorMessage { get; }
}
