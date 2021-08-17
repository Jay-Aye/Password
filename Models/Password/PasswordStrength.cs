using System.Runtime.Serialization;

namespace Models.Password
{
    public enum PasswordStrength
    {
        [EnumMember(Value = "Blank")]
        Blank = 0,
        [EnumMember(Value = "Very Weak")]
        VeryWeak = 1,
        [EnumMember(Value = "Weak")]
        Weak = 2,
        [EnumMember(Value = "Medium")]
        Medium = 3,
        [EnumMember(Value = "Strong")]
        Strong = 4,
        [EnumMember(Value = "Very Strong")]
        VeryStrong = 5
    }
}
