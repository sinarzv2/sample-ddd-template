using Common.Resources.Messages;

namespace Common.Constant;

public static class ConstantValidation
{
    public static readonly string Required = string.Format(Validations.Required, "{PropertyName}");
    public static readonly string MaxLength = string.Format(Validations.MaxLength, "{PropertyName}", "{MaxLength}");
    public static readonly string MinLength = string.Format(Validations.MinLength, "{PropertyName}", "{MinLength}");
    public static readonly string RegularExpression = string.Format(Validations.RegularExpression, "{PropertyName}");
    public static readonly string Range = string.Format(Validations.Range, "{PropertyName}", "{From}", "{To}");
    public static readonly string InvalidCode = string.Format(Validations.InvalidCode, "{PropertyName}");

    public static string FixLengthNumeric(int fixLength) => string.Format(Validations.FixLengthNumeric, "{PropertyName}", fixLength);

}