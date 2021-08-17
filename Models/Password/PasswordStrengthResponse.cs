using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Models.Password
{
    public class PasswordStrengthResponse
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public PasswordStrength PasswordStrength { get; set; }
        public int BreachCount { get; set; }
    }
}
